using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPizzaTempSabadCheese
    {
        public virtual long IdPizzaTempSabadCheese { get; set; }
        public virtual TPizzaTempSabad TPizzaTempSabad { get; set; }
        public virtual TPizzaCheese TPizzaCheese { get; set; }
        public virtual int? Price { get; set; }
        public virtual DateTime? DateCreate { get; set; }
    }
}