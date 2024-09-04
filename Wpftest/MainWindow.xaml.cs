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
    delegate void Message();

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

        void Hello(n) {
            MessageBox.Show("Hello METANIT.COM" + n);
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

        int n;

        public TestDelegate(Message d)
        {
            Delegate = d;
        }


        public void DelInfo()
        {
            Delegate(n);
        }
    }
}
