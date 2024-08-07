using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common
{
    public class Encriptation
    {

        public string Encript(string texto)
        {
            byte[] Encript=Encoding.Unicode.GetBytes(texto);
            string result=Convert.ToBase64String(Encript);
            return result;
        }
        public string Decript(string texto)
        {
            string result =string.Empty;
            byte[] decript = Convert.FromBase64String(texto);  
            result=Encoding.Unicode.GetString(decript);
            return result ;
        }
    }
}
