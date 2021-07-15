using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLVaziyatTahvil
    {
        public TLVaziyatTahvil()
        {
            TOrder = new List<TOrder>();
        }
        public virtual int IdVaziyatTahvil { get; set; }
        [StringLength(50)]
        public virtual string TitelsVaziyatTahvil { get; set; }

        public virtual string TextForUser { get; set; }
        public virtual string TextForServiser { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
    }
}
