using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TActivationCode
    {
        public virtual long IdActivationCode { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(13)]
        public virtual string Tell { get; set; }
        [StringLength(50)]
        public virtual string DeviceId { get; set; }
        [StringLength(6)]
        public virtual string Code { get; set; }
        public virtual DateTime? CodeGenerationTime { get; set; }
        public virtual int? EnterCount { get; set; }
        public virtual DateTime? IsDeletedTime { get; set; }
        public virtual int? AdminId { get; set; }
    }
}
