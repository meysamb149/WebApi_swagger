using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TGroup
    {
        public TGroup()
        {
            TGroupProduct = new List<TGroupProduct>();
            TNoeProduct = new List<TNoeProduct>();
        }
        public virtual int IdGroup { get; set; }
        [StringLength(50)]
        public virtual string NameGroup { get; set; }
        public virtual IList<TGroupProduct> TGroupProduct { get; set; }
        public virtual IList<TNoeProduct> TNoeProduct { get; set; }
    }
}
