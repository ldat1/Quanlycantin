using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using QuanlycantinWF.DTO;

namespace QuanlycantinWF.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            set { instance = value; }
        }
        private AccountDAO() { }

        public bool Login(string username, string password)
        {


            string query = "usp_login @username , @password";                                                               

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {username, password});

            return result.Rows.Count > 0;
        }

        public bool UpdateAccount(string username, string displayname, string pass, string newpass)
        {
     int result = DataProvider.Instance.ExecuteNonquery("exec USP_UppdateAccount @username , @displayname , @password , @newpassword", new object[] {username, displayname,pass,newpass});

            return result > 0;
        }

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExecuteQuery("select username, displayname, type from account");
        }

        public Account GetAccountByUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from account where username = '" + username +"'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool InsertAccount(string name, string displayname, int type, string password)
        {
            string query = string.Format("INSERT dbo.Account (username, displayname, type, password )VALUES (N'{0}', N'{1}', {2}, N'{3}')", name, displayname, type, password);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;
        }

        public bool UpdateAccount(string name, string displayname, int type)
        {
            string query = string.Format("UPDATE dbo.Account set displayname = '{1}', type = {2} where username = '{0}'", name, displayname, type);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;
        }

        public bool DeleteAccount(string name)
        {


            string query = string.Format("delete account where username = '{0}'", name); 
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {

            string query = string.Format(" update account set password = N'0' where username = '{0}'", name);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;
        }


    }

    
}
