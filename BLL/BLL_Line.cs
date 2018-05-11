using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;
namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_Line
    {
        /// <summary>
        /// 获取线路文件夹名称
        /// </summary>
        /// <param name="lid"></param>
        /// <returns></returns>
        public string GetLineFolderName(int lid)
        {
            using (sartasEntities db = new sartasEntities())
            {
                line l = db.line.Where(n => n.Line_ID == lid).FirstOrDefault();
                return l != null ? l.GQCode : null;
            }
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<line> GetAllLines()
        {
            using (sartasEntities db = new sartasEntities())
            {
                List<line> ls = db.line.ToList();
                return ls;
            }
        }
    }
}
