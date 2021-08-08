using System;

namespace LogToApi.Common.Models
{
    public class LogRecord
    {
        /// <summary>
        /// Дата время
        /// </summary>
        public DateTime DateTime { get; set; }
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
        /// Сообщение об ошибке, если есть
        /// </summary>
        public string ErrorText { get; set; }
    }
}