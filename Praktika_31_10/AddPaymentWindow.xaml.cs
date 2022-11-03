using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Praktika_31_10
{
    /// <summary>
    /// Логика взаимодействия для AddPaymentWindow.xaml
    /// </summary>
    public partial class AddPaymentWindow : Window
    {
        public AddPaymentWindow()
        {
            InitializeComponent();
            addPaymentComboBox.ItemsSource = PaymentDBEntities.GetContext().categories.ToList();
        }

        private void addPaymentWindowBtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(addPaymentComboBox.SelectedItem != null && countPicker.Text != "" && priceTextBox.Text != "" && paymentKeyTextBox.Text != "")
            {

                PaymentDBEntities.GetContext().payments.Add(new payments(paymentKeyTextBox.Text, Convert.ToDecimal(priceTextBox.Text), Convert.ToInt32(countPicker.Text), Convert.ToDecimal(Convert.ToDecimal(priceTextBox.Text) * Convert.ToInt32(countPicker.Value)), 1, 1, DateTime.Now, addPaymentComboBox.SelectedItem.ToString()));
                PaymentDBEntities.GetContext().SaveChanges();
                Close();
                MessageBox.Show("Новый платёж добавлен");
            }
            
        }
    }
}

