using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Bank_Project
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;

        Label err = new Label();
        Label scss = new Label();

        //Hata Metini/TAMAMLANDI
        void err_Fonk()
        {
            err.Location = new Point(175, 415);
            err.ForeColor = Color.Red;
            err.AutoSize = true;
            this.Controls.Add(err);
            err.Hide();
            scss.Hide();
            err.Show();
            err.BringToFront();
        }
        //Başarı Metini/TAMAMLANDI
        void scss_Fonk()
        {
            scss.Location = new Point(175, 415);
            scss.ForeColor = Color.Blue;
            scss.AutoSize = true;
            this.Controls.Add(scss);
            err.Hide();
            scss.Hide();
            scss.Show();
            scss.BringToFront();
        }

        public Form1()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=Mms7105158;SslMode=none");  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            allHide();
            this.Text = "Bank Project";

            groupLogin.Show();
            buttonRepresentative.Show();
            buttonManager.Show();
        }
        public void allHide()
        {
            groupBoxBankManager.Hide();
            groupBoxCustomerRepresentative.Hide();
            buttonCustomer.Hide();
            buttonCustomer2.Hide();
            buttonRepresentative.Hide();
            buttonManager.Hide();
            groupBoxCustomerRepresentative.Hide();
            groupLogin.Hide();
            groupBoxBankManager.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            customer customer = new customer();
            cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM customer where mail='" + txtKullanici.Text + "' AND password='" + txtSifre.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                customer.userInfo.Add(dr.GetString("id"));
                customer.userInfo.Add(dr.GetString("name"));
                customer.userInfo.Add(dr.GetString("surname"));
                scss.Text = "Oturum Açma Başarılı...";
                scss_Fonk();
                this.Hide();
                customer.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre Girdiniz.");
            }
            conn.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
        }

        private void buttonManager_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxBankManager.Show();
            buttonRepresentative.Show();
            buttonCustomer.Show();
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            allHide();
            groupLogin.Show();
            buttonManager.Show();
            buttonRepresentative.Show();
        }

        private void buttonCustomer2_Click(object sender, EventArgs e)
        {
            allHide();
            groupLogin.Show();
            buttonManager.Show();
            buttonRepresentative.Show();
        }

        private void buttonRepresentative_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxCustomerRepresentative.Show();
            buttonManager.Show();
            buttonCustomer2.Show();
        }

        private void buttonRepresentativeLogin_Click(object sender, EventArgs e)
        {
            customerRepresentative representative = new customerRepresentative();
            cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM customer_representative where mail='" + textBoxRepresentativeMail.Text + "' AND password='" + textBoxRepresentativePwd.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                representative.userInfo.Add(dr.GetString("id"));
                representative.userInfo.Add(dr.GetString("name"));
                representative.userInfo.Add(dr.GetString("surname"));
                scss.Text = "Oturum Açma Başarılı...";
                scss_Fonk();
                this.Hide();
                representative.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre Girdiniz.");
            }
            conn.Close();
        }

        private void Form1_FormClosing_Click(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit?", "Dialog Title", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void buttonManagerLogin_Click(object sender, EventArgs e)
        {
            bankManager manager = new bankManager();
            cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM bank_manager where mail='" + textBoxManagerMail.Text + "' AND password='" + textBoxManagerPwd.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                manager.userInfo.Add(dr.GetString("id"));
                manager.userInfo.Add(dr.GetString("name"));
                manager.userInfo.Add(dr.GetString("surname"));
                scss.Text = "Oturum Açma Başarılı...";
                scss_Fonk();
                this.Hide();
                manager.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre Girdiniz.");
            }
            conn.Close();
        }

        private void Form1_FormClosed_Click(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
