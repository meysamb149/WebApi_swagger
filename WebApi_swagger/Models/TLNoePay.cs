using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLNoePay
    {
        public TLNoePay()
        {
            TTempOrder = new List<TTempOrder>();
        }
        public virtual int IdNoePay { get; set; }
        [StringLength(10)]
        public virtual string Title { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}
