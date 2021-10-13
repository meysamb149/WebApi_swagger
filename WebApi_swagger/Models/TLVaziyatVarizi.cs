using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TLVaziyatVarizi
    {
        public TLVaziyatVarizi()
        {
            TTempOrder = new List<TTempOrder>();
        }
        public virtual int IdVaziyatVarizi { get; set; }
        [StringLength(50)]
        public virtual string TitelsVaziyatVarizi { get; set; }
        public virtual IList<TTempOrder> TTempOrder { get; set; }
    }
}
