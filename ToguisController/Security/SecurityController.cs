/***********************************************************************************************
 * Project: Tourist Guide System Toguis Web Services
 * University: UNIAJC
 * Authors: Julieth Candia and Carlos Morante
 * Year: 2014 - 2015
 * Version: 1.0 
 * License: GPL V2
 ***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToguisModel;

namespace ToguisController.Security
{
    public class SecurityController
    {
        public TG_USER GetUser(string login)
        {
            TG_USER loResult = null;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Configuration.ProxyCreationEnabled = false;
                    loResult = (from item in loContext.TG_USER
                                where item.USR_ID.ToLower().Equals(login)
                                select item).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }

            }
            return loResult;
        }

        public int CreateUser(TG_USER user)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.TG_USER.Add(user);
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }

            }
            return 0;
        }

        public int UpdateUser(TG_USER user)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Entry<TG_USER>(user).State = System.Data.Entity.EntityState.Modified;
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }

            }
            return 0;
        }

        public int RecoverUser(String email)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    TG_TEMPORAL_RECOVERY loEntiy = new TG_TEMPORAL_RECOVERY();
                    loEntiy.REC_GUID = Guid.NewGuid().ToString().Replace("-", "");
                    loEntiy.REC_MAX_VALID_DATE = DateTime.Now.AddMinutes(30);
                    loEntiy.USR_ID = email;
                    loContext.TG_TEMPORAL_RECOVERY.Add(loEntiy);
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }

            }
            return 0;
        }

    }
}
