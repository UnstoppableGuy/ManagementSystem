using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Domain.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidName(string value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                   value.Trim().Length >= 2 &&
                   value.Trim().Length <= 50 &&
                   value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'');
        }

        public static bool IsValidPosition(string position)
        {
            return !string.IsNullOrWhiteSpace(position) &&
                   position.Trim().Length >= 2 &&
                   position.Trim().Length <= 100;
        }

        public static bool IsValidBirthYear(int year)
        {
            int currentYear = DateTime.Now.Year;
            return year >= currentYear - 100 && year <= currentYear - 14;
        }

        public static bool IsValidSalary(decimal salary)
        {
            return salary > 0;
        }
    }
}
