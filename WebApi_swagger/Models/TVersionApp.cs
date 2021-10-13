using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TVersionApp
    {
        public virtual int IdVersionApp { get; set; }
        public virtual string TextForVersion { get; set; }
        [StringLength(15)]
        public virtual string VersionCode { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(128)]
        public virtual string UserId { get; set; }
        [StringLength(15)]
        public virtual string Versionapptype { get; set; }
    }
}
