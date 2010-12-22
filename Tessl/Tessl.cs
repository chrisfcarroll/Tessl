using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace unforgettablemeuk
{
    [ CLSCompliant(true)]
    public static class Tessl
    {
        public static T New<T>() where T : new()
        {
            return new T();
        }
    }
}
