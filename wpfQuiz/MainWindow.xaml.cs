using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        int Live = 3;
        List<Issue> issues = new List<Issue>();
        public MainWindow()
        {
            InitializeComponent();
            Mongo.FindAll(issues);
            CreateIssue();
        }

        private void CreateIssue()
        {
            Random rnd = new Random();
            if (issues.Count == 0 ) 
            {
                MessageBox.Show("Нет вопросов! Добавьте хотя бы 1");
                Window1 wd = new Window1();
                wd.ShowDialog();
                Mongo.FindAll(issues);
            }
            int num = rnd.Next(0, issues.Count);
            for (int i = 0; i < issues.Count; i++)
            {
                if (num == i)
                {
                    question = issues[i].Question;
                    word = issues[i].Answer;
                    issues.Remove(issues[i]);
                    break;
                }
            }
            answer = new string[word.Length];
            blockBtn = new Button[40];
            allTextBox = new TextBox[word.Length];
            CreateBoard();
            CreateButton();
        }

        private void CreateBoard()
        {
            Label lbQuestion = new Label();
            lbQuestion.Content = question;
            lbQuestion.FontSize = 20;
            wpQuestion.Children.Add(lbQuestion);
            for (int i = 0; i < word.Length; i++)
            {
                TextBox textBox = new();
                textBox.FontSize = 30;
                textBox.TextAlignment = TextAlignment.Center;
                textBox.Width = 40;
                textBox.Height = 40;
                textBox.IsReadOnly = true;
                if (i == word.Length - 1)
                    textBox.Tag = "last";
                allTextBox[i] = textBox;
                wpBoard.Children.Add(textBox);
            }
        }

        private void CreateButton()
        {
            Random rnd = new Random();
            Button[] btnArray = new Button[40];
            for (int i = 0; i < 40; i++)
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
                    button.Content = Convert.ToChar(rnd.Next(1040, 1072));
                btnArray[i] = button;
            }
            BtnArrayShuffle(ref btnArray);
        }

        private void BtnArrayShuffle(ref Button[] data)
        {
            Random rnd = new Random();
            for (int i = data.Length - 1; i >= 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = data[j];
                data[j] = data[i];
                data[i] = temp;
                wpKeyboard.Children.Add(data[i]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var currentButton = (sender as Button);
            for (int i = 0; i < allTextBox.Length; i++)
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
            for (int i = allTextBox.Length - 1; i >= 0; i--)
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
            if (allTextBox[allTextBox.Length - 1].Text != string.Empty)
            {
                string result = string.Empty;
                foreach (var i in answer)
                {
                    result += i;
                }
                if (result == word)
                {
                    MessageBox.Show("Молодец!");
                    wpQuestion.Children.Clear();
                    wpBoard.Children.Clear();
                    wpKeyboard.Children.Clear();
                    ClearForms();
                    CreateIssue();
                }
                else
                {
                    if(Live <= 0)
                    {
                        MessageBox.Show("Ты Проиграл!");
                        wpKeyboard.Visibility = Visibility.Hidden;
                        return;
                    }
                    Live--;
                    MessageBox.Show($"Попробуй ещё раз! Осталось жизней: {Live} ");
                    ClearForms();
                    return;
                }
            }
        }

        private void ClearForms()
        {
            for (int i = allTextBox.Length - 1; i >= 0; i--)
            {
                allTextBox[i].Text = string.Empty;
                blockBtn[i].IsEnabled = true;
                blockBtn[i] = null;
            }
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            Window1 wd = new Window1();
            wd.ShowDialog();
        }


    }
}
