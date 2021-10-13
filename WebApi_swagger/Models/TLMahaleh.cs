using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLMahaleh
    {
        public TLMahaleh()
        {
            TAddresses = new List<TAddresses>();
        }
        public virtual int IdMahaleh { get; set; }
        public virtual TLCity TLCity { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(30)]
        public virtual string TitelsMahaleh { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
    }
}