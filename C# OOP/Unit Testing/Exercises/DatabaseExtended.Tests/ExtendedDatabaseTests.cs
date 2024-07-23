namespace DatabaseExtended.Tests;

using System;
using System.Collections.Generic;
using ExtendedDatabase;
using NUnit.Framework;

[TestFixture]
public class ExtendedDatabaseTests
{
    private const int MaxCapacity = 16;

    [TestCase(0)]
    [TestCase(MaxCapacity / 2)]
    [TestCase(MaxCapacity)]
    public void DatabaseShouldBeCreatedSuccessfully(int elementsCount)
    {
        var data = GenerateRandomPeople(elementsCount);
        var database = new Database(data);
        
        Assert.AreEqual(elementsCount, database.Count);
        for (var i = 0; i < elementsCount; i++) AssertIsFound(database, data[i]);
    }

    [TestCase(MaxCapacity + 1)]
    [TestCase(MaxCapacity * 2)]
    public void DatabaseShouldNotBeCreatedSuccessfullyIfTooManyElementsArePassed(int elementsCount)
    {
        var data = GenerateRandomPeople(elementsCount);

        Assert.Throws<ArgumentException>(() => _ = new Database(data));
    }

    [Test]
    public void AddShouldWorkCorrectly()
    {
        var database = new Database();

        var addedPeople = new List<Person>();
        for (var i = 0; i < 16; i++)
        {
            var value = GenerateRandomPerson();
            addedPeople.Add(value);
            
            database.Add(value);
            
            Assert.AreEqual(i + 1, database.Count);
            foreach (var person in addedPeople) AssertIsFound(database, person);
        }
    }

    [Test]
    public void AddShouldThrowIfDatabaseIsAlreadyFilled()
    {
        var data = GenerateRandomPeople(MaxCapacity);
        var database = new Database(data);

        Assert.Throws<InvalidOperationException>(() => database.Add(GenerateRandomPerson()));
    }

    [Test]
    public void AddShouldThrowExceptionIfUsernameIsNotUnique()
    {
        var database = new Database();

        var originalPerson = GenerateRandomPerson();
        database.Add(originalPerson);

        var newPerson = new Person(originalPerson.Id + 1, originalPerson.UserName);
        Assert.Throws<InvalidOperationException>(() => database.Add(newPerson));
    }

    [Test]
    public void AddShouldThrowExceptionIfIdIsNotUnique()
    {
        var database = new Database();

        var originalPerson = GenerateRandomPerson();
        database.Add(originalPerson);

        var newPerson = new Person(originalPerson.Id, $"{originalPerson.UserName}-123");
        Assert.Throws<InvalidOperationException>(() => database.Add(newPerson));
    }

    [Test]
    public void RemoveShouldThrownExceptionIfDatabaseIsEmpty()
    {
        var database = new Database();
        Assert.Throws<InvalidOperationException>(() => database.Remove());
    }

    [Test]
    public void RemoveShouldWorkCorrectly()
    {
        var data = GenerateRandomPeople(MaxCapacity);
        var database = new Database(data);

        for (var i = 0; i < MaxCapacity; i++)
        {
            database.Remove();

            var activePeopleCount = MaxCapacity - (i + 1);
            Assert.AreEqual(activePeopleCount, database.Count);

            for (var j = 0; j < activePeopleCount; j++) AssertIsFound(database, data[j]);
        }
    }

    [Test]
    public void FindByUsernameShouldThrowExceptionIfInvalidUsernameIsProvided()
    {
        var database = new Database();
        Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
    }

    [Test]
    public void FindByUsernameShouldThrowExceptionIfNotFound()
    {
        var database = new Database();
        
        var person = GenerateRandomPerson();
        database.Add(person);
        
        Assert.Throws<InvalidOperationException>(() => database.FindByUsername($"{person}-123"));
    }

    [Test]
    public void FindByIdShouldThrowExceptionIfInvalidIdIsProvided()
    {
        var database = new Database();
        Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
    }
    
    [Test]
    public void FindByIdShouldThrowExceptionIfNotFound()
    {
        var database = new Database();
        
        var person = GenerateRandomPerson();
        database.Add(person);
        
        Assert.Throws<InvalidOperationException>(() => database.FindById(person.Id + 1));
    }

    private static void AssertIsFound(Database database, Person person)
    {
        var matchByUsername = database.FindByUsername(person.UserName);
        Assert.AreSame(person, matchByUsername);

        var matchById = database.FindById(person.Id);
        Assert.AreSame(person, matchById);
    }
    
    private static Person[] GenerateRandomPeople(int length)
    {
        var result = new Person[length];
        for (var i = 0; i < length; i++) result[i] = GenerateRandomPerson();

        return result;
    }

    private static Person GenerateRandomPerson() => new(Random.Shared.NextInt64(), GenerateRandomString());

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