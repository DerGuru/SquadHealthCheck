using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SquadHealthCheck.Models
{
    public class BaseModel
    {
        protected Guid GetUserHash(string userName)
        {
            SHA256 sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(userName));
            return new Guid(bytes);
        }

        public Guid Userhash { get; private set; }

        protected BaseModel(string userName)
        {
            Userhash = GetUserHash(userName);
        }
    }
}
