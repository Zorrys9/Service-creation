using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingServices.Services
{
    public class SmsMessageSender:IMessageSender
    {
        public string Send()
        {
            return "Sent by SMS";
        }
    }
}
