using HTTPClient_CSharp_Nunit.Common_HTTP_Methods;
using HTTPClient_CSharp_Nunit.Config;
using HTTPClient_CSharp_Nunit.ExtentReport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient_CSharp_Nunit.TestCases
{
    public class CreateUser : BaseTestClass
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Read_Ini_File read;
        public ReadEnvType readEnv;
        public HTTPMethods method;

        public CreateUser()
        {
            method = new HTTPMethods();
            read = new Read_Ini_File();
            readEnv = new ReadEnvType();
        }

        [Test, Description("Test case to retrieve score card details")]
        [Category("1")]
        [Category("Regression")]
        public async Task CreateNewUser()
        {
            string Rnd_string = Path.GetRandomFileName();
            Rnd_string = Rnd_string.Replace(".", "");
            JObject file = JObject.Parse(File.ReadAllText(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\payloads\CreateUser.json")));
            file["name"] = Rnd_string;
            file["email"] = Rnd_string + "@getnada.com";
            StringContent CreateUserPayload = new StringContent(file.ToString(), Encoding.UTF8, "application/json");
            log.Info("modified payload is   " + file.ToString());
            test.Info("modified payload is   " + file.ToString());
            string Authtoken = readEnv.GetProperty("base", "token");
            test.Info("token is    " + Authtoken);
            HttpResponseMessage output = await method.PostMethod(read.GetProperty("environment", "createUser"), CreateUserPayload, Authtoken);
            log.Info("out put is   " + output);
            test.Info("out put is   " + output);
            int resultStatus = (int)output.StatusCode;
            string result = await output.Content.ReadAsStringAsync();
            log.Info("Response Status: " + resultStatus);
            log.Info("Response Content: " + result);
            test.Info("Response Status: " + resultStatus);
            test.Info("Response Content: " + result);
            Assert.AreEqual(200, resultStatus);
            log.Info("Test case to create user Passed");
            test.Info("Test case to create user Passed");
        }
    }
}