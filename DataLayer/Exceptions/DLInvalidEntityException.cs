using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Exceptions
{
    [Serializable]
    public class DLInvalidEntityException : Exception
    {
        public DLInvalidEntityException()
        {
        }

        public DLInvalidEntityException(string message) : base(message)
        {
        }

        public DLInvalidEntityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
