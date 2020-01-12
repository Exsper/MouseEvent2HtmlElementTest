using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MouseEvent2HtmlElementTest
{

    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            new ConsoleHelper(LogTextBox);
        }

        private void StartServer()
        {
            FleckLog.Level = LogLevel.Info;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://127.0.0.1:1919");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("开启服务端");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("关闭服务端");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    //Console.WriteLine(message);
                    //allSockets.ToList().ForEach(s => s.Send("服务端收到消息：" + message));
                    JsonReader reader = new JsonTextReader(new StringReader(message));
                    JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                    int locX = (int)jsonObject["x"];
                    int locY = (int)jsonObject["y"];
                    Console.WriteLine("移动至：(" + locX + ", " + locY + ")");
                    MouseEvents.MouseMoveTo(locX, locY, 50, 20);
                    MouseEvents.MouseLeftClick();
                };
            });

            /*
            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }
            */
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(new ThreadStart(StartServer));
            thread1.Start();
        }
    }
}
