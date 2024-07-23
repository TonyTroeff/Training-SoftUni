namespace Database.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class DatabaseTests
{
    private const int MaxCapacity = 16;
    
    [TestCase(0)]
    [TestCase(MaxCapacity / 2)]
    [TestCase(MaxCapacity)]
    public void DatabaseShouldBeCreatedSuccessfully(int elementsCount)
    {
        var data = GenerateRandomIntegers(elementsCount);

        var database = new Database(data);
        Assert.AreEqual(elementsCount, database.Count);

        Assert.AreEqual(data, database.Fetch());
    }

    [TestCase(MaxCapacity + 1)]
    [TestCase(MaxCapacity * 2)]
    public void DatabaseShouldNotBeCreatedSuccessfullyIfTooManyElementsArePassed(int elementsCount)
    {
        var data = GenerateRandomIntegers(elementsCount);

        Assert.Throws<InvalidOperationException>(() => _ = new Database(data));
    }

    [Test]
    public void AddShouldWorkCorrectly()
    {
        var database = new Database();
        
        var addedNumbers = new List<int>();
        for (var i = 0; i < 16; i++)
        {
            var value = Random.Shared.Next();
            addedNumbers.Add(value);

            database.Add(value);

            Assert.AreEqual(i + 1, database.Count);
            Assert.AreEqual(addedNumbers, database.Fetch());
        }
    }

    [Test]
    public void AddShouldThrowIfDatabaseIsAlreadyFilled()
    {
        var data = GenerateRandomIntegers(MaxCapacity);
        var database = new Database(data);

        Assert.Throws<InvalidOperationException>(() => database.Add(Random.Shared.Next()));
    }

    [Test]
    public void RemoveShouldWorkCorrectly()
    {
        var data = GenerateRandomIntegers(MaxCapacity);

        var database = new Database(data);
        
        for (var i = 0; i < data.Length; i++)
        {
            database.Remove();

            var expectedCount = data.Length - (i + 1);
            Assert.AreEqual(expectedCount, database.Count);
            Assert.AreEqual(data.Take(expectedCount), database.Fetch());
        }
    }

    [Test]
    public void RemoveShouldThrownExceptionIfDatabaseIsEmpty()
    {
        var database = new Database();
        Assert.Throws<InvalidOperationException>(() => database.Remove());
    }

    private static int[] GenerateRandomIntegers(int length)
    {
        var result = new int[length];
        for (var i = 0; i < length; i++) result[i] = Random.Shared.Next();

        return result;
    }
}