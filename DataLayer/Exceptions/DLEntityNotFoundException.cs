using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Exceptions
{
    [Serializable]
    public class DLEntityNotFoundException : Exception
    {
        public DLEntityNotFoundException()
        {
        }

        public DLEntityNotFoundException(string message) : base(message)
        {
        }

        public DLEntityNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
