using HM_EmailSender.EmailDetails;
using MimeKit;
using System.IO;
using System.Text;
using System.Windows;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace HM_EmailSender
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == true)
            {
                EmailConfiguration emailConfiguration = new EmailConfiguration();

                string htmlFilePath = string.Empty;

                Message messageConf = new Message();
                messageConf.To = this.emailTB.Text;

                if (this.birthdayRB.IsChecked == true)
                {
                    htmlFilePath = @"D:\VisualStudioProjects\HTML\htmlEmailSender\htmlEmailSender\EmailDetails\birthdayCongrats.html";
                    messageConf.Subject = "Happy birthday!";
                }
                else if (this.salesRB.IsChecked == true)
                {
                    htmlFilePath = @"D:\VisualStudioProjects\HTML\htmlEmailSender\htmlEmailSender\EmailDetails\sales.html";
                    messageConf.Subject = "Sales coming!";
                }
                else
                {
                    htmlFilePath = @"D:\VisualStudioProjects\HTML\htmlEmailSender\htmlEmailSender\EmailDetails\notification.html";
                    messageConf.Subject = "Notification";
                }
                messageConf.Body = File.ReadAllText(htmlFilePath, Encoding.UTF8);
                messageConf.Body = messageConf.Body.Replace("{{name}}", this.nameTB.Text);


                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Pet shop", emailConfiguration.From));
                message.To.Add(new MailboxAddress(this.nameTB.Text, messageConf.To));
                message.Subject = messageConf.Subject;


                var body = new TextPart("html")
                {
                    Text = messageConf.Body
                };


                message.Body = body;


                using var client = new SmtpClient();
                try
                {

                    client.Connect(emailConfiguration.SmtpServer, emailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(emailConfiguration.UserName, emailConfiguration.Password);


                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");


                    client.Disconnect(true);
                    MessageBox.Show("Email has been sent!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Wrong input!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValid()
        {
            if (String.IsNullOrEmpty(this.emailTB.Text) || String.IsNullOrEmpty(this.nameTB.Text))
            {
                return false;
            }
            if (this.salesRB.IsChecked == false && this.notificationRB.IsChecked == false && this.birthdayRB.IsChecked == false)
            {
                return false;
            }
            return true;
        }
    }
}