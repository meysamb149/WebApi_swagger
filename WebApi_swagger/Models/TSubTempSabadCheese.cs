using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TSubTempSabadCheese
    {
        public virtual long IdSubTempSabadCheese { get; set; }
        public virtual TSubTempSabad TSubTempSabad { get; set; }
        public virtual TSubCheese TSubCheese { get; set; }
        public virtual int? Price { get; set; }
        public virtual DateTime? DateCreate { get; set; }
    }
}