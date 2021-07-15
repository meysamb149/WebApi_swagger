using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLActive
    {
        public TLActive()
        {
            TAddresses = new List<TAddresses>();
            TCodeTakhfif = new List<TCodeTakhfif>();
            TLCity = new List<TLCity>();
            TLMahaleh = new List<TLMahaleh>();
            TLOstan = new List<TLOstan>();
            TNazaratForServicer = new List<TNazaratForServicer>();
            TNoeProduct = new List<TNoeProduct>();
            TPeyks = new List<TPeyks>();
            TServicer = new List<TServicer>();
            TServicerForMahaleh = new List<TServicerForMahaleh>();
            TServicerForMahalehByPeyk = new List<TServicerForMahalehByPeyk>();
            TShift = new List<TShift>();
        }
        public virtual int IdActive { get; set; }
        [StringLength(10)]
        public virtual string TitelsActive { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TCodeTakhfif> TCodeTakhfif { get; set; }
        public virtual IList<TLCity> TLCity { get; set; }
        public virtual IList<TLMahaleh> TLMahaleh { get; set; }
        public virtual IList<TLOstan> TLOstan { get; set; }
        public virtual IList<TNazaratForServicer> TNazaratForServicer { get; set; }
        public virtual IList<TNoeProduct> TNoeProduct { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
        public virtual IList<TServicer> TServicer { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
        public virtual IList<TServicerForMahalehByPeyk> TServicerForMahalehByPeyk { get; set; }
        public virtual IList<TShift> TShift { get; set; }
    }
}
