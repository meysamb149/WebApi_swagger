using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLVaziyatVarizi
    {
        public TLVaziyatVarizi()
        {
            TOrder = new List<TOrder>();
            TPayForServicer = new List<TPayForServicer>();
            TTemporder = new List<TTemporder>();
        }
        public virtual int IdVaziyatVarizi { get; set; }
        [StringLength(50)]
        public virtual string TitelsVaziyatVarizi { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TPayForServicer> TPayForServicer { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
