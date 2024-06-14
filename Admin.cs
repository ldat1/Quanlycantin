using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using QuanlycantinWF.DAO;
using QuanlycantinWF.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanlycantinWF
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();

        public Account LoginAccount;
        public fAdmin()
        {
            InitializeComponent();
            load();
         
        
        }
       
    
        #region Methods

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listfood = FoodDAO.Instance.SearchFoodByName(name);

            return listfood;
        }
        void load()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBiding();
            AddAcountBinding();
         
        }

     

        void AddAcountBinding()
        {
            txbUsername.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Username"));
            txbDisplayname.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Displayname"));
            nmTypeAccount.DataBindings.Add(new Binding("value", dtgvAccount.DataSource, "Type"));
            
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void AddAccount(string username, string displayname, int type, string password)
        {
            
            if(AccountDAO.Instance.InsertAccount(username, displayname, type, password))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại !");
            }
            LoadAccount();
  
        }
        void EditAccount(string username, string displayname, int type)
        {

            if (AccountDAO.Instance.UpdateAccount(username, displayname, type))
            {
                MessageBox.Show("Sửa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại !");
            }
            LoadAccount();

        }
        void DeleteAccount(string username)
        {
            if (LoginAccount.Username.Equals(username))
            {
                MessageBox.Show("Sao bạn lại xóa chính mmình chứ ?");
                return;
            }

            if (AccountDAO.Instance.DeleteAccount(username))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại !");
            }
            LoadAccount();
        }
        void ResetPassword(string username)
        {

            if (AccountDAO.Instance.ResetPassword(username))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Lỗi đặt lại mật khẩu ! ");
            }
            LoadAccount();

        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFormDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }
            
        void LoadListBillByDate(DateTime checkin, DateTime checkout)
        {
           dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkin, checkout);
        }
        void AddFoodBiding()
        {
            tbxFoodname.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name",true, DataSourceUpdateMode.Never));
            //từ cái txbfoodname hãy cho tôi cái giá trị text thay đổi theo giá trị của name nằm trong cái dtgvfood 
            tbxidfood.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
           
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }



        #endregion

        #region event

        private void btnAddAcccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string displayname = txbDisplayname.Text;
            int type = (int)nmTypeAccount.Value;

            AddAccount(username, displayname, type, "0");
            
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;

            DeleteAccount(username);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string displayname = txbDisplayname.Text;
            int type = (int)nmTypeAccount.Value;

            EditAccount(username, displayname, type);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;

            ResetPassword(username);
        }
        /* void LoadListBillByDate(DateTime checkin, DateTime checkout)
         {
             dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkin, checkout);
         }*/
        private void btnViewbill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }


        private void btnShowfood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddfood_Click(object sender, EventArgs e)
        {

            
            string name = tbxFoodname.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if (insertfood != null)
                    insertfood(this, new EventArgs());
                
            }
            else
            {
                MessageBox.Show("Thêm món ăn thất bại !");  
            }
        }

       

       private void tbxidfood_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                cbFoodCategory.SelectedItem = cateogory;

                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.Id == cateogory.Id)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbFoodCategory.SelectedIndex = index;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbxidfood.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show(" Xóa món thành công");
                LoadListFood();
                if (deletefood != null)
                    deletefood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xóa món thất bại !");
            }
        }

        private void btnEditfood_Click(object sender, EventArgs e)
        {
            string name = tbxFoodname.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).Id;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(tbxidfood.Text);

            if (FoodDAO.Instance.UpdatetFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if(updatefood != null)
                    updatefood(this, new EventArgs());  
            }
            else
            {
                MessageBox.Show("Sửa món ăn thất bại !");
            }
        }

        private event EventHandler insertfood;
        public event EventHandler Insertfood
        {
            add { insertfood += value; }
            remove { insertfood -= value; }
        }
        private event EventHandler deletefood;
        public event EventHandler Deletefood
        {
            add { deletefood += value; }
            remove { deletefood -= value; }
        }
        private event EventHandler updatefood;
        public event EventHandler Updatefood
        {
            add { updatefood += value; }
            remove { updatefood -= value; }
        }



        #endregion


    }
}
