using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuanlycantinWF.DTO;

namespace QuanlycantinWF.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
  

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
           private set { MenuDAO.instance = value; }
        }

        public MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listmenu = new List<Menu>();

            string query = " select f.name, bi.count, f.price, bi.count*f.price as totalPrice from food as f, billinfo as bi, bill as b where bi.idbill = b.id and bi.idfood = f.id and b.idtable = " + id　+ " and status = 0";

                DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listmenu.Add(menu);

            }

            return listmenu;
           
        }
    }
}
