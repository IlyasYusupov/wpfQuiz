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
using System.Windows.Markup;
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
        string? word;
        string? question;
        string[]? answer;
        TextBox[] allTextBox;
        Button[] blockBtn;
        List<Issue> issues = new List<Issue>();
        public MainWindow()
        {
            InitializeComponent();
            
            Issue i = new Issue("Программа помогающая писать на разных языка?", "TRANSLATER");
            i.Number = 0;
            Issue t = new Issue("Столица России", "MOSCOW");
            t.Number = 1;
            Issue e = new Issue("Столица Франции", "LONDON");
            e.Number = 2;
            Issue r = new Issue("Столица Америки", "WASHINGTON");
            r.Number = 3;
            issues.Add(i);
            issues.Add(t);
            issues.Add(e);
            issues.Add(r);
            CreateIssue();
        }

        private void CreateIssue()
        {
            Random rnd = new Random();
            int num = rnd.Next(0,3);
            for(int i = 0; i < 4; i++)
            {
                if (i == num)
                {
                    word = issues.Find(x => x.Number == num).Answer;
                    question = issues.Find(x => x.Number == num).Question;
                }
                   
            }
            answer = new string[word.Length];
            blockBtn = new Button[30];
            allTextBox = new TextBox[word.Length];
            CreateBoard();
            CreateButton();
        }

        private void CreateBoard()
        {
            Label lb = new Label();
            lb.Content = question;
            Board.Children.Add(lb);
            Grid.SetRow(lb, 0);
            Grid.SetColumn(lb, 2);
            WrapPanel wp = new();
            wp.Orientation = Orientation.Horizontal;
            wp.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < word.Length; i++)
            {
                TextBox textBox = new();
                textBox.FontSize = 30;
                textBox.TextAlignment = TextAlignment.Center;
                textBox.Width = 45;
                textBox.Height = 45;
                textBox.IsReadOnly = true;
                if (i == word.Length-1)
                    textBox.Tag = "last";
                allTextBox[i] = textBox;
                wp.Children.Add(textBox);
            }
            Board.Children.Add(wp);
            Grid.SetRow(wp, 1);
            Grid.SetColumn(wp, 2);
        }

        private void CreateButton()
        {
            Random rnd = new Random();
            WrapPanel wp = new();
            wp.Orientation = Orientation.Horizontal;
            wp.HorizontalAlignment = HorizontalAlignment.Center;
            Button[] btnArray = new Button[30];
            for (int i = 0; i < 30; i++)
            {
                Button button = new Button()
                {
                    Margin = new Thickness(0, 0, 5, 5)
                };
                button.Height = 45;
                button.Width = 45;
                button.FontSize = 30;
                button.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
                if (i < word.Length) 
                    button.Content = word[i];
                else 
                    button.Content = Convert.ToChar(rnd.Next(65, 90));
                btnArray[i] = button;
            }
            BtnArrayShuffle(ref btnArray, wp);
            Board.Children.Add(wp);
            Grid.SetRow(wp, 2);
            Grid.SetColumn(wp, 2);
        }

        private void BtnArrayShuffle(ref Button[] data, WrapPanel wp)
        { 
            Random rnd = new Random();
            for (int i = data.Length -1; i >= 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
                wp.Children.Add(data[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = (sender as Button);
            for(int i = 0; i < allTextBox.Length; i++)
            {
                if (allTextBox[i].Text == string.Empty)
                {
                    allTextBox[i].Text = currentButton.Content.ToString();
                    answer[i] = currentButton.Content.ToString();
                    currentButton.IsEnabled = false;
                    blockBtn[i] = currentButton;
                    break;
                }
            }
        }

        private void btnСlear_Click(object sender, RoutedEventArgs e)
        {
            for(int i = allTextBox.Length - 1; i >= 0; i--)
            {
                if (allTextBox[i].Text != string.Empty)
                {
                    allTextBox[i].Text = string.Empty;
                    answer[i] = string.Empty;
                    blockBtn[i].IsEnabled = true;
                    blockBtn[i] = null;
                    break;
                }
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (allTextBox[allTextBox.Length-1].Text != string.Empty)
            {
                string result = string.Empty;
                foreach(var i in answer)
                {
                    result += i;
                }
                if(result == word)
                {
                    MessageBox.Show("Молодец!");
                    for (int i = allTextBox.Length - 1; i >= 0; i--)
                    {
                        allTextBox[i].Text = string.Empty;
                        //if (blockBtn != null)
                            blockBtn[i].IsEnabled = true;
                        blockBtn[i] = null;
                    }
                }
                else
                {
                    MessageBox.Show("Попробуй ещё раз!");
                    for (int i = allTextBox.Length - 1; i >= 0; i--)
                    {
                        allTextBox[i].Text = string.Empty;
                        blockBtn[i].IsEnabled = true;
                        blockBtn[i] = null;
                    }
                }
                CreateIssue();
            }
        }
    }
}
