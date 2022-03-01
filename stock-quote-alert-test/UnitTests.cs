using System;
using System.Text;
using System.Threading;
using AE.Net.Mail;
using NUnit.Framework;
using stock_quote_alert;

namespace stock_quote_alert_test; 

public class UnitTests {
    
    [SetUp]
    public void Setup() {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [Test]
    public void SendSellOrder() {
        ImapClient ic = new ImapClient(
            "imap.ethereal.email",
            "kyra.jacobi28@ethereal.email",
            "ndKHWa9fpa3t9QhmhH",
            AuthMethods.Login,
            993,
            true
        );
        ic.SelectMailbox("INBOX");
        int messageCount = ic.GetMessageCount();
        EmailNotificator notificator = new EmailNotificator();
        notificator.SendSellOrder("ATIVO RANDOMICO", "3.14");
        Thread.Sleep(5000);
        Assert.AreEqual(
            "Hora de Vender ATIVO RANDOMICO!!!",
            ic.GetMessage(ic.GetMessageCount() - 1).Subject
        );
        Assert.Greater(ic.GetMessageCount(), messageCount);
    }

    [Test]
    public void SendBuyOrder() {
        ImapClient ic = new ImapClient(
            "imap.ethereal.email",
            "kyra.jacobi28@ethereal.email",
            "ndKHWa9fpa3t9QhmhH",
            AuthMethods.Login,
            993,
            true
        );
        ic.SelectMailbox("INBOX");
        int messageCount = ic.GetMessageCount();
        EmailNotificator notificator = new EmailNotificator();
        notificator.SendBuyOrder("ATIVO RANDOMICO", "3.14");
        Thread.Sleep(5000);
        Assert.AreEqual(
            "Hora de Comprar ATIVO RANDOMICO!!!",
            ic.GetMessage(ic.GetMessageCount() - 1).Subject
        );
        Assert.Greater(ic.GetMessageCount(), messageCount);
    }

    [Test]
    [TestCase("PETR4","5.0","10.0", 1)]
    [TestCase("PETR4","50000.0","MAX", 2)]
    [TestCase("PETR4","MIN","5000.0", 1)]
    [TestCase("ATIVOOOOOOOOOO","5.0","10.0", 0)]
    public void TestConsultant(string asset, string ceil, string floor, int expectedResult) {
        Consultant consultant = null;
        if (floor.Equals("MAX")) {
            consultant = new Consultant(asset, ceil, Double.MaxValue.ToString(), new StaticClock());
        }
        else if (ceil.Equals("MIN")) {
            consultant = new Consultant(asset, Double.MinValue.ToString(), floor, new StaticClock());
        }
        else {
            consultant = new Consultant(asset, ceil, floor, new StaticClock());
        }
        Assert.AreEqual(
            (AssetOperation) expectedResult,
            consultant.Consult().Result
        );
    }
}