using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TContactus
    {
        public virtual long IdContactus { get; set; }
        public virtual long? UserId { get; set; }
        [StringLength(12)]
        public virtual string UserPhone { get; set; }
        public virtual string UserText { get; set; }
    }
}
