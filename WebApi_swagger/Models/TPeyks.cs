using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPeyks
    {
        public TPeyks()
        {
            TActivationCode = new List<TActivationCode>();
        }
        public virtual long IdPeyks { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLTransport TLTransport { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(50)]
        public virtual string NameFamilyStr { get; set; }
        [StringLength(50)]
        public virtual string NumPelakTransportStr { get; set; }
        public virtual string ImgStr { get; set; }
        [StringLength(10)]
        public virtual string CodeMaliStr { get; set; }
        [StringLength(13)]
        public virtual string PhoneStr { get; set; }
        public virtual int? ActivationInt { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLoginStr { get; set; }
        public virtual int? LastLawAcceptedId { get; set; }
        public virtual int? IsDeletedInt { get; set; }
        [StringLength(50)]
        public virtual string PassStr { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
    }
}
