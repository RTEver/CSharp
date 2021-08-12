using System;
using System.Text;

namespace MulticastDelegate_GetInvocationList
{
    internal static class Program : Object
    {
        private delegate String GetStatus();

        private static void Main(String[] args)
        {
            var getStatus = default(GetStatus);

            getStatus += new GetStatus(new Light().SwitchPosition);
            getStatus += new GetStatus(new Fan().Speed);
            getStatus += new GetStatus(new Speaker().Volume);

            Console.WriteLine(GetComponentStatusReport(getStatus));
        }

        private static String GetComponentStatusReport(GetStatus status)
        {
            if (status == null)
            {
                return null;
            }

            var report = new StringBuilder();

            var arrayOfDelegates = status.GetInvocationList();

            foreach (GetStatus getStatus in arrayOfDelegates)
            {
                try
                {
                    report.AppendFormat("{0}{1}{1}", getStatus(), Environment.NewLine);
                }
                catch (InvalidOperationException ex)
                {
                    var component = getStatus.Target;

                    report.AppendFormat(
                        "Failed to get status from {1}{2}{0} Error: {3}{0}{0}",
                        Environment.NewLine,
                        ((component == null) ? "" : component.GetType() + "."),
                        getStatus.Method.Name,
                        ex.Message);
                }
            }

            return report.ToString();
        }
    }
}