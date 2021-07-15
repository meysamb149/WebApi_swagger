using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApi_swagger.Models
{

    public partial class TAllQuestions
    {
        public virtual int IdQuestions { get; set; }
        public virtual string TitrQuestions { get; set; }
        public virtual string TextQuestions { get; set; }
        public virtual int? GroupId { get; set; }
    }
}