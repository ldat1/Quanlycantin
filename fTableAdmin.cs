using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlycantinWF.DAO;
using QuanlycantinWF.DTO;


namespace QuanlycantinWF
{
    public partial class fTableAdmin : Form
    {

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAcount(loginAccount.Type); }
        }

        public fTableAdmin(Account acc)    
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();

            LoadCategory();
          
        }

        #region Method  

        void ChangeAcount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thoToolStripMenuItem.Text += " (" + loginAccount.Displayname + ")";
        }


        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listfood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listfood;
            cbFood.DisplayMember = "Name"; 
        }
        void LoadTable()
        {
       
            flpTable.Controls.Clear();
               

            List<Table> tablelist = TableDAO.Instance.LoadTableList();

            foreach (Table item in tablelist)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight};
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Green;
                        break;
                    default:
                        btn.BackColor = Color.Red;
                        break;
                } 

                flpTable.Controls.Add(btn);
            }

        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

            float TotalPrice = 0;
       
            foreach (DTO.Menu item in listBillInfo)

            {
                ListViewItem lsvitem = new ListViewItem(item.Foodname.ToString()); 
                lsvitem.SubItems.Add(item.Count.ToString());
                lsvitem.SubItems.Add(item.Price.ToString());
                lsvitem.SubItems.Add(item.TotalPrice.ToString());
                TotalPrice += item.TotalPrice; 
                lsvBill.Items.Add(lsvitem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            //Thread.CurrentThread.CurrentCulture = culture;

            txbTotalPrice.Text = TotalPrice.ToString("c", culture);
            LoadTable();
         

        }

        #endregion


        #region events

        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
            
          
            


        private void thôngTinCáToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

            void f_UpdateAccount(object sender,AccountEvent e)
        {
            thoToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.Displayname + ")";
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.Insertfood += f_Insertfood;
            f.Deletefood += f_Deletefood;
            f.Updatefood += f_Updatefood;
            f.LoginAccount = loginAccount;
            f.ShowDialog();
        }

        private void f_Updatefood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void f_Deletefood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void f_Insertfood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryID((cbCategory.SelectedItem as Category).Id);
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

       /* private void cbFood_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Food selected = cb.SelectedItem as Food;
            id = selected.Id;

            LoadFoodListByCategoryID(id);
        }*/

        private void btADDFOOD_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if(table == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }

            int idbill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            int foodID = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;
            if (idbill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idbill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.Id;

            LoadFoodListByCategoryID(id);
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

           
            int idBIll = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            double totalprice  = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
            double finaltotalprice = totalprice;

            if (idBIll != -1)
            {
                if (MessageBox.Show("Bạn có muốn thanh toán bàn " + table.Name + " không ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBIll,(float )finaltotalprice);
          
                   
                    LoadTable();
                    ShowBill(table.ID);
         
                 
                }
            }  
           
        }

       
        #endregion 


    }
}
