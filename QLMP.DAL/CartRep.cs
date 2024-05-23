using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLMP.DAL.Models;
using QLMP.Common.Rsp;
using QLMP.Common.DAL;
using Microsoft.EntityFrameworkCore;

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

        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m);
            return m.Id;
        }

        #endregion
        public Cart GetCartByUserId(int userId)
        {
            var cart = All.Include(c => c.CartItems)
                 .ThenInclude(i => i.Product)
                 .FirstOrDefault(c => c.UserId == userId);

            return cart ?? throw new Exception("Cart not found for the given user ID."); /* cart == null tương đương cart ??*/
        }

        public CartItem AddItemToCart(int cartId, int productId, int quantity)
        {
            var cart = All.FirstOrDefault(p => p.Id == cartId);
            CartItem cartItem;
            if (cart!= null)
            {
                cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
                if(cartItem != null)
                    cartItem.Quantity += quantity;
                else
                {
                    cartItem = new CartItem
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                Context.SaveChanges();
                return cartItem;
            }
            else
            {
                throw new Exception("Không tìm thấy giỏ hàng với ID đã cung cấp.");
            }
        }
        public CartItem UpdateItemQuantity(int cartId, int productId, int quantity)
        {
            var cart = All.FirstOrDefault(p => p.Id == cartId) ?? throw new Exception("Không tìm thấy giỏ hàng với ID đã cung cấp.");
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                Context.SaveChanges();
            }

            return cartItem;
        }

        public bool RemoveItemFromCart(int cartId, int productId)
        {
            var cart = All.FirstOrDefault(p => p.Id == cartId);
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                Context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

