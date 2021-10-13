using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLPortionPizza
    {
        public TLPortionPizza()
        {
            TPizzaTempSabadTopping = new List<TPizzaTempSabadTopping>();
        }
        public virtual int IdPortionPizza { get; set; }
        [StringLength(20)]
        public virtual string TitlesPortionPizza { get; set; }
        public virtual IList<TPizzaTempSabadTopping> TPizzaTempSabadTopping { get; set; }
    }
}