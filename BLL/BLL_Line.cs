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
            using (sartas3 db = new sartas3())
            {
                line l = db.line.Where(n => n.ID == lid).FirstOrDefault();
                return l != null ? l.LineCode : null;
            }
        }
        /// <summary>
        /// 获取所有线路
        /// </summary>
        /// <returns></returns>
        public List<line> GetAllLines()
        {
            using (sartas3 db = new sartas3())
            {
                List<line> ls = db.line.ToList();
                return ls;
            }
        }
    }
}
