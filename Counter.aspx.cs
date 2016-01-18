
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PubNubCounter
{
    public partial class Counter : System.Web.UI.Page
    {
        const string SUBSCRIBE_KEY = "<subkeygoeshere>";
        const string UUID = "<UUIDGoesHere>";
        long TotalMessageCounter = 0;
        long TotalParentSectionMessageCounter = 0;
        long TotalParent2ParentMessageCounter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void ParentsSectionChat_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var tempMessageCount = 0;
            var actualMessageCount = 0;
            //Put ur db logic to get channel name'
            //foreach (var item in collection)
            //{
            var channel = "fcf173dc-5d2c-4e1a-80a5-b92068d91019";

            tempMessageCount = 0;
            actualMessageCount = 0; //Your total message count
            string content = GetHistoryContent(channel);

            if (content.Length > 47)
            {
                var messageList = json_serializer.DeserializeObject(content.Substring(1, content.Length - 38));
                tempMessageCount = ((object[])messageList).Count();
                actualMessageCount = tempMessageCount;
                while (tempMessageCount == 100)
                {
                    var startCounter = content.Substring(content.Length - 36, 17);
                    var morecontent = GetHistoryContent(channel, startCounter);
                    if (content.Length > 47)
                    {
                        var moreMessageList = json_serializer.DeserializeObject(content.Substring(1, content.Length - 38));
                        tempMessageCount = ((object[])moreMessageList).Count();
                        actualMessageCount = actualMessageCount + tempMessageCount;
                    }
                }
            }
            //} 
        }

        private static string GetHistoryContent(string channel, string start = "")
        {
            var urlstring = "http://pubsub.pubnub.com/v2/history/sub-key/"+ SUBSCRIBE_KEY +"/ channel/" + channel;
            urlstring = urlstring + "?count=100&uuid="+ UUID +"&pnsdk=PubNub-CSharp-.NET/3.7";
            if (start != "")
                urlstring = urlstring + "&start=" + start;
            var MyClient = WebRequest.Create(urlstring) as HttpWebRequest;
            MyClient.Method = WebRequestMethods.Http.Get;
            var content = "";
            using (HttpWebResponse response = (HttpWebResponse)MyClient.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            return content;
        }
    }
}