using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlycantinWF.DAO;
using QuanlycantinWF.DTO;

namespace QuanlycantinWF
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; changeAccount(loginAccount); }
        }

        public fAccountProfile(Account acc)
        {

            InitializeComponent();

            loginAccount = acc;
        }
        void changeAccount(Account acc)
        {
            txbusername.Text = loginAccount.Username;
            txbdisplayname.Text = loginAccount.Password;
        }

        void UpdateAccountinfo()
        {
            string displayname = txbdisplayname.Text;
            string password = txbpassword.Text;
            string newpass = txbnewpassword.Text;
            string reamkepass = txbremakepassword.Text;
            string username = txbusername.Text;

           

            if (!newpass.Equals(reamkepass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu trùng khớp với mật khẩu mới !");
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(username, displayname, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công");
                    if (updateAccount != null)
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(username)));
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            UpdateAccountinfo();
        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }    
        }
        public AccountEvent(Account acc)
        {
           this.acc = acc;  
        }
    }
}
