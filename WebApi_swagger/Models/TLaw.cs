using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLaw
    {
        public TLaw()
        {
            TUsers = new List<TUsers>();
            TPeyks = new List<TPeyks>();
            TServicer = new List<TServicer>();
        }
        public virtual int IdLaw { get; set; }
        public virtual TUserRol TUserRol { get; set; }
        public virtual string LawText { get; set; }
        public virtual string LawType { get; set; }
        public virtual DateTime? LawDate { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
        public virtual IList<TServicer> TServicer { get; set; }
        public virtual IList<TUsers> TUsers { get; set; }
    }
}
