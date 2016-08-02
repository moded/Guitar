using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    class messageDB
    {
        List<message> DB;

        public messageDB()
        {
            DB = new List<message>();
        }

        public void addNewMessage(message msg)
        {
            DB.Add(msg);
        }

    }
}
