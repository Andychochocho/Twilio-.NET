using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Twilio.Models
{
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }

        public static List<Message> GetMessages()
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            var request = new RestRequest("Accounts/{{Account SID}}/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("{{Account SID}}", "{{Auth Token}}");
            var response = client.Execute(request);
            JObject jsonResponse = (JObject)JsonConvert.DeserializeObject(response.Content);
            var messageList = JsonConvert.DeserializeObject<List<Message>>(jsonResponse["messages"].ToString());
            return messageList;
        }
        public void Send()
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");
            var request = new RestRequest("Accounts/{{Account SID}}/Messages", Method.POST);
            request.AddParameter("To", To);
            request.AddParameter("From", From);
            request.AddParameter("Body", Body);
            request.AddParameter("Status", Status);
            client.Authenticator = new HttpBasicAuthenticator("{{Account SID}}", "{{Auth Token}}");
            client.Execute(request);
        }
    }
}
