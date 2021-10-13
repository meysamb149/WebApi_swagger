﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class TPizzaSauce
    {
        public TPizzaSauce()
        {
            TPizzaTempSabadSauce = new List<TPizzaTempSabadSauce>();
        }
        public virtual int IdPizzaSauce { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(50)]
        public virtual string TitlesStr { get; set; }
        [StringLength(150)]
        public virtual string DescriptionStr { get; set; }
        [StringLength(500)]
        public virtual string ImgUrlStr { get; set; }
        public virtual int? PriceForpizzaSize1Int { get; set; }
        public virtual int? PriceForpizzaSize2Int { get; set; }
        public virtual int? PriceForpizzaSize3Int { get; set; }
        public virtual IList<TPizzaTempSabadSauce> TPizzaTempSabadSauce { get; set; }
    }
}
