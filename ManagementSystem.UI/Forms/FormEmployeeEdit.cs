
using ManagementSystem.Domain.Helpers;
using ManagementSystem.Domain.Interfaces;
using ManagementSystem.Domain.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class FormEmployeeEdit : Form
    {
        public Employee Employee { get; private set; }
        private readonly ILogger _logger;

        public FormEmployeeEdit(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            Employee = new Employee
            {
                FirstName = textBoxFirstName.Text.Trim(),
                LastName = textBoxLastName.Text.Trim(),
                Position = textBoxPosition.Text.Trim(),
                BirthYear = (int)numericBirthYear.Value,
                Salary = numericSalary.Value
            };

            _logger.Info($"Форма: создан сотрудник {Employee.FirstName} {Employee.LastName}");
            DialogResult = DialogResult.OK;
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;

            var originalText = textBox.Text;
            var caretPos = textBox.SelectionStart;

            string formatted = ToTitleCase(textBox.Text);

            if (textBox.Text != formatted)
            {
                textBox.TextChanged -= TextBoxName_TextChanged;
                textBox.Text = formatted;
                textBox.SelectionStart = Math.Min(caretPos, formatted.Length);
                textBox.SelectionLength = 0;
                textBox.TextChanged += TextBoxName_TextChanged;
            }
        }
        private string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text ?? "";

            return string.Join("-",
                text.ToLower()
                    .Split('-')
                    .Select(part => part.Length > 0
                        ? char.ToUpper(part[0]) + part.Substring(1)
                        : part)
                    .ToArray());
        }
        private bool ValidateInput()
        {
            if (!ValidationHelper.IsValidName(textBoxFirstName.Text))
            {
                MessageBox.Show(
                    "Введите корректное имя. Только буквы, от 2 до 50 символов.",
                    "Ошибка ввода имени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidName(textBoxLastName.Text))
            {
                MessageBox.Show(
                    "Введите корректную фамилию. Только буквы, от 2 до 50 символов.",
                    "Ошибка ввода фамилии", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidPosition(textBoxPosition.Text))
            {
                MessageBox.Show(
                    "Должность обязательна и должна быть от 2 до 100 символов.",
                    "Ошибка ввода должности", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPosition.Focus();
                return false;
            }

            int year = (int)numericBirthYear.Value;
            if (!ValidationHelper.IsValidBirthYear(year))
            {
                MessageBox.Show(
                    "Год рождения должен быть от 1925 до 2011.",
                    "Ошибка ввода года", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericBirthYear.Focus();
                return false;
            }

            if (!ValidationHelper.IsValidSalary(numericSalary.Value))
            {
                MessageBox.Show(
                    "Зарплата должна быть больше 0.",
                    "Ошибка ввода зарплаты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericSalary.Focus();
                return false;
            }

            return true;
        }
    }
}