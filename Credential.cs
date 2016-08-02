using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    public class Credential
    {
        public string id { get; set; }
        public string Password { get; set; }
        public string pathToPic { get; set; }
        public int lastLesson { get; set; }
        public int unreadMsg { get; set; }
    }
}
