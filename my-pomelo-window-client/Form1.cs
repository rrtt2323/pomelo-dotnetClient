using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pomelo.DotNetClient;
using SimpleJson;

namespace my_pomelo_window_client
{
    public partial class Form1 : Form
    {
        private PomeloClient _pomeloClient = null;

        private string _logStr = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label3.Text = "开始连接服务器";

            string host = this.textBox1.Text.Trim();
            int port = int.Parse(this.textBox2.Text.Trim());

            _pomeloClient = new PomeloClient(host, port);
            _pomeloClient.OnConnect += delegate()
            {
                Console.WriteLine("OnConnect");
            };
            _pomeloClient.OnDisconnect += delegate()
            {
                Console.WriteLine("OnDisconnect");
            };
            _pomeloClient.OnException += delegate(string error)
            {
                Console.WriteLine("OnException = " + error);
            };

            _pomeloClient.on("testOn", (data) =>
            {
                Console.WriteLine("On = " + data.ToString());
            });

            _pomeloClient.connect((data) =>
            {
                this._logStr = "服务器连接成功，返回json：" + data.ToString() + "\n";
                Action del = () =>
                {
                    this.label3.Text = this._logStr;
                };
                this.label3.Invoke(del, null);

                JsonObject msg = new JsonObject();
                msg["uid"] = "wy";
                _pomeloClient.request("connector.entryHandler.entry", msg, (ret) =>
                {
                    Console.WriteLine("connector.entryHandler.entry = " + ret.ToString());
                });

                Console.WriteLine("Connected");
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label3.Text = "断开连接";

            _pomeloClient.disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.label3.Text = "request";

            string str = "中文";

            JsonObject msg = new JsonObject();
            msg["uid"] = "request2";
            msg["binnary"] = Encoding.UTF8.GetBytes(str);

            _pomeloClient.request("test.testHandler.testRequest1", msg, (data) =>
            {
                Console.WriteLine("test.testHandler.testRequest = " + data.ToString());
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.label3.Text = "notify";

            JsonObject msg = new JsonObject();
            msg["uid"] = "notify";

            _pomeloClient.notify("test.testHandler.testNotify", msg);
        }
    }
}
