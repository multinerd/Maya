using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;

namespace Multinerd.Extensions
{
    public static class ApplicationExt
    {
        public static string CurrentVersion()
        {
            var fileName = Assembly.GetExecutingAssembly().Location;
            if (fileName != null)
                return ApplicationDeployment.IsNetworkDeployed
                    ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                    : FileVersionInfo.GetVersionInfo(fileName).ProductVersion;
            return "n/a";
        }

    }
}
