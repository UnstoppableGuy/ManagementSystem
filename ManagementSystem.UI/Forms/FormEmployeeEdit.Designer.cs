using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ManagementSystem.UI.Forms
{
    partial class FormEmployeeEdit
    {
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelLastName = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelBirthYear = new System.Windows.Forms.Label();
            this.labelSalary = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxPosition = new System.Windows.Forms.TextBox();
            this.numericBirthYear = new System.Windows.Forms.NumericUpDown();
            this.numericSalary = new System.Windows.Forms.NumericUpDown();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBirthYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSalary)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel.Controls.Add(this.labelFirstName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelLastName, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelPosition, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelBirthYear, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.labelSalary, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.textBoxFirstName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.textBoxLastName, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.textBoxPosition, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.numericBirthYear, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.numericSalary, 1, 4);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(320, 180);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelFirstName
            // 
            this.labelFirstName.Location = new System.Drawing.Point(13, 10);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(84, 20);
            this.labelFirstName.TabIndex = 0;
            this.labelFirstName.Text = "Имя:";
            // 
            // labelLastName
            // 
            this.labelLastName.Location = new System.Drawing.Point(13, 30);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(84, 20);
            this.labelLastName.TabIndex = 1;
            this.labelLastName.Text = "Фамилия:";
            // 
            // labelPosition
            // 
            this.labelPosition.Location = new System.Drawing.Point(13, 50);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(84, 20);
            this.labelPosition.TabIndex = 2;
            this.labelPosition.Text = "Должность:";
            // 
            // labelBirthYear
            // 
            this.labelBirthYear.Location = new System.Drawing.Point(13, 70);
            this.labelBirthYear.Name = "labelBirthYear";
            this.labelBirthYear.Size = new System.Drawing.Size(84, 20);
            this.labelBirthYear.TabIndex = 3;
            this.labelBirthYear.Text = "Год рождения:";
            // 
            // labelSalary
            // 
            this.labelSalary.Location = new System.Drawing.Point(13, 90);
            this.labelSalary.Name = "labelSalary";
            this.labelSalary.Size = new System.Drawing.Size(84, 23);
            this.labelSalary.TabIndex = 4;
            this.labelSalary.Text = "Зарплата:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFirstName.Location = new System.Drawing.Point(103, 13);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(204, 22);
            this.textBoxFirstName.TabIndex = 5;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLastName.Location = new System.Drawing.Point(103, 33);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(204, 22);
            this.textBoxLastName.TabIndex = 6;
            // 
            // textBoxPosition
            // 
            this.textBoxPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPosition.Location = new System.Drawing.Point(103, 53);
            this.textBoxPosition.Name = "textBoxPosition";
            this.textBoxPosition.Size = new System.Drawing.Size(204, 22);
            this.textBoxPosition.TabIndex = 7;
            // 
            // numericBirthYear
            // 
            this.numericBirthYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBirthYear.Location = new System.Drawing.Point(103, 73);
            this.numericBirthYear.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.numericBirthYear.Minimum = new decimal(new int[] {
            1925,
            0,
            0,
            0});
            this.numericBirthYear.Name = "numericBirthYear";
            this.numericBirthYear.Size = new System.Drawing.Size(204, 22);
            this.numericBirthYear.TabIndex = 8;
            this.numericBirthYear.Value = new decimal(new int[] {
            1925,
            0,
            0,
            0});
            // 
            // numericSalary
            // 
            this.numericSalary.DecimalPlaces = 2;
            this.numericSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericSalary.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericSalary.Location = new System.Drawing.Point(103, 93);
            this.numericSalary.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericSalary.Name = "numericSalary";
            this.numericSalary.Size = new System.Drawing.Size(204, 22);
            this.numericSalary.TabIndex = 9;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(222, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "ОК";
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(141, 13);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.buttonOk);
            this.buttonPanel.Controls.Add(this.buttonCancel);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonPanel.Location = new System.Drawing.Point(0, 220);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonPanel.Size = new System.Drawing.Size(320, 40);
            this.buttonPanel.TabIndex = 0;
            // 
            // FormEmployeeEdit
            // 
            this.AcceptButton = this.buttonOk;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(320, 260);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FormEmployeeEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить сотрудника";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBirthYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSalary)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel;
        private Label labelFirstName;
        private Label labelLastName;
        private Label labelPosition;
        private Label labelBirthYear;
        private Label labelSalary;
        private TextBox textBoxFirstName;
        private TextBox textBoxLastName;
        private TextBox textBoxPosition;
        private NumericUpDown numericBirthYear;
        private NumericUpDown numericSalary;
        private Button buttonOk;
        private Button buttonCancel;
        private FlowLayoutPanel buttonPanel;
    }
}