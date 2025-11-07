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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string apiURL = "https://localhost:7227/api/Cars";
        private int selectedID = -1;

        public MainWindow()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(apiURL);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<List<CarDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                table.ItemsSource = data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteDataAsync()
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{apiURL}?id={txbId.Text}");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (table.SelectedItem is CarDto selected)
            {
                MessageBox.Show(selected.Id.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteDataAsync();
        }
    }
}
