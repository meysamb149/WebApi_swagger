using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TServicerForMahaleh
    {
        public TServicerForMahaleh()
        {
            TServicerForMahalehByPeyk = new List<TServicerForMahalehByPeyk>();
        }
        public virtual long IdServicerForMahaleh { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLNoeHPeyk TLNoeHPeyk { get; set; }
        public virtual TLOstan TLOstan { get; set; }
        public virtual TLCity TLCity { get; set; }
        public virtual TLMahaleh TLMahaleh { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual string TitelsForLookups { get; set; }
        public virtual IList<TServicerForMahalehByPeyk> TServicerForMahalehByPeyk { get; set; }
    }
}
