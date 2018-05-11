using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOEC_SASTAD_Data.Model;

namespace TOEC_SASTAD_Data.BLL
{
    public class BLL_account_t_function
    {
        public List<account_t_function> Get_Function(string RoleID)
        {
            try
            {
                using (sartasEntities db = new sartasEntities())
                {
                    var q = from f_r in db.account_r_role_function
                            join f in db.account_t_function on f_r.FunctionID equals f.ID
                            where f_r.RoleID == RoleID
                            select f;
                    return q.ToList();
                }
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
