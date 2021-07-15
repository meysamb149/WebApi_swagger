using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TNoeProduct
    {
        public TNoeProduct()
        {
            TSabad = new List<TSabad>();
        }
        public virtual long IdNoeProduct { get; set; }
        public virtual TGroupProduct TGroupProduct { get; set; }
        public virtual TNoeKhedmat TNoeKhedmat { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TGroup TGroup { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(50)]
        public virtual string NameProduct { get; set; }
        public virtual string Img { get; set; }
        [StringLength(10)]
        public virtual string Price { get; set; }
        public virtual DateTime? DateCreate { get; set; }
        [StringLength(128)]
        public virtual string UserRoleId { get; set; }
        public virtual int? Maxcount { get; set; }
        public virtual int? Mincount { get; set; }
        public virtual IList<TSabad> TSabad { get; set; }
    }
}

