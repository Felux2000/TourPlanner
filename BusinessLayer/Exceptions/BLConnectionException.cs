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
