using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TServicer
    {
        public TServicer()
        {
            TActivationCode = new List<TActivationCode>();
            TCodeDiscount = new List<TCodeDiscount>();
            THomeApp = new List<THomeApp>();
            THomeProducts = new List<THomeProducts>();
            TPeyks = new List<TPeyks>();
            TPizzaCheese = new List<TPizzaCheese>();
            TPizzaCrust = new List<TPizzaCrust>();
            TPizzaSauce = new List<TPizzaSauce>();
            TPizzaSizeBread = new List<TPizzaSizeBread>();
            TPizzaTempSabad = new List<TPizzaTempSabad>();
            TPizzaTopping = new List<TPizzaTopping>();
            TSubCheese = new List<TSubCheese>();
            TSubSizeBread = new List<TSubSizeBread>();
            TSubTopping = new List<TSubTopping>();
            TSubTypeBread = new List<TSubTypeBread>();
            TTempOrder = new List<TTempOrder>();
        }
        public virtual int IdServicer { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(70)]
        public virtual string NameServicerStr { get; set; }
        public virtual string AddressStr { get; set; }
        public virtual string ImgStr { get; set; }
        [StringLength(13)]
        public virtual string Phone1Str { get; set; }
        [StringLength(13)]
        public virtual string Phone2Str { get; set; }
        [StringLength(50)]
        public virtual string NameFamilyAdminServicerStr { get; set; }
        [StringLength(13)]
        public virtual string PhoneAdminStr { get; set; }
        [StringLength(10)]
        public virtual string CodeMaliAdminStr { get; set; }
        [StringLength(20)]
        public virtual string NumberContractStr { get; set; }
        [StringLength(50)]
        public virtual string NumberCartBankStr { get; set; }
        [StringLength(50)]
        public virtual string ShabaCartBankStr { get; set; }
        [StringLength(50)]
        public virtual string NameBankStr { get; set; }
        [StringLength(50)]
        public virtual string UsernameCartBankStr { get; set; }
        public virtual decimal? LatitudeDec { get; set; }
        public virtual decimal? LongitudeDec { get; set; }
        public virtual int? AvgRankInt { get; set; }
        public virtual int? OstanIdInt { get; set; }
        public virtual int? CityIdInt { get; set; }
        public virtual int? CCommentsInt { get; set; }
        public virtual int? ActivationInt { get; set; }
        [StringLength(50)]
        public virtual string DeviceIdLoginStr { get; set; }
        public virtual int? LastLawAcceptedId { get; set; }
        public virtual int? IsDeletedInt { get; set; }
        [StringLength(50)]
        public virtual string PassStr { get; set; }
        [StringLength(50)]
        public virtual string TelegramStr { get; set; }
        [StringLength(50)]
        public virtual string InstagramStr { get; set; }
        [StringLength(50)]
        public virtual string WhatsappStr { get; set; }
        [StringLength(50)]
        public virtual string OtherPageStr { get; set; }
        public virtual string DescriptionServiceStr { get; set; }
        public virtual IList<TActivationCode> TActivationCode { get; set; }
        public virtual IList<TCodeDiscount> TCodeDiscount { get; set; }
        public virtual IList<THomeApp> THomeApp { get; set; }
        public virtual IList<THomeProducts> THomeProducts { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
        public virtual IList<TPizzaCheese> TPizzaCheese { get; set; }
        public virtual IList<TPizzaCrust> TPizzaCrust { get; set; }
        public virtual IList<TPizzaSauce> TPizzaSauce { get; set; }
        public virtual IList<TPizzaSizeBread> TPizzaSizeBread { get; set; }
        public virtual IList<TPizzaTempSabad> TPizzaTempSabad { get; set; }
        public virtual IList<TPizzaTopping> TPizzaTopping { get; set; }
        public virtual IList<TSubCheese> TSubCheese { get; set; }
        public virtual IList<TSubSizeBread> TSubSizeBread { get; set; }
        public virtual IList<TSubTopping> TSubTopping { get; set; }
        public virtual IList<TSubTypeBread> TSubTypeBread { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}