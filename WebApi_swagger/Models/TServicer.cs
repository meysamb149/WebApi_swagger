using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TServicer
    {
        public TServicer()
        {
            TActivationCode = new List<TActivationCode>();
            TCGoZarfiyatShServiser = new List<TCGoZarfiyatShServiser>();
            TCodeTakhfif = new List<TCodeTakhfif>();
            THoliday = new List<THoliday>();
            TNazaratForServicer = new List<TNazaratForServicer>();
            TNoeProduct = new List<TNoeProduct>();
            TOrder = new List<TOrder>();
            TPayForServicer = new List<TPayForServicer>();
            TServicerForMahaleh = new List<TServicerForMahaleh>();
            TTemporder = new List<TTemporder>();
            TZarfiyatShiftPeyks = new List<TZarfiyatShiftPeyks>();
            TZarfiyatShiftServicer = new List<TZarfiyatShiftServicer>();
        }
        public virtual int IdServicer { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TLaw TLaw { get; set; }
        [StringLength(70)]
        public virtual string NameServicer { get; set; }
        public virtual string Address { get; set; }
        public virtual string Img { get; set; }
        [StringLength(13)]
        public virtual string Phone1 { get; set; }
        [StringLength(13)]
        public virtual string Phone2 { get; set; }
        [StringLength(50)]
        public virtual string NameFamilyAdminServicer { get; set; }
        [StringLength(13)]
        public virtual string PhoneAdmin { get; set; }
        [StringLength(10)]
        public virtual string CodeMaliAdmin { get; set; }
        [StringLength(20)]
        public virtual string NumberGharardad { get; set; }
        [StringLength(50)]
        public virtual string NumberCartBank { get; set; }
        [StringLength(50)]
        public virtual string ShabaCartBank { get; set; }
        [StringLength(50)]
        public virtual string NameBank { get; set; }
        [StringLength(50)]
        public virtual string UsernameCartBank { get; set; }
        public virtual decimal? Latitude { get; set; }
        public virtual decimal? Longitude { get; set; }
        public virtual int? AvgRank { get; set; }
        public virtual int? OstanId { get; set; }
        public virtual int? CityId { get; set; }
        public virtual int? CNazarat { get; set; }
        public virtual int? Activation { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLogin { get; set; }
        public virtual int? IsDeleted { get; set; }
        [StringLength(50)]
        public virtual string Pass { get; set; }
        [StringLength(50)]
        public virtual string Telegram { get; set; }
        [StringLength(50)]
        public virtual string Instagram { get; set; }
        [StringLength(50)]
        public virtual string Whatsapp { get; set; }
        [StringLength(50)]
        public virtual string OtherPage { get; set; }
        public virtual string TozihatKhedmat { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
        public virtual IList<TCGoZarfiyatShServiser> TCGoZarfiyatShServiser { get; set; }
        public virtual IList<TCodeTakhfif> TCodeTakhfif { get; set; }
        public virtual IList<THoliday> THoliday { get; set; }
        public virtual IList<TNazaratForServicer> TNazaratForServicer { get; set; }
        public virtual IList<TNoeProduct> TNoeProduct { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TPayForServicer> TPayForServicer { get; set; }
        public virtual IList<TServicerForMahaleh> TServicerForMahaleh { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
        public virtual IList<TZarfiyatShiftPeyks> TZarfiyatShiftPeyks { get; set; }
        public virtual IList<TZarfiyatShiftServicer> TZarfiyatShiftServicer { get; set; }
    }
}
