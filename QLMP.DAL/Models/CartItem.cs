using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual SanPham Product { get; set; } = null!;
    }
}
