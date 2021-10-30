using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TastApp.models
{
    public class ErrorsLog
    {
        public int ID { get; set; }
        public DateTime DateTimeErr { get; set; }
        public string ErrMessage { get; set; }
    }
}
