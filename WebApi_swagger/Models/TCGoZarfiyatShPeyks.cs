using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TCGoZarfiyatShPeyks
    {
        public virtual long IdCGoZarfiyatShPeyk { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual int? CountGoPeyks { get; set; }
        public virtual DateTime? Datenowgo { get; set; }
    }
}
