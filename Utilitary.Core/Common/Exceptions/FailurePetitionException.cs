namespace Utilitary.Core.Common.Exceptions
{
    using System;

    public class FailurePetitionException : Exception
    {
        public FailurePetitionException(string message)
            : base(message)
        {
        }
    }

}
