using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotTop
{
    public class Message
    {
        public long chat_id { get; set; }
        public string text { get; set; }
    }
    public class Updates
    {
        public int offset { get; set; } 
        public int limit { get; set; }
        public int timeout { get; set; }

    }
}
