using Microsoft.Reporting.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    partial class FormReport
    {
        private System.ComponentModel.IContainer components = null;
        private ReportViewer reportViewer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();

            // 
            // reportViewer
            // 
            this.reportViewer.Dock = DockStyle.Fill;
            this.reportViewer.Location = new Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new Size(800, 450);
            this.reportViewer.TabIndex = 0;

            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.reportViewer);
            this.Name = "FormReport";
            this.Text = "Отчёт о средней зарплате";
            this.Load += new System.EventHandler(this.FormReport_Load);
            this.ResumeLayout(false);
        }
    }
}