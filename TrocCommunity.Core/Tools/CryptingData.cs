using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace TrocCommunity.Core.Tools
{
    public class CryptingData
    {
        public static string Protect(string unprotectedText)
        {
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedText);
            var protectedBytes = MachineKey.Protect(unprotectedBytes);
            var protectedText = Convert.ToBase64String(protectedBytes);

            return protectedText;
        }

        public static string Unprotect(string protectedText)
        {

            string realText = "";
            foreach (char item in protectedText)
            {
                if (item != ' ')
                {
                    realText += item;
                }
                else
                {
                    realText += '+';
                }
            }
            var protectedBytes = Convert.FromBase64String(realText);
            var unprotectedBytes = MachineKey.Unprotect(protectedBytes);
            var unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);
            return unprotectedText;
        }




    }
}
