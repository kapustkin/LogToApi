using System;

namespace LogToApi.Models
{
    public class LogRecord
    {
        /// <summary>
        /// Дата время
        /// </summary>
        public string DateTime { get; set; }
        /// <summary>
        /// Уровень
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Объем
        /// </summary>
        public string Capacity { get; set; }
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}