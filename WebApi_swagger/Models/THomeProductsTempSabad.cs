using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_swagger.Models
{
    public class THomeProductsTempSabad
    {
        public virtual long IdHomeProductsTempSabad { get; set; }
        public virtual THomeProducts THomeProducts { get; set; }
        public virtual int? CountHomeProductsInt { get; set; }
        public virtual int? PriceInt { get; set; }
        public virtual int? TTempOrderId { get; set; }
    }
}
