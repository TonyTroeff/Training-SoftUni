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

    [TestCase(0), TestCase(1), TestCase(2)]
    public void LogTaskShouldThrowIfInvalidArgumentIsProvided(int index)
    {
        var args = new string[3];
        for (var i = 0; i < args.Length; i++)
            if (i != index) args[i] = GenerateRandomString();

        Assert.Throws<ArgumentException>(
            () => this._departmentCloud.LogTask(args));
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
        var firstTask = this._departmentCloud.Tasks.First();

        var secondArgs = new[] { Random.Shared.Next(1, 5).ToString(), GenerateRandomString(), resourceName };
        Assert.Multiple(() =>
        {
            Assert.That(this._departmentCloud.LogTask(secondArgs), Is.EqualTo($"{resourceName} is already logged."));
            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));
            Assert.That(this._departmentCloud.Tasks.First(), Is.SameAs(firstTask));
        });
    }

    [Test]
    public void CreateResourceShouldFailIfThereAreNoTasks()
    {
        var createResourceResult = this._departmentCloud.CreateResource();

        Assert.Multiple(() =>
        {
            Assert.That(createResourceResult, Is.False);
            Assert.That(this._departmentCloud.Resources, Is.Empty);
        });
    }

    [Test]
    public void CreateResourceShouldWorkCorrectly()
    {
        var priority = Random.Shared.Next(2, 5);
        var regularTaskArgs = new[] { priority.ToString(), GenerateRandomString(), GenerateRandomString() };
        
        this._departmentCloud.LogTask(regularTaskArgs);
        var firstTask = this._departmentCloud.Tasks.First();
        
        var moreImportantTaskArgs = new[] { (priority - 1).ToString(), GenerateRandomString(), GenerateRandomString() };
        this._departmentCloud.LogTask(moreImportantTaskArgs);

        var createResourceResult = this._departmentCloud.CreateResource();

        Assert.Multiple(() =>
        {
            Assert.That(createResourceResult, Is.True);
            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));
            Assert.That(this._departmentCloud.Resources, Has.Count.EqualTo(1));

            var resource = this._departmentCloud.Resources.First();
            Assert.That(resource.Name, Is.EqualTo(moreImportantTaskArgs[2]));
            Assert.That(resource.ResourceType, Is.EqualTo(moreImportantTaskArgs[1]));
            Assert.That(resource.IsTested, Is.False);

            Assert.That(this._departmentCloud.Tasks, Has.Count.EqualTo(1));
            Assert.That(this._departmentCloud.Tasks.First(), Is.SameAs(firstTask));
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
        
        var resource = this._departmentCloud.Resources.First();

        var testResourceResult = this._departmentCloud.TestResource(resourceName);
        Assert.Multiple(() =>
        {
            Assert.That(testResourceResult, Is.Not.Null);
            Assert.That(testResourceResult, Is.SameAs(resource));
            Assert.That(resource.IsTested, Is.True);

            Assert.That(this._departmentCloud.Resources, Has.Count.EqualTo(1));
            Assert.That(this._departmentCloud.Resources.First(), Is.SameAs(resource));
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