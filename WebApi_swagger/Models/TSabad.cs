using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TSabad
    {
        public virtual long IdSabad { get; set; }
        public virtual TNoeProduct TNoeProduct { get; set; }
        public virtual TNoeKhedmat TNoeKhedmat { get; set; }
        public virtual TTemporder TTemporder { get; set; }
        public virtual TOrder TOrder { get; set; }
        public virtual TLVaziyatSabad TLVaziyatSabad { get; set; }
        public virtual int? Tedad { get; set; }
        public virtual long? ShenasehSefaresh { get; set; }
        public virtual int? PriceAll { get; set; }
        public virtual int? PriceBof { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(250)]
        public virtual string TozihatMoshtari { get; set; }
        [StringLength(350)]
        public virtual string TozihatSevviser { get; set; }
    }
}
