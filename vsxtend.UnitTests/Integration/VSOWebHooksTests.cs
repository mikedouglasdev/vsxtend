using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSOWebHooks.Common;

namespace Vsxtend.UnitTests.Integration
{
    [TestClass]
    public class VSOWebHooksTests
    {
        [TestMethod]
        [Ignore]
        public void VSOWebHooksCommon_SendMailMessage_MessageSent()
        {
            var mailHelper = new MailHelper();

            mailHelper.SendMessage(
                "jeff@moonspace.net",
                "Test Message",
                "jeff@moonspace.net",
                "This is a test mail message",
                "smtp.office365.com",
                587,
                "***" // Make sure to set the password if this test is enabled
                );
        }
    }
}
