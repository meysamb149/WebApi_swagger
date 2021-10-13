using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLTransport
    {
        public TLTransport()
        {
            TPeyks = new List<TPeyks>();
        }
        public virtual int IdTransport { get; set; }
        [StringLength(50)]
        public virtual string TitlesTransport { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
    }
}