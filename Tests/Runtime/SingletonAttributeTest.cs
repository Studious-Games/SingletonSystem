using System;
using System.Collections.Generic;
using Studious.Singleton;
using NUnit.Framework;

public class SingletonAttributeTest
{
    private IList<AttributeUsageAttribute> attributes;

    [SetUp]
    public void Setup()
    {
        attributes = (IList<AttributeUsageAttribute>)typeof(SingletonAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
    }

    [Test]
    public void CheckNoMultiple()
    {
        var attribute = attributes[0];
        Assert.IsFalse(attribute.AllowMultiple, "Allow multiple is not allowed.");
    }

    [Test]
    public void InheritedIsTrue()
    {
        var attribute = attributes[0];
        Assert.IsTrue(attribute.Inherited, "Attribute must be inherited.");
    }

    [Test]
    public void AttributeIsClass()
    {
        var attribute = attributes[0];
        Assert.AreEqual(AttributeTargets.Class, attribute.ValidOn, "Attribute Target can only be used on a Class.");
    }

}
