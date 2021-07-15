
using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public partial class TCGoZarfiyatShServiser
    {
        public virtual long IdCGoZarfiyatShServiser { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual int? ShiftId { get; set; }
        public virtual int? CountGoService { get; set; }
        public virtual DateTime? Datenowgo { get; set; }
        public virtual int? CountDeliveryService { get; set; }
    }
}