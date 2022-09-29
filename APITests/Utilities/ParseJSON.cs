using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APITests.Utilities
{
    /// <summary>
    /// Содержит методы для парсинга JSON
    /// </summary>
    static class ParseJSON
    {
        /// <summary>
        /// Распарсить строку как JObject
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static JObject Parse_JObject(string text)
        {
            return Parse_JToken(text).Value<JObject>();
        }

        /// <summary>
        /// Распарсить строку как JToken
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static JToken Parse_JToken(string text)
        {
            try
            {
                return JToken.Parse(text);
            }
            catch (JsonReaderException e)
            {
                throw new JsonReaderException($"Не удалось распознать JSON:\n\"{text}\";\n{e.Message}");
            }
        }
    }
}
