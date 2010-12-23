using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace unforgettablemeuk
{
    public static class Tessl
    {
        public enum ConfigurationType
        {
            Showtime=0,
            Test=1
        }

        public static ConfigurationType ConfigurationMode
        {
            get { return _mode; }
            set 
            {
                if ( _implementation==null )
                {
                    lock ( instanceLock )
                    {
                        if ( _implementation == null )
                        {
                            _implementation= new object();
                            _mode= value;
                        }
                    }
                } else
                {
                    throw new InvalidOperationException( "Tessl has already been instantiated, so the mode can no longer be set." );
                }
            }
        }
        private static ConfigurationType _mode;
        private static object _implementation;
        private static object instanceLock = new object();

        public static T New<T>() where T : new()
        {
            return new T();
        }

        public static T New<T,P1>(P1 p1) where T : new()
        {
            return (T)typeof(T).GetConstructor(new System.Type[] { typeof(P1) }).Invoke(new object[] { p1 });
        }

        public static T New<T,P1,P2>(P1 p1, P2 p2) where T : new()
        {
            return (T)typeof(T).GetConstructor(new System.Type[] { typeof(P1), typeof(P2) }).Invoke(new object[] { p1, p2 });
        }

        public static T New<T, P1, P2, P3>(P1 p1, P2 p2, P3 p3) where T : new()
        {
            return (T)typeof(T).GetConstructor(new System.Type[] { typeof(P1), typeof(P2), typeof(P3) }).Invoke(new object[] { p1, p2, p3 });
        }

        public static T New<T, P1, P2, P3, P4>( P1 p1, P2 p2, P3 p3, P4 p4) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ), typeof( P3 ), typeof( P4 ) } ).Invoke( new object[] { p1, p2, p3, p4 } );
        }

        public static T New<T, P1, P2, P3, P4, P5>( P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ), typeof( P3 ), typeof( P4 ), typeof( P5 ) } ).Invoke( new object[] { p1, p2, p3, p4, p5 } );
        }

        public static T Init<T>( T prototype ) where T : new()
        {
            return prototype;
        }

        public static T Build<T>( Delegate method, params object[] args )
        {
            return (T)method.DynamicInvoke( args );
        }

        public static TResult Build<TResult, P1>( Func<P1, TResult> method, P1 p1 )
        {
            return (TResult)method( p1 );
        }

        public static TResult Build<TResult, P1, P2>( Func<P1, P2, TResult> method, P1 p1, P2 p2 )
        {
            return (TResult)method( p1, p2 );
        }

        public static TResult Build<TResult, P1, P2, P3>( Func<P1, P2, P3, TResult> method, P1 p1, P2 p2, P3 p3 )
        {
            return (TResult)method( p1, p2, p3 );
        }

        public static TResult Build<TResult, P1, P2, P3, P4>( Func<P1, P2, P3, P4, TResult> method, P1 p1, P2 p2, P3 p3, P4 p4 )
        {
            return (TResult)method( p1, p2, p3, p4 );
        }

    }
}
