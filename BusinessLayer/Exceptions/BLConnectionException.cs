using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Exceptions
{
    [Serializable]
    public class BLConnectionException : Exception
    {
        public BLConnectionException()
        {
        }

        public BLConnectionException(string message) : base(message)
        {
        }

        public BLConnectionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
