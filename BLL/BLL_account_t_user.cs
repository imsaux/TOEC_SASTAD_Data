using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;

namespace TOEC_SASTAD_Data.BLL
{
    /// <summary>
    /// 用户相关业务逻辑
    /// </summary>
    public class BLL_account_t_user
    {
        public account_t_user Get_User(string UserName, string Password)
        {
            try
            {
                using (sartas3 db = new sartas3())
                {
                    account_t_user user = db.account_t_user.Where(n => n.UserName == UserName && n.Password == Password).FirstOrDefault();

                    return user;
                }
            }
            catch (Exception ex) { throw ex; }

        }
        public account_t_user Get_User(string UserID)
        {
            try
            {
                using (sartas3 db = new sartas3())
                {
                    account_t_user user = db.account_t_user.Where(n => n.UserID == UserID).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
