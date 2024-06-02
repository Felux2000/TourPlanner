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