namespace LimoncelloShop.Business.Exceptions
{
    [Serializable]
    public class ObjectExistsException : Exception
    {
        private ObjectExistsException()
        { }

        public ObjectExistsException(string message) : base(message)
        {
        }

        public ObjectExistsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ObjectExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
