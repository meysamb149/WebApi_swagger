using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLNoePay
    {
        public TLNoePay()
        {
            TOrder = new List<TOrder>();
            TTemporder = new List<TTemporder>();
        }
        public virtual int IdNoePay { get; set; }
        [StringLength(10)]
        public virtual string Title { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}

