using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TTempOrder
    {
        public virtual long TTempOrderId { get; set; }
        public virtual TAddresses TAddresses { get; set; }
        public virtual TLVaziyatVarizi TLVaziyatVarizi { get; set; }
        public virtual TLNoePay TLNoePay { get; set; }
        public virtual TLVaziyatSabad TLVaziyatSabad { get; set; }
        public virtual TCodeDiscount TCodeDiscount { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual bool? IsTHomeProductsTempSabad { get; set; }
        public virtual bool? IsTPizzaTempSabad { get; set; }
        public virtual bool? IsTSubTempSabad { get; set; }
        public virtual string AuthorityStr { get; set; }
        public virtual int? AmountOfDiscountInt { get; set; }
        [StringLength(50)]
        public virtual string CodeDescountStr { get; set; }
        public virtual int? TempOrderPriceBof { get; set; }
        [StringLength(50)]
        public virtual string ShenasehOrderStr { get; set; }
        public virtual int? PriceAllInt { get; set; }
    }
}