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

        public static T New<T,P1>(P1 p1) where T : new()
        {
            return (T)typeof(T).GetConstructor(new System.Type[] { typeof(P1) }).Invoke(new object[] { p1 });
        }

    }
}
