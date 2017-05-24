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
                
                var query= (from t in db.ExpenceTypes
                           where text == t.Name
                           select t).SingleOrDefault();
                var type = query as ExpenceTypes;
                if (type == null)
                    throw new Exception();

                return type.IdExpenceType;
            }
        }
        //Updates if record exists inserts if there is no such record
        public static void InsertExpence(Expences expence)
        {
            using (var db = new DBExpenceContext())
            {

                var id = expence.IdExpence;
                if (db.Expences.Any(e => e.IdExpence == id))
                {
                    db.Expences.Attach(expence);
                    db.Entry(expence).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Expences.Add(expence);
                }

                db.SaveChanges();
            }
        }

        public static void RegisterOrUpdate(Users user, LoginInfo loginInfo)
        {
            using (var db = new DBExpenceContext())
            {

                var userId = user.IdUser;
               //check
                if (db.LoginInfo.Any(li => li.LoginName == loginInfo.LoginName) && userId != -1)
                    throw new Exception("User with this login name already exists");

                if (db.Users.Any(e => e.IdUser == userId))
                {
                    loginInfo.IdUser = userId;
                    db.Users.Attach(user);
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                    db.LoginInfo.Attach(loginInfo);
                    db.Entry(loginInfo).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    loginInfo.IdUser = user.IdUser;
                    db.LoginInfo.Add(loginInfo);
                }

                db.SaveChanges();
            }
        }

        public static Users GetUserById(int userId)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from u in db.Users
                             where u.IdUser == userId
                             select u).SingleOrDefault();

                return query;
            }
        }
        // For user login
        public static int GetUserId(string login,string password)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from li in db.LoginInfo
                            where li.Pass == password && li.LoginName == login
                            select li).SingleOrDefault();
                
                if (query == null)
                    throw new Exception();
                
                return query.IdUser;
            }
        }

        public static int GetLoginIdByUserId(int userId)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from li in db.LoginInfo
                             where li.IdUser == userId
                             select li).Single();

                return query.IdLoginInfo;
            }
        }

        public static DateTime GetDateOfEarliestExpence(int userId)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from e in db.Expences
                             where e.IdUser == userId
                             select e.BillDate).Min();

                return query;
                            
            }
        }

        public static DateTime GetDateOfLatestExpence(int userId)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from e in db.Expences
                             where e.IdUser == userId
                             select e.BillDate).Max();

                return query;

            }
        }

        public static bool CheckIfExpencesEmpty(int userId)
        {
            using (var db = new DBExpenceContext())
            {
               bool result = db.Expences.Any(e => e.IdUser == userId);

               return result;

            }
        }

        public static List<Expences> GetListExpances(int userId)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from e in db.Expences
                             where e.IdUser == userId
                             select e).ToList();

                foreach (var el in query)
                {
                    el.ExpenceTypes = (from t in db.ExpenceTypes
                                       where t.IdExpenceType == el.IdExpenceType
                                       select t).Single();
                }


                return query;

            }
        }

        public static List<Expences> GetFilteredListExpances(int userId,DateTime dateFrom, DateTime dateTo, string type)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from e in db.Expences
                             join t in db.ExpenceTypes on e.IdExpenceType equals t.IdExpenceType
                             where e.IdUser == userId && e.BillDate >= dateFrom &&
                                   e.BillDate <= dateTo && t.Name == type
                             select e).ToList();

                foreach (var el in query)
                {
                    el.ExpenceTypes = (from t in db.ExpenceTypes
                                       where t.IdExpenceType == el.IdExpenceType
                                       select t).Single();
                }


                return query;

            }
        }

        public static List<Expences> GetFilteredListExpances(int userId, DateTime dateFrom, DateTime dateTo)
        {
            using (var db = new DBExpenceContext())
            {
                var query = (from e in db.Expences
                             where e.IdUser == userId && e.BillDate >= dateFrom && e.BillDate <= dateTo
                             select e).ToList();

                foreach (var el in query)
                {
                    el.ExpenceTypes = (from t in db.ExpenceTypes
                                       where t.IdExpenceType == el.IdExpenceType
                                       select t).Single();
                }


                return query;

            }
        }
    }
}
