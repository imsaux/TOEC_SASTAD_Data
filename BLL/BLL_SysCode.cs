using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOEC_SASTAD_Data.Model;
namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_SysCode
    {

        public List<sys_code> Get_AlarmTypeList()
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_codemap map = db.sys_codemap.SingleOrDefault(n => n.Code == "AlarmType");
                if (map != null)
                {
                    List<sys_code> res = db.sys_code.Where(n => n.CodeMap_ID == map.CodeMap_ID && n.Remark != "smart").ToList();
                    return res;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 通过MapCode，查询系统字典信息
        /// </summary>
        /// <param name="MapCode"></param>
        /// <returns></returns>
        public List<sys_code> Get_SysCodeByMapCode(string MapCode)
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_codemap map = db.sys_codemap.SingleOrDefault(n => n.Code == MapCode);
                if (map != null)
                {
                    List<sys_code> res = db.sys_code.Where(n => n.CodeMap_ID == map.CodeMap_ID).ToList();
                    return res;
                }
                else
                    return null;
            }
        }

        public string Get_SysMapCodeValue(string MapCode)
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_codemap map = db.sys_codemap.SingleOrDefault(n => n.Code == MapCode);
                if (map != null)
                {
                    return map.Value;
                }
                else
                    return null;
            }
        }

        public void Set_SysCode(sys_code c)
        {
            using (sartasEntities db = new sartasEntities())
            {
                //sys_code tmp = db.sys_code.SingleOrDefault(n => n.Code_ID == c.Code_ID);
                db.sys_code.Attach(c);
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 只更新备注字段
        /// </summary>
        /// <param name="c"></param>
        public void Set_SysCodeRemark(sys_code c)
        {
            using (sartasEntities db = new sartasEntities())
            {
                sys_code tmp = db.sys_code.SingleOrDefault(n => n.Code_ID == c.Code_ID);
                if (tmp != null)
                {
                    tmp.Remark = c.Remark;
                    db.SaveChanges();
                }
            }
        }
    }
}
