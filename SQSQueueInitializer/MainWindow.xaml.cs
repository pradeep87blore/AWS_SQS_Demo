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
using SQSQueueCreator;

namespace SQSQueueInitializer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            string queueName = "MySQSQueue";
            if (CreateQueues.CreateSQSQueue(queueName))
                MessageBox.Show(string.Format("Queue {0} created successfully", queueName));
            else
            {
                MessageBox.Show(string.Format("Failed to create queue {0}", queueName));
            }

           this.Close();
        }
    }
}
