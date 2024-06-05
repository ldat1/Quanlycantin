using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanlycantinWF.DTO;

namespace QuanlycantinWF.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }
        private BillInfoDAO() { }


        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.billinfo where idfood = " + id);
        }
        public List<Billinfo> GetBillinfos(int id)
        {
            List<Billinfo> listBillInfo = new List<Billinfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.billinfo where idbill = " + id);

            foreach (DataRow item in data.Rows)
            {
                Billinfo info = new Billinfo(item);
                listBillInfo.Add(info);

            }
            return listBillInfo; 
        }
        public void InsertBillInfo(int idbillID, int idfoodID, int count)
        {
            DataProvider.Instance.ExecuteQuery(" USP_InsertBillInfo @idbill , @idfood , @count", new object[] { idbillID , idfoodID , count });
        }
    }
}
