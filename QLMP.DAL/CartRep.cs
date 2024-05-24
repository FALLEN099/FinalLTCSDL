using Microsoft.EntityFrameworkCore;
using QLMP.Common.DAL;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;
using System.Linq;

namespace QLMP.DAL
{
    public class CartRep : GenericRep<QuanLyMyPhamContext, Cart>
    {
        #region -- Overrides --

        public override Cart Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }

        #endregion

        #region -- Methods --
        public Cart GetCartByUserId(int userId)
        {
            var cart = All.Include(c => c.CartItems)
                 .ThenInclude(i => i.Product)
                 .FirstOrDefault(c => c.UserId == userId);

            return cart; /*?? throw new Exception("Cart not found for the given user ID."); /* cart == null tương đương cart ??*/
        }
        public SingleRsp AddProductToCart(int cartId, int productId, int quantity)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var cartItem = new CartItem
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    context.CartItems.Add(cartItem);
                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public SingleRsp PlaceOrder(int cartId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var cartItems = context.CartItems.Where(ci => ci.CartId == cartId).ToList();
                    if (!cartItems.Any())
                    {
                        res.SetError("Cart is empty.");
                        return res;
                    }

                    var h = new HoaDon();


                    h.MaKh = context.Carts.FirstOrDefault(c=>c.Id== cartId).UserId;
                    h.NgayLapHd = DateTime.Now;
                    h.TongSl = cartItems.Sum(ci => ci.Quantity);
                    

                    context.HoaDons.Add(h);
                    context.SaveChanges();

                    foreach (var item in cartItems)
                    {
                        var c = new ChiTietHoaDon();
                        //{
                        //    MaHoaDon = hoaDon.MaHoaDon,
                        //    MaSp = item.ProductId,
                        //    SoLuong = (short)item.Quantity,
                        //    DonGia = (float?)context.SanPhams.First(sp => sp.MaSp == item.ProductId).Gia
                        //};
                            c.MaHoaDon = h.MaHoaDon;
                            c.MaSp = item.ProductId;
                            c.SoLuong = (short)item.Quantity;
                            c.DonGia = (float?)context.SanPhams.First(sp => sp.MaSp == item.ProductId).Gia;

                        context.ChiTietHoaDons.Add(c);
                    }

                    context.SaveChanges();
                    context.CartItems.RemoveRange(cartItems);
                    context.SaveChanges();

                    tran.Commit();
                    res.Data = h;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }
        public SingleRsp GetCartById(int cartId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var cart = context.Carts
                    .Where(c => c.Id == cartId)
                    .Select(c => new
                    {
                        c.Id,
                        c.UserId,
                        CartItems = c.CartItems.Select(ci => new
                        {
                            ci.Id,
                            ci.ProductId,
                            ci.Quantity,
                            ProductName = ci.Product.TenSp,
                            ProductPrice = ci.Product.Gia
                        }).ToList()
                    }).FirstOrDefault();

                if (cart == null)
                {
                    res.SetError("Cart not found.");
                }
                else
                {
                    res.Data = cart;
                }
            }
            return res;
        }

        public SingleRsp RemoveProductFromCart(int cartItemId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var cartItem = context.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
                    if (cartItem == null)
                    {
                        res.SetError("Cart item not found.");
                    }
                    else
                    {
                        context.CartItems.Remove(cartItem);
                        context.SaveChanges();
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        #endregion
    }
}