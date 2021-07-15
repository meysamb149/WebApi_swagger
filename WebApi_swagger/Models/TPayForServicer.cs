using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TPayForServicer
    {
        public virtual int IdPayForServicer { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TCartForPayServicer TCartForPayServicer { get; set; }
        public virtual TLVaziyatVarizi TLVaziyatVarizi { get; set; }
        public virtual int? MablaghVarizi { get; set; }
        public virtual DateTime DateVarizi { get; set; }
    }
}
