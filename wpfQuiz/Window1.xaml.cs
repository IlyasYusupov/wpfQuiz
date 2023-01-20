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

namespace wpfQuiz
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(tbQuestion.Text == string.Empty || tbAnswer.Text == string.Empty)
            {
                MessageBox.Show("Заполините textboxы");
                return;
            }
            else
            {
                Issue issue = new Issue(tbQuestion.Text, tbAnswer.Text.ToUpper());
                tbQuestion.Text = string.Empty;
                tbAnswer.Text = string.Empty;   
                Mongo.AddToDB(issue);
            }
        }

    }
}
