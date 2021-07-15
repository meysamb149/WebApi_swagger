using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TShift
    {
        public TShift()
        {
            TCGoZarfiyatShPeyks = new List<TCGoZarfiyatShPeyks>();
            THoliday = new List<THoliday>();
            TOrder = new List<TOrder>();
            TOrder2 = new List<TOrder>();
            TServicerForMahalehByPeyk = new List<TServicerForMahalehByPeyk>();
            TTemporder = new List<TTemporder>();
            TTemporder2 = new List<TTemporder>();
            TZarfiyatShiftPeyks = new List<TZarfiyatShiftPeyks>();
            TZarfiyatShiftServicer = new List<TZarfiyatShiftServicer>();
        }
        public virtual int IdShift { get; set; }
        public virtual TLActive TLActive { get; set; }
        [StringLength(15)]
        public virtual string ShiftText { get; set; }
        public virtual int? ShiftHhShowStartTime { get; set; }
        public virtual int? ShiftMmShowStartTime { get; set; }
        public virtual int? ShiftHhEndStartTime { get; set; }
        public virtual int? ShiftMmEndStartTime { get; set; }
        public virtual IList<THoliday> THoliday { get; set; }
        public virtual IList<TOrder> TOrder { get; set; }
        public virtual IList<TOrder> TOrder2 { get; set; }
        public virtual IList<TCGoZarfiyatShPeyks> TCGoZarfiyatShPeyks { get; set; }
        public virtual IList<TServicerForMahalehByPeyk> TServicerForMahalehByPeyk { get; set; }
        public virtual IList<TTemporder> TTemporder { get; set; }
        public virtual IList<TTemporder> TTemporder2 { get; set; }
        public virtual IList<TZarfiyatShiftPeyks> TZarfiyatShiftPeyks { get; set; }
        public virtual IList<TZarfiyatShiftServicer> TZarfiyatShiftServicer { get; set; }
    }
}
