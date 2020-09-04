namespace Application.Common.Models
{
    using System.Collections.Generic;

    public class EmailRequest
    {
        public string FromMail { get; set; }
        public string FromDisplayName { get; set; }
        public List<string> ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
