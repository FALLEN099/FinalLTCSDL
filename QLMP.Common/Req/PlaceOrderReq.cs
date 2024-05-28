using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMP.Common.Req
{
    public class PlaceOrderReq
    {
        [Required]
        public int UserId { get; set; }
    }
}
