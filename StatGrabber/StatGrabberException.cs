using System;
using System.Collections.Generic;
using System.Text;

namespace StatGrabber
{
    [Serializable]
    public class StatGrabberException : Exception
    {
        public StatGrabberException()
        {
        }

        public StatGrabberException( string message )
            : base( message )
        {
        }
        public StatGrabberException( string message, Exception innerException )
            : base( message, innerException )
        {
        }
    }
}
