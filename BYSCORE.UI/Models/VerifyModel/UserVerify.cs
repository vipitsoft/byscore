using System;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class UserVerify
    {
        public UserVerify()
        {
        }

        public static Tuple<string, bool> LoginVerify(User user)
        {
            Tuple<string, bool> tuple = null;
            if (string.IsNullOrEmpty(user.UserName))
            {
                return tuple = new Tuple<string, bool>("用户名为空！", false);
            }

            if (string.IsNullOrEmpty(user.PassWord))
            {
                return tuple = new Tuple<string, bool>("密码为空！", false);
            }

            return tuple = new Tuple<string, bool>("", true);
        }
    }
}
