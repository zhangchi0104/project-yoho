using CreditCardValidator.libs;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CreditCardValidator {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TrieRep brands = new TrieRep();
     
        public MainWindow()
        {
            InitializeComponent();
            BrandLogo.Visibility = Visibility.Hidden;
            validity.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cardNumber = CardNumberBox.Text.Replace(" ", "");
            try {
                Validator.CheckNumber(cardNumber);
            } catch (ContainNonDigitException){
                MessageBox.Show("Your card number contains non-digits or may be empty!");
                return; // execution must be ended here
            }
            string cardBrand = brands.GetBrand(cardNumber);
            loadImage(cardBrand);
            bool isValidNumber = Validator.ValidateNumber(cardNumber);
            setValidity(isValidNumber);
            BrandLogo.Visibility = Visibility.Visible;
            validity.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BrandLogo.Visibility = Visibility.Hidden;
            validity.Visibility = Visibility.Hidden;
        }

        private void showAbout(object sender, RoutedEventArgs e)
        {
            AboutInfo aboutInfo = new AboutInfo();
            aboutInfo.Show();
        }
        private void loadImage(string cardBrand) {
            StringBuilder pathToImage = new StringBuilder("pack://application:,,,/CreditCardValidator;component/asset/");
            pathToImage.Append(cardBrand);
            pathToImage.Append(".png");
            try {
                BrandLogo.Source = new BitmapImage(new Uri(pathToImage.ToString()));
            }
            catch (System.IO.IOException) {
                BrandLogo.Source = new BitmapImage(new Uri("pack://application:,,,/CreditCardValidator;component/asset/Unknown.png"));
            }
        }
        private void setValidity(bool cardValidity) {
            if(cardValidity) {
                validity.Content = "Valid";
                validity.Foreground = new SolidColorBrush(Colors.ForestGreen);
            } else {
                validity.Content = "Invalid";
                validity.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void CardNumberBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Button_Click(sender, e);
            }
        }
    }
}
