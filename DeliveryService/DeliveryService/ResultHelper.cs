using BLL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService
{
    public static class ResultHelper
    {
        public static string Result(int answer, UserModel usr, string role, string op, ILogger logger)
        {
            if (usr == null)
            {
                usr = new UserModel();
                usr.UserName = "Unauthorized user";
                role = "Guest";
            }
            switch (answer)
            {
                case 1: logger.LogInformation(op + ": Success, " + usr.UserName + "(" + role + ")"); return null;
                case 2: logger.LogWarning(op + ": Not enough rights, " + usr.UserName + "(" + role + ")"); return "Не достаточно прав";
                case 3: logger.LogWarning(op + ": Wrong date, " + usr.UserName + "(" + role + ")"); return "Выберите корректную дату";
                case 4: logger.LogWarning(op + ": Wasn't found, " + usr.UserName + "(" + role + ")"); return "Не найдено";
                case 5: logger.LogWarning(op + ": Courier has already been appointed, " + usr.UserName + "(" + role + ")"); return "Курьер уже назаначен";
                case 6: logger.LogWarning(op + ": Wrong data, " + usr.UserName + "(" + role + ")"); return "Некорректные данные";
                case 7: logger.LogWarning(op + ": No changes, " + usr.UserName + "(" + role + ")"); return "Нет изменений";
                default: logger.LogError(op + ": Unexpected error, " + usr.UserName + "(" + role + ")"); return "Возникла непредвиденная ошибка";
            }
        }

        public static string Result(int answer, string op, ILogger logger)
        {
            switch (answer)
            {
                case 1: logger.LogInformation(op + ": Success"); return null;
                case 2: logger.LogWarning(op + ": Not enough rights"); return "Не достаточно прав";
                case 3: logger.LogWarning(op + ": Wrong date"); return "Выберите корректную дату";
                case 4: logger.LogWarning(op + ": Wasn't found"); return "Не найдено";
                case 5: logger.LogWarning(op + ": Courier has already been appointed"); return "Курьер уже назаначен";
                case 6: logger.LogWarning(op + ": Wrong data"); return "Некорректные данные";
                case 7: logger.LogWarning(op + ": No changes"); return "Нет изменений";
                default: logger.LogError(op + ": Unexpected error"); return "Возникла непредвиденная ошибка";
            }
        }

    }
}
