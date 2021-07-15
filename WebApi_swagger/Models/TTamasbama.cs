using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TTamasbama
    {
        public virtual long IdTamasbama { get; set; }
        public virtual TUsers TUsers { get; set; }
        [StringLength(12)]
        public virtual string UserPhone { get; set; }
        public virtual string UserText { get; set; }
    }
}