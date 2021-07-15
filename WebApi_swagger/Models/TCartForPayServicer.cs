using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TCartForPayServicer
    {
        public TCartForPayServicer()
        {
            TPayForServicer = new List<TPayForServicer>();
        }
        public virtual int IdCartForPayServicer { get; set; }
        public virtual int? ServicerId { get; set; }
        [StringLength(50)]
        public virtual string NumberCart { get; set; }
        [StringLength(50)]
        public virtual string ShabaCart { get; set; }
        [StringLength(50)]
        public virtual string NumHesab { get; set; }
        [StringLength(50)]
        public virtual string NameBank { get; set; }
        [StringLength(50)]
        public virtual string NameShobeh { get; set; }
        [StringLength(50)]
        public virtual string NameMalekCart { get; set; }
        public virtual IList<TPayForServicer> TPayForServicer { get; set; }
    }
}
