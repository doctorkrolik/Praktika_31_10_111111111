using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using Syncfusion.Data;
using Syncfusion.Pdf;

using Syncfusion.Pdf.Grid;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converters;
using Syncfusion.Pdf.Graphics;

namespace Praktika_31_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments.ToList();
            categoryCombo.ItemsSource = PaymentDBEntities.GetContext().categories.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPaymentWindow addPaymentWindow = new AddPaymentWindow();
            addPaymentWindow.Show();
            this.IsEnabled = false;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            IsEnabled = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.IsEnabled = false;
        }

        private void mainWindowBtnMinus_Click(object sender, RoutedEventArgs e)
        {
            var currentItem = dataGrid.SelectedItem;

            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                PaymentDBEntities.GetContext().payments.Remove(currentItem as payments);
                PaymentDBEntities.GetContext().SaveChanges();

                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments.ToList();
            }
        }

        private void categoryCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(categoryCombo.SelectedItem != null)
            {
                var id = (categoryCombo.SelectedItem as categories).PK_category_id;

                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.products.categories.PK_category_id == id)
                    .ToList();
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datepicker1.SelectedDate != null && datepicker2.SelectedDate != null)
            {
                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.date >= datepicker1.SelectedDate && p.date <= datepicker2.SelectedDate)
                    .ToList();
            }
        }

        private void datepicker2_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (datepicker1.SelectedDate != null && datepicker2.SelectedDate != null)
            {
                dataGrid.ItemsSource = PaymentDBEntities.GetContext().payments
                    .Where(p => p.date >= datepicker1.SelectedDate && p.date <= datepicker2.SelectedDate)
                    .ToList();
            }
        }

        private void reportBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            datepicker1.SelectedDate = null;
            datepicker2.SelectedDate = null;
            categoryCombo.SelectedItem = null;
            dataGrid.SelectedItem = null;
        }

        private void reportBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                using (PdfDocument document = new PdfDocument())
                {
                    PdfPage page = document.Pages.Add();

                    PdfGraphics graphics = page.Graphics;

                    string path = @"C:\Users\pr119shmyrev\Desktop\ARIALUNI.TTF";

                    Stream fontStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

                    PdfTrueTypeFont font = new PdfTrueTypeFont(fontStream, 20, PdfFontStyle.Bold);

                    graphics.DrawString("Список платежей", font, PdfBrushes.Black, new PointF(0, 0));
                    PdfGrid pdfGrid = new PdfGrid();
                    //Create a DataTable.
                    DataTable dataTable = new DataTable();
                    //Add columns to the DataTable
                    dataTable.Columns.Add("ID");
                    dataTable.Columns.Add("Name");
                    //Add rows to the DataTable.

                    var currentItem = dataGrid.SelectedItem;

                    //var categoryName = PaymentDBEntities.GetContext().payments
                       // .Where(p => p.products.categories.category_name == );

                    //dataTable.Rows.Add(new object[] { categoryName.ToString(), "" });
                    dataTable.Rows.Add(new object[] { "E02", "Thomas" });
                    dataTable.Rows.Add(new object[] { "E03", "Andrew" });
                    dataTable.Rows.Add(new object[] { "E04", "Paul" });
                    dataTable.Rows.Add(new object[] { "E05", "Gary" });

                    pdfGrid.DataSource = dataTable;
                    //Draw grid to the page of PDF document.
                    pdfGrid.Draw(page, new PointF(10, 10));
                    //Save the document
                    document.Save(@"C:\Users\pr119shmyrev\Desktop\Output.pdf");

                    MessageBox.Show("PDF успешно сгенерирован!");
                }
            }
        }
    }
}
