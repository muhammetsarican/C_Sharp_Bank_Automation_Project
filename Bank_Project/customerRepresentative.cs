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
    public partial class customerRepresentative : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataReader dr;
        MySqlDataAdapter da;
        DataSet ds;

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
        Form1 anasayfa = new Form1();

        public List<string> userInfo=new List<string>();

        public customerRepresentative()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=744.erdem;SslMode=none");
        }

        private void customerRepresentative_Load(object sender, EventArgs e)
        {
            this.Text = "Müşteri Temsilcisi";
            labelUserInfo.Text ="ID: "+ userInfo[0] +"      İsim Soyisim: " + userInfo[1] + " " + userInfo[2];
            allHide();
        }
        //Functions
        public void allHide()
        {
            groupBoxCustomer.Hide();
            groupBoxAddCustomer.Hide();
            groupBoxRequests.Hide();
            groupBoxTransactions.Hide();
            groupBoxSelectRequest.Hide();
            groupBoxAreYouSure.Hide();
            groupBoxUpdateCustomer.Hide();
            groupBoxProfileSettings.Hide();

            groupBoxCurrencyandGold.Hide();
        }

        public void showCustomer()
        {
            groupBoxCustomer.Show();
            da = new MySqlDataAdapter("Select id as ID, tc_no as 'T.C. No.', name as Ad, surname as Soyad, phone as Telefon, address as Adres, mail as 'E-Posta' from customer where representative_id='" + userInfo[0] +"'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "customer");
            dataGridViewCustomer.DataSource = ds.Tables["customer"];
            this.dataGridViewCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        public void showRequests(string request_type)
        {
            groupBoxRequests.Show();
            if (request_type == "account")
            {
                da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi', status as Durum from account where status='New'", conn);

            }
            else
            {
                da = new MySqlDataAdapter("Select id as ID, customer_id as 'Kullanıcı ID', amount_requested as Tutar, amount_to_be_paid as 'Ödenecek Tutar', currency_id as 'Kur ID', maturity as Vade,start_date as 'Başlangıç Tarihi', end_date as 'Bitiş Tarihi', status as Durum from loans where status='New'", conn);

            }

            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "request");
            dataGridViewRequests.DataSource = ds.Tables["request"];
            this.dataGridViewRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        public void updateRequest(int id)
        {
            string sql;
            if (table_name == "account")
            {
                sql = "UPDATE account set status='Onaylandı' where id=@id";
            }
            else
            {
                sql = "UPDATE loans set status='Onaylandı' where id=@id";

            }
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Talep Başarıyla Güncellendi...";
            scss_Fonk();
            allHide();
            showSelectRequests();
        }

        public void denyRequest(int id)
        {
            string sql;
            if (table_name == "account")
            {
                sql = "UPDATE account set status='Reddedildi' where id=@id";
            }
            else
            {
                sql = "UPDATE loans set status='Reddedildi' where id=@id";
            }
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Talep Başarıyla Güncellendi...";
            scss_Fonk();
            allHide();
            showSelectRequests();
        }

        void showSelectRequests()
        {
            groupBoxSelectRequest.Show();
        }

        void showTransactions()
        {
            groupBoxTransactions.Show();
            da = new MySqlDataAdapter("Select id as Id, customer_id as 'Müşteri Id', transaction as İşlem, source_id as 'Kaynak Id', target_id as 'Hedef Id', amount as Tutar, source_balance as 'Kaynak Bakiye', target_balance as 'Hedef Bakiye', date as Tarih from transactions", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "transactions");
            dataGridViewTransactions.DataSource = ds.Tables["transactions"];
            this.dataGridViewTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        void areYouSure()
        {
            groupBoxAreYouSure.Show();
        }

        void deleteCustomer(int id)
        {
            string sql = "Delete from customer where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updateCustomer()
        {
            string sql = "Update customer set tc_no=@tc_no, id=@Id, name=@name, surname=@surname, phone=@phone, address=@address, mail=@mail where id=@id";
            cmd = new MySqlCommand(sql,conn);
            cmd.Parameters.AddWithValue("@tc_no",textBoxUpdateCustomerTcNo.Text);
            cmd.Parameters.AddWithValue("@Id",textBoxUpdateCustomerId.Text);
            cmd.Parameters.AddWithValue("@name",textBoxUpdateCustomerName.Text);
            cmd.Parameters.AddWithValue("@surname",textBoxUpdateCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@phone",textBoxUpdateCustomerPhone.Text);
            cmd.Parameters.AddWithValue("@address",textBoxUpdateCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@mail",textBoxUpdateCustomerMail.Text);
            cmd.Parameters.AddWithValue("@id",labelId.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Kayıt Başarıyla Güncellendi...";
            scss_Fonk();
            allHide();
            showCustomer();
        }

        void updateProfile()
        {
            string sql = "Update customer_representative set tc_no=@tc_no, password=@pwd, name=@name, surname=@surname, phone=@phone, address=@address, mail=@mail where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tc_no", textBoxProfileTcNo.Text);
            cmd.Parameters.AddWithValue("@pwd", textBoxProfilePassword.Text);
            cmd.Parameters.AddWithValue("@name", textBoxProfileName.Text);
            cmd.Parameters.AddWithValue("@surname", textBoxProfileSurname.Text);
            cmd.Parameters.AddWithValue("@phone", textBoxProfilePhone.Text);
            cmd.Parameters.AddWithValue("@address", textBoxProfileAddress.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxProfileMail.Text);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userInfo[0]));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Kayıt Başarıyla Güncellendi...";
            scss_Fonk();
            groupBoxProfileSettings.Hide();
        }

        void deleteProfile()
        {
            string sql = "delete from customer_representative where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userInfo[0]));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Hesap Başarıyla Silindi...";
            scss_Fonk();
            logout();
        }

        void logout()
        {
            userInfo.Clear();
            this.Hide();
            anasayfa.Show();
        }

        void showCurrencyAndGold()
        {
            groupBoxCurrencyandGold.Show();
            da = new MySqlDataAdapter("Select id as ID, name as Ad, ratio as Oran from currency ", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "currency");
            dataGridViewCurrencyAndGold.DataSource = ds.Tables["currency"];
            this.dataGridViewCurrencyAndGold.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            conn.Close();
        }


        void changeCurrency(string currency)
        {
            setCurrency(currency);
            showCurrencyAndGold();
        }

        void setCurrency(string currency)
        {
            cmd = new MySqlCommand("Select ratio from currency Where name=@currency", conn);
            cmd.Parameters.AddWithValue("@currency", currency);
            conn.Open();
            var ratio = cmd.ExecuteScalar();
            conn.Close();
            if ((float)ratio != 1)
            {
                string sql = "UPDATE currency set ratio=ratio/@ratio";
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ratio", ratio);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                scss.Text = "Kur Başarıyla Güncellendi...";
                scss_Fonk();
            }
            else
            {
                err.Text = "Kur değiştirilemez...";
                err_Fonk();
            }
        }
        //Buttons

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            allHide();
            showCustomer();
        }

        private void buttonTransactions_Click(object sender, EventArgs e)
        {
            allHide();
            showTransactions();
        }

        private void buttonRequests_Click(object sender, EventArgs e)
        {
            allHide();
            showSelectRequests();
        }

        private void buttonAddRepresentative_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxAddCustomer.Show();
        }

        private void buttonAcceptRequest_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewRequests.SelectedRows)
            {
                err.Hide();
                int id = Convert.ToInt32(drow.Cells[0].Value);
                num = id;
                updateRequest(id);
            }
            if (num == -1)
            {
                err.Text = "Onaylanacak talebi seçiniz\nTekrar 'Onayla' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonDenyRequest_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewRequests.SelectedRows)
            {
                err.Hide();
                int id = Convert.ToInt32(drow.Cells[0].Value);
                num = id;
                denyRequest(id);
            }
            if (num == -1)
            {
                err.Text = "Reddedilecek talebi seçiniz\nTekrar 'Reddet' butonuna basınız...";
                err_Fonk();
            }
        }
        string table_name=" ";
        private void buttonAccountRequests_Click(object sender, EventArgs e)
        {
            allHide();
            table_name = "account";
            showRequests(table_name);
        }

        private void buttonLoanRequests_Click(object sender, EventArgs e)
        {
            allHide();
            table_name = "loans";
            showRequests(table_name);
        }

        private void buttonDeleteRepresentative_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewCustomer.SelectedRows)
            {
                err.Hide();
                int id = Convert.ToInt32(drow.Cells[0].Value);
                num = id;
                deleteCustomer(id);
                scss.Text = "Müşteri Başarıyla Silindi...";
                scss_Fonk();
            }
            if (num == -1)
            {
                err.Text = "Silinecek müşteriyi seçiniz\nTekrar 'Sil' butonuna basınız...";
                err_Fonk();
            }
            allHide();
            showCustomer();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            deleteProfile();
        }

        private void buttonUpdateRepresentative_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewCustomer.SelectedRows)
            {
                err.Hide();
                int id = Convert.ToInt32(drow.Cells[0].Value);
                num = id;
                textBoxUpdateCustomerId.Text = drow.Cells[0].Value.ToString();
                textBoxUpdateCustomerTcNo.Text= drow.Cells[1].Value.ToString();
                textBoxUpdateCustomerName.Text = drow.Cells[2].Value.ToString();
                textBoxUpdateCustomerSurname.Text = drow.Cells[3].Value.ToString();
                textBoxUpdateCustomerPhone.Text = drow.Cells[4].Value.ToString();
                textBoxUpdateCustomerAddress.Text = drow.Cells[5].Value.ToString();
                textBoxUpdateCustomerMail.Text= drow.Cells[6].Value.ToString();
                labelId.Text = drow.Cells[0].Value.ToString();
                groupBoxUpdateCustomer.Show();
            }
            if (num == -1)
            {
                err.Text = "Güncellenecek müşteriyi seçiniz\nTekrar 'Güncelle' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonAddCustomerAcc_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO customer(name, surname, phone, address, mail, representative_id, tc_no, password) values(@name, @surname, @phone, @address, @mail, @representative_id, @tc_no, @password)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name",textBoxCustomerName.Text);
            cmd.Parameters.AddWithValue("@surname", textBoxCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@phone", textBoxCustomerPhone.Text);
            cmd.Parameters.AddWithValue("@address", textBoxCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxCustomerMail.Text);
            cmd.Parameters.AddWithValue("@representative_id",Convert.ToInt32(userInfo[0]));
            cmd.Parameters.AddWithValue("@tc_no", textBoxTcNo.Text);
            cmd.Parameters.AddWithValue("@password","1234");
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Müşteri Başarıyla Eklendi...";
            scss_Fonk();
            allHide();
            showCustomer();
        }

        private void buttonUpdateCustomer_Click(object sender, EventArgs e)
        {
            updateCustomer();
        }

        private void customerRepresentative_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            logout();
        }

        private void buttonUpdateProfile_Click(object sender, EventArgs e)
        {
            string sql = "Select * from customer_representative where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userInfo[0]));
            conn.Open();
            dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                textBoxProfileTcNo.Text = dr["tc_no"].ToString();
                textBoxProfileName.Text = dr["name"].ToString();
                textBoxProfileSurname.Text = dr["surname"].ToString();
                textBoxProfileMail.Text = dr["mail"].ToString();
                textBoxProfileAddress.Text = dr["address"].ToString();
                textBoxProfilePhone.Text = dr["phone"].ToString();
                textBoxProfilePassword.Text = dr["password"].ToString();
            }
            conn.Close();
            groupBoxProfileSettings.Show();

        }

        private void buttonProfileUpdate_Click(object sender, EventArgs e)
        {
            updateProfile();
        }

        private void buttonProfileDelete_Click(object sender, EventArgs e)
        {
            groupBoxAreYouSure.Show();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            groupBoxAreYouSure.Hide();
        }

        private void customerRepresentative_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonCurrencyandGold_Click(object sender, EventArgs e)
        {
            allHide();
            showCurrencyAndGold();
        }

        private void buttonratioTl_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("TL");
        }

        private void buttonratioEur_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("EUR");
        }

        private void buttonratioUsd_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("USD");
        }
    }
}
