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
        private static SHA256 sha256 = SHA256.Create();
        protected byte[] GetUserHash(string userName)
            => sha256.ComputeHash(Encoding.ASCII.GetBytes(userName));

        public byte[] Userhash { get; }

        protected BaseModel(string userName)
        {
            Userhash = GetUserHash(userName);
        }
    }
}
