using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TUsers
    {
        public TUsers()
        {
            TActivationCode = new List<TActivationCode>();
            TAddresses = new List<TAddresses>();
            TNazaratForServicer = new List<TNazaratForServicer>();
            TOrder = new List<TOrder>();
            TTamasbama = new List<TTamasbama>();
            TTemporder = new List<TTemporder>();
        }
        public virtual long IdUsrer { get; set; }
        public virtual TLaw TLaw { get; set; }
        [StringLength(50)]
        public virtual string NameFamily { get; set; }
        [StringLength(13)]
        public virtual string Tell { get; set; }
        public virtual int? IsDeleted { get; set; }
        public virtual int? Activation { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLogin { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TNazaratForServicer> TNazaratForServicer { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TTamasbama> TTamasbama { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
