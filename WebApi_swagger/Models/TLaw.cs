using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLaw
    {
        public virtual int IdLaw { get; set; }
        public virtual DateTime? LawDateDt { get; set; }
        public virtual int? UserRolId { get; set; }
        [StringLength(15)]
        public virtual string LawtypeStr { get; set; }
        public virtual string LawTextStr { get; set; }
    }
}
