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

namespace Wpftest
{
    delegate int Message(int n);

    public partial class MainWindow : Window
    {
        TestDelegate delTest;

        public MainWindow()
        {
            InitializeComponent();

            Binding binding = new Binding();

            binding.ElementName = "myTextBox"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            myTextBlock.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
            myTextBox.AppendText("szos osoznane");

            delTest = new TestDelegate(this.Hello);

        }

        int Hello(int n) {
            MessageBox.Show("Hello METANIT.COM " + n);

            TestDelegate.testStaticMetod();
            return (n / 2);
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(checkBox.ToString() + " отмечен");
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(checkBox.ToString() + " не отмечен");
        }

        private void checkBox_Indeterminate(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(checkBox.ToString() + " в неопределенном состоянии");
        }


        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(myTextBlock.Text);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            usersList.Items.Add("Kate");
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            peopleComboBox.Items.Add(new Person { Name = "Tweffe", Company = "Microsoft" }) ;
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            tables.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Ноутбуки" }
            });

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Hello";
            string text = (string)textBlock.ReadLocalValue(TextBlock.TextProperty); // Hello
            textBlock.ClearValue(TextBlock.TextProperty); // теперь значение отсутствует
        }

        private void Button_ClickmainVkladka(object sender, RoutedEventArgs e)
        {
            delTest.DelInfo();
        }

        void DisplayMessage(Account sender, AccountEventArgs e) => MessageBox.Show(e.Message);
        void DisplayMessageTest(Account sender, AccountEventArgs e) => MessageBox.Show("e.Message");

        private void Button_ClickmainVkladkaEvent(object sender, RoutedEventArgs e)
        {
            Account account = new Account(100);
            account.Notify += DisplayMessage;   // Добавляем обработчик для события Notify
            account.Put(20);
            account.Notify += DisplayMessageTest;
            account.Take(70);
            account.Notify -= DisplayMessage;
            account.Take(180);

            
        }
    }

    public class Person
    {
        public string Name { get; set; } = "";
        public string Company { get; set; } = "";
        public override string ToString() => $"{Name} ({Company})";

        

        public Person()
        {
        }
    }

    class TestDelegate
    {
        Message Delegate;

        static int n = 0;
        

        public TestDelegate(Message d)
        {
            Delegate = d;
        }


        public void DelInfo()
        {
            MessageBox.Show(Delegate(n).ToString() );
        }

        public static void testStaticMetod()
        {
            n ++;
        }
    }

    delegate void AccountHandler(string message);

    class AccountEventArgs
    {
        // Сообщение
        public string Message { get; }
        // Сумма, на которую изменился счет
        public int Sum { get; }
        public AccountEventArgs(string message, int sum)
        {
            Message = message;
            Sum = sum;
        }
    }
    class Account
    {
        public delegate void AccountHandler(Account sender, AccountEventArgs e);
        public event AccountHandler Notify;

        public int Sum { get; private set; }

        public Account(int sum) => Sum = sum;

        public void Put(int sum)
        {
            Sum += sum;

            if (Notify != null)  Notify(this, new AccountEventArgs($"На счет поступило {sum}", sum));
        }
        public void Take(int sum)
        {
            if (Sum >= sum)
            {
                Sum -= sum;

                if (Notify != null) Notify(this, new AccountEventArgs($"Сумма {sum} снята со счета", sum));
            }
            else
            {

                if (Notify != null)  Notify(this, new AccountEventArgs("Недостаточно денег на счете", sum));
            }
        }
    }
}
