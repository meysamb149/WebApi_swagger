using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TZarfiyatShiftPeyks
    {
        public virtual long IdZarfiyatShiftPeyks { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual int? CountService { get; set; }
        public virtual DateTime? Datenowgiv { get; set; }
    }
}
