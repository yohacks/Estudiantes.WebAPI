namespace Utilitary.Core.Common.Exceptions
{
    using System;

    public class DeleteException : Exception
    {
        public DeleteException(string name, object key, string message)
            : base($"Deletion of entity \"{name}\" ({key}) failed. {message}")
        {
        }
    }
}
