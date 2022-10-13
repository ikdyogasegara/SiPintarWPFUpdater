using System;

namespace AppBusiness.Data.DTOs
{
    public class ParamTelegramDto
    {
        public string Token { get; set; }
        public string Key { get; set; }
        public string ChatId { get; set; }
        public string ChatHeader { get; set; }
        public string ChatMessage { get; set; }
    }
}
