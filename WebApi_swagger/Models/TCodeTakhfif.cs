using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TCodeTakhfif
    {
        public TCodeTakhfif()
        {
            TOrder = new List<TOrder>();
            TTemporder = new List<TTemporder>();
        }
        public virtual int IdCodeTakhfif { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(250)]
        public virtual string TitelsTakhfif { get; set; }
        public virtual int? DarsadTakhfif { get; set; }
        public virtual int? MablaghTakhfif { get; set; }
        public virtual int? TakhfifForMinmablagh { get; set; }
        public virtual DateTime? DateAz { get; set; }
        public virtual DateTime? DateTa { get; set; }
        [StringLength(124)]
        public virtual string UserCreateId { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(10)]
        public virtual string CodeTakfif { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
