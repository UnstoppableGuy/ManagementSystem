using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.Helpers
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Проверяет, является ли строка корректным именем или фамилией
        /// </summary>
        public static bool IsValidName(string value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                   value.Trim().Length >= 2 &&
                   value.Trim().Length <= 50 &&
                   value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'');
        }

        /// <summary>
        /// Проверяет корректность должности
        /// </summary>
        public static bool IsValidPosition(string position)
        {
            return !string.IsNullOrWhiteSpace(position) &&
                   position.Trim().Length >= 2 &&
                   position.Trim().Length <= 100;
        }

        /// <summary>
        /// Проверяет корректность года рождения
        /// </summary>
        public static bool IsValidBirthYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            return year >= 1900 && year <= currentYear;
        }

        /// <summary>
        /// Проверяет корректность зарплаты
        /// </summary>
        public static bool IsValidSalary(decimal salary)
        {
            return salary > 0 && salary <= 10_000_000;
        }

        /// <summary>
        /// Возвращает понятное сообщение об ошибке
        /// </summary>
        public static string GetErrorMessage(string fieldName)
        {
            return fieldName switch
            {
                "FirstName" or "LastName" => "Имя и фамилия: 2–50 букв, без цифр",
                "Position" => "Должность: 2–100 символов",
                "BirthYear" => "Год рождения: от 1900 до текущего",
                "Salary" => "Зарплата должна быть больше 0",
                _ => "Некорректное значение"
            };
        }
    }
}
