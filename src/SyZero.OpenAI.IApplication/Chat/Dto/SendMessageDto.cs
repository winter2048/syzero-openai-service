using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyZero.OpenAI.IApplication.Chat.Dto
{
    public class SendMessageDto
    {
        public string SessionId { get; set; }

        public string Message { get; set; }
    }
}
