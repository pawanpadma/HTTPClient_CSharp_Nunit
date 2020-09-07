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
    public class Read_Ini_File
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IniData parsedData;

        public Read_Ini_File()
        {
            string fileName = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\config.ini");
            if (!File.Exists(fileName))
            {
                throw new IOException("File " + fileName + " is not exist");
            }
            IniParser.FileIniDataParser parser = new FileIniDataParser();
            parsedData = parser.ReadFile(fileName);
        }

        public string GetProperty(string section, string key)
        {
            KeyDataCollection keyDataCollection = parsedData[section];
            log.Info("Key " + key + " value is " + keyDataCollection[key]);
            return keyDataCollection[key];
        }
    }
}