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

        public SingleRsp AddProductToCart(int userId, int productId, int quantity)
        {
            var res = new SingleRsp();
            var cart = cartRep.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                cartRep.Create(cart);
                cart = cartRep.GetCartByUserId(userId); // Ensure the cart is reloaded with an ID
            }

            var cartItem = cartRep.AddProductToCart(cart.Id, productId, quantity);
            res.Data = cartItem;
            return res;
           
        }

        public SingleRsp PlaceOrder(int cartId)
        {
            return cartRep.PlaceOrder(cartId);
        }
        public SingleRsp GetCartById(int cartId)
        {
            return cartRep.GetCartById(cartId);
        }

        public SingleRsp RemoveProductFromCart(int cartItemId)
        {
            return cartRep.RemoveProductFromCart(cartItemId);
        }
    }

}

