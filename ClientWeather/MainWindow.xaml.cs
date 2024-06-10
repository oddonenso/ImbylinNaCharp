using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ClientWeather
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGetWeatherClick(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text;
            string dateText = DateTextBox.Text; 

            // Проверка введен ли город
            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Введите город", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка введена ли дата
            if (string.IsNullOrWhiteSpace(dateText) || !DateTime.TryParse(dateText, out DateTime date))
            {
                MessageBox.Show("Введите корректную дату в формате ГГГГ-ММ-ДД", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            string requestMessage = $"{city};{date:yyyy-MM-dd}";
            byte[] requestData = Encoding.UTF8.GetBytes(requestMessage);

            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 12345);

            try
            {
                // Отправка запроса
                udpClient.Send(requestData, requestData.Length, serverEndPoint);

                // Получение ответа
                IPEndPoint remoteEndPoint = null;
                byte[] responseData = udpClient.Receive(ref remoteEndPoint);
                string responseMessage = Encoding.UTF8.GetString(responseData);

                // Отображение результата
                Dispatcher.Invoke(() =>
                {
                    ResultTextBlock.Text = ParseWeatherResponse(responseMessage);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }


        private string ParseWeatherResponse(string responseMessage)
        {
            var responseParts = responseMessage.Split(';');
            if (responseParts.Length == 6)
            {
                return $"Город: {responseParts[0]}\n" +
                       $"Дата: {responseParts[1]}\n" +
                       $"Температура: {responseParts[2]}°C\n" +
                       $"Влажность: {responseParts[3]}%\n" +
                       $"Давление: {responseParts[4]} мм рт. ст.\n" +
                       $"Тип погоды: {responseParts[5]}";
            }
            else
            {
                return "Ошибка при разборе ответа от сервера.";
            }
        }
    }
}
