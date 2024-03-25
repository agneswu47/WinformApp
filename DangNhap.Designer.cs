
namespace QLCTBDS
{
    partial class Dangnhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dangnhap));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mnv = new System.Windows.Forms.TextBox();
            this.mk = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_datlai = new System.Windows.Forms.Button();
            this.btn_dangnhap = new System.Windows.Forms.Button();
            this.showpass = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(305, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(81, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nhập mã Nhân Viên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(81, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mật khẩu:";
            // 
            // mnv
            // 
            this.mnv.Location = new System.Drawing.Point(335, 134);
            this.mnv.Name = "mnv";
            this.mnv.Size = new System.Drawing.Size(248, 31);
            this.mnv.TabIndex = 3;
            this.mnv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mnv_KeyDown);
            // 
            // mk
            // 
            this.mk.Location = new System.Drawing.Point(335, 206);
            this.mk.Name = "mk";
            this.mk.Size = new System.Drawing.Size(248, 31);
            this.mk.TabIndex = 4;
            this.mk.UseSystemPasswordChar = true;
            this.mk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mk_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.btn_datlai);
            this.groupBox1.Controls.Add(this.btn_dangnhap);
            this.groupBox1.Controls.Add(this.showpass);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mk);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mnv);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(477, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 404);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // btn_datlai
            // 
            this.btn_datlai.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_datlai.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_datlai.ForeColor = System.Drawing.Color.White;
            this.btn_datlai.Location = new System.Drawing.Point(502, 281);
            this.btn_datlai.Name = "btn_datlai";
            this.btn_datlai.Size = new System.Drawing.Size(189, 65);
            this.btn_datlai.TabIndex = 7;
            this.btn_datlai.Text = "Đặt lại";
            this.btn_datlai.UseVisualStyleBackColor = false;
            this.btn_datlai.Click += new System.EventHandler(this.btn_datlai_Click);
            // 
            // btn_dangnhap
            // 
            this.btn_dangnhap.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_dangnhap.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_dangnhap.ForeColor = System.Drawing.Color.White;
            this.btn_dangnhap.Location = new System.Drawing.Point(164, 281);
            this.btn_dangnhap.Name = "btn_dangnhap";
            this.btn_dangnhap.Size = new System.Drawing.Size(189, 65);
            this.btn_dangnhap.TabIndex = 6;
            this.btn_dangnhap.Text = "Đăng nhập";
            this.btn_dangnhap.UseVisualStyleBackColor = false;
            this.btn_dangnhap.Click += new System.EventHandler(this.btn_dangnhap_Click);
            // 
            // showpass
            // 
            this.showpass.AutoSize = true;
            this.showpass.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.showpass.Location = new System.Drawing.Point(629, 204);
            this.showpass.Name = "showpass";
            this.showpass.Size = new System.Drawing.Size(193, 33);
            this.showpass.TabIndex = 5;
            this.showpass.Text = "Hiện mật khẩu";
            this.showpass.UseVisualStyleBackColor = true;
            this.showpass.CheckedChanged += new System.EventHandler(this.showpass_CheckedChanged);
            // 
            // Dangnhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1618, 754);
            this.Controls.Add(this.groupBox1);
            this.Name = "Dangnhap";
            this.Text = "DangNhap";
            this.Load += new System.EventHandler(this.Dangnhap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mnv;
        private System.Windows.Forms.TextBox mk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_datlai;
        private System.Windows.Forms.Button btn_dangnhap;
        private System.Windows.Forms.CheckBox showpass;
    }
}