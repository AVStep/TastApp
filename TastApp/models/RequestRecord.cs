using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TastApp.models
{
    public class RequestRecord
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public string CityName { get; set; }
        public string Body { get; set; }
    }
}
