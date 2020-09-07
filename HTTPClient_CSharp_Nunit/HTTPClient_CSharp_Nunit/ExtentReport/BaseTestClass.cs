using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using HTTPClient_CSharp_Nunit.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient_CSharp_Nunit.ExtentReport
{
    public abstract class BaseTestClass
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ExtentReports extent;
        public static Read_Ini_File handler;
        public ExtentTest test;
        private static int i = 0;

        [OneTimeSetUp]
        public void Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            // Console.SetOut(TestContext.Progress);
            log.Info("OneTimeSetUp");
            BaseTestClass.BeginExecution();
        }

        /// <summary>
        /// Run Method Only Once After tests
        /// </summary>
        [OneTimeTearDown]
        public void End()
        {
            BaseTestClass.ExitExecution();
        }

        ///<summary>
        ///Run Before every Test and setup Tests.
        ///</summary>
        [SetUp]
        public void TestSetup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            string s = (string)TestContext.CurrentContext.Test.Properties.Get("Description");
            test.Info("Test Case Started  --> " + s);
        }

        /// <summary>
        /// Runs after every Test and Cleans up Test.
        /// </summary>
        [TearDown]
        public void TestCleanUp()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;

                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;

                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;

                default:
                    logstatus = Status.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            extent.Flush();
        }

        /// <summary>
        /// Begin execution of tests
        /// </summary>
        public static void BeginExecution()
        {
            if (i <= 0)
            {
                ExtentHtmlReporter extentReports = ExtentReport.Instance;
                handler = new Read_Ini_File();
                extent = new ExtentReports();
                extent.AddSystemInfo("Environment", handler.GetProperty("environment", "environmentType"));
                extent.AttachReporter(extentReports);
            }
            i++;
        }

        /// <summary>
        /// Finish Execution of tests
        /// </summary>
        public static void ExitExecution()
        {
            // _reportingTasks.CleanUpReporting();
        }
    }
}