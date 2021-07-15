using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TUserRol
    {
        public TUserRol()
        {
            TLaw = new List<TLaw>();
        }
        public virtual int IdUserRol { get; set; }
        [StringLength(15)]
        public virtual string RolTitels { get; set; }
        public virtual IList<TLaw> TLaw { get; set; }
    }
}
