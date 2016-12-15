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
using System.Data.Entity;

namespace library
{
    /// <summary>
    /// Логика взаимодействия для blank.xaml
    /// </summary>
    public partial class blank : Window
    {
        libraryEntities db;
        public blank()
        {
            InitializeComponent();
            db = new libraryEntities();
            db.cStudent.Load();
            db.cBook.Load();
            studentGrid.ItemsSource = db.cStudent.Local.ToBindingList();
            bookGrid.ItemsSource = db.cBook.Local.ToBindingList();
        }

        private void adding(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (status_box.SelectedIndex == -1 || studentGrid.SelectedItems.Count != 1 || bookGrid.SelectedItems.Count != 1)
            {
                error += "You did not select the student (only one), or a book (only one), or status.\n";
                MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (open_date_pick.SelectedDate == null || (close_date_pick.SelectedDate == null && ((ComboBoxItem)status_box.SelectedItem).Content.ToString() != "Open"))
                {
                    error += "You did not select Open date or Close date(if status \"Closed\")";
                    MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (((ComboBoxItem)status_box.SelectedItem).Content.ToString() != "Open" && (open_date_pick.SelectedDate.Value > close_date_pick.SelectedDate.Value))
                    {
                        error += "Incorrect dates";
                        MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            cStudent student = studentGrid.SelectedItems[0] as cStudent;
            cBook book = bookGrid.SelectedItems[0] as cBook;
            vBlank new_blank = new vBlank
            {
                status = ((ComboBoxItem)status_box.SelectedItem).Content.ToString(),
                open_date = open_date_pick.SelectedDate.Value,
                book_title = book.title,
                author = book.author,
                year_pub = book.year_pub.ToString(),
                book_id = book.ID,
                student_id = student.ID
            };
            if (close_date_pick.SelectedDate != null)
                new_blank.close_date = close_date_pick.SelectedDate.Value;
            else
            {
                student.books_amount += 1;
                book.available -= 1;
            }
            db.vBlank.Add(new_blank);
            db.SaveChanges();
            MessageBox.Show("Blank will be added. Please update table.", "Adding blank", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
