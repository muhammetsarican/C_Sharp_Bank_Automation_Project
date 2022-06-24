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
    public partial class bankManager : Form
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

        public List<string> userInfo = new List<string>();
        string[] months = { " ", "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"};
        DateTime sysDate = DateTime.Now;

        public bankManager()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=Mms7105158;SslMode=none");
        }

        private void buttonTransactions_Click(object sender, EventArgs e)
        {
            allHide();
            showTransactions();
        }

        private void bankManager_Load(object sender, EventArgs e)
        {
            this.Text = "Banka Müdürü";
            labelUserInfo.Text = "ID: " + userInfo[0] + "      İsim Soyisim: " + userInfo[1] + " " + userInfo[2];
            labelSysDate.Text = sysDate.ToString();
            allHide();
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

        //Functions
        public void allHide()
        {
            groupBoxCustomer.Hide();
            groupBoxAddCustomer.Hide();
            groupBoxGeneralSituation.Hide();
            groupBoxInterestRates.Hide();
            groupBoxTransactions.Hide();
            groupBoxSelectRequest.Hide();
            groupBoxAreYouSure.Hide();
            groupBoxUpdateCustomer.Hide();
            groupBoxProfileSettings.Hide();
            groupBoxRepresentative.Hide();
            groupBoxUpdateSalary.Hide();
            groupBoxAddCurrency.Hide();
            groupBoxUpdateCurrency.Hide();
            groupBoxAddInterestRates.Hide();
            groupBoxUpdateInterestRates.Hide();

            groupBoxCurrencyandGold.Hide();
        }

        void logout()
        {
            userInfo.Clear();
            this.Hide();
            anasayfa.Show();
        }

        public void showCustomer()
        {
            groupBoxCustomer.Show();
            da = new MySqlDataAdapter("Select id as ID, tc_no as 'T.C. No.', name as Ad, surname as Soyad, phone as Telefon, address as Adres, mail as 'E-Posta' from customer where representative_id='" + userInfo[0] + "'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "customer");
            dataGridViewCustomer.DataSource = ds.Tables["customer"];
            this.dataGridViewCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
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
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tc_no", textBoxUpdateCustomerTcNo.Text);
            cmd.Parameters.AddWithValue("@Id", textBoxUpdateCustomerId.Text);
            cmd.Parameters.AddWithValue("@name", textBoxUpdateCustomerName.Text);
            cmd.Parameters.AddWithValue("@surname", textBoxUpdateCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@phone", textBoxUpdateCustomerPhone.Text);
            cmd.Parameters.AddWithValue("@address", textBoxUpdateCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxUpdateCustomerMail.Text);
            cmd.Parameters.AddWithValue("@id", labelId.Text);
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
            string sql = "Update bank_manager set tc_no=@tc_no, password=@pwd, name=@name, surname=@surname, phone=@phone, address=@address, mail=@mail where id=@id";
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
            string sql = "delete from bank_manager where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(userInfo[0]));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Hesap Başarıyla Silindi...";
            scss_Fonk();
            logout();
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

        void changeCurrency(string currency)
        {
            setCurrency(currency);
            showCurrencyAndGold();
        }

        void showRepresentative()
        {
            groupBoxRepresentative.Show();
            da = new MySqlDataAdapter("Select id as ID, tc_no as 'T.C. No.', name as Ad, surname as Soyad, phone as Telefon, address as Adres, mail as 'E-Posta', salary as Maaş from customer_representative", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "representative");
            dataGridViewRepresentative.DataSource = ds.Tables["representative"];
            this.dataGridViewRepresentative.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        void showUpdateSalary(int representative_id)
        {
            allHide();
            groupBoxUpdateSalary.Show();
            string sql = "Select salary from customer_representative where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", representative_id);
            conn.Open();
            var salary = cmd.ExecuteScalar();
            conn.Close();
            textBoxOldSalary.Text = salary.ToString();
        }

        void updateSalary()
        {
            string sql = "Update customer_representative set salary=@new_salary where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@new_salary", Convert.ToDouble(textBoxNewSalary.Text));
            cmd.Parameters.AddWithValue("@id", representative_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Maaş başarıyla güncellendi";
            scss_Fonk();
            allHide();
            showRepresentative();
        }

        void addCurrency()
        {
            string sql = "insert into currency(name, ratio) values(@name, @ratio)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", textBoxCurrencyName.Text);
            cmd.Parameters.AddWithValue("@ratio", Convert.ToDouble(textBoxCurrencyRatio.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text="Kur başarıyla eklendi...";
            scss_Fonk();
            allHide();
            showCurrencyAndGold();
        }

        void showUpdateCurrency(int currency_id)
        {
            string sql = "select * from currency where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", currency_id);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBoxUpdateCurrencyName.Text = dr["name"].ToString();
                textBoxUpdateCurrencyRatio.Text = dr["ratio"].ToString();
            }
            conn.Close();
            groupBoxUpdateCurrency.Show();
        }

        void updateCurrency()
        {
            string sql = "update currency set name=@name, ratio=@ratio where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", textBoxUpdateCurrencyName.Text);
            cmd.Parameters.AddWithValue("@ratio", Convert.ToDouble(textBoxUpdateCurrencyRatio.Text));
            cmd.Parameters.AddWithValue("id", currency_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Kur başarıyla güncellendi...";
            scss_Fonk();
            allHide();
            showCurrencyAndGold();
        }

        void showInterestRates()
        {
            groupBoxInterestRates.Show();
            da = new MySqlDataAdapter("select * from interest_rates", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "interest_rates");
            dataGridViewInterestRates.DataSource = ds.Tables["interest_rates"];
            this.dataGridViewInterestRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
        }

        void AddInterestRates()
        {
            string sql = "insert into interest_rates(name, ratio) values(@name, @ratio)";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", textBoxAddInterestRatesName.Text);
            cmd.Parameters.AddWithValue("@ratio", Convert.ToDouble(textBoxAddInterestRatesRatio.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Faiz oranı başarıyla eklendi...";
            scss_Fonk();
            allHide();
            showInterestRates();
        }

        void showUpdateInterestRates(int interestRates_id)
        {
            string sql = "Select * from interest_rates where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", interestRates_id);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBoxUpdateInterestRatesName.Text = dr["name"].ToString();
                textBoxUpdateInterestRatesRatio.Text = dr["ratio"].ToString();
            }
            conn.Close();
            groupBoxUpdateInterestRates.Show();
        }

        void updateInterestRates()
        {
            string sql = "Update interest_rates set name=@name, ratio=@ratio where id=@id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", textBoxUpdateInterestRatesName.Text);
            cmd.Parameters.AddWithValue("@ratio", Convert.ToDouble(textBoxUpdateInterestRatesRatio.Text));
            cmd.Parameters.AddWithValue("id", interestRates_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Faiz oranı başarıyla güncellendi...";
            scss_Fonk();
            allHide();
            showInterestRates();
        }

        double getSalary()
        {
            double salary = 0;
            string sql = "Select salary from customer_representative";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                salary += Convert.ToDouble(dr["salary"]);
            }
            conn.Close();
            return salary;
        }
        
        double income;
        double getIncome()
        {
            income = 0;
            setCurrency("TL");

            MySqlConnection conn2;
            conn2 = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=744.erdem;SslMode=none");

            string sql = "Select id, balance from account";
            MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
            conn2.Open();
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                income = income + Convert.ToDouble(dr["balance"])/getCurrency(Convert.ToInt32(dr["id"]));
            }
            conn2.Close();
            return income;
        }

        double expense;
        double profit;
        double maturity_expense;
        void getExpenseAndProfit()
        {
            expense = 0;
            profit = 0;
            maturity_expense = 0;
            string sql = "Select amount_requested, maturity, start_date, amount_to_be_paid from loans";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                expense += Convert.ToDouble(dr["amount_requested"]);
                profit += Convert.ToDouble(dr["amount_to_be_paid"]);
                maturity_expense+= Convert.ToDouble(dr["amount_to_be_paid"])/Convert.ToDouble(dr["maturity"]) *(sysDate - Convert.ToDateTime(dr["start_date"])).Days/30;
            }
            conn.Close();
            labelIncome.Text = "Gelir: " + Math.Round(getIncome()+maturity_expense, 2).ToString() + " TL";
            labelExpense.Text = "Gider: " + Math.Round(expense-maturity_expense,2).ToString() + " TL";
            labelProfit.Text = "Kâr: " + Math.Round((profit-expense),2).ToString() + " TL";
            labelSalariesToBePaid.Text = "Ödenecek Maaşlar: " + getSalary() * ((sysDate - DateTime.Now).Days / 30) + " TL";
            labelTotalBalance.Text = "Toplam Bakiye: " + Math.Round((income - expense - (getSalary() * ((sysDate - DateTime.Now).Days / 30))), 2).ToString() + " TL";
        }

        double getCurrency(int account_id)
        {
            cmd = new MySqlCommand("Select currency_id from account Where id=@account_id", conn);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            var currency_id = cmd.ExecuteScalar();
            conn.Close();
            cmd = new MySqlCommand("Select ratio from currency Where id=@currency_id", conn);
            cmd.Parameters.AddWithValue("@currency_id", currency_id);
            conn.Open();
            var currency_ratio = cmd.ExecuteScalar();
            conn.Close();
            return Convert.ToDouble(currency_ratio);
        }

        void showGeneralSituation()
        {
            labelSysDate.Text = sysDate.ToString();
            getExpenseAndProfit();
            groupBoxGeneralSituation.Show();
        }

        void setSysDate(int new_date)
        {
            sysDate = DateTime.Now;
            sysDate = sysDate.AddMonths(new_date);
        }

        void clearPayment()
        {
            string sql = "delete from payments where status='New'";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void addPayment2()
        {
            clearPayment();
            int customer_id=0;
            int loan_id = 0;
            int maturity=0;
            double amount_to_be_paid=0;
            DateTime start_date=DateTime.Now;
            string sql = "Select * from loans where status='Onaylandı' and amount_to_be_paid!=0";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                for (int i=0;i< ((sysDate - Convert.ToDateTime(dr["start_date"])).Days / 30); i++)
                {
                    loan_id = Convert.ToInt32(dr["id"]);
                    customer_id = Convert.ToInt32(dr["customer_id"]);
                    maturity = Convert.ToInt32(dr["maturity"]);
                    amount_to_be_paid = Convert.ToDouble(dr["amount_to_be_paid"]);
                    start_date = Convert.ToDateTime(dr["start_date"]);
                    addPayment(loan_id, customer_id, maturity, amount_to_be_paid, start_date, i);
                }
            }
            conn.Close();
            
        }

        void addPayment(int loan_id, int customer_id, int maturity, double amount_to_be_paid, DateTime start_date, int maturity_number)
        {
            MySqlConnection conn1 = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=744.erdem;SslMode=none");
            string sql= "Insert into payments(loan_id, maturity_number, maturity_amount, maturity_date, customer_id, payment_name, payment_type, due_date) " +
                "values(@loan_id, @maturity_number, @maturity_amount, @maturity_date, @customer_id, @payment_name, 'Kredi', @due_date)";
            cmd = new MySqlCommand(sql, conn1);
            cmd.Parameters.AddWithValue("@loan_id", loan_id);
            cmd.Parameters.AddWithValue("@maturity_number",maturity_number+1);
            cmd.Parameters.AddWithValue("@maturity_amount",amount_to_be_paid/maturity);
            cmd.Parameters.AddWithValue("@maturity_date",start_date.AddMonths(maturity_number+1));
            cmd.Parameters.AddWithValue("@customer_id",customer_id);
            cmd.Parameters.AddWithValue("@payment_name", months[start_date.AddMonths(maturity_number).Month]);
            cmd.Parameters.AddWithValue("@check_id", loan_id);
            cmd.Parameters.AddWithValue("@due_date", start_date.AddMonths(maturity_number).AddDays(5));

            conn1.Open();
            cmd.ExecuteNonQuery();
            conn1.Close();
            scss.Text = "Ödeme başarıyla oluşturuldu";
            scss_Fonk();
        }
        //Buttons
        private void buttonLogout_Click_1(object sender, EventArgs e)
        {
            logout();
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            allHide();
            showCustomer();
        }

        private void buttonAddRepresentative_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxAddCustomer.Show();
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

        private void buttonUpdateRepresentative_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewCustomer.SelectedRows)
            {
                err.Hide();
                int id = Convert.ToInt32(drow.Cells[0].Value);
                num = id;
                textBoxUpdateCustomerId.Text = drow.Cells[0].Value.ToString();
                textBoxUpdateCustomerTcNo.Text = drow.Cells[1].Value.ToString();
                textBoxUpdateCustomerName.Text = drow.Cells[2].Value.ToString();
                textBoxUpdateCustomerSurname.Text = drow.Cells[3].Value.ToString();
                textBoxUpdateCustomerPhone.Text = drow.Cells[4].Value.ToString();
                textBoxUpdateCustomerAddress.Text = drow.Cells[5].Value.ToString();
                textBoxUpdateCustomerMail.Text = drow.Cells[6].Value.ToString();
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
            cmd.Parameters.AddWithValue("@name", textBoxCustomerName.Text);
            cmd.Parameters.AddWithValue("@surname", textBoxCustomerSurname.Text);
            cmd.Parameters.AddWithValue("@phone", textBoxCustomerPhone.Text);
            cmd.Parameters.AddWithValue("@address", textBoxCustomerAddress.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxCustomerMail.Text);
            cmd.Parameters.AddWithValue("@representative_id", Convert.ToInt32(userInfo[0]));
            cmd.Parameters.AddWithValue("@tc_no", textBoxTcNo.Text);
            cmd.Parameters.AddWithValue("@password", "1234");
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

        private void buttonUpdateProfile_Click(object sender, EventArgs e)
        {
            string sql = "Select * from bank_manager where id=@id";
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

        private void buttonYes_Click(object sender, EventArgs e)
        {
            deleteProfile();
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

        private void bankManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonRepresentative_Click(object sender, EventArgs e)
        {
            allHide();
            showRepresentative();
        }
        int representative_id;
        private void buttonUpdateSalary_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewRepresentative.SelectedRows)
            {
                err.Hide();
                representative_id = Convert.ToInt32(drow.Cells[0].Value);
                num = representative_id;
                showUpdateSalary(representative_id);
            }
            if (num == -1)
            {
                err.Text = "Maaşı Güncellenecek çalışanı seçiniz\nTekrar 'Maaşı Güncelle' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonUpdateSalary2_Click(object sender, EventArgs e)
        {
            updateSalary();
        }

        private void buttonAddCurrency_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxAddCurrency.Show();
        }

        private void buttonAddCurrency2_Click(object sender, EventArgs e)
        {
            addCurrency();
        }

        int currency_id;
        private void buttonUpdateCurrency_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewCurrencyAndGold.SelectedRows)
            {
                err.Hide();
                currency_id = Convert.ToInt32(drow.Cells[0].Value);
                num = currency_id;
                allHide();
                showUpdateCurrency(currency_id);
            }
            if (num == -1)
            {
                err.Text = "Güncellenecek kuru seçiniz\nTekrar 'Güncelle' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonInterestRates_Click(object sender, EventArgs e)
        {
            allHide();
            showInterestRates();
        }

        private void buttonAddInterestRates_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxAddInterestRates.Show();
        }
        int interestRates_id;
        private void buttonUpdateInterestRates_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewInterestRates.SelectedRows)
            {
                err.Hide();
                interestRates_id = Convert.ToInt32(drow.Cells[0].Value);
                num = interestRates_id;
                allHide();
                showUpdateInterestRates(interestRates_id);
            }
            if (num == -1)
            {
                err.Text = "Güncellenecek faiz oranını seçiniz\nTekrar 'Güncelle' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonAddInterestRates2_Click(object sender, EventArgs e)
        {
            AddInterestRates();
        }

        private void buttonUpdateInterestRates2_Click(object sender, EventArgs e)
        {
            updateInterestRates();
        }

        private void buttonUpdateCurrency2_Click(object sender, EventArgs e)
        {
            updateCurrency();
        }

        private void buttonGeneralSituation_Click(object sender, EventArgs e)
        {
            allHide();
            showGeneralSituation();
        }

        private void buttonAdvanceTheSystem_Click(object sender, EventArgs e)
        {
            setSysDate(Convert.ToInt32(comboBox1.Text));
            addPayment2();
            showGeneralSituation();
        }
    }
}
