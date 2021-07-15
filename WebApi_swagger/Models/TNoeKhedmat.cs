using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TNoeKhedmat
    {
        public TNoeKhedmat()
        {
            TNoeProduct = new List<TNoeProduct>();
        }
        public virtual int IdNoeKhedmat { get; set; }
        [StringLength(50)]
        public virtual string TitelsNoeKhedmat { get; set; }
        public virtual IList<TNoeProduct> TNoeProduct { get; set; }
        public virtual IList<TSabad> TSabad { get; set; }
    }
}
