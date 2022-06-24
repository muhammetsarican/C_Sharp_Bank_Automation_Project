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
    public partial class customer : Form
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
        bankManager manager = new bankManager();

        public List<String> userInfo = new List<string>();

        public customer()
        {
            InitializeComponent();
            conn = new MySqlConnection("Server=localhost;Database=bank_project;user=root;Pwd=744.erdem;SslMode=none");
        }

        private void customer_Load(object sender, EventArgs e)
        {
            this.Text = "Müşteri";
            labelUserInfo.Text = "ID: " + userInfo[0] + "      İsim Soyisim: " + userInfo[1] + " " + userInfo[2];
            allHide();
        }
        void allHide()
        {
            islem.Hide();
            groupBoxAddAccount.Hide();
            groupBoxTransfers.Hide();
            groupBoxCurrencyandGold.Hide();
            groupBoxLoans.Hide();
            groupBoxPayments.Hide();
            buttonSenderAccount.Hide();
            labelCalculatedLoan.Hide();
            groupBoxLoanRequest.Hide();
            groupBoxTransferBetweenAccount.Hide();
            buttonSelectAccount.Hide();
            groupBoxWithdrawLoadMoney.Hide();
            groupBoxWithdrawMoney.Hide();
            groupBoxLoadMoney.Hide();
            groupBoxProfileSettings.Hide();
            groupBoxAreYouSure.Hide();
            groupBoxSelectPayments.Hide();
            groupBoxPayMaturity.Hide();
            groupBoxPayInstallment.Hide();
            buttonPayInInstallments.Hide();
            buttonPayMaturityChooseAccount.Hide();
            buttonChooseAccountPayLoan.Hide();
            buttonPayAllLoanAmount.Hide();
            groupBoxPayAllLoan.Hide();
        }
        //Functions
        void listele()
        {
            //k_Oturum_Sayfasi();
            islem.Show();
            da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi', status as Durum from account where status!='New' and customer_id='"+userInfo[0]+"'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "account");
            table.DataSource = ds.Tables["account"];
            this.table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            conn.Close();
        }

        void hesapSil(int numara, int bakiye)
        {
            if (bakiye != 0)
            {
                err.Text = "Bakiyesi '0' olmayan hesaplar silinemez.";
                err_Fonk();
            }
            else
            {
                string sql = "Delete from account where id=@numara";
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@numara", numara);
                cmd.ExecuteNonQuery();
                conn.Close();
                scss.Text = "Hesap Başarıyla Silindi...";
                scss_Fonk();
            }
        }

        void hesapEkle()
        {
            string sql = "INSERT INTO account( name, customer_id, currency_id, balance, last_operation_date) values(@accountName, @customer_id, @accountType, 0, @date)";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@accountName", textBoxAccountName.Text);
            cmd.Parameters.AddWithValue("@customer_id", userInfo[0]);
            cmd.Parameters.AddWithValue("@accountType", Convert.ToInt32(textBoxAccountType.Text));
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Hesap Başarıyla Oluşturuldu...";
            scss_Fonk();
            allHide();
            listele();
        }

        void showTransfers()
        {
            allHide();
            groupBoxTransfers.Show();
            buttonSelectAccount.Show();
            da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi' from account ", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "account");
            dataGridViewTransfers.DataSource = ds.Tables["account"];
            this.dataGridViewTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            conn.Close();
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

        double getInterest(string interest)
        {
            cmd = new MySqlCommand("Select ratio from interest_rates Where name=@interest", conn);
            cmd.Parameters.AddWithValue("@interest", interest);
            conn.Open();
            var ratio = cmd.ExecuteScalar();
            conn.Close();
            return Convert.ToDouble(ratio);
        }

        void addLoanRequest()
        {
            string sql = "INSERT INTO loans( customer_id, currency_id, amount_requested, amount_to_be_paid, maturity, start_date, end_date, salary) values(@customer_id, 1, @amount_requested, @amount_to_be_paid, @maturity, @start_date, @end_date, @salary)";
            cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@customer_id", Convert.ToInt32(userInfo[0]));
            cmd.Parameters.AddWithValue("@amount_requested", textBoxAmountRequested.Text);
            cmd.Parameters.AddWithValue("@amount_to_be_paid", tutar+toplamFaiz);
            cmd.Parameters.AddWithValue("@maturity", textBoxMaturity.Text);
            cmd.Parameters.AddWithValue("@start_date", DateTime.Now);
            cmd.Parameters.AddWithValue("@end_date", DateTime.Now.AddMonths(Convert.ToInt32(textBoxMaturity.Text)));
            cmd.Parameters.AddWithValue("@salary", textBoxSalary.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Kredi Talebi Başarıyla Oluşturuldu...";
            scss_Fonk();
            allHide();
            showLoans();
        }

        void showLoans()
        {
            groupBoxLoans.Show();
            da = new MySqlDataAdapter("Select id as ID, currency_id as Türü, amount_requested as Anapara, amount_to_be_paid as 'Ödenecek Tutar', maturity as Vade, start_date as 'Başlangıç Tarihi', end_date as 'Bitiş Tarihi', salary as Maaş, status as Durum from loans ", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "loans");
            dataGridViewLoans.DataSource = ds.Tables["loans"];
            this.dataGridViewLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            conn.Close();
        }

        void showTransferBetweenAccount()
        {
            groupBoxTransferBetweenAccount.Show();
            cmd = new MySqlCommand("Select balance from account Where id=@sender_id", conn);
            cmd.Parameters.AddWithValue("@sender_id", senderId);
            conn.Open();
            textBoxAllBalance.Text = cmd.ExecuteScalar().ToString();
            conn.Close();

        }

        double getCurrency(int account_id)
        {
            cmd = new MySqlCommand("Select currency_id from account Where id=@account_id", conn);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            var currency_id = cmd.ExecuteScalar();
            conn.Close();
            cmd = new MySqlCommand("Select name from currency Where id=@currency_id", conn);
            cmd.Parameters.AddWithValue("@currency_id", currency_id);
            conn.Open();
            string currency_name = cmd.ExecuteScalar().ToString();
            conn.Close();
            cmd = new MySqlCommand("Select ratio from currency Where id=@currency_id", conn);
            cmd.Parameters.AddWithValue("@currency_id", currency_id);
            conn.Open();
            var currency_ratio = cmd.ExecuteScalar();
            conn.Close();
            setCurrency(currency_name);
            return Convert.ToDouble(currency_ratio);
        }

        void updateBalance(int account_id,Boolean transac)
        {
            string sql;
            if (transac == true)
            {
                sql = "UPDATE account set balance=balance+@newBalance where id=@account_id";
            }
            else
            {
                sql = "UPDATE account set balance=balance-@newBalance where id=@account_id";
            }
            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
            cmd2.Parameters.AddWithValue("@newBalance", Convert.ToDouble(textBoxAmountToBeSend.Text)*getCurrency(account_id));
            cmd2.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
        }

        void performTheTransfer()
        {
            if (Convert.ToDouble(textBoxAllBalance.Text)>Convert.ToDouble(textBoxAmountToBeSend.Text))
            {
                updateBalance(senderId,false);
                updateBalance(receiverId,true);
                addTransaction("Transfer");
                scss.Text = "Para Transferi Başarılı...";
                scss_Fonk();
            }
            else
            {
                err.Text = "Bakiyenizden fazla tutar aktarılamaz...";
                err_Fonk();
            }
            showTransfers();
        }

        double getBalance(int account_id)
        {
            string sql = "Select balance from account where id=@account_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            var balance=cmd.ExecuteScalar();
            conn.Close();

            return Convert.ToDouble(balance);
        }

        void addTransaction(string transaction_name)
        {
            string sql = "INSERT INTO transactions(customer_id, transaction, source_id, target_id, amount, source_balance, target_balance, date) values(@customer_id, @transaction, @source_id, @target_id, @amount, @source_balance, @target_balance, @date)";
            MySqlCommand cmd3 = new MySqlCommand(sql, conn);
            cmd3.Parameters.AddWithValue("@customer_id", Convert.ToInt32(userInfo[0]));
            cmd3.Parameters.AddWithValue("@transaction",transaction_name);
            cmd3.Parameters.AddWithValue("@source_id",senderId);
            cmd3.Parameters.AddWithValue("@target_id",receiverId);
            cmd3.Parameters.AddWithValue("@amount",Convert.ToDouble(textBoxAmountToBeSend.Text));
            cmd3.Parameters.AddWithValue("@source_balance",getBalance(senderId));
            cmd3.Parameters.AddWithValue("@target_balance",getBalance(receiverId));
            cmd3.Parameters.AddWithValue("@date",DateTime.Now);
            conn.Open();
            cmd3.ExecuteNonQuery();
            conn.Close();
        }

        void showPayments()
        {
            da = new MySqlDataAdapter("Select id as ID, currency_id as Türü, amount_requested as Anapara, amount_to_be_paid as 'Ödenecek Tutar', maturity as Vade, start_date as 'Başlangıç Tarihi', end_date as 'Bitiş Tarihi', salary as Maaş, status as Durum from loans where status!='New'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "loans");
            dataGridViewPayments.DataSource = ds.Tables["loans"];
            this.dataGridViewPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.Close();
            buttonPayAllLoanAmount.Show();
            groupBoxPayments.Show();
        }

        void showWithdrawLoadMoney()
        {
            groupBoxWithdrawLoadMoney.Show();
            da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi', status as Durum from account where status!='New' ", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "withdrawloadmoney");
            dataGridViewWithdrawLoadMoney.DataSource = ds.Tables["withdrawloadmoney"];
            this.dataGridViewWithdrawLoadMoney.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            conn.Close();
        }

        void showWithdrawMoney(int account_id)
        {
            string sql="Select balance from account where id=@account_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            var all_balance = cmd.ExecuteScalar();
            conn.Close();
            textBoxWithdrawAllBalance.Text = all_balance.ToString();
            allHide();
            groupBoxWithdrawMoney.Show();
        }

        void updateWithdrawLoadMoney(int account_id, Boolean withdrawOrLoad, double new_balance)
        {
            string sql = " ";
            if (withdrawOrLoad == true)
            {
                sql = "Update account set balance=balance+@new_balance where id=@account_id";
            }
            else
            {
                sql = "Update account set balance=balance-@new_balance where id=@account_id";
            }
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@new_balance", new_balance);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            scss.Text = "Para çekme işlemi başarıyla tamamlandı...";
            scss_Fonk();
            allHide();
            showWithdrawLoadMoney();
        }

        void showLoadMoney(int account_id)
        {
            string sql = "Select balance from account where id=@account_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@account_id", account_id);
            conn.Open();
            var all_balance = cmd.ExecuteScalar();
            conn.Close();
            textBoxLoadMoneyAllBalance.Text = all_balance.ToString();
            allHide();
            groupBoxLoadMoney.Show();
        }

        void updateProfile()
        {
            string sql = "Update customer set tc_no=@tc_no, password=@pwd, name=@name, surname=@surname, phone=@phone, address=@address, mail=@mail where id=@id";
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

        void logout()
        {
            userInfo.Clear();
            this.Hide();
            anasayfa.Show();
        }

        void showMaturity()
        {
            da=new MySqlDataAdapter("Select id as id, loan_id as 'Kredi Numarası', maturity_number as 'Vade Numarası', payment_name as 'Vade İsmi', payment_type as 'Vade Türü', maturity_amount as 'Ödeme Tutarı', due_date as 'Son Ödeme Tarihi', status as Durum from payments where customer_id='"+userInfo[0]+"'", conn);
            ds = new DataSet();
            da.Fill(ds, "maturity");
            dataGridViewPayMaturity.DataSource = ds.Tables["maturity"];
            this.dataGridViewPayMaturity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            buttonPayInInstallments.Show();
            groupBoxPayMaturity.Show();
        }

        void showPayInInstallments(int installment_id)
        {
            string sql = "Select maturity_amount from payments where id=@installment_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@installment_id", installment_id);
            conn.Open();
            double maturity_amount= Convert.ToDouble(cmd.ExecuteScalar());
            conn.Close();
            textBoxPayInstallmentMaturityAmount.Text = maturity_amount.ToString();
            groupBoxPayInstallment.Show();
        }

        void showPayAllLoans()
        {
            string sql = "Select amount_to_be_paid from loans where id=@loan_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@loan_id", pay_all_loan_id);
            conn.Open();
            double maturity_amount = Convert.ToDouble(cmd.ExecuteScalar());
            conn.Close();
            allHide();
            textBoxPayAllLoanAllLoan.Text = maturity_amount.ToString();
            groupBoxPayAllLoan.Show();
        }

        void updatePayments()
        {
            string sql = " ";
            if (Convert.ToDouble(textBoxPayInstallmentMaturityAmount.Text) < Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text))
            {
                err.Text = "Vade tutarından fazla ödeme yapamazsınız...";
                err_Fonk();
            }
            else
            {
                if (Convert.ToDouble(textBoxPayInstallmentMaturityAmount.Text) == Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text))
                {
                    sql = "update payments set status='Ödendi', maturity_amount=0 where id=@installment_id";
                    cmd = new MySqlCommand(sql, conn);
                }
                else
                {
                    sql = "update payments set status='Kısmi Ödeme', maturity_amount=maturity_amount-@pay_amount where id=@installment_id";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text));
                }
                cmd.Parameters.AddWithValue("@installment_id", installment_id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                scss.Text = "Ödeme başarıyla yapıldı...";
                scss_Fonk();
                updateLoans();
                updateAccountBalanceAfterPayMaturity();
                allHide();
                showMaturity();
            }
            
        }

        void updateLoansPayAllLoan()
        {
            string sql = " ";
            if (Convert.ToDouble(textBoxPayAllLoanAllLoan.Text) < Convert.ToDouble(textBoxPayAllLoanAmountToBePaid.Text))
            {
                err.Text = "Kredi tutarından fazla ödeme yapamazsınız...";
                err_Fonk();
            }
            else
            {
                if (Convert.ToDouble(textBoxPayAllLoanAllLoan.Text) == Convert.ToDouble(textBoxPayAllLoanAmountToBePaid.Text))
                {
                    sql = "update loans set status='Ödendi', amount_to_be_paid=0 where id=@loan_id";
                    cmd = new MySqlCommand(sql, conn);
                    changeAllPayments();
                }
                else
                {
                    sql = "update loans set status='Kısmi Ödeme', amount_to_be_paid=amount_to_be_paid-@pay_amount where id=@loan_id";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayAllLoanAmountToBePaid.Text));
                }
                cmd.Parameters.AddWithValue("@loan_id", pay_all_loan_id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                scss.Text = "Ödeme başarıyla yapıldı...";
                scss_Fonk();
                updateLoansAfterPayAllLoans();
                updateAccountBalanceAfterPayLoan();
                allHide();
                showPayments();
            }
        }

        void changeAllPayments()
        {
            string sql = "update payments set status='Ödendi', maturity_amount=0 where loan_id=@loan_id2";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@loan_id2", pay_all_loan_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updateLoans()
        {
            string sql = "Update loans set amount_to_be_paid=amount_to_be_paid-@pay_amount where id=@loan_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text));
            cmd.Parameters.AddWithValue("@loan_id", loan_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updateLoansAfterPayAllLoans()
        {
            string sql = "Update loans set amount_to_be_paid=amount_to_be_paid-@pay_amount where id=@loan_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayAllLoanAmountToBePaid.Text));
            cmd.Parameters.AddWithValue("@loan_id", pay_all_loan_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updateAccountBalanceAfterPayMaturity()
        {
            string sql = "Update account set balance=balance-@pay_amount where id=@account_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text));
            cmd.Parameters.AddWithValue("@account_id", pay_maturity_account_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updateAccountBalanceAfterPayLoan()
        {
            string sql = "Update account set balance=balance-@pay_amount where id=@account_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayAllLoanAmountToBePaid.Text));
            cmd.Parameters.AddWithValue("@account_id", pay_all_loan_account_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void updatePayAllLoans()
        {
            string sql = "Update loans set amount_to_be_paid=amount_to_be_paid-@pay_amount where id=@loan_id";
            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pay_amount", Convert.ToDouble(textBoxPayInstallmentAmountToBePaid.Text));
            cmd.Parameters.AddWithValue("@loan_id", loan_id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        void showChooseAccount()
        {
            da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi', status as Durum from account where status!='New' and customer_id='"+userInfo[0]+ "'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "choose_account");
            dataGridViewPayMaturity.DataSource = ds.Tables["choose_account"];
            this.dataGridViewPayMaturity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.Close();
            allHide();
            buttonPayMaturityChooseAccount.Show();
            groupBoxPayMaturity.Show();
        }

        void showChooseAccountPayAllLoan()
        {
            da = new MySqlDataAdapter("Select id as ID, name as Ad, balance as Bakiye, currency_id as Tür, last_operation_date as 'Son İşlem Tarihi', status as Durum from account where status!='New' and customer_id='" + userInfo[0] + "'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "choose_account");
            dataGridViewPayments.DataSource = ds.Tables["choose_account"];
            this.dataGridViewPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            conn.Close();
            allHide();
            buttonChooseAccountPayLoan.Show();
            groupBoxPayments.Show();
        }
        //Buttons
        private void showAccount_Click(object sender, EventArgs e)
        {
            allHide();
            listele();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            anasayfa.Show();
        }

        private void buttonDeleteAccount_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in table.SelectedRows)  //Seçili Satırları Silme
            {
                int numara = Convert.ToInt32(drow.Cells[0].Value);
                int bakiye= Convert.ToInt32(drow.Cells[2].Value);
                num = numara;
                hesapSil(numara, bakiye);
            }
            listele();
            if (num == -1)
            {
                err.Text = "Silinecek kaydı seçiniz\nTekrar sil butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonAddAccount2_Click(object sender, EventArgs e)
        {
            hesapEkle();
        }

        private void buttonAddAccount_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxAddAccount.Show();
        }

        private void buttonTransfers_Click(object sender, EventArgs e)
        {
            allHide();
            showTransfers();
        }

        private void buttonPayments_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxSelectPayments.Show();
        }

        private void buttonCurrencyandGold_Click(object sender, EventArgs e)
        {
            allHide();
            showCurrencyAndGold();
        }

        private void buttonLoans_Click(object sender, EventArgs e)
        {
            allHide();
            showLoans();
        }

        int receiverId;
        int senderId;
        private void buttonSelectAccount_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewTransfers.SelectedRows)
            {
                receiverId = Convert.ToInt32(drow.Cells[0].Value);
                num = receiverId;
                err.Hide();
                buttonSelectAccount.Hide();
                buttonSenderAccount.Show();
            }
            listele();
            if (num == -1)
            {
                err.Text = "Alıcı hesabı seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonSenderAccount_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewTransfers.SelectedRows)
            {
                if (receiverId == Convert.ToInt32(drow.Cells[0].Value))
                {
                    err.Text = "Farklı bir hesap seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                    err_Fonk();
                }
                else
                {
                    senderId = Convert.ToInt32(drow.Cells[0].Value);
                    num = senderId;
                    err.Hide();
                    allHide();
                    showTransferBetweenAccount();
                }
            }
            if (num == -1)
            {
                err.Text = "Gönderen hesabı seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonratioUsd_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("USD");
        }

        private void buttonratioEur_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("EUR");
        }

        private void buttonratioTl_Click(object sender, EventArgs e)
        {
            allHide();
            changeCurrency("TL");
        }

        double tutar;
        double toplamFaiz;
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            
            tutar=Convert.ToDouble(textBoxAmountRequested.Text);
            double vade = Convert.ToDouble(textBoxMaturity.Text);
            double maas = Convert.ToDouble(textBoxSalary.Text);
            toplamFaiz =tutar / 100 * getInterest("Gecikme Faizi") + tutar / 100 * getInterest("KKDF") + tutar / 100 * getInterest("BSMF");
            if (maas < tutar)
            {
                err.Text = "Talep ettiğiniz tutar aylık gelirinizden yüksek...";
                err_Fonk();
            }
            else
            {
                labelCalculatedLoan.Text ="Kredi Faizi: "+ Math.Round(tutar /100*getInterest("Kredi Faizi"),2)+"\n"
                    +"KKDF: "+ Math.Round(tutar /100*getInterest("KKDF"),2)+"\n" 
                    + "BSMF: " + Math.Round(tutar /100 * getInterest("BSMF"),2) + "\n" 
                    +"Toplam Faiz: "+ Math.Round(toplamFaiz,2)+"\n"
                    + "Anapara: " + Math.Round(tutar /vade,2) + "\n" 
                    + "Taksit Tutarı: " + Math.Round(tutar /vade+toplamFaiz,2) + "\n"
                    + vade + " Aylık vade sonunda ödenecek toplam tutar: " + Math.Round(tutar+(vade * toplamFaiz), 2);
                labelCalculatedLoan.Show();
            }
        }

        private void buttonCreateRequest_Click(object sender, EventArgs e)
        {
            addLoanRequest();
        }

        private void buttonGetLoan_Click(object sender, EventArgs e)
        {
            allHide();
            groupBoxLoanRequest.Show();
        }

        private void buttonTransferSent_Click(object sender, EventArgs e)
        {
            performTheTransfer();
        }

        private void buttonWithdrawLoadMoney_Click(object sender, EventArgs e)
        {
            allHide();
            showWithdrawLoadMoney();
        }
        int withdrawId;
        private void buttonWithdrawMoney_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewWithdrawLoadMoney.SelectedRows)
            {
                withdrawId = Convert.ToInt32(drow.Cells[0].Value);
                num = withdrawId;
                err.Hide();
                allHide();
                showWithdrawMoney(withdrawId);

            }
            if (num == -1)
            {
                err.Text = "Gönderen hesabı seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonWithdrawMoney2_Click(object sender, EventArgs e)
        {
            updateWithdrawLoadMoney(withdrawId,false, Convert.ToDouble(textBoxWithdrawMoney.Text));
        }

        private void buttonLoadMoney_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewWithdrawLoadMoney.SelectedRows)
            {
                withdrawId = Convert.ToInt32(drow.Cells[0].Value);
                num = withdrawId;
                err.Hide();
                allHide();
                showLoadMoney(withdrawId);

            }
            if (num == -1)
            {
                err.Text = "İşlemin yapılcağı hesabı seçiniz\nTekrar butona basınız...";
                err_Fonk();
            }
        }

        private void buttonLoadMoney2_Click(object sender, EventArgs e)
        {
            updateWithdrawLoadMoney(withdrawId, true, Convert.ToDouble(textBoxLoadMoneyNewBalance.Text));
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            logout();
        }

        private void customer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonUpdateProfile_Click(object sender, EventArgs e)
        {
            string sql = "Select * from customer where id=@id";
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

        private void buttonYes_Click(object sender, EventArgs e)
        {
            deleteProfile();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            groupBoxAreYouSure.Hide();
        }

        private void buttonMyLoans_Click(object sender, EventArgs e)
        {
            allHide();
            showPayments();
        }

        private void buttonPayLoan_Click(object sender, EventArgs e)
        {
            allHide();
            showMaturity();
        }

        int installment_id;
        int loan_id;
        private void buttonPayInInstallments_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewPayMaturity.SelectedRows)
            {
                installment_id = Convert.ToInt32(drow.Cells[0].Value);
                loan_id = Convert.ToInt32(drow.Cells[1].Value);
                num = installment_id;
                err.Hide();
                allHide();
                showChooseAccount();
            }
            if (num == -1)
            {
                err.Text = "Ödenecek taksiti seçiniz\nTekrar 'Taksit Öde' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonPayInstallment_Click(object sender, EventArgs e)
        {
            updatePayments();
        }

        int pay_maturity_account_id;
        private void buttonPayMaturityChooseAccount_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewPayMaturity.SelectedRows)
            {
                pay_maturity_account_id = Convert.ToInt32(drow.Cells[0].Value);
                num = pay_maturity_account_id;
                if(Convert.ToInt32(drow.Cells[2].Value) > 0)
                {
                    err.Hide();
                    allHide();
                    showPayInInstallments(installment_id);
                }
                else
                {
                    err.Text = "Bakiyesi 0'dan fazla olan bir hesap seçiniz...";
                    err_Fonk();
                }
            }
            if (num == -1)
            {
                err.Text = "Taksitin ödeneceği hesabı seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                err_Fonk();
            }
        }

        int pay_all_loan_id;
        private void buttonPayAllLoanAmount_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewPayments.SelectedRows)
            {
                pay_all_loan_id = Convert.ToInt32(drow.Cells[0].Value);
                num = pay_all_loan_id;
                err.Hide();
                allHide();
                showChooseAccountPayAllLoan();
            }
            if (num == -1)
            {
                err.Text = "Ödenecek krediyi seçiniz\nTekrar 'Kredi Öde' butonuna basınız...";
                err_Fonk();
            }
        }

        int pay_all_loan_account_id;
        private void buttonChooseAccountPayLoan_Click(object sender, EventArgs e)
        {
            int num = -1;
            foreach (DataGridViewRow drow in dataGridViewPayments.SelectedRows)
            {
                pay_all_loan_account_id = Convert.ToInt32(drow.Cells[0].Value);
                num = pay_all_loan_account_id;
                if (Convert.ToInt32(drow.Cells[2].Value) > 0)
                {
                    err.Hide();
                    allHide();
                    showPayAllLoans();
                }
                else
                {
                    err.Text = "Bakiyesi 0'dan fazla olan bir hesap seçiniz...";
                    err_Fonk();
                }
            }
            if (num == -1)
            {
                err.Text = "Kredinin ödeneceği hesabı seçiniz\nTekrar 'Hesap Seç' butonuna basınız...";
                err_Fonk();
            }
        }

        private void buttonPayAllLoan_Click(object sender, EventArgs e)
        {
            updateLoansPayAllLoan();
        }
    }
}
