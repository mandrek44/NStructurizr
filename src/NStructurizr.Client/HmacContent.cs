using System;
using System.Text;

namespace NStructurizr.Client
{
    public class HmacContent
    {
        private string[] strings;

        public HmacContent(params string[] strings)
        {
            this.strings = strings;
        }

        public override String ToString()
        {
            var buffer = new StringBuilder();
            foreach (string s in strings)
            {
                buffer.Append(s);
                buffer.Append("\n");
            }

            return buffer.ToString();
        }
    }
}