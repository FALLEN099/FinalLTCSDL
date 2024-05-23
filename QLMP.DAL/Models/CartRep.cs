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
        public Cart GetCartByUserId(int userId)
        {
             return All.Include(c => c.CartItems).ThenInclude(i => i.Product).FirstOrDefault(c => c.UserId == userId);
        }

        public CartItem AddItemToCart(int cartId, int productId, int quantity)
        {
            var cart = Read(cartId);
            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
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

        public CartItem UpdateItemQuantity(int cartId, int productId, int quantity)
        {
            var cart = Read(cartId);
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
            var cart = Read(cartId);
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

