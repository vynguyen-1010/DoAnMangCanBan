﻿
namespace FinalTermProject
{
    partial class TCPClient
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
            this.requestBtn = new System.Windows.Forms.Button();
            this.URLbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.succeedResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.failResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // requestBtn
            // 
            this.requestBtn.BackColor = System.Drawing.Color.GreenYellow;
            this.requestBtn.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestBtn.Location = new System.Drawing.Point(582, 51);
            this.requestBtn.Name = "requestBtn";
            this.requestBtn.Size = new System.Drawing.Size(146, 41);
            this.requestBtn.TabIndex = 0;
            this.requestBtn.Text = "Request";
            this.requestBtn.UseVisualStyleBackColor = false;
            this.requestBtn.Click += new System.EventHandler(this.requestBtn_Click);
            // 
            // URLbox
            // 
            this.URLbox.AcceptsTab = true;
            this.URLbox.Font = new System.Drawing.Font("Century Gothic", 13.8F);
            this.URLbox.Location = new System.Drawing.Point(134, 51);
            this.URLbox.Multiline = true;
            this.URLbox.Name = "URLbox";
            this.URLbox.Size = new System.Drawing.Size(337, 41);
            this.URLbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F);
            this.label1.Location = new System.Drawing.Point(17, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL";
            // 
            // succeedResult
            // 
            this.succeedResult.BackColor = System.Drawing.Color.LimeGreen;
            this.succeedResult.Font = new System.Drawing.Font("Segoe UI Light", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.succeedResult.Location = new System.Drawing.Point(259, 164);
            this.succeedResult.Multiline = true;
            this.succeedResult.Name = "succeedResult";
            this.succeedResult.Size = new System.Drawing.Size(275, 35);
            this.succeedResult.TabIndex = 3;
            this.succeedResult.Text = "Yêu cầu thành công";
            this.succeedResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.succeedResult.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results from server";
            // 
            // failResult
            // 
            this.failResult.BackColor = System.Drawing.Color.Red;
            this.failResult.Font = new System.Drawing.Font("Segoe UI Light", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failResult.ForeColor = System.Drawing.SystemColors.MenuText;
            this.failResult.Location = new System.Drawing.Point(259, 164);
            this.failResult.Multiline = true;
            this.failResult.Name = "failResult";
            this.failResult.Size = new System.Drawing.Size(275, 35);
            this.failResult.TabIndex = 5;
            this.failResult.Text = "Yêu cầu thất bại";
            this.failResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.failResult.Visible = false;
            // 
            // TCPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(800, 233);
            this.Controls.Add(this.failResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.succeedResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.URLbox);
            this.Controls.Add(this.requestBtn);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TCPClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCPClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.closeClient);
            this.Load += new System.EventHandler(this.TCPClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button requestBtn;
        private System.Windows.Forms.TextBox URLbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox succeedResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox failResult;
    }
}