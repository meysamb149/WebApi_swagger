using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TAddresses
    {
        public TAddresses()
        {
            TOrder = new List<TOrder>();
            TTemporder = new List<TTemporder>();
        }
        public virtual long IdAddresses { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TLMahaleh TLMahaleh { get; set; }
        public virtual TLCity TLCity { get; set; }
        public virtual TLOstan TLOstan { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(100)]
        public virtual string AddressName { get; set; }

        public virtual string Titels_Address { get; set; }

        public virtual decimal? Latitude { get; set; }
        public virtual decimal? Longitude { get; set; }
        public virtual string Phone_Addresss { get; set; }
        public virtual long? Lastorderid { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
