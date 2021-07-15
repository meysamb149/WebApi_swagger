using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TServicerForMahalehByPeyk
    {
        public virtual long IdServicerForMahalehByPeyk { get; set; }
        public virtual TServicerForMahaleh TServicerForMahaleh { get; set; }
        public virtual TPeyks TPeyks { get; set; }
        public virtual TShift TShift { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual int? OlaviatEntekhab { get; set; }
        public virtual int? Countservice { get; set; }
    }
}
