using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMP.Common.Req
{
    public class CartReq
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemReq> CartItems { get; set; }

    }
}
