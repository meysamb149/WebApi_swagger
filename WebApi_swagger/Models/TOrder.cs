using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TOrder
    {
        public TOrder()
        {
            TSabad = new List<TSabad>();
        }
        public virtual long IdOrder { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual TLMahaleh TLMahaleh { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TAddresses TAddresses { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLVaziyatVarizi TLVaziyatVarizi { get; set; }
        public virtual TLNoePay TLNoePay { get; set; }
        public virtual TLVaziyatSabad TLVaziyatSabad { get; set; }
        public virtual TLVaziyatTahvil TLVaziyatTahvil { get; set; }
        public virtual TShift TShift2 { get; set; }
        public virtual TCodeTakhfif TCodeTakhfif { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual int? OrderPrice { get; set; }
        public virtual DateTime? RahgiriDate { get; set; }
        public virtual long? RahgiriCode { get; set; }
        public virtual string Authority { get; set; }
        public virtual DateTime? PeyktahvilDate { get; set; }
        public virtual int? TakhfifPardakhti { get; set; }
        public virtual int? OrderPriceBof { get; set; }
        public virtual DateTime OrderNow { get; set; }
        public virtual long? ShenaseSefaresh { get; set; }
        public virtual int? PeykPrice { get; set; }
        public virtual DateTime Datetahvil { get; set; }
        public virtual int? MizanTakhfif { get; set; }
        public virtual IList<TSabad> TSabad { get; set; }
    }
}

