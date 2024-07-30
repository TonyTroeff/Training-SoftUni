namespace RecourceCloud.Tests;

using System;
using System.Linq;
using NUnit.Framework;

public class DepartmentCloudTests
{
    private DepartmentCloud _departmentCloud;

    [SetUp]
    public void Setup()
    {
        this._departmentCloud = new DepartmentCloud();
    }

    [Test]
    public void DepartmentCloudShouldBeInstantiatedSuccessfully()
    {
        Assert.Multiple(() =>
        {
            Assert.That(this._departmentCloud.Resources, Is.Empty);
            Assert.That(this._departmentCloud.Tasks, Is.Empty);
        });
    }

    [TestCase(0), TestCase(1), TestCase(2), TestCase(4), TestCase(5)]
    public void LogTaskShouldThrowIfInvalidNumberOfArgumentsAreProvided(int argsCount)
    {
        var args = new string[argsCount];
        for (var i = 0; i < args.Length; i++) args[i] = GenerateRandomString();
        
        Assert.Throws<ArgumentException>(() => this._departmentCloud.LogTask(args));
    }

    [TestCase(true, true, true)]
    [TestCase(true, true, false), TestCase(true, false, true), TestCase(false, true, true)]
    public void LogTaskShouldThrowIfNullArgumentsAreProvided(bool firstIsNull, bool secondIsNull, bool thirdIsNull)
    {
        var args = new string[3];
        if (!firstIsNull) args[0] = GenerateRandomString();
        if (!secondIsNull) args[1] = GenerateRandomString();
        if (!thirdIsNull) args[2] = GenerateRandomString();

        Assert.Throws<ArgumentException>(() => this._departmentCloud.LogTask(args));
    }

    [Test]
    public void LogTaskShouldWorkCorrectly()
    {
        var priority = Random.Shared.Next(1, 5);
        var label = GenerateRandomString();
        var resourceName = GenerateRandomString();
        var args = new[] { priority.ToString(), label, resourceName };

        Assert.Multiple(() =>
        {
            Assert.That(this._departmentCloud.LogTask(args), Is.EqualTo("Task logged successfully."));
            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));

            var task = this._departmentCloud.Tasks.First();
            Assert.That(task.Priority, Is.EqualTo(priority));
            Assert.That(task.Label, Is.EqualTo(label));
            Assert.That(task.ResourceName, Is.EqualTo(resourceName));
        });
    }

    [Test]
    public void LogTaskShouldFailIfResourceNamesAreDuplicate()
    {
        var resourceName = GenerateRandomString();
        var firstArgs = new[] { Random.Shared.Next(1, 5).ToString(), GenerateRandomString(), resourceName };
        this._departmentCloud.LogTask(firstArgs);

        var secondArgs = new[] { Random.Shared.Next(1, 5).ToString(), GenerateRandomString(), resourceName };
        Assert.Multiple(() =>
        {
            Assert.That(this._departmentCloud.LogTask(secondArgs), Is.EqualTo($"{resourceName} is already logged."));
            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void CreateResourceShouldFailIfThereAreNoTasks()
    {
        Assert.That(this._departmentCloud.CreateResource(), Is.False);
    }

    [Test]
    public void CreateResourceShouldWorkCorrectly()
    {
        var priority = Random.Shared.Next(2, 5);
        var regularTaskArgs = new[] { priority.ToString(), GenerateRandomString(), GenerateRandomString() };
        this._departmentCloud.LogTask(regularTaskArgs);
        
        var moreImportantTaskArgs = new[] { (priority - 1).ToString(), GenerateRandomString(), GenerateRandomString() };
        this._departmentCloud.LogTask(moreImportantTaskArgs);

        var createResourceResult = this._departmentCloud.CreateResource();

        Assert.Multiple(() =>
        {
            Assert.That(createResourceResult, Is.True);
            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));
            Assert.That(this._departmentCloud.Resources, Has.Count.EqualTo(1));

            var task = this._departmentCloud.Tasks.First();
            var resource = this._departmentCloud.Resources.First();

            Assert.That(task.ResourceName, Is.Not.EqualTo(resource.Name));
            Assert.That(resource.Name, Is.EqualTo(moreImportantTaskArgs[2]));
            Assert.That(resource.ResourceType, Is.EqualTo(moreImportantTaskArgs[1]));
            Assert.That(resource.IsTested, Is.False);
        });
    }

    [Test]
    public void TestResourceShouldFailIfResourceDoesNotExist()
    {
        Assert.That(this._departmentCloud.TestResource(GenerateRandomString()), Is.Null);
    }

    [Test]
    public void TestResourceShouldWorkCorrectly()
    {
        var resourceName = GenerateRandomString();
        var regularTaskArgs = new[] { Random.Shared.Next(1, 5).ToString(), GenerateRandomString(), resourceName };
        this._departmentCloud.LogTask(regularTaskArgs);
        
        this._departmentCloud.CreateResource();

        var testResourceResult = this._departmentCloud.TestResource(resourceName);
        Assert.Multiple(() =>
        {
            Assert.That(this._departmentCloud.Resources, Has.Count.EqualTo(1));
            var resource = this._departmentCloud.Resources.First();
            
            Assert.That(testResourceResult, Is.SameAs(resource));
            Assert.That(resource.IsTested, Is.True);
        });
    }

    private static string GenerateRandomString()
    {
        var randomTextLength = Random.Shared.Next(minValue: 5, maxValue: 50);
        return GenerateRandomString(randomTextLength);
    }

    private static string GenerateRandomString(int length)
    {
        var symbols = new char[length];
        for (var i = 0; i < length; i++)
        {
            var randomLetterIndex = Random.Shared.Next(maxValue: 26);
            symbols[i] = (char)('a' + randomLetterIndex);
        }

        return new string(symbols);
    }
}