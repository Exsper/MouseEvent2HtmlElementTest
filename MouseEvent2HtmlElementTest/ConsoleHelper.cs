using System;
using System.IO;
using System.Text;
using System.Threading;

namespace MouseEvent2HtmlElementTest
{
    public class ConsoleHelper : TextWriter
    {
        private System.Windows.Forms.TextBox TextBox1 { set; get; }

        public ConsoleHelper(System.Windows.Forms.TextBox textBox)
        {
            this.TextBox1 = textBox;
            Console.SetOut(this);
        }
        public override void Write(string value)
        {
            if (TextBox1.IsHandleCreated)
                TextBox1.BeginInvoke(new ThreadStart(() => TextBox1.AppendText(value)));
        }
        public override void WriteLine(string value)
        {
            if (TextBox1.IsHandleCreated)
                TextBox1.BeginInvoke(new ThreadStart(() => TextBox1.AppendText(value + "\r\n")));
        }
        public override Encoding Encoding//重写write必须重写编码类型
        {
            get { return Encoding.UTF8; }
        }
    }
}
