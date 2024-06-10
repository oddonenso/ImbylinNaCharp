using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherLibrary.Interfaces;
using WeatherLibrary.Services;
using Data.Table;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WeatherLibrary.Model;

class Program
{
    static void Main(string[] args)
    {
        //добавление зависимостей 
        var serviceProvider = new ServiceCollection()
            .AddDbContext<Connection>(options =>
                options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=111;Database=PrognozPogodi;"))  // Настройка базы данных берется из библиотеки
            .AddScoped<IWeatherService, WeatherService>()
            .BuildServiceProvider();

        // Разрешение WeatherService
        var weatherService = serviceProvider.GetService<IWeatherService>();

        //sett server
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        Console.WriteLine("Добрый день, Александр, сервак запущен");

        while (true)
        {
            try
            {
                //преобразование сообщения клиента
                byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedData);
                Console.WriteLine($"Получено: {receivedMessage} от {remoteEndPoint}");

                var requestParts = receivedMessage.Split(';');
                //и вторая часть может быть преобразована в DateTime
                if (requestParts.Length == 2 && DateTime.TryParse(requestParts[1], out DateTime requestDate))
                {

                    requestDate = DateTime.SpecifyKind(requestDate, DateTimeKind.Utc);

                    var weatherRequest = new WeatherRequest
                    {
                        City = requestParts[0],
                        Date = requestDate
                    };

                    var weatherResponse = weatherService.GetWeather(weatherRequest);

                    string responseMessage = $"{weatherResponse.City};{weatherResponse.Date};{weatherResponse.Temperature};{weatherResponse.Humidity};{weatherResponse.Pressure};{weatherResponse.WeatherType}";
                    byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);

                    // Отправка ответа
                    udpClient.Send(responseData, responseData.Length, remoteEndPoint);
                    Console.WriteLine($"Отправлено: {responseMessage} к {remoteEndPoint}");
                }
                else
                {
                    Console.WriteLine("Неверный формат запроса.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
        }
    }
}
