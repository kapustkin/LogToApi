using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogToApi.Common.Interfaces;
using LogToApi.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogToApi.Core.Services
{
    /// <summary>
    /// Сервис парсинга логов Hpool
    /// </summary>
    public class HpoolService : ILogParserService
    {
        private readonly ILogger<HpoolService> _logger;
        private readonly Configuration _configuration;

        public HpoolService(ILogger<HpoolService> logger, 
            IOptions<Configuration> configuration)
        {
            _logger = logger;
            _configuration = configuration?.Value;
        }

        public async Task<IEnumerable<LogRecord>> GetLog(int? takeLast)
        {
            if (_configuration?.PathToLogFile == null)
            {
                _logger.LogWarning($"Configuration not found");
                return null;
            }
            
            if (!File.Exists(_configuration.PathToLogFile))
            {
                _logger.LogWarning($"File not found: '{_configuration.PathToLogFile}'");
                return null;
            }
            
            var rawLines = File.ReadLines(_configuration.PathToLogFile)?.TakeLast(takeLast ?? 10);

            return rawLines?.Select(GetRecord);
        }

        /// <summary>
        /// Метод разбирает входную строку на объект LogRecord
        /// </summary>
        /// <param name="rawData">Строка с данными</param>
        /// <returns></returns>
        public static LogRecord GetRecord(string rawData)
        {
            if (rawData == null) return null;

            var result = new LogRecord
            {
                DateTime = DateTime.ParseExact(GetValue(rawData, "time", " level").Trim(new[] {'"'}),
                    "yyyy-MM-dd'T'hh:mm:sszzz",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None),
                Level = GetValue(rawData, "level", " msg"),
                Message = GetValue(rawData, "msg", "\" ")?.Trim(new[] {'"'}),
                Capacity = GetValue(rawData, "capacity", "\" ")?.Trim(new[] {'"'}),
                ErrorText = GetValue(rawData, "error", "\" ")?.Trim(new[] {'"'})
            };

            if (result.ErrorText == null) return result;
            
            // Если есть ошибка, добавляем в нее файл, если он есть
            var fileInfo = GetValue(rawData, "file", " ")?.Trim(new[] {'"'});
            if (fileInfo != null)
            {
                result.ErrorText += $" File: \"{fileInfo}\"";
            }

            return result;
        }

        /// <summary>
        /// Метод возвращает значение между ключей
        /// </summary>
        /// <param name="rawData">Строка с данными</param>
        /// <param name="key">Начальный ключ</param>
        /// <param name="nextKey">Следующий ключ</param>
        /// <returns></returns>
        private static string GetValue(string rawData, string key, string nextKey)
        {
            return Regex.Matches(rawData, $"(?<={key}=).+?(?={nextKey})")?.FirstOrDefault()?.Value;
        }
    }
}