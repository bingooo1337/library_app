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
    /// Логика взаимодействия для student.xaml
    /// </summary>
    public partial class student : Window
    {
        libraryEntities db;
        public student()
        {
            InitializeComponent();
        }

        private void adding(object sender, RoutedEventArgs e)
        {
            db = new libraryEntities();
            string error = "";
            if (nameTB.Text == "" || codeTB.Text == "" || numberTB.Text == "")
            {
                error += "You did not enter one of the values.\n";
                MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (!Int32.TryParse(numberTB.Text, out int result))
                {
                    error += "Enter numeric value in Number";
                    MessageBox.Show(error, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            cStudent new_stud = new cStudent
            {
                full_name = nameTB.Text,
                student_code = codeTB.Text,
                mob_number = numberTB.Text,
                books_amount = 0
            };
            db.cStudent.Add(new_stud);
            db.SaveChanges();
            MessageBox.Show("Student will be added. Please update table.", "Adding student", MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
        }
    }
}
