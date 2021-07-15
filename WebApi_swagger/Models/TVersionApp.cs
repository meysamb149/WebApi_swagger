using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TVersionApp
    {
        public virtual int IdVersionApp { get; set; }
        public virtual string TextForVersion { get; set; }
        [StringLength(15)]
        public virtual string VersionCode { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(128)]
        public virtual string UserId { get; set; }
        public virtual string VersionAppType { get; set; }
    }
}
