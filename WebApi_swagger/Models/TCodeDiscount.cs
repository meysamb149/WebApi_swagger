using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TCodeDiscount
    {
        public TCodeDiscount()
        {
            TTempOrder = new List<TTempOrder>();
        }
        public virtual int IdCodeDiscount { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(250)]
        public virtual string TitelsDiscountStr { get; set; }
        public virtual int? DarsadDiscountInt { get; set; }
        public virtual int? MablaghDiscountInt { get; set; }
        public virtual int? DiscountForMinmablaghInt { get; set; }
        public virtual DateTime? DateAz { get; set; }
        public virtual DateTime? DateTa { get; set; }
        [StringLength(124)]
        public virtual string UserCreateId { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(10)]
        public virtual string CodeTakfifStr { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}
