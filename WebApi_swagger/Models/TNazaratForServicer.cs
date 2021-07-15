using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TNazaratForServicer
    {
        public virtual long IdNazaratForServicer { get; set; }
        public virtual TUsers TUsers { get; set; }
        public virtual TServicer TServicer { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual DateTime DateCreate { get; set; }
        public virtual long? OrderId { get; set; }
        public virtual int? NumRank { get; set; }
        public virtual string TextNazar { get; set; }
    }
}
