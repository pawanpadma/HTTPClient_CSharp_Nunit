using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace HTTPClient_CSharp_Nunit.Config
{
    public class ReadEnvType
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IniData parsedData;
        public Read_Ini_File handler;

        public ReadEnvType()
        {
            handler = new Read_Ini_File();
            string env = handler.GetProperty("environment", "environmentType");

            if (env.Equals("qa", StringComparison.OrdinalIgnoreCase))
            {
                IniParser.FileIniDataParser parser = new FileIniDataParser();

                parsedData = parser.ReadFile(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\QA_Environment.ini"));
            }
            else if (env.Equals("dev", StringComparison.OrdinalIgnoreCase))
            {
                IniParser.FileIniDataParser parser = new FileIniDataParser();

                parsedData = parser.ReadFile(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\dev.ini"));
            }
            else if (env.Equals("int", StringComparison.OrdinalIgnoreCase))
            {
                IniParser.FileIniDataParser parser = new FileIniDataParser();

                parsedData = parser.ReadFile(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\Int_Environment.ini"));
            }
        }

        public string GetProperty(string section, string key)
        {
            KeyDataCollection keyDataCollection = parsedData[section];
            log.Info("Key " + key + " value is " + keyDataCollection[key]);
            return keyDataCollection[key];
        }
    }
}