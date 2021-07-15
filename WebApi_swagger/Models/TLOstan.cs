using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLOstan
    {
        public TLOstan()
        {
            TAddresses = new List<TAddresses>();
            TLCity = new List<TLCity>();
            TServicerForMahaleh = new List<TServicerForMahaleh>();
        }
        public virtual int IdOstan { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(20)]
        public virtual string TitelsOstan { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TLCity> TLCity { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
    }
}
