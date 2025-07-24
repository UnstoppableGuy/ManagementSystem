using ManagementSystem.Domain.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace ManagementSystem.UI.Forms
{
    public partial class FormReport : Form
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger _logger;

        public FormReport(IEmployeeRepository repository, ILogger logger)
        {
            InitializeComponent();
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            _logger.Info("Загрузка отчёта о средней зарплате");

            try
            {
                // Получаем данные из репозитория
                var salaryData = _repository.GetAverageSalaryByPosition();

                if (salaryData == null || salaryData.Count == 0)
                {
                    _logger.Warning("Нет данных для отчета по средней зарплате");
                    MessageBox.Show("Нет данных для отображения отчета", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Создаём DataTable с корректными именами столбцов
                var dataTable = CreateReportDataTable(salaryData);

                _logger.Info($"Подготовлено {dataTable.Rows.Count} записей для отчета");

                // Настраиваем ReportViewer
                ConfigureReportViewer(dataTable);

                _logger.Info("Отчёт успешно загружен");
            }
            catch (Exception ex)
            {
                _logger.Error("Ошибка загрузки отчёта", ex);
                MessageBox.Show($"Ошибка загрузки отчёта: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CreateReportDataTable(System.Collections.Generic.List<ManagementSystem.Domain.Models.SalaryReport> salaryData)
        {
            var dataTable = new DataTable("SalaryReport");

            // Добавляем столбцы с точными именами, соответствующими RDLC
            dataTable.Columns.Add("Position", typeof(string));
            dataTable.Columns.Add("AverageSalary", typeof(decimal));

            // Заполняем данными
            foreach (var item in salaryData)
            {
                if (item != null && !string.IsNullOrWhiteSpace(item.Position))
                {
                    var row = dataTable.NewRow();
                    row["Position"] = item.Position;
                    row["AverageSalary"] = item.AverageSalary;
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        private void ConfigureReportViewer(DataTable dataTable)
        {
            try
            {
                // Очищаем предыдущие источники данных
                reportViewer.LocalReport.DataSources.Clear();

                // Устанавливаем путь к отчету
                reportViewer.LocalReport.ReportEmbeddedResource = "ManagementSystem.UI.Reports.Report.rdlc";

                // Добавляем источник данных
                var reportDataSource = new ReportDataSource("DataSet1", dataTable);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                // Обновляем отчет
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                _logger.Error("Ошибка конфигурации ReportViewer", ex);
                throw new InvalidOperationException("Не удалось настроить отчет", ex);
            }
        }
    }
}