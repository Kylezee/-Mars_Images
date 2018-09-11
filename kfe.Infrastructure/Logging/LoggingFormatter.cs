using Newtonsoft.Json;
using System;

namespace kfe.Infrastructure.Logging
{
    public static class LoggingFormatter
    {
        public static string FormatGeneralMessage(string methodName, string message)
        {
            var output = new FormatOutput()
            {
                MethodName = methodName,
                LogDate = DateTime.Now,
                Message = $"{methodName} {message}"
            };

            return JsonConvert.SerializeObject(output);
        }

        public static string FormatStartMethodMessage(string methodName)
        {
            var output = new FormatOutput()
            {
                MethodName = methodName,
                LogDate = DateTime.Now,
                Message = $"Starting {methodName}"
            };

            return JsonConvert.SerializeObject(output);
        }

        public static string FormatExitMethodMessage(string methodName)
        {
            var output = new FormatOutput()
            {
                MethodName = methodName,
                LogDate = DateTime.Now,
                Message = $"Exit {methodName}"
            };

            return JsonConvert.SerializeObject(output);
        }

        public static string FormatExceptionMethodMessage(string methodName, Exception exception)
        {
            var output = new FormatOutput()
            {
                MethodName = methodName,
                LogDate = DateTime.Now,
                Message = $"Exception in:{methodName}:{exception.Message}:{exception.StackTrace}"
            };

            return JsonConvert.SerializeObject(output);
        }


    }
}
