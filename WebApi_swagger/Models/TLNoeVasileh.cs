using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLNoeVasileh
    {
        public TLNoeVasileh()
        {
            TPeyks = new List<TPeyks>();
        }
        public virtual int IdNoeVasileh { get; set; }
        [StringLength(50)]
        public virtual string TitelsNoeVasileh { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
    }
}
