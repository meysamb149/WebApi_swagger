using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TSubTopping
    {
        public TSubTopping()
        {
            TSubTempSabadTopping = new List<TSubTempSabadTopping>();
        }
        public virtual int IdSubTopping { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(50)]
        public virtual string TitlesStr { get; set; }
        [StringLength(150)]
        public virtual string DescriptionStr { get; set; }
        [StringLength(500)]
        public virtual string ImgUrlStr { get; set; }
        public virtual int? PriceInt { get; set; }
        public virtual IList<TSubTempSabadTopping> TSubTempSabadTopping { get; set; }
    }
}