using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPizzaTempSabadTopping
    {
        public virtual long IdPizzaTempSabadTopping { get; set; }
        public virtual TPizzaTopping TPizzaTopping { get; set; }
        public virtual TLPortionPizza TLPortionPizza { get; set; }
        public virtual TPizzaTempSabad TPizzaTempSabad { get; set; }
        public virtual int? Price { get; set; }
        public virtual DateTime? DateCreate { get; set; }
    }
}