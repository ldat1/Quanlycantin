using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanlycantinWF.DTO;

namespace QuanlycantinWF.DAO
{
    internal class BillDAO
    {
        private static BillDAO instance;

        internal static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            set { BillDAO.instance = value; }

        }

        private BillDAO() { }

        /// <summary>
        /// Thành công : bill ID
        /// thất bại : -1
        /// </summary>
      
        /// <returns></returns>
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from bill where idtable = " + id + "and status = 0");

            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }

        public void CheckOut(int id,float totalprice)
        {
            string query = "update dbo.bill set datecheckout = GETDATE(), status = 1,"+" totalprice = " + totalprice + "  where id = " + id;
            DataProvider.Instance.ExecuteNonquery(query);

        }
        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteQuery("USP_InsertBill @idtable",new object[] {id});
        }

        public DataTable GetBillListByDate(DateTime checkin, DateTime checkout)
        {
            return DataProvider.Instance.ExecuteQuery("exec USP_GetListBillByDate @checkin , @checkout ", new object[] { checkin, checkout });
        }

        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(id) from bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
