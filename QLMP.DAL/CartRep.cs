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

            return cart;
        }
        public SingleRsp AddProductToCart(int cartId, int productId, int quantity)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    // Check if the product already exists in the cart
                    var existingCartItem = context.CartItems
                    .FirstOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);

                    if (existingCartItem != null)
                    {
                        // Product already exists in the cart, update the quantity
                        existingCartItem.Quantity += quantity;
                    }
                    else
                    {
                        // Product does not exist in the cart, create a new cart item
                        var cartItem = new CartItem
                        {
                            CartId = cartId,
                            ProductId = productId,
                            Quantity = quantity
                        };
                        context.CartItems.Add(cartItem);
                        res.Data = cartItem;
                    }

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


        public SingleRsp PlaceOrder(int userId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var cart = context.Carts.FirstOrDefault(c => c.UserId == userId);
                    if (cart == null)
                    {
                        res.SetError("Cart not found for the user.");
                        return res;
                    }
                    var cartItems = context.CartItems.Where(ci => ci.CartId == cart.Id).ToList();
                    if (!cartItems.Any())
                    {
                        res.SetError("Cart is empty.");
                        return res;
                    }

                    var h = new HoaDon();


                    h.MaKh = cart.UserId/*context.Carts.FirstOrDefault(c=>c.Id== cart.Id).UserId*/;
                    h.NgayLapHd = DateTime.Now;
                    h.TongSl = cartItems.Sum(ci => ci.Quantity);
                    context.HoaDons.Add(h);
                    context.SaveChanges();

                    foreach (var item in cartItems)
                    {
                        var c = new ChiTietHoaDon();
                            c.MaHoaDon = h.MaHoaDon;
                            c.MaSp = item.ProductId;
                            c.SoLuong = (short)item.Quantity;
                            c.DonGia = ((float?)context.SanPhams.First(sp => sp.MaSp == item.ProductId).Gia)* item.Quantity;

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
        public SingleRsp GetCartById(int userId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var cart = context.Carts
                    .Where(c => c.UserId == userId)
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
                        res.Data=cartItem;
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
        public SingleRsp GetOrderByOrderId(int orderId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var order = context.HoaDons
                    .Where(o => o.MaHoaDon == orderId)
                    .Select(o => new
                    {
                        o.MaHoaDon,
                        o.MaKh,
                        o.NgayLapHd,
                        o.TongSl,
                        OrderDetails = o.ChiTietHoaDons.Select(od => new
                        {
                            od.MaSp,
                            od.SoLuong,
                            od.DonGia,
                            ProductName = od.MaSpNavigation.TenSp,
                            CategoryName = od.MaSpNavigation.MaLoaiSpNavigation.TenLoaiSp
                        }).ToList()
                    }).FirstOrDefault();

                if (order == null)
                {
                    res.SetError("Order not found.");
                }
                else
                {
                    res.Data = order;
                }
            }
            return res;
        }
        public SingleRsp GetOrderById(int userId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var order = context.HoaDons
                    .Where(o => o.MaKh == userId)
                    .Select(o => new
                    {
                        o.MaHoaDon,
                        o.MaKh,
                        o.NgayLapHd,
                        o.TongSl,
                        OrderDetails = o.ChiTietHoaDons.Select(od => new
                        {
                            od.MaSp,
                            od.SoLuong,
                            od.DonGia,
                            ProductName = od.MaSpNavigation.TenSp,
                            CategoryName = od.MaSpNavigation.MaLoaiSpNavigation.TenLoaiSp
                        }).ToList()
                    }).FirstOrDefault();

                if (order == null)
                {
                    res.SetError("Order not found.");
                }
                else
                {
                    res.Data = order;
                }
            }
            return res;
        }
        public SingleRsp GetAllOrders()
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var orders = context.HoaDons
                    .Select(o => new
                    {
                        o.MaHoaDon,
                        o.MaKh,
                        o.NgayLapHd,
                        o.TongSl,
                        OrderDetails = o.ChiTietHoaDons.Select(od => new
                        {
                            od.MaSp,
                            od.SoLuong,
                            od.DonGia,
                            ProductName = od.MaSpNavigation.TenSp,
                            CategoryName = od.MaSpNavigation.MaLoaiSpNavigation.TenLoaiSp
                        }).ToList()
                    }).ToList();

                res.Data = orders;
            }
            return res;
        }
        public SingleRsp GetSalesStatisticsByProductType()
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var statistics = context.ChiTietHoaDons
                    .Include(cthd => cthd.MaSpNavigation)
                    .ThenInclude(sp => sp.MaLoaiSpNavigation)
                    .GroupBy(cthd => new
                    {
                        cthd.MaSpNavigation.MaLoaiSpNavigation.MaLoaiSp,
                        cthd.MaSpNavigation.MaLoaiSpNavigation.TenLoaiSp
                    })
                    .Select(g => new
                    {
                        ProductTypeId = g.Key.MaLoaiSp,
                        TenLoaiSanPham = g.Key.TenLoaiSp,
                        SoLuong = g.Sum(x => x.SoLuong),
                        TotalSales = g.Sum(x => x.SoLuong * x.DonGia)
                    })
                    .ToList();

            res.Data = statistics;
        }
            return res;
        }
        public SingleRsp GetMonthlySalesStatistics()
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var statistics = context.HoaDons
                    .Where(hd => hd.NgayLapHd.HasValue)
                    .Select(hd => new
                    {
                        hd.NgayLapHd,
                        ChiTietHoaDons = hd.ChiTietHoaDons.Select(cthd => new
                        {
                            cthd.SoLuong,
                            DonGia = (double)(cthd.DonGia ?? 0)
                        }).ToList()
                    })
                    .ToList()
                    .GroupBy(hd => new
                    {
                        Year = hd.NgayLapHd.Value.Year,
                        Month = hd.NgayLapHd.Value.Month
                    })
                    .Select(g => new
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        NumberOfOrders = g.Count(),
                        TotalRevenue = g.Sum(hd => hd.ChiTietHoaDons.Sum(cthd => cthd.SoLuong * cthd.DonGia))
                    })
                    .OrderBy(s => s.Year)
                    .ThenBy(s => s.Month)
                    .ToList();

                res.Data = statistics;
            }
            return res;
        }
        public SingleRsp GetRecentOrders()
        {
            int count = 5;
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                var recentOrders = context.HoaDons
                    .OrderByDescending(o => o.NgayLapHd)
                    .Take(count)
                    .Select(o => new
                    {
                        o.MaHoaDon,
                        KhachHang = o.MaKhNavigation.TenKh,
                        o.NgayLapHd
                    })
                    .ToList();

                res.Data = recentOrders;
            }
            return res;
        }

        #endregion
    }
}