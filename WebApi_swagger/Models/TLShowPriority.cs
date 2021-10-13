using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLShowPriority
    {
        public TLShowPriority()
        {
            THomeProducts = new List<THomeProducts>();
        }
        public virtual int IdShowPriority { get; set; }
        [StringLength(50)]
        public virtual string TitlesShowPriorityStr { get; set; }
        public virtual IList<THomeProducts> THomeProducts { get; set; }
    }
}
