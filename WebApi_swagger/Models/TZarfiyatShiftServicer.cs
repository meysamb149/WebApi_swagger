using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TZarfiyatShiftServicer
    {
        public virtual long IdZarfiyatShiftServicer { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual int? CountService { get; set; }
        public virtual DateTime? Datenowgiv { get; set; }
    }
}
