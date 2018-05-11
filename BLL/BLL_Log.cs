using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TOEC_SASTAD_Data.Model;
namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_Log
    {
        public static void Success(string ApplicationName, string FunctionName, string Detail)
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_log l = new sys_log();
                l.LogID = Guid.NewGuid().ToString();
                l.ApplicationName = ApplicationName;
                l.FunctionName = FunctionName;
                l.Detail = Detail;
                l.OpTime = DateTime.Now;
                l.State = "成功";
                db.sys_log.Add(l);
                db.SaveChanges();
            }
        }
        public static void Error(string ApplicationName, string FunctionName, string Detail)
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_log l = new sys_log();
                l.LogID = Guid.NewGuid().ToString();
                l.ApplicationName = ApplicationName;
                l.FunctionName = FunctionName;
                l.Detail = Detail;
                l.OpTime = DateTime.Now;
                l.State = "异常";
                db.sys_log.Add(l);
                db.SaveChanges();
            }
        }

        public static string Return_StateOfResult(bool SaveIsOK, string ver)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement Result = xml.CreateElement("Result");
            xml.AppendChild(Result);
            XmlElement version = xml.CreateElement("Version");
            Result.AppendChild(version);
            version.InnerText = ver;
            XmlElement State = xml.CreateElement("State");
            Result.AppendChild(State);
            State.InnerText = SaveIsOK.ToString().ToLower();
            return xml.OuterXml;
        }

    }
}
