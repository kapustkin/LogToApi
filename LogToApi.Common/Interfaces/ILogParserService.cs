using System.Collections.Generic;
using System.Threading.Tasks;
using LogToApi.Common.Models;

namespace LogToApi.Common.Interfaces
{
    /// <summary>
    /// Интерфейс парсера логов
    /// </summary>
    public interface ILogParserService
    {
        /// <summary>
        /// Метод возвращает последние записи из лога
        /// </summary>
        /// <param name="takeLast">Вернуть последни N записей</param>
        /// <returns></returns>
        Task<IEnumerable<LogRecord>> GetLog(int? takeLast = 10);
    }
}