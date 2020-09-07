using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient_CSharp_Nunit.ExtentReport
{
    public class ExtentReport
    {
        /// <summary>
        /// Create new instance of Extent report
        /// </summary>
        // private static readonly ExtentReports _instance = new ExtentReports(TestContext.CurrentContext.TestDirectory + "\\TestResults.html");
        // public IniHandler handler = new IniHandler(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\config.ini"));

        private static string dir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));

        public static string fileName = dir + "//HTTPClient_CSharp_Nunit//HtmlReports//" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").Replace("-", "_").Replace(":", "_") + ".html";

        //ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(fileName);
        private static readonly ExtentHtmlReporter _instance = new ExtentHtmlReporter(fileName);

        static ExtentReport()
        {
        }

        private ExtentReport()
        {
        }

        /// <summary>
        /// Property to return the instance of the report.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ExtentHtmlReporter Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}