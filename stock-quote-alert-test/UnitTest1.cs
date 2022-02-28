using System;
using NUnit.Framework;
using stock_quote_alert;

namespace stock_quote_alert_test;

public class Tests
{
    /*[SetUp]
    public void Setup() {
    }*/

    [Test]
    public void InvalidAsset() {
        int result = Program.Main(
            new string[3]{"ATIVO","33.05","34.12"}
        ).Result;
        Assert.AreEqual(-3,result);
    }

    [Test]
    public void LimitsFormattedPoorly() {
        int result = Program.Main(
            new string[3]{"PETR4","bad","format"}
        ).Result;
        Assert.AreEqual(-2,result);
    }

    [Test]
    [TestCase(null,null,null)]
    [TestCase("PETR4", null, null)]
    [TestCase("PETR4", "31.40", null)]
    public void MissingArguments(string a, string b, string c) {
        string[] args = null;
        if (a is null && b is null && c is null) {
            args = System.Array.Empty<string>();
        } else if (a is not null && b is null && c is null) {
            args = new string[1] {a};
        } else if (a is not null && b is not null && c is null) {
            args = new string[2] {a,b};
        }
        int result = Program.Main(
            args
        ).Result;
        Assert.AreEqual(-1,result);
    }

    [Test]
    public void AlertIsWorkingProperlyMaxLimit() {
        int result = Program.Main(
            new string[3]{"PETR4","30.21",Double.MaxValue.ToString()}
        ).Result;
        Assert.AreEqual(1,result);
    }

    [Test]
    public void AlertIsWorkingProperlyMinLimit() {
        int result = Program.Main(
            new string[3]{"PETR4", Double.MinValue.ToString(),"30.21"}
        ).Result;
        Assert.AreEqual(1,result);
    }

    [Test]
    public void AlertIsWorkingNormalValues() {
        int result = Program.Main(
            new string[3]{"PETR4","0.07","100.02"}
        ).Result;
        Assert.AreEqual(1,result);
    }
}