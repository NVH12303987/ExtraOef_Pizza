using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

namespace ExtraOef_Pizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public byte TotalPizzaSum;
        private List<PizzaItem> PizzaItems;
        public MainWindow()
        {
            InitializeComponent();
            PizzaItems = new List<PizzaItem>();
            PizzaItems.Add(Pizza1);
            PizzaItems.Add(Pizza2);
            PizzaItems.Add(Pizza3);
            PizzaItems.Add(Pizza4);
            PizzaItems.Add(Pizza5);
            PizzaItems.Add(Pizza6);
            PizzaItems.Add(Pizza7);
            PizzaItems[0].Price = 12.50;
            PizzaItems[1].Price = 13.00;
            PizzaItems[2].Price = 12.00;
            PizzaItems[3].Price = 11.00;
            PizzaItems[4].Price = 12.50;
            PizzaItems[5].Price = 12.00;
            PizzaItems[6].Price = 9.00;
            PizzaItems[0].Pizzaname = "Quattro Stagioni (€ 12.50)";
            PizzaItems[1].Pizzaname = "Capricciosa (€ 13)";
            PizzaItems[2].Pizzaname = "Salami al Fuoco (€ 12)";
            PizzaItems[3].Pizzaname = "Prosciutto Cotto (€ 11)";
            PizzaItems[4].Pizzaname = "Quattro Formaggi (€ 12.50)";
            PizzaItems[5].Pizzaname = "Hawai (€ 12)";
            PizzaItems[6].Pizzaname = "Margherita (€ 9)";
            // Add event handlers for PropertyChanged events in PizzaItem instances
            foreach (var item in PizzaItems) item.PropertyChanged += UpdateTotalSum;
            // Initialize the DataContext and update the total sum
            DataContext = this;
            TotalPizzaSum = CalculateTotalSum();
        }

        private void UpdateTotalSum(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SliderValue")
            {
                // Recalculate the total sum when any PizzaItem's quantity changes
                TotalPizzaSum = CalculateTotalSum();
                //MessageBox.Show($"totaal pizzas = {TotalPizzaSum}");  
            }
        }

        private byte CalculateTotalSum()
        {
            // Calculate the total sum of pizza quantities from your PizzaItem instances
            byte sum = 0;
            foreach (var item in PizzaItems)
            {
                sum += item.SliderValue;
            }
            foreach (var item in PizzaItems) item.Increaseble_amount = (byte)(10 - sum);
            return sum;
        }

        private void btnBestel_Click(object sender, RoutedEventArgs e)
        {
            double totbedrag = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Naam: {txtNaam.Text}");
            sb.AppendLine($"Telefoonnummer: {txtTel.Text}");
            sb.AppendLine($"E-Mail: {txtmail.Text}");
            sb.AppendLine($"Adres: {txtAdres.Text}");
            sb.AppendLine($"Woonplaats: {txtwoonplaats.Text}");
            sb.AppendLine($"Postcode: {txtpostcode.Text}");
            foreach (var item in PizzaItems)
            {
                if(item.SliderValue != 0)
                {
                    sb.AppendLine($"{item.SliderValue} x € {item.Price} - {item.Pizzaname}");
                    totbedrag += (double)(item.SliderValue * item.Price);
                }
            }
            sb.AppendLine($"Totaalbedrag = €{totbedrag}");
            txtbill.Text = sb.ToString();
        }
    }
}
