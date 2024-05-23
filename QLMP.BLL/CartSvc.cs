using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLMP.DAL;
using QLMP.Common.Rsp;
using QLMP.Common.BLL;
using QLMP.DAL.Models;

namespace QLMP.BLL
{
    public class CartSvc : GenericSvc<CartRep, Cart>
    {
        private CartRep cartRep;

        public CartSvc()
        {
            cartRep = new CartRep();
        }

        public SingleRsp AddItemToCart(int userId, int productId, int quantity)
        {
            var res = new SingleRsp();
            var cart = cartRep.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                cartRep.Create(cart);
                cart = cartRep.GetCartByUserId(userId); // Ensure the cart is reloaded with an ID
            }

            var cartItem = cartRep.AddItemToCart(cart.Id, productId, quantity);
            res.Data = cartItem;
            return res;
        }

        public SingleRsp UpdateItemQuantity(int userId, int productId, int quantity)
        {
            var res = new SingleRsp();
            var cart = cartRep.GetCartByUserId(userId);

            if (cart == null)
            {
                res.SetError("Cart not found.");
                return res;
            }

            var cartItem = cartRep.UpdateItemQuantity(cart.Id, productId, quantity);
            res.Data = cartItem;
            return res;
        }

        public SingleRsp RemoveItemFromCart(int userId, int productId)
        {
            var res = new SingleRsp();
            var cart = cartRep.GetCartByUserId(userId);

            if (cart == null)
            {
                res.SetError("Cart not found.");
                return res;
            }

            var success = cartRep.RemoveItemFromCart(cart.Id, productId);
            res.Data = success;
            return res;
        }

        public SingleRsp GetCartByUserId(int userId)
        {
            var res = new SingleRsp();
            var cart = cartRep.GetCartByUserId(userId);
            res.Data = cart;
            return res;
        }
    }

}

