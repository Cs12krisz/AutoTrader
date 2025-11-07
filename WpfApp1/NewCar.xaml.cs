using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for NewCar.xaml
    /// </summary>
    public partial class NewCar : Window
    {
        public readonly HttpClient _httpClient = new HttpClient();
        public NewCar()
        {
            InitializeComponent();
        }

        public async Task AddNewCar()
        {
            var newCar = new CarDto()
            {
                Brand = txbBrand.Text,
                Type = txbType.Text,
                Color = txbColor.Text,
                Year = Convert.ToDateTime(txbYear.Text)
            };

            var json = JsonSerializer.Serialize(newCar);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(MainWindow.apiURL, content);
            response.EnsureSuccessStatusCode();

            MainWindow mainWindow = new MainWindow();
            await mainWindow.LoadDataAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewCar();

        }
    }
}
