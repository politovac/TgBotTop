using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgBotTop
{
    internal class ApiSeves
    {
        public static async Task<string> Get(string Url)
        {
            HttpClient client = new(); // создаём объект хттп_клиента
            var response = await client.GetAsync(Url); // отправляем асинхронный запрос гет
            return await response.Content.ReadAsStringAsync();// возвращаем контент
        }

        public static async Task<string> Post<T>(T obj, string Url)
        {
            try
            {
                HttpClient client = new(); // создаём объект хттп_клиента

                //формировка из объекта в json запрос
                StringContent jsonContent = new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                // получаем ответ сервера на наш POST запрос
                HttpResponseMessage response = await client.PostAsync(Url, jsonContent);
                return await response.Content.ReadAsStringAsync(); // возвращаем контент
            }
            catch { return "false"; }
        }

    }
}

