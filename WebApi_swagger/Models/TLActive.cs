using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLActive
    {
        public TLActive()
        {
            TAddresses = new List<TAddresses>();
            TCodeDiscount = new List<TCodeDiscount>();
            THomeApp = new List<THomeApp>();
            THomeProducts = new List<THomeProducts>();
            TLCity = new List<TLCity>();
            TLMahaleh = new List<TLMahaleh>();
            TLOstan = new List<TLOstan>();
            TPeyks = new List<TPeyks>();
            TPizzaCheese = new List<TPizzaCheese>();
            TPizzaCrust = new List<TPizzaCrust>();
            TPizzaSauce = new List<TPizzaSauce>();
            TPizzaSizeBread = new List<TPizzaSizeBread>();
            TPizzaTopping = new List<TPizzaTopping>();
            TServicer = new List<TServicer>();
            TSubCheese = new List<TSubCheese>();
            TSubSizeBread = new List<TSubSizeBread>();
            TSubTopping = new List<TSubTopping>();
            TSubTypeBread = new List<TSubTypeBread>();
        }
        public virtual int IdActive { get; set; }
        [StringLength(10)]
        public virtual string TitelsActive { get; set; }
        public virtual IList<TAddresses> TAddresses { get; set; }
        public virtual IList<TCodeDiscount> TCodeDiscount { get; set; }
        public virtual IList<THomeApp> THomeApp { get; set; }
        public virtual IList<THomeProducts> THomeProducts { get; set; }
        public virtual IList<TLCity> TLCity { get; set; }
        public virtual IList<TLMahaleh> TLMahaleh { get; set; }
        public virtual IList<TLOstan> TLOstan { get; set; }
        public virtual IList<TPeyks> TPeyks { get; set; }
        public virtual IList<TPizzaCheese> TPizzaCheese { get; set; }
        public virtual IList<TPizzaCrust> TPizzaCrust { get; set; }
        public virtual IList<TPizzaSauce> TPizzaSauce { get; set; }
        public virtual IList<TPizzaSizeBread> TPizzaSizeBread { get; set; }
        public virtual IList<TPizzaTopping> TPizzaTopping { get; set; }
        public virtual IList<TServicer> TServicer { get; set; }
        public virtual IList<TSubCheese> TSubCheese { get; set; }
        public virtual IList<TSubSizeBread> TSubSizeBread { get; set; }
        public virtual IList<TSubTopping> TSubTopping { get; set; }
        public virtual IList<TSubTypeBread> TSubTypeBread { get; set; }
    }
}
