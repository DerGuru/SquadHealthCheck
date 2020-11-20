using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SquadHealthCheck.Models
{
    public class BinaryComparer : IEqualityComparer<byte[]>
    {
        public BinaryComparer()
        {
        }

        public bool Equals(byte[] x, byte[] y)
        {
            if (x.Length != y.Length) return false;

            for (long i = 0; i < x.LongLength; i++) {
                if (x[i] != y[i]) return false;
            }

            return true;
        }

        public int GetHashCode([DisallowNull] byte[] obj) => obj.Length;
    }
}