using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPizzaTempSabadSauce
    {
        public virtual long IdPizzaTempSabadSauce { get; set; }
        public virtual TPizzaSauce TPizzaSauce { get; set; }
        public virtual TPizzaTempSabad TPizzaTempSabad { get; set; }
        public virtual DateTime? DateCreate { get; set; }
    }
}