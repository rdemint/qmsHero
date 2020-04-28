using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public static class QResultExtensions
    {
        public static QResult OnSuccess(this QResult result, Func<QResult> func)
        {
            if (result.Failure)
                return result;

            return func();
        }

        public static QResult OnSuccess(this QResult result, Action action)
        {
            if (result.Failure)
                return result;

            action();

            return QResult.Ok();
        }

        public static QResult OnSuccess<T>(this QResult<T> result, Action<T> action)
        {
            if (result.Failure)
                return result;

            action(result.Value);

            return QResult.Ok();
        }

        public static QResult<T> OnSuccess<T>(this QResult result, Func<T> func)
        {
            if (result.Failure)
                return QResult.Fail<T>(result.Error);

            return QResult.Ok(func());
        }

        public static QResult<T> OnSuccess<T>(this QResult result, Func<QResult<T>> func)
        {
            if (result.Failure)
                return QResult.Fail<T>(result.Error);

            return func();
        }

        public static QResult OnSuccess<T>(this QResult<T> result, Func<T, QResult> func)
        {
            if (result.Failure)
                return result;

            return func(result.Value);
        }

        public static QResult OnFailure(this QResult result, Action action)
        {
            if (result.Failure)
            {
                action();
            }

            return result;
        }

        public static QResult OnBoth(this QResult result, Action<QResult> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this QResult result, Func<QResult, T> func)
        {
            return func(result)
        }
    }
}
