using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar
{
    class user
    {
        private String name;
        private String password;
        private String mail;
        private String picPath;

        user(String name, String password, String mail, String picPath)
        {
            this.name = name;
            this.password = password;
            this.mail = mail;
            this.picPath = picPath;
        }

        public bool isSamePassword (String password)
        {
            return this.password.Equals(password);
        }

        public String getName()
        {
            return this.name;
        }

        public String getPicPath()
        {
            return this.picPath;
        }


    }


}
