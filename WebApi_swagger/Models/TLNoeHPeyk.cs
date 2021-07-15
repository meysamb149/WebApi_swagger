using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLNoeHPeyk
    {
        public TLNoeHPeyk()
        {
            TServicerForMahaleh = new List<TServicerForMahaleh>();
        }
        public virtual int IdNoeHPeyk { get; set; }
        [StringLength(10)]
        public virtual int TitelsNoeHPeyk { get; set; }
        public virtual int? HazinehKolSefareshBishtarAz { get; set; }
        public virtual int? HazinehKolSefareshKamtarAz { get; set; }
        [StringLength(100)]
        public virtual string MatnNamayesh { get; set; }

        public virtual int TitelsNoeHPeykElse { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
    }
}

