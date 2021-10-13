using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLVaziyatSabad
    {
        public TLVaziyatSabad()
        {
            TTempOrder = new List<TTempOrder>();
        }
        public virtual int IdVaziyatSabad { get; set; }
        [StringLength(25)]
        public virtual string Titels { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}
