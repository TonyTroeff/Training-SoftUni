using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureOpsSystem.Tests;

[TestFixture]
public class SecureHubTests
{
    [TestCase(0), TestCase(-1)]
    public void SecureHubShouldNotBeInstantiatedWithInvalidCapacity(int invalidCapacity)
    {
        Assert.That(
            () => new SecureHub(invalidCapacity),
            Throws.ArgumentException
        );
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void SecureHubShouldBeInstantiatedCorrectly(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);
        Assert.That(secureHub.Capacity, Is.EqualTo(capacity));
        Assert.That(secureHub.Tools, Is.Not.Null);
        Assert.That(secureHub.Tools, Is.Empty);
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void AddShouldWorkCorrectly(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);

        SecurityTool[] controlledTools = GenerateTools(capacity).ToArray();
        for (int i = 0; i < capacity; i++)
        {
            string result = secureHub.AddTool(controlledTools[i]);
            Assert.That(result, Is.EqualTo($"Security Tool {controlledTools[i].Name} added successfully."));
        }

        Assert.That(secureHub.Tools, Is.EqualTo(controlledTools));
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void AddShouldReturnErrorMessageIfCapacityIsExceeded(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);

        foreach (SecurityTool currentTool in GenerateTools(capacity))
            secureHub.AddTool(currentTool);

        SecurityTool lastTool = new SecurityTool("last", "_", -1);
        string result = secureHub.AddTool(lastTool);
        Assert.That(result, Is.EqualTo("Secure Hub is at full capacity."));
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void AddShouldReturnErrorMessageIfNameIsNotUnique(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);

        foreach (SecurityTool currentTool in GenerateTools(capacity))
        {
            secureHub.AddTool(currentTool);

            string result = secureHub.AddTool(currentTool);
            Assert.That(result, Is.EqualTo($"Security Tool {currentTool.Name} already exists in the hub."));
        }
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void RemoveToolShouldWorkCorrectly(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);

        SecurityTool[] controlledTools = GenerateTools(capacity).ToArray();
        for (int i = 0; i < capacity; i++)
            secureHub.AddTool(controlledTools[i]);

        for (int i = 0; i < capacity; i++)
        {
            bool result = secureHub.RemoveTool(controlledTools[i]);
            Assert.That(result, Is.True);
        }

        Assert.That(secureHub.Tools, Is.Empty);
    }

    [TestCase(0), TestCase(1), TestCase(5), TestCase(10)]
    public void RemoveToolShouldReturnFalseIfNonExistent(int capacity)
    {
        SecureHub secureHub = new SecureHub(Math.Max(1, capacity));

        foreach (SecurityTool currentTool in GenerateTools(capacity))
            secureHub.AddTool(currentTool);

        SecurityTool nonExistentTool = new SecurityTool("non_existent", "_", -1);
        bool result = secureHub.RemoveTool(nonExistentTool);
        Assert.That(result, Is.False);
    }

    [TestCase(1), TestCase(5), TestCase(10)]
    public void DeployToolShouldWorkCorrectly(int capacity)
    {
        SecureHub secureHub = new SecureHub(capacity);

        SecurityTool[] controlledTools = GenerateTools(capacity).ToArray();
        for (int i = 0; i < capacity; i++)
            secureHub.AddTool(controlledTools[i]);

        for (int i = 0; i < capacity; i++)
        {
            SecurityTool? result = secureHub.DeployTool(controlledTools[i].Name);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(controlledTools[i]));

            // Is.SameAs uses object.ReferenceEquals(a, b)
            // Is.EqualTo uses object.Equals(a, b)
        }

        Assert.That(secureHub.Tools, Is.Empty);
    }

    [TestCase(0), TestCase(1), TestCase(5), TestCase(10)]
    public void DeployToolShouldReturnNullIfNonExistent(int capacity)
    {
        SecureHub secureHub = new SecureHub(Math.Max(1, capacity));

        foreach (SecurityTool currentTool in GenerateTools(capacity))
            secureHub.AddTool(currentTool);

        SecurityTool? result = secureHub.DeployTool("non_existent");
        Assert.That(result, Is.Null);
    }

    [Test]
    public void SystemReportShouldBeGeneratedSuccessfullyForEmptyHub()
    {
        SecureHub secureHub = new SecureHub(5);

        StringBuilder expectedBuilder = new StringBuilder();
        expectedBuilder.AppendLine("Secure Hub Report:");
        expectedBuilder.Append("Available Tools: 0");
        string expected = expectedBuilder.ToString();

        string report = secureHub.SystemReport();
        Assert.That(report, Is.EqualTo(expected));
    }

    [Test]
    public void SystemReportShouldBeGeneratedCorrectly()
    {
        SecureHub secureHub = new SecureHub(5);

        SecurityTool tool1 = new SecurityTool("tool_1", "cat_1", 3.14);
        SecurityTool tool2 = new SecurityTool("tool_2", "cat_2", 1.652);
        SecurityTool tool3 = new SecurityTool("tool_3", "cat_1", 7.9);

        secureHub.AddTool(tool1);
        secureHub.AddTool(tool2);
        secureHub.AddTool(tool3);

        StringBuilder expectedBuilder = new StringBuilder();
        expectedBuilder.AppendLine("Secure Hub Report:");
        expectedBuilder.AppendLine("Available Tools: 3");
        expectedBuilder.AppendLine("Name: tool_3, Category: cat_1, Effectiveness: 7.90");
        expectedBuilder.AppendLine("Name: tool_1, Category: cat_1, Effectiveness: 3.14");
        expectedBuilder.Append("Name: tool_2, Category: cat_2, Effectiveness: 1.65");
        string expected = expectedBuilder.ToString();

        string report = secureHub.SystemReport();
        Assert.That(report, Is.EqualTo(expected));
    }

    private static IEnumerable<SecurityTool> GenerateTools(int n)
    {
        for (int i = 0; i < n; i++)
            yield return new SecurityTool($"Tool #{i + 1}", "_", -1);
    }
}
