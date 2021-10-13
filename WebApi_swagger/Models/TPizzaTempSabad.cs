using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPizzaTempSabad
    { 
     public TPizzaTempSabad()
    {
        TPizzaTempSabadCheese = new List<TPizzaTempSabadCheese>();
        TPizzaTempSabadSauce = new List<TPizzaTempSabadSauce>();
        TPizzaTempSabadTopping = new List<TPizzaTempSabadTopping>();
    }
    public virtual long IdPizzaTempSabad { get; set; }
    public virtual TPizzaSizeBread TPizzaSizeBread { get; set; }
    public virtual TPizzaCrust TPizzaCrust { get; set; }
    public virtual TUsers TUsers { get; set; }
    public virtual TServicer TServicer { get; set; }
    public virtual int? PricePizzaSizeSub { get; set; }
    public virtual int? PricePizzaCrust { get; set; }
    public virtual int? PricePizzaSumCheese { get; set; }
    public virtual int? PricePizzaSumSauce { get; set; }
    public virtual int? PricePizzaSumTopping { get; set; }
    public virtual int? CountPizzaInt { get; set; }
    public virtual int? PriceOnepizza { get; set; }
    public virtual int? PriceSumPizza { get; set; }
    public virtual long? TTempOrderId { get; set; }
    public virtual IList<TPizzaTempSabadCheese> TPizzaTempSabadCheese { get; set; }
    public virtual IList<TPizzaTempSabadSauce> TPizzaTempSabadSauce { get; set; }
    public virtual IList<TPizzaTempSabadTopping> TPizzaTempSabadTopping { get; set; }
}
}