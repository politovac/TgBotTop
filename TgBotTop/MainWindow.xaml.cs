using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TgBotTop
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
        int update_id = 0;
        long Ide = 0;
        Updates json;
        Message message;

        async void SendMessage(Message message)
        {
            await ApiSeves.Post(message, "https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/sendMessage");
        }
        async void FirstGetUpdates()
        {
            string zap = await
            ApiSeves.Get("https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/getUpdates");//первый упдейт
            try
            {
                int Ide = 0;
                int x = zap.LastIndexOf("update_id");//находим номер последнего обновления

                int y = zap.IndexOf(",", x); //вытаскиваем последнего обновления

                string sup = zap.Substring(x, y - x); //вытаскиваем последнего обновления

                x = sup.IndexOf(":"); //вытаскиваем номер обновления

                sup = sup.Remove(0, x + 1); //вытаскиваем последнего обновления
                //Ide = long.Parse(sup);
                message.chat_id = Ide;

                update_id = Convert.ToInt32(sup); //вытаскиваем последнего обновления

                json = new Updates();
                json.offset = update_id;
                json.timeout = 100;
                json.limit = 1;
                GetUpdates();
            }
            catch
            {

            }
        }
        async void GetUpdates()
        {
            json.offset++;//увеличение номера последнего обновления
            string s = await ApiSeves.Post(json, "https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/getUpdates");
            if (s == "{\"ok\":true,\"result\":[]}")
            {
                await Task.Delay(100);
                GetUpdates();
            }
            else
            {
                Message messenge = new Message();
                messenge.chat_id = 1251464862;
                messenge.text = "ну привет";
                SendMessage(messenge);
                await Task.Delay(100);
                GetUpdates();
            }
        }

    }
}