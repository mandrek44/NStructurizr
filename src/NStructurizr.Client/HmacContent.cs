using System;
using System.Text;

namespace NStructurizr.Core.Client
{
    public class HmacContent
    {

        private String[] strings;

        public HmacContent(params String[] strings)
        {
            this.strings = strings;
        }

        public override String ToString()
        {
            StringBuilder buf = new StringBuilder();
            foreach (String s in strings)
            {
                buf.Append(s);
                buf.Append("\n");
            }

            return buf.ToString();
        }

    }
}