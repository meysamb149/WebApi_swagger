using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLMahaleh
    {
        public TLMahaleh()
        {
            TAddresses = new List<TAddresses>();
            TOrder = new List<TOrder>();
            TServicerForMahaleh = new List<TServicerForMahaleh>();
            TTemporder = new List<TTemporder>();
        }
        public virtual int IdMahaleh { get; set; }
        public virtual TLCity TLCity { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(30)]
        public virtual string TitelsMahaleh { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
