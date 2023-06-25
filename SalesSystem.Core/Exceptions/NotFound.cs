namespace SalesSystem.Core.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(Guid id, Type type) : base(type.Name + $" not found with id : {id.ToString()}")
        {
        }

        public NotFound(string email, Type type) : base(type.Name + $" not found with : {email}")
        {
        }

        public NotFound(string userId, string message) : base($"{message}{userId}")
        {
        }
    }
}
