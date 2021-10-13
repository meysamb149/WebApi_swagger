using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_swagger.Models
{
    public class TAllQuestions
    {
        public virtual int IdQuestions { get; set; }
        public virtual string TitrQuestions { get; set; }
        public virtual string TextQuestions { get; set; }
        public virtual int? GroupId { get; set; }
    }
}