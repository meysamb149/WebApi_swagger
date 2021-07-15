using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLCity
    {
        public TLCity()
        {
            TAddresses = new List<TAddresses>();
            TLMahaleh = new List<TLMahaleh>();
            TServicerForMahaleh = new List<TServicerForMahaleh>();
        }
        public virtual int IdCity { get; set; }
        public virtual TLOstan TLOstan { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(50)]
        public virtual string TitelsCity { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TLMahaleh> TLMahaleh { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
    }
}
