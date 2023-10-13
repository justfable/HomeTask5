using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeTask5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Инициализация и выполнение примера 1
            Example1 example1 = new Example1();
            await example1.Run();

            // Инициализация и выполнение примера 2
            //Example2 example2 = new Example2();
            //example2.Run();

            // Инициализация и выполнение примера 3
            //Example3 example3 = new Example3();
            //example3.Run();
        }
    }

    // Пример 1: обработка исключения при запросе к несуществующему веб-сайту
    class Example1
    {
        public async Task Run()
        {
            string url = "http://nonexistentwebsite123456.com";
            try
            {
                // Пытаемся получить содержимое веб-сайта
                string content = await FetchWebsiteContent(url);
                Console.WriteLine(content);
            }
            // Ловим конкретные исключения
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при запросе к {url}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }
            finally
            {
                // Этот блок будет выполнен в любом случае
                Console.WriteLine("Программа Example1 завершила работу корректно.");
            }
        }

        // Метод для получения содержимого веб-сайта
        private async Task<string> FetchWebsiteContent(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }

    // Пример 2: обработка исключения при выходе за пределы массива
    class Example2
    {
        public void Run()
        {
            int[] array = new int[5] { 1, 2, 3, 4, 5 };
            try
            {
                // Пытаемся обратиться к несуществующему элементу массива
                int value = array[10];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Исключение перехвачено: {ex.Message}");
            }
            finally
            {
                // Этот блок будет выполнен в любом случае
                Console.WriteLine("Обработка массива в Example2 завершена.");
            }
        }
    }

    // Пример 3: генерация и "поднятие" исключения через стек вызовов
    class Example3
    {
        public void Run()
        {
            try
            {
                // Вызов метода, который вызовет другой метод, где будет сгенерировано исключение
                Method1();
            }
            catch (Exception ex)
            {
                // Перехватываем исключение, поднятое из Method2 через Method1
                Console.WriteLine($"Исключение перехвачено в Run: {ex.Message}");
            }
        }

        // Метод, вызывающий другой метод
        private void Method1()
        {
            Console.WriteLine("Выполнение Method1...");
            Method2();
        }

        // Метод, генерирующий исключение
        private void Method2()
        {
            Console.WriteLine("Выполнение Method2...");
            // Генерация исключения
            throw new InvalidOperationException("Произошла ошибка в Method2");
        }
    }
}
