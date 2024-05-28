using QLMP.Common.DAL;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;
using System.Linq;

namespace QLMP.DAL
{
    public class UserRep : GenericRep<QuanLyMyPhamContext, User>
    {
        
        private  QuanLyMyPhamContext _context = new QuanLyMyPhamContext();
        
        public override User Read(int id)
        {
            var res = All.FirstOrDefault(u => u.Id == id);
            return res;
        }

        public User Read(string username)
        {
            var res =All.FirstOrDefault(u=>u.UserName == username);
            return res;
        }
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByUserName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public List<User> GetAllUsersExceptAdmin()
        {
            return _context.Users.Where(u => u.Role != "admin").ToList();
        }
        public SingleRsp UpdateUser(User user)
        {
            var res = new SingleRsp();
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                res.Data = user;
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }
            return res;
        }

        public SingleRsp Delete(int userId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find the user by userId
                        var user = context.Users.FirstOrDefault(u=>u.Id ==userId);
                        if (user != null)
                        {
                            var khachHang = context.KhachHangs.FirstOrDefault(kh => kh.UserId == userId);
                            if (khachHang != null)
                            {
                                var cart = context.Carts.FirstOrDefault(c => c.UserId == khachHang.MaKh);
                                if (cart != null)
                                {
                                    var cartItems = context.CartItems.Where(ci => ci.CartId == cart.Id).ToList();
                                    if (cartItems != null)
                                    {
                                        foreach (var item in cartItems)
                                        {
                                            context.CartItems.Remove(item);

                                            context.SaveChanges();
                                        }

                                    }

                                    context.Carts.Remove(cart);
                                    context.SaveChanges();
                                }
                                var hoaDons = context.HoaDons.Where(h => h.MaKh == khachHang.MaKh).ToList();
                                if (hoaDons != null)
                                {
                                    var h = context.HoaDons.FirstOrDefault(hd => hd.MaKh == khachHang.MaKh);
                                    var chiTietHds = context.ChiTietHoaDons.Where(ci => ci.MaHoaDon == h.MaHoaDon).ToList();
                                    if (chiTietHds != null)
                                    {
                                        foreach (var cth in chiTietHds)
                                        {
                                            context.ChiTietHoaDons.Remove(cth);
                                            context.SaveChanges();
                                        }

                                    }
                                    foreach (var hoaDon in hoaDons)
                                    {
                                        context.HoaDons.Remove(hoaDon);

                                        context.SaveChanges();
                                    }
                                }
                                context.KhachHangs.Remove(khachHang);
                                context.SaveChanges();
                            }
                            context.Users.Remove(user);
                            context.SaveChanges();
                            tran.Commit();
                            res.Data = user;
                        }
                        else
                        {
                            res.SetError("User not found");
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }


        public void DeleteByUserName(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public SingleRsp CreateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Users.Add(user);
                        context.SaveChanges();

                        KhachHang kh = new KhachHang();
                        kh.UserId = user.Id;
                        kh.TenKh = user.UserName;
                        kh.Email = user.Email;
                        kh.DiaChi = user.Address;
                        kh.Sdt = int.Parse(user.Phone);
                        context.KhachHangs.Add(kh);
                        context.SaveChanges();

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
        public bool ExistsUserName(string username, int id)
        {
            return _context.Users.Any(u => u.UserName == username && u.Id != id);
        }

    }
}
