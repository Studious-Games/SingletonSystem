using Cronus.Singleton;
using NUnit.Framework;
using System;
using UnityEngine;

public class SingletonTest
{
    GameObject go;
    SampleSingleton ss;

    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        ss = go.AddComponent<SampleSingleton>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(go);
    }

    [Test]
    public void HasAttribute()
    {
        Attribute attribute = Attribute.GetCustomAttribute(typeof(SampleSingleton), typeof(SingletonAttribute));
        Assert.NotNull(attribute, "Does not have the SingletonAttribute on the class.");
    }

    [Test]
    public void AttributeNameNotEmpty()
    {
        SingletonAttribute _customAttribute = (SingletonAttribute)Attribute.GetCustomAttribute(typeof(SampleSingleton), typeof(SingletonAttribute));
        Assert.That("Singleton UnitTest", Is.EqualTo(_customAttribute.Name), "Attribute name mismatch.");
    }

    [Test]
    public void IsAutomaticTrue()
    {
        SingletonAttribute _customAttribute = (SingletonAttribute)Attribute.GetCustomAttribute(typeof(SampleSingleton), typeof(SingletonAttribute));
        Assert.IsTrue(_customAttribute.Automatic, "Autmatic was not set to true..");
    }

    [Test]
    public void IsPersistentTrue()
    {
        SingletonAttribute _customAttribute = (SingletonAttribute)Attribute.GetCustomAttribute(typeof(SampleSingleton), typeof(SingletonAttribute));
        Assert.IsTrue(_customAttribute.Persistent, "Persitent was not set to true..");
    }

    [Test]
    public void HasGotInstanceProperty()
    {
        var res = typeof(SampleSingleton).GetProperty("Instance") != null;
        Assert.IsTrue(res, "Missing Instance in class, please make sure it is added.");
    }
}
