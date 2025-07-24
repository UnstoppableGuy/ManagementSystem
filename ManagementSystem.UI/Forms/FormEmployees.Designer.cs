using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ManagementSystem.UI.Forms
{
    partial class FormEmployees
    {
        private System.ComponentModel.IContainer components = null;

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
            var tableLayoutPanel = new TableLayoutPanel();
            var panelFilter = new Panel();
            var labelFilter = new Label();
            var buttonAdd = new Button();
            var buttonDelete = new Button();
            var buttonReport = new Button();
            var panelButtons = new Panel();
            this.comboBoxPositionFilter = new ComboBox();
            this.dataGridViewEmployees = new DataGridView();

            SuspendLayout();

            // tableLayoutPanel
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Controls.Add(panelFilter, 0, 0);
            tableLayoutPanel.Controls.Add(dataGridViewEmployees, 0, 1);
            tableLayoutPanel.Controls.Add(panelButtons, 1, 1);
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Size = new Size(800, 450);
            tableLayoutPanel.TabIndex = 0;

            // panelFilter
            panelFilter.Controls.Add(comboBoxPositionFilter);
            panelFilter.Controls.Add(labelFilter);
            panelFilter.Dock = DockStyle.Fill;
            panelFilter.Location = new Point(3, 3);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new Size(674, 34);
            panelFilter.TabIndex = 0;

            // labelFilter
            labelFilter.AutoSize = true;
            labelFilter.Location = new Point(10, 12);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(62, 13);
            labelFilter.TabIndex = 0;
            labelFilter.Text = "Должность:";

            // comboBoxPositionFilter
            comboBoxPositionFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPositionFilter.FormattingEnabled = true;
            comboBoxPositionFilter.Location = new Point(78, 9);
            comboBoxPositionFilter.Name = "comboBoxPositionFilter";
            comboBoxPositionFilter.Size = new Size(200, 21);
            comboBoxPositionFilter.TabIndex = 1;
            comboBoxPositionFilter.SelectedIndexChanged += new EventHandler(ComboBoxPositionFilter_SelectedIndexChanged);

            // dataGridViewEmployees
            dataGridViewEmployees.AllowUserToAddRows = false;
            dataGridViewEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmployees.Dock = DockStyle.Fill;
            dataGridViewEmployees.Location = new Point(3, 43);
            dataGridViewEmployees.Name = "dataGridViewEmployees";
            dataGridViewEmployees.ReadOnly = true;
            dataGridViewEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmployees.Size = new Size(674, 404);
            dataGridViewEmployees.TabIndex = 1;
            dataGridViewEmployees.Columns.Add("Id", "Id");
            dataGridViewEmployees.Columns.Add("FirstName", "Имя");
            dataGridViewEmployees.Columns.Add("LastName", "Фамилия");
            dataGridViewEmployees.Columns.Add("Position", "Должность");
            dataGridViewEmployees.Columns.Add("BirthYear", "Год рождения");
            dataGridViewEmployees.Columns.Add("Salary", "Зарплата");
            dataGridViewEmployees.Columns["Id"].Visible = false;

            // panelButtons
            panelButtons.Controls.Add(buttonReport);
            panelButtons.Controls.Add(buttonDelete);
            panelButtons.Controls.Add(buttonAdd);
            panelButtons.Dock = DockStyle.Fill;
            panelButtons.Location = new Point(683, 43);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(114, 404);
            panelButtons.TabIndex = 2;
            
            #region Buttons
            // buttonAdd
            buttonAdd.Location = new Point(10, 10);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(90, 30);
            buttonAdd.TabIndex = 0;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += new EventHandler(ButtonAdd_Click);

            // buttonDelete
            buttonDelete.Location = new Point(10, 50);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(90, 30);
            buttonDelete.TabIndex = 1;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += new EventHandler(ButtonDelete_Click);

            // buttonReport
            buttonReport.Location = new Point(10, 90);
            buttonReport.Name = "buttonReport";
            buttonReport.Size = new Size(90, 30);
            buttonReport.TabIndex = 2;
            buttonReport.Text = "Отчет";
            buttonReport.UseVisualStyleBackColor = true;
            buttonReport.Click += new EventHandler(ButtonReport_Click);

            #endregion

            // FormEmployees
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel);
            Name = "FormEmployees";
            Text = "Сотрудники";
            Load += new EventHandler(FormEmployees_Load);
            tableLayoutPanel.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmployees).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}