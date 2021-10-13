using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class THomeProducts
    {
        public THomeProducts()
        {
            THomeProductsTempSabad = new List<THomeProductsTempSabad>();
        }
        public virtual int IdHomeProductsInt { get; set; }
        public virtual TLShowPriority TLShowPriority { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(80)]
        public virtual string NameProductsStr { get; set; }
        public virtual int? PriceInt { get; set; }
        [StringLength(500)]
        public virtual string ImgUrlStr { get; set; }
        [StringLength(350)]
        public virtual string ImgRouteStr { get; set; }
        public virtual DateTime? StartShowDt { get; set; }
        public virtual DateTime? EndShowDt { get; set; }
        [StringLength(500)]
        public virtual string ImgBanerStr { get; set; }
        public virtual IList<THomeProductsTempSabad> THomeProductsTempSabad { get; set; }
    }
}
