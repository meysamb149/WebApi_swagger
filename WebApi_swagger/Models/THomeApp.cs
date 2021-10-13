using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi_swagger.Models
{
    public class THomeApp
    {
        public virtual int IdHomeApp { get; set; }
        public virtual TLActive TLActive { get; set; }
        public virtual TServicer TServicer { get; set; }
        [StringLength(7)]
        public virtual string BackgroundColorStr { get; set; }
        [StringLength(500)]
        public virtual string IconImgUrlStr { get; set; }
        [StringLength(50)]
        public virtual string TitlseStr { get; set; }
        public virtual DateTime? StartShowDt { get; set; }
        public virtual DateTime? EndShowDt { get; set; }
    }
}
