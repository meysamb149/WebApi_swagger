using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TActivationCode
    {
        public virtual long IdActivationCode { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(13)]
        public virtual string TellStr { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdStr { get; set; }
        [StringLength(6)]
        public virtual string CodeStr { get; set; }
        public virtual DateTime? CodeGenerationTime { get; set; }
        public virtual int? EnterCountInt { get; set; }
        public virtual DateTime? IsDeletedTime { get; set; }
        public virtual int? AdminId { get; set; }
    }
}