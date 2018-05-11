using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;
namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_account_t_role
    {
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public account_t_role Get_Role(string UserID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    account_r_users_roles user_role = db.account_r_users_roles.Where(n => n.UserID == UserID).FirstOrDefault();
                    if (user_role != null)
                    {
                        account_t_role r = db.account_t_role.FirstOrDefault(n => n.RoleID == user_role.RoleID);
                        return r;
                    }
                    else return null;
                }
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
