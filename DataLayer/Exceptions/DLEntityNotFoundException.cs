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
