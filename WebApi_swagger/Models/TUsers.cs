using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TUsers
    {
        public TUsers()
        {
            TActivationCode = new List<TActivationCode>();
            TAddresses = new List<TAddresses>();
            TPizzaTempSabad = new List<TPizzaTempSabad>();
            TTempOrder = new List<TTempOrder>();
        }
        public virtual long IdUsrer { get; set; }
        [StringLength(50)]
        public virtual string NameFamily { get; set; }
        [StringLength(13)]
        public virtual string Tell { get; set; }
        public virtual int? IsDeleted { get; set; }
        public virtual int? Activation { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLogin { get; set; }
        public virtual int? LastLawAcceptedId { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TPizzaTempSabad> TPizzaTempSabad { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}