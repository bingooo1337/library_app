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

namespace library
{
    /// <summary>
    /// Логика взаимодействия для book.xaml
    /// </summary>
    public partial class book : Window
    {
        libraryEntities db;
        public book()
        {
            InitializeComponent();
        }

        private void adding(object sender, RoutedEventArgs e)
        {
            db = new libraryEntities();
            string error = "";
            if (titleTB.Text == "" || authorTB.Text == "" || yearTB.Text == "" || availTB.Text == "")
            {
                error += "You did not enter one of the values.\n";
                MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if(!Int32.TryParse(yearTB.Text, out int result) || !Int32.TryParse(availTB.Text, out int result1))
                {
                    error += "Enter numeric values in Year and Available";
                    MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            cBook new_book = new cBook
            {
                title = titleTB.Text,
                author = authorTB.Text,
                year_pub = Int32.Parse(yearTB.Text),
                available = Int32.Parse(availTB.Text)
            };
            db.cBook.Add(new_book);
            db.SaveChanges();
            MessageBox.Show("Book will be added. Please update table.", "Adding Book", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
