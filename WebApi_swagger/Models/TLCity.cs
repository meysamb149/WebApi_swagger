using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLCity
    {
        public TLCity()
        {
            TAddresses = new List<TAddresses>();
            TLMahaleh = new List<TLMahaleh>();
        }
        public virtual int IdCity { get; set; }
        public virtual TLOstan TLOstan { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(50)]
        public virtual string TitelsCity { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TLMahaleh> TLMahaleh { get; set; }
    }
}
