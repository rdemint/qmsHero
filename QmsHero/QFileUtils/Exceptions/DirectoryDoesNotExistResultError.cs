using FluentResults;

namespace QFileUtil.Exceptions
{
    public class DirectoryDoesNotExistResultError : Error
    {
        public DirectoryDoesNotExistResultError()
        {
        }

        public DirectoryDoesNotExistResultError(string message) : base(message)
        {
        }

        public DirectoryDoesNotExistResultError(string message, Error causedBy) : base(message, causedBy)
        {
        }
    }
}