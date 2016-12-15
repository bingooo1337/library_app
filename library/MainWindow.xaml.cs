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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        libraryEntities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new libraryEntities();
            db.cStudent.Load();
            db.cBook.Load();
            db.vBlank.Load();
            studentGrid.ItemsSource = db.cStudent.Local.ToBindingList();
            bookGrid.ItemsSource = db.cBook.Local.ToBindingList();
            blankGrid.ItemsSource = db.vBlank.Local.ToBindingList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db = new libraryEntities();
            int Tab_open = tabs.SelectedIndex;
            switch (Tab_open)
            {
                case 0:
                    db.cStudent.Load();
                    studentGrid.ItemsSource = db.cStudent.Local.ToBindingList();
                    break;
                case 1:
                    db.cBook.Load();
                    bookGrid.ItemsSource = db.cBook.Local.ToBindingList();
                    break;
                case 2:
                    db.vBlank.Load();
                    blankGrid.ItemsSource = db.vBlank.Local.ToBindingList();
                    break;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int Tab_open = tabs.SelectedIndex;
            switch (Tab_open)
            {
                case 0:
                    student add_new_stud = new student();
                    add_new_stud.Show();
                    break;
                case 1:
                    book add_new_book = new book();
                    add_new_book.Show();
                    break;
                case 2:
                    blank add_new_blank = new blank();
                    add_new_blank.Show();
                    break;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            int Tab_open = tabs.SelectedIndex;
            switch (Tab_open)
            {
                case 0:
                    if (studentGrid.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < studentGrid.SelectedItems.Count; i++)
                        {
                            cStudent student = studentGrid.SelectedItems[i] as cStudent;
                            if (student != null && student.books_amount == 0)
                                db.cStudent.Remove(student);
                            else
                                MessageBox.Show("You can not delete the student with books.","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    break;
                case 1:
                    if (bookGrid.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < bookGrid.SelectedItems.Count; i++)
                        {
                            cBook book = bookGrid.SelectedItems[i] as cBook;
                            if (book != null)
                                db.cBook.Remove(book);
                        }
                    }
                    break;
                case 2:
                    if (blankGrid.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < blankGrid.SelectedItems.Count; i++)
                        {
                            vBlank blank = blankGrid.SelectedItems[i] as vBlank;
                            if (blank != null && blank.status != "Open")
                                db.vBlank.Remove(blank);
                            else
                                MessageBox.Show("You can not delete blank with the status \"Open\". \nIt contains information about the books outside of the library", "Error deleting", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    break;
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                MessageBox.Show("You can not delete the book, if there are blanks with it!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                db = new libraryEntities();
                db.cBook.Load();
                bookGrid.ItemsSource = db.cBook.Local.ToBindingList();
            }
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Application for the course work of DATA BASES\n" +
                "Made by Kamyshanov Volodymyr\n" +
                "FICT, IS-41, 28 variant","INFORMATION", MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
