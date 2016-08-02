using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    enum msgType { video, text } ;
    class message
    {
        private String msgContent { get; set; }
        private bool isAccessed { get; set; }
        private msgType type { get; set; }
        private String path { get; set; }

        public message(String msgContent, msgType type, String path = null)
        {
            this.msgContent = msgContent;
            this.type = type;
            this.path = path;
        }

        
    }

}
