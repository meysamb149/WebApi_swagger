using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TAddresses
    {
        public TAddresses()
        {
            TTempOrder = new List<TTempOrder>();
        }
        public virtual long IdAddresses { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TLMahaleh TLMahaleh { get; set; }
        public virtual TLCity TLCity { get; set; }
        public virtual TLOstan TLOstan { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(100)]
        public virtual string AddressNameStr { get; set; }
        public virtual string TitelsAddressStr { get; set; }
        public virtual decimal? LatitudeDec { get; set; }
        public virtual decimal? LongitudeDec { get; set; }
        [StringLength(13)]
        public virtual string PhoneAddresssStr { get; set; }
        public virtual long? Lastorderid { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}
