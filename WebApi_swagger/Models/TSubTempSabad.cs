using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TSubTempSabad
    {
        public TSubTempSabad()
        {
            TSubTempSabadCheese = new List<TSubTempSabadCheese>();
            TSubTempSabadTopping = new List<TSubTempSabadTopping>();
        }
        public virtual long IdSubTempSabad { get; set; }
        public virtual int? TSubSizeBreadId { get; set; }
        public virtual int? PriceSubSizeBread { get; set; }
        public virtual int? TSubTypeBreadId { get; set; }
        public virtual int? PriceSubTypeBread { get; set; }
        public virtual int? PriceSubSumCheese { get; set; }
        public virtual int? PriceSubSumTopping { get; set; }
        public virtual int? CountSubInt { get; set; }
        public virtual int? PriceOnesub { get; set; }
        public virtual int? PriceSumSub { get; set; }
        public virtual long? TTempOrderId { get; set; }
        public virtual long? TUsersId { get; set; }
        public virtual int? TServicerId { get; set; }
        public virtual IList<TSubTempSabadCheese> TSubTempSabadCheese { get; set; }
        public virtual IList<TSubTempSabadTopping> TSubTempSabadTopping { get; set; }
    }
}