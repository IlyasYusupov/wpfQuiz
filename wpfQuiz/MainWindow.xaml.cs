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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateBoard();
            CreateButton();
        }

        private void CreateBoard()
        {
            WrapPanel sp = new();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < 20; i++)
            {
                TextBox textBox = new();
                textBox.Name = "tb" + $"{i}";
                //textBox.Text = i.ToString();
                textBox.FontSize = 35;
                textBox.TextAlignment = TextAlignment.Center;
                textBox.Width = 50;
                textBox.Height = 50;
                textBox.IsReadOnly = true;
                sp.Children.Add(textBox);
                
            }
            Board.Children.Add(sp);
            Grid.SetRow(sp, 1);
            Grid.SetColumn(sp, 1);
        }

        private void CreateButton()
        {
            Random rnd = new Random();
            WrapPanel sp = new();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < 30; i++)
            {
                Button button = new Button();
                char a = Convert.ToChar(rnd.Next(65, 90));
                button.Content = a;
                button.Height = 45;
                button.Width = 45;
                button.FontSize = 30;
                button.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
                //button.Click += new RoutedEventHandler(Button_Click);
                sp.Children.Add(button);
                
            }
            Board.Children.Add(sp);
            Grid.SetRow(sp, 2);
            Grid.SetColumn(sp, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = (sender as Button);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(Board); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(Board, i);
                if (childVisual is StackPanel)
                {
                    for (int j = 0; j < VisualTreeHelper.GetChildrenCount(childVisual); j++)
                    {
                        Visual childStackVisual = (Visual)VisualTreeHelper.GetChild(childVisual, j);
                        if (childStackVisual is TextBox && (childStackVisual as TextBox).Text == "")
                        {
                            (childStackVisual as TextBox).Text = currentButton.Content.ToString();
                            break;
                        }
                    }
                }
            }
        }
    }
}
