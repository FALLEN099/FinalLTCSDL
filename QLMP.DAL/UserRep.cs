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

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
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
