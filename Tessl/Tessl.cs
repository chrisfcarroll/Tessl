using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CafeEncounterNet
{
    public static class Tessl
    {
        public static ITessl Configuration
        {
            get 
            {
                if ( _implementation==null )
                {
                    Configuration= new TesslProductionImplementation();
                }
                return _implementation; 
            }
            set 
            {
                if ( _implementation==null )
                {
                    lock ( instanceLock )
                    {
                        if ( _implementation == null )
                        {
                            _implementation= value;
                            return;
                        }
                    }
                }
                throw new InvalidOperationException( "Tessl has already been instantiated, so the mode can no longer be set." );
            }
        }

        /// <summary>
        /// Should only be called from a TestFixture which is testing Tessl.
        /// </summary>
        internal static void deconfigure()
        {
            _implementation=null;
        }

        private static ITessl _implementation;
        private static object instanceLock = new object();

        public static T New<T>() where T : new()
        {
            return Configuration.New<T>();
        }

        public static T New<T,P1>(P1 p1) where T : new()
        {
            return Configuration.New<T,P1>(p1);
        }

        public static T New<T,P1,P2>(P1 p1, P2 p2) where T : new()
        {
            return Configuration.New<T, P1, P2>( p1, p2 );
        }

        public static T New<T, P1, P2, P3>(P1 p1, P2 p2, P3 p3) where T : new()
        {
            return Configuration.New<T, P1, P2, P3>( p1, p2, p3 );
        }

        public static T New<T, P1, P2, P3, P4>( P1 p1, P2 p2, P3 p3, P4 p4) where T : new()
        {
            return Configuration.New<T, P1, P2, P3, P4>( p1, p2, p3, p4 );
        }

        public static T New<T, P1, P2, P3, P4, P5>( P1 p1, P2 p2, P3 p3, P4 p4, P5 p5) where T : new()
        {
            return Configuration.New<T, P1, P2, P3, P4, P5>( p1, p2, p3, p4, p5 );
        }

        public static T Init<T>( T prototype ) where T : new()
        {
            return Configuration.Init<T>( prototype );
        }

        public static T From<T>( Delegate method, params object[] args )
        {
            return Configuration.From<T>( method, args );
        }

        public static TResult From<P1, TResult>( Func<P1, TResult> method, P1 p1 )
        {
            return Configuration.From<P1, TResult>( method, p1 );
        }

        public static TResult From<P1, P2, TResult>( Func<P1, P2, TResult> method, P1 p1, P2 p2 )
        {
            return Configuration.From<P1, P2, TResult>( method, p1, p2 );
        }

        public static TResult From<P1, P2, P3, TResult>( Func<P1, P2, P3, TResult> method, P1 p1, P2 p2, P3 p3 )
        {
            return Configuration.From<P1, P2, P3, TResult>( method, p1, p2, p3 );
        }

        public static TResult From<P1, P2, P3, P4, TResult>( Func<P1, P2, P3, P4, TResult> method, P1 p1, P2 p2, P3 p3, P4 p4 )
        {
            return Configuration.From<P1, P2, P3, P4, TResult>( method, p1, p2, p3, p4 );
        }

    }

    public interface ITessl
    {
        T New<T>() where T : new();

        T New<T, P1>( P1 p1 ) where T : new();

        T New<T, P1, P2>( P1 p1, P2 p2 ) where T : new();

        T New<T, P1, P2, P3>( P1 p1, P2 p2, P3 p3 ) where T : new();

        T New<T, P1, P2, P3, P4>( P1 p1, P2 p2, P3 p3, P4 p4 ) where T : new();

        T New<T, P1, P2, P3, P4, P5>( P1 p1, P2 p2, P3 p3, P4 p4, P5 p5 ) where T : new();

        T Init<T>( T prototype ) where T : new();

        T From<T>( Delegate method, params object[] args );

        TResult From<P1, TResult>( Func<P1, TResult> method, P1 p1 );

        TResult From<P1, P2, TResult>( Func<P1, P2, TResult> method, P1 p1, P2 p2 );

        TResult From<P1, P2, P3, TResult>( Func<P1, P2, P3, TResult> method, P1 p1, P2 p2, P3 p3 );

        TResult From<P1, P2, P3, P4, TResult>( Func<P1, P2, P3, P4, TResult> method, P1 p1, P2 p2, P3 p3, P4 p4 );
    }

    public class TesslProductionImplementation : ITessl
    {
        public T New<T>() where T : new()
        {
            return new T();
        }

        public T New<T, P1>( P1 p1 ) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ) } ).Invoke( new object[] { p1 } );
        }

        public T New<T, P1, P2>( P1 p1, P2 p2 ) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ) } ).Invoke( new object[] { p1, p2 } );
        }

        public T New<T, P1, P2, P3>( P1 p1, P2 p2, P3 p3 ) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ), typeof( P3 ) } ).Invoke( new object[] { p1, p2, p3 } );
        }

        public T New<T, P1, P2, P3, P4>( P1 p1, P2 p2, P3 p3, P4 p4 ) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ), typeof( P3 ), typeof( P4 ) } ).Invoke( new object[] { p1, p2, p3, p4 } );
        }

        public T New<T, P1, P2, P3, P4, P5>( P1 p1, P2 p2, P3 p3, P4 p4, P5 p5 ) where T : new()
        {
            return (T)typeof( T ).GetConstructor( new System.Type[] { typeof( P1 ), typeof( P2 ), typeof( P3 ), typeof( P4 ), typeof( P5 ) } ).Invoke( new object[] { p1, p2, p3, p4, p5 } );
        }

        public T Init<T>( T prototype ) where T : new()
        {
            return prototype;
        }

        public T From<T>( Delegate method, params object[] args )
        {
            return (T)method.DynamicInvoke( args );
        }

        public TResult From<P1, TResult>( Func<P1, TResult> method, P1 p1 )
        {
            return (TResult)method( p1 );
        }

        public TResult From<P1, P2, TResult>( Func<P1, P2, TResult> method, P1 p1, P2 p2 )
        {
            return (TResult)method( p1, p2 );
        }

        public TResult From<P1, P2, P3, TResult>( Func<P1, P2, P3, TResult> method, P1 p1, P2 p2, P3 p3 )
        {
            return (TResult)method( p1, p2, p3 );
        }

        public TResult From<P1, P2, P3, P4, TResult>( Func<P1, P2, P3, P4, TResult> method, P1 p1, P2 p2, P3 p3, P4 p4 )
        {
            return (TResult)method( p1, p2, p3, p4 );
        }
    }
}
