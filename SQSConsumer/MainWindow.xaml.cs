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

namespace SQSConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQSMessageConsumer consumer;
        private SQSMessageSender msgSender;
        public MainWindow()
        {
            InitializeComponent();

            textbox_MessageToSend.TextWrapping = TextWrapping.Wrap;
            textbox_MessageToSend.AcceptsReturn = true;
            textbox_MessageToSend.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textbox_MessageToSend.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            consumer = new SQSMessageConsumer(MessageReceived);
            msgSender = new SQSMessageSender();
        }

        private void MessageReceived(string message)
        {
            // TODO: Switch to UI context and add the message to the listbox

            try
            {
                Console.WriteLine(message);
                listbox_ReceivedMessages.Items.Add(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private void Checkbox_AutoPullMessages_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_PullMessages_Click(object sender, RoutedEventArgs e)
        {
            consumer.FetchMessages();
        }

        private void Button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (msgSender.SendMessage(textbox_MessageToSend.Text))
            {
                listbox_SentMessageList.Items.Add(textbox_MessageToSend.Text);
            }
            else
            {
                MessageBox.Show("Error sending the message out");
            }

            textbox_MessageToSend.Text = "";
        }

        private void Textbox_MessageToSend_KeyDown(object sender, KeyEventArgs e)
        {
            // NOTE: This event doesnt get captured since the textbox has "AcceptReturn" turned on
            if (e.IsDown == true && e.Key == Key.Enter)
            {
                SendMessage();
            }
        }
    }
}
