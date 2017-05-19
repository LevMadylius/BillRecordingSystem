using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BillRecordingSystem.DB
{
    public static class Queries
    {
        public static int GetExpenceTypeId(string text)
        {
            using (var db = new DBExpenceContext())
            {
                int id = 1;
                
                var query= (from t in db.ExpenceTypes
                           where text == t.Name
                           select t).SingleOrDefault();
                var type = query as ExpenceTypes;


                return type.IdExpenceType;
            }
        }

        public static void InsertExpence(Expences expence)
        {
            using (var db = new DBExpenceContext())
            {
                db.Expences.Add(expence);

                db.SaveChanges();
            }
        }

        public static int GetUserId(string login,string password)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from li in db.LoginInfo
                            where li.Pass == password && li.LoginName == login
                            select li).SingleOrDefault();
                var loginInfo = query as LoginInfo;
                if (loginInfo == null)
                    throw new Exception();
                return loginInfo.IdUser;
            }
        }
    }
}
