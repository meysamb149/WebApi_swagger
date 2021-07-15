using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TLVaziyatSabad
    {
        public TLVaziyatSabad()
        {
            TSabad = new List<TSabad>();
            TTemporder = new List<TTemporder>();
        }
        public virtual int IdVaziyatSabad { get; set; }
        [StringLength(25)]
        public virtual string Titels { get; set; }
        public virtual IList<TSabad> TSabad { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
    }
}
