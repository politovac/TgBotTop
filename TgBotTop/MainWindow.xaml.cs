using Newtonsoft.Json.Linq;
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
            //FirstGetUpdates();
        }
        int update_id = 0;
        long Ide = 0;
        Updates json;
        string messageUser;

        async void SendMessage(Message message)
        {
            await ApiSeves.Post(message, "https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/sendMessage");
        }
        async void FirstGetUpdates()
        {
            string zap = await
            ApiSeves.Get("https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/getupdates");//первый упдейт
            try
            {

                int x = zap.LastIndexOf("update_id");//находим номер последнего обновления

                int y = zap.IndexOf(",", x); //вытаскиваем последнего обновления

                string sup = zap.Substring(x, y - x); //вытаскиваем последнего обновления

                x = sup.IndexOf(":"); //вытаскиваем номер обновления

                sup = sup.Remove(0, x + 1); //вытаскиваем последнего обновления


                update_id = Convert.ToInt32(sup); //вытаскиваем последнего обновления

                json = new Updates();
                json.offset = update_id;
                json.timeout = 3;
                json.limit = 3;
                GetUpdates();
            }
            catch
            {

            }
        }
        private Random rnd = new Random();
        async void GetUpdates()
        {
            string s = await ApiSeves.Post(json, "https://api.telegram.org/bot7994061690:AAF2Tk3daHfU7IcJn5AnZ0ZjmZizfVfv6Dg/getUpdates");
            if (s == "{\"ok\":true,\"result\":[]}")
            {
                await Task.Delay(10);
                GetUpdates();
            }
            else
            {
                json.offset++;//увеличение номера последнего обновления
                Message messenge = new Message();
                
                int x = s.LastIndexOf("id");     //находим номер последнего id
                int y = s.IndexOf(",", x);         //вытаскиваем номер последнего id
                 
                string sup = s.Substring(x, y - x); //вытаскиваем номер последнего id
                x = sup.IndexOf(":"); //вытаскиваем номер последнего id

                sup = sup.Remove(0, x + 1); //вытаскиваем номер последнего id
                Ide = long.Parse(sup); //вытаскиваем номер последнего id

                messenge.chat_id = Ide; //присваиваем chat_id id последнего чата, где его трогали
                UserText(s, out messageUser);

                
                switch (messageUser)
                {
                    case "/start":
                        messenge.text = $"Приветествую тебя пользователь. Сегодня {DateTime.Now:dd.MM.yyyy} Чего соизволите?";
                        break;
                   
                    case "/picture":
                        messenge.text = "Тут должна быть твоя фотка!";
                        break;
                    
                    case "/game":
                        string random = Convert.ToString(rnd.Next(1, 4));
                        switch (random)
                        {
                            case "1":
                                messenge.text = "У меня очко, я победил";
                                break;
                            case "2":
                                messenge.text = "К вашему сожалению, ничья";
                                break;
                            case "3":
                                messenge.text = "Забирай своё очко и проваливай, победитель";
                                break;
                        }
                        break;
                    
                        default:
                        messenge.text = "Пока что я не знаю такой команды, но не унывай мой друг скоро я познаю весь этот мир!";
                        break;
                }


                SendMessage(messenge);
                await Task.Delay(50);
                GetUpdates();

            }
        }
        void UserText(string ishodnik, out string txt)
        {
            JObject jsonObject = JObject.Parse(ishodnik);

            txt = (string)jsonObject["result"][0]["message"]["text"]; //Извлечение значения:
          //Мы обращаемся к нужному элементу с помощью индексов и ключей.
          //В данном случае мы идем по структуре: result[0].message.text.

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FirstGetUpdates();
        }
    }

}
