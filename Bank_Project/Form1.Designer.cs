
namespace Bank_Project
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtKullanici = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupLogin = new System.Windows.Forms.GroupBox();
            this.groupBoxCustomerRepresentative = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRepresentativeMail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRepresentativePwd = new System.Windows.Forms.TextBox();
            this.buttonRepresentativeLogin = new System.Windows.Forms.Button();
            this.groupBoxBankManager = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxManagerMail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxManagerPwd = new System.Windows.Forms.TextBox();
            this.buttonManagerLogin = new System.Windows.Forms.Button();
            this.buttonRepresentative = new System.Windows.Forms.Button();
            this.buttonManager = new System.Windows.Forms.Button();
            this.buttonCustomer = new System.Windows.Forms.Button();
            this.buttonCustomer2 = new System.Windows.Forms.Button();
            this.groupLogin.SuspendLayout();
            this.groupBoxCustomerRepresentative.SuspendLayout();
            this.groupBoxBankManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKullanici
            // 
            this.txtKullanici.Location = new System.Drawing.Point(86, 45);
            this.txtKullanici.Name = "txtKullanici";
            this.txtKullanici.Size = new System.Drawing.Size(100, 20);
            this.txtKullanici.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(86, 71);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(100, 20);
            this.txtSifre.TabIndex = 1;
            this.txtSifre.UseSystemPasswordChar = true;
            this.txtSifre.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // buttonLogin
            // 
            this.buttonLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonLogin.Location = new System.Drawing.Point(86, 97);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(100, 24);
            this.buttonLogin.TabIndex = 2;
            this.buttonLogin.Text = "Oturum Aç";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "E-Posta:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Şifre:";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupLogin
            // 
            this.groupLogin.BackColor = System.Drawing.Color.Transparent;
            this.groupLogin.Controls.Add(this.label1);
            this.groupLogin.Controls.Add(this.txtKullanici);
            this.groupLogin.Controls.Add(this.label2);
            this.groupLogin.Controls.Add(this.txtSifre);
            this.groupLogin.Controls.Add(this.buttonLogin);
            this.groupLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupLogin.Location = new System.Drawing.Point(404, 105);
            this.groupLogin.Name = "groupLogin";
            this.groupLogin.Padding = new System.Windows.Forms.Padding(10);
            this.groupLogin.Size = new System.Drawing.Size(239, 180);
            this.groupLogin.TabIndex = 4;
            this.groupLogin.TabStop = false;
            this.groupLogin.Text = "Müşteri";
            this.groupLogin.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBoxCustomerRepresentative
            // 
            this.groupBoxCustomerRepresentative.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxCustomerRepresentative.Controls.Add(this.label3);
            this.groupBoxCustomerRepresentative.Controls.Add(this.textBoxRepresentativeMail);
            this.groupBoxCustomerRepresentative.Controls.Add(this.label4);
            this.groupBoxCustomerRepresentative.Controls.Add(this.textBoxRepresentativePwd);
            this.groupBoxCustomerRepresentative.Controls.Add(this.buttonRepresentativeLogin);
            this.groupBoxCustomerRepresentative.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxCustomerRepresentative.Location = new System.Drawing.Point(404, 105);
            this.groupBoxCustomerRepresentative.Name = "groupBoxCustomerRepresentative";
            this.groupBoxCustomerRepresentative.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxCustomerRepresentative.Size = new System.Drawing.Size(239, 180);
            this.groupBoxCustomerRepresentative.TabIndex = 5;
            this.groupBoxCustomerRepresentative.TabStop = false;
            this.groupBoxCustomerRepresentative.Text = "Müşteri Temsilcisi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "E-Posta:";
            // 
            // textBoxRepresentativeMail
            // 
            this.textBoxRepresentativeMail.Location = new System.Drawing.Point(86, 45);
            this.textBoxRepresentativeMail.Name = "textBoxRepresentativeMail";
            this.textBoxRepresentativeMail.Size = new System.Drawing.Size(100, 20);
            this.textBoxRepresentativeMail.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Şifre:";
            // 
            // textBoxRepresentativePwd
            // 
            this.textBoxRepresentativePwd.Location = new System.Drawing.Point(86, 71);
            this.textBoxRepresentativePwd.Name = "textBoxRepresentativePwd";
            this.textBoxRepresentativePwd.Size = new System.Drawing.Size(100, 20);
            this.textBoxRepresentativePwd.TabIndex = 1;
            this.textBoxRepresentativePwd.UseSystemPasswordChar = true;
            // 
            // buttonRepresentativeLogin
            // 
            this.buttonRepresentativeLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonRepresentativeLogin.Location = new System.Drawing.Point(86, 97);
            this.buttonRepresentativeLogin.Name = "buttonRepresentativeLogin";
            this.buttonRepresentativeLogin.Size = new System.Drawing.Size(100, 24);
            this.buttonRepresentativeLogin.TabIndex = 2;
            this.buttonRepresentativeLogin.Text = "Oturum Aç";
            this.buttonRepresentativeLogin.UseVisualStyleBackColor = true;
            this.buttonRepresentativeLogin.Click += new System.EventHandler(this.buttonRepresentativeLogin_Click);
            // 
            // groupBoxBankManager
            // 
            this.groupBoxBankManager.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxBankManager.Controls.Add(this.label5);
            this.groupBoxBankManager.Controls.Add(this.textBoxManagerMail);
            this.groupBoxBankManager.Controls.Add(this.label6);
            this.groupBoxBankManager.Controls.Add(this.textBoxManagerPwd);
            this.groupBoxBankManager.Controls.Add(this.buttonManagerLogin);
            this.groupBoxBankManager.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxBankManager.Location = new System.Drawing.Point(404, 105);
            this.groupBoxBankManager.Name = "groupBoxBankManager";
            this.groupBoxBankManager.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxBankManager.Size = new System.Drawing.Size(239, 180);
            this.groupBoxBankManager.TabIndex = 6;
            this.groupBoxBankManager.TabStop = false;
            this.groupBoxBankManager.Text = "Banka Müdürü";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "E-Posta:";
            // 
            // textBoxManagerMail
            // 
            this.textBoxManagerMail.Location = new System.Drawing.Point(86, 45);
            this.textBoxManagerMail.Name = "textBoxManagerMail";
            this.textBoxManagerMail.Size = new System.Drawing.Size(100, 20);
            this.textBoxManagerMail.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Şifre:";
            // 
            // textBoxManagerPwd
            // 
            this.textBoxManagerPwd.Location = new System.Drawing.Point(86, 71);
            this.textBoxManagerPwd.Name = "textBoxManagerPwd";
            this.textBoxManagerPwd.Size = new System.Drawing.Size(100, 20);
            this.textBoxManagerPwd.TabIndex = 1;
            this.textBoxManagerPwd.UseSystemPasswordChar = true;
            // 
            // buttonManagerLogin
            // 
            this.buttonManagerLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonManagerLogin.Location = new System.Drawing.Point(86, 97);
            this.buttonManagerLogin.Name = "buttonManagerLogin";
            this.buttonManagerLogin.Size = new System.Drawing.Size(100, 24);
            this.buttonManagerLogin.TabIndex = 2;
            this.buttonManagerLogin.Text = "Oturum Aç";
            this.buttonManagerLogin.UseVisualStyleBackColor = true;
            this.buttonManagerLogin.Click += new System.EventHandler(this.buttonManagerLogin_Click);
            // 
            // buttonRepresentative
            // 
            this.buttonRepresentative.BackColor = System.Drawing.Color.Transparent;
            this.buttonRepresentative.Location = new System.Drawing.Point(701, 404);
            this.buttonRepresentative.Name = "buttonRepresentative";
            this.buttonRepresentative.Size = new System.Drawing.Size(87, 34);
            this.buttonRepresentative.TabIndex = 4;
            this.buttonRepresentative.Text = "Müşteri\r\nTemsilcisi";
            this.buttonRepresentative.UseVisualStyleBackColor = false;
            this.buttonRepresentative.Click += new System.EventHandler(this.buttonRepresentative_Click);
            // 
            // buttonManager
            // 
            this.buttonManager.BackColor = System.Drawing.Color.Transparent;
            this.buttonManager.Location = new System.Drawing.Point(608, 404);
            this.buttonManager.Name = "buttonManager";
            this.buttonManager.Size = new System.Drawing.Size(87, 34);
            this.buttonManager.TabIndex = 7;
            this.buttonManager.Text = "Banka\r\nMüdürü";
            this.buttonManager.UseVisualStyleBackColor = false;
            this.buttonManager.Click += new System.EventHandler(this.buttonManager_Click);
            // 
            // buttonCustomer
            // 
            this.buttonCustomer.BackColor = System.Drawing.Color.Transparent;
            this.buttonCustomer.Location = new System.Drawing.Point(608, 404);
            this.buttonCustomer.Name = "buttonCustomer";
            this.buttonCustomer.Size = new System.Drawing.Size(87, 34);
            this.buttonCustomer.TabIndex = 8;
            this.buttonCustomer.Text = "Müşteri";
            this.buttonCustomer.UseVisualStyleBackColor = false;
            this.buttonCustomer.Click += new System.EventHandler(this.buttonCustomer_Click);
            // 
            // buttonCustomer2
            // 
            this.buttonCustomer2.BackColor = System.Drawing.Color.Transparent;
            this.buttonCustomer2.Location = new System.Drawing.Point(701, 404);
            this.buttonCustomer2.Name = "buttonCustomer2";
            this.buttonCustomer2.Size = new System.Drawing.Size(87, 34);
            this.buttonCustomer2.TabIndex = 9;
            this.buttonCustomer2.Text = "Müşteri";
            this.buttonCustomer2.UseVisualStyleBackColor = false;
            this.buttonCustomer2.Click += new System.EventHandler(this.buttonCustomer2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Bank_Project.Properties.Resources.digital_currency;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonManager);
            this.Controls.Add(this.buttonCustomer);
            this.Controls.Add(this.buttonRepresentative);
            this.Controls.Add(this.buttonCustomer2);
            this.Controls.Add(this.groupLogin);
            this.Controls.Add(this.groupBoxBankManager);
            this.Controls.Add(this.groupBoxCustomerRepresentative);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed_Click);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupLogin.ResumeLayout(false);
            this.groupLogin.PerformLayout();
            this.groupBoxCustomerRepresentative.ResumeLayout(false);
            this.groupBoxCustomerRepresentative.PerformLayout();
            this.groupBoxBankManager.ResumeLayout(false);
            this.groupBoxBankManager.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtKullanici;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupLogin;
        private System.Windows.Forms.GroupBox groupBoxCustomerRepresentative;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRepresentativeMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRepresentativePwd;
        private System.Windows.Forms.Button buttonRepresentativeLogin;
        private System.Windows.Forms.GroupBox groupBoxBankManager;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxManagerMail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxManagerPwd;
        private System.Windows.Forms.Button buttonManagerLogin;
        private System.Windows.Forms.Button buttonRepresentative;
        private System.Windows.Forms.Button buttonManager;
        private System.Windows.Forms.Button buttonCustomer;
        private System.Windows.Forms.Button buttonCustomer2;
    }
}

