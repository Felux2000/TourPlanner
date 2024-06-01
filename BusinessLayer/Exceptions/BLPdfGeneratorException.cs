using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Exceptions
{
    [Serializable]
    public class BLPdfGeneratorException : Exception
    {
        public BLPdfGeneratorException()
        {
        }

        public BLPdfGeneratorException(string message) : base(message)
        {
        }

        public BLPdfGeneratorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}