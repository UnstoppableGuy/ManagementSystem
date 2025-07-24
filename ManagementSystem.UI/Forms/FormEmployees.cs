using ManagementSystem.Domain.Interfaces;
using ManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class FormEmployees : Form
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger _logger;
        private List<Employee> _allEmployees;

        private ComboBox comboBoxPositionFilter;
        private DataGridView dataGridViewEmployees;

        public FormEmployees(IEmployeeRepository repository, ILogger logger)
        {
            InitializeComponent();
            _repository = repository;
            _logger = logger;
        }

        private void FormEmployees_Load(object sender, EventArgs e)
        {
            _logger.Info("Загрузка формы сотрудников");
            try
            {
                _allEmployees = _repository.GetAllEmployees();
                LoadPositions();
                LoadEmployees(_allEmployees);
            }
            catch (Exception ex)
            {
                _logger.Error("Ошибка загрузки сотрудников", ex);
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPositions()
        {
            try
            {
                var positions = _repository.GetAllPositions();
                positions.Insert(0, "Все");
                comboBoxPositionFilter.DataSource = positions;
            }
            catch (Exception ex)
            {
                _logger.Error("Ошибка загрузки должностей", ex);
            }
        }

        private void LoadEmployees(List<Employee> employees)
        {
            dataGridViewEmployees.Rows.Clear();
            foreach (var emp in employees)
            {
                dataGridViewEmployees.Rows.Add(emp.Id,
                                               emp.FirstName,
                                               emp.LastName,
                                               emp.Position,
                                               emp.BirthYear,
                                               emp.Salary);
            }
        }

        private void ComboBoxPositionFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxPositionFilter.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected) || selected == "Все")
                LoadEmployees(_allEmployees);
            else
                LoadEmployees(_repository.GetEmployeesByPosition(selected));
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var form = new FormEmployeeEdit(_logger))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repository.AddEmployee(form.Employee);
                        _allEmployees = _repository.GetAllEmployees();
                        RefreshData();
                        LoadEmployees(_allEmployees);
                        _logger.Info($"Сотрудник добавлен: {form.Employee.FirstName} {form.Employee.LastName}");

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Ошибка добавления сотрудника", ex);
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
       
        private void RefreshData()
        {
            _allEmployees = _repository.GetAllEmployees();
            LoadPositions();
            string selected = comboBoxPositionFilter.SelectedItem?.ToString();
            if (selected == "Все" || string.IsNullOrEmpty(selected))
                LoadEmployees(_allEmployees);
            else
                LoadEmployees(_repository.GetEmployeesByPosition(selected));
        }
        
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите сотрудника для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dataGridViewEmployees.SelectedRows[0];
            int id = (int)row.Cells["Id"].Value;
            string name = $"{row.Cells["FirstName"].Value} {row.Cells["LastName"].Value}";

            if (MessageBox.Show($"Удалить {name}?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _repository.DeleteEmployee(id);
                    _allEmployees = _repository.GetAllEmployees();
                    LoadEmployees(_allEmployees);
                    RefreshData();
                    _logger.Info($"Сотрудник удалён: {name}");
                }
                catch (Exception ex)
                {
                    _logger.Error($"Ошибка удаления сотрудника ID={id}", ex);
                    MessageBox.Show("Ошибка удаления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            using (var form = new FormReport(_repository, _logger))
            {
                form.ShowDialog();
            }
        }
    }
}