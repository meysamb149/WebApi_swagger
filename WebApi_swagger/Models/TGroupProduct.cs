using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TGroupProduct
    {
        public TGroupProduct()
        {
            TNoeProduct = new List<TNoeProduct>();
        }
        public virtual int IdGroupProduct { get; set; }
        public virtual TGroup TGroup { get; set; }
        [StringLength(50)]
        public virtual string GroupProductTitels { get; set; }
        public virtual IList<TNoeProduct> TNoeProduct { get; set; }
    }
}
