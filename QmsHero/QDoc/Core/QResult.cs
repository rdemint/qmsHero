using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QResult
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected QResult(bool success, string error)
        {
            Contract.Requires(success || !string.IsNullOrEmpty(error));
            Contract.Requires(!success || string.IsNullOrEmpty(error));

            Success = success;
            Error = error;
        }

        public static QResult Fail(string message)
        {
            return new QResult(false, message);
        }

        public static QResult<T> Fail<T>(string message)
        {
            return new QResult<T>(default(T), false, message);
        }

        public static QResult Ok()
        {
            return new QResult(true, String.Empty);
        }

        public static QResult<T> Ok<T>(T value)
        {
            return new QResult<T>(value, true, String.Empty);
        }

        public static QResult Combine(params QResult[] results)
        {
            foreach (QResult result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }

    public class QResult<T> : QResult
    {
        private T _value;

        public T Value
        {
            get
            {
                Contract.Requires(Success);

                return _value;
            }
            private set { _value = value; }
        }

        protected internal QResult(T value, bool success, string error)
            : base(success, error)
        {
            Contract.Requires(value != null || !success);

            Value = value;
        }
    }
}
