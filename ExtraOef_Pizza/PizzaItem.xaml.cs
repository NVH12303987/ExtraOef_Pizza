using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for PizzaItem.xaml
    /// </summary>
    public partial class PizzaItem : UserControl, INotifyPropertyChanged
    {
        private string labelText;
        private double price;
        private byte sliderValue;
        private bool canincrease;
        private byte increaseble_amount;
        public PizzaItem()
        {
            InitializeComponent();
            Pizzaname = "son of a pizza";
            Price = 0.0; // Set an initial price if needed
            DataContext = this;
            canincrease = true;
        }
        public PizzaItem(string pizzaname_, double price_)
        {
            Pizzaname = pizzaname_;
            Price = price_;
        }
        private void DecreaseSliderValue(object sender, RoutedEventArgs e)
        {
            if(SliderValue!= 0)SliderValue -= 1;
        }
        private void IncreaseSliderValue(object sender, RoutedEventArgs e)
        {
            if(sliderValue < 10) SliderValue += 1;
        }
        public byte SliderValue
        {
            get { return sliderValue; }
            set
            {
                if (!(value >=0 && value <= 10)) { 
                    MessageBox.Show($"value:{value} is invalid. please give a number between 0 and 10."); 
                    txtpizza.Text = sliderValue.ToString(); 
                }else if (sliderValue != value && (sliderValue > value || canincrease))
                {
                    if (value-sliderValue <= increaseble_amount)
                    sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                    sldpizza.Value = sliderValue;
                }
            }
        }
        public byte Increaseble_amount
        {
            get => increaseble_amount;
            set {
                if (increaseble_amount != value) increaseble_amount = value;
                if (increaseble_amount == 0) { 
                    canincrease = false;
                    btnpluspizza.IsEnabled = false;
                }else { 
                    canincrease = true; 
                    btnpluspizza.IsEnabled = true;
                }
                
            }
        }

        public string Pizzaname
        {
            get { return labelText; }
            set
            {
                if (labelText != value)
                {
                    labelText = value;
                    OnPropertyChanged(nameof(Pizzaname));
                }
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
