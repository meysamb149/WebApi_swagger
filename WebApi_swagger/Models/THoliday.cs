using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class THoliday
    {
        public virtual int HolidayId { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual DateTime? HolidayDate { get; set; }
    }
}
