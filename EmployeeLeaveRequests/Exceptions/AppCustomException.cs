using System.Globalization;

namespace EmployeeLeaveRequests.Exceptions
{
    public class AppCustomException : Exception
    {
        public AppCustomException() : base()
        {
        }

        public AppCustomException(string message) : base(message)
        {
        }

        public AppCustomException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
