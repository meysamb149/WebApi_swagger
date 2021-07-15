using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TPeyks
    {
        public TPeyks()
        {
            TActivationCode = new List<TActivationCode>();
            TCGoZarfiyatShPeyks = new List<TCGoZarfiyatShPeyks>();
            TOrder = new List<TOrder>();
            TServicerForMahalehByPeyk = new List<TServicerForMahalehByPeyk>();
            TTemporder = new List<TTemporder>();
            TZarfiyatShiftPeyks = new List<TZarfiyatShiftPeyks>();
        }
        public virtual long IdPeyks { get; set; }
        public virtual TLNoeVasileh TLNoeVasileh { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TLaw TLaw { get; set; }
        public virtual int? ServicerId { get; set; }
        [StringLength(50)]
        public virtual string NameFamily { get; set; }
        [StringLength(50)]
        public virtual string NumPelakVasileh { get; set; }
        public virtual string Img { get; set; }
        [StringLength(10)]
        public virtual string CodeMali { get; set; }
        [StringLength(13)]
        public virtual string Phone { get; set; }
        public virtual int? Activation { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLogin { get; set; }
        public virtual int? IsDeleted { get; set; }
        [StringLength(50)]
        public virtual string Pass { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
        public virtual IList<TCGoZarfiyatShPeyks> TCGoZarfiyatShPeyks { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TServicerForMahalehByPeyk> TServicerForMahalehByPeyk { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
        public virtual IList<TZarfiyatShiftPeyks> TZarfiyatShiftPeyks { get; set; }
    }
}
