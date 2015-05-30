using System;
using NStructurizr.Core.Client;
using NUnit.Framework;

namespace NStructurizr.Test
{
    public class Class1
    {
        [NUnit.Framework.Test]
        public void DoTest()
        {
            Console.WriteLine(new HashBasedMessageAuthenticationCode("key").generate("The quick brown fox jumps over the lazy dog"));

            Assert.AreEqual("String1\nString2\nString3\n", new HmacContent("String1", "String2", "String3").ToString());
        }
    }
}