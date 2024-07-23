namespace CarManager.Tests;

using System;
using NUnit.Framework;

[TestFixture]
public class CarManagerTests
{
    private string _make;
    private string _model;
    private double _fuelConsumption;
    private double _fuelCapacity;

    [SetUp]
    public void SetUp()
    {
        this._make = GenerateRandomString();
        this._model = GenerateRandomString();
        this._fuelConsumption = Random.Shared.NextDouble();
        this._fuelCapacity = this._fuelConsumption * 5;
    }

    [Test]
    public void CarShouldBeCreatedSuccessfully()
    {
        var car = this.CreateCar();

        Assert.AreEqual(this._make, car.Make);
        Assert.AreEqual(this._model, car.Model);
        Assert.AreEqual(this._fuelConsumption, car.FuelConsumption);
        Assert.AreEqual(this._fuelCapacity, car.FuelCapacity);
        Assert.AreEqual(0, car.FuelAmount);
    }

    [TestCase(null)]
    [TestCase("")]
    public void CarShouldNotBeCreatedIfMakeIsInvalid(string make)
    {
        Assert.Throws<ArgumentException>(() => _ = new Car(make, this._model, this._fuelConsumption, this._fuelCapacity));
    }

    [TestCase(null)]
    [TestCase("")]
    public void CarShouldNotBeCreatedIfModelIsInvalid(string model)
    {
        Assert.Throws<ArgumentException>(() => _ = new Car(this._make, model, this._fuelConsumption, this._fuelCapacity));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void CarShouldNotBeCreatedIfFuelConsumptionIsInvalid(double fuelConsumption)
    {
        Assert.Throws<ArgumentException>(() => _ = new Car(this._make, this._model, fuelConsumption, this._fuelCapacity));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void CarShouldNotBeCreatedIfFuelCapacityIsInvalid(double fuelCapacity)
    {
        Assert.Throws<ArgumentException>(() => _ = new Car(this._make, this._model, this._fuelConsumption, fuelCapacity));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void RefuelShouldThrowIfInvalidArgumentIsPassed(double refuelAmount)
    {
        var car = this.CreateCar();
        Assert.Throws<ArgumentException>(() => car.Refuel(refuelAmount));
    }

    [TestCase(0.25)]
    [TestCase(0.5)]
    [TestCase(0.75)]
    [TestCase(1)]
    public void RefuelShouldWorkCorrectly(double fillPercentage)
    {
        var car = this.CreateCar();
        var refuelAmount = car.FuelCapacity * fillPercentage;

        car.Refuel(refuelAmount);

        Assert.AreEqual(refuelAmount, car.FuelAmount);
    }

    [Test]
    public void RefuelShouldBeLimitedToTheFuelCapacity()
    {
        var car = this.CreateCar();
        var refuelAmount = car.FuelCapacity * 2;

        car.Refuel(refuelAmount);

        Assert.AreEqual(car.FuelCapacity, car.FuelAmount);
    }

    [Test]
    public void DriveShouldWorkCorrectly()
    {
        var car = this.CreateCar();

        car.Refuel(car.FuelCapacity);
        car.Drive(100);

        Assert.AreEqual(car.FuelCapacity - car.FuelConsumption, car.FuelAmount);
    }

    [Test]
    public void DriveShouldThrowIfFuelIsNotEnough()
    {
        var car = this.CreateCar();

        car.Refuel(car.FuelCapacity);

        Assert.Throws<InvalidOperationException>(() => car.Drive(10000));
    }

    private Car CreateCar() => new(this._make, this._model, this._fuelConsumption, this._fuelCapacity);

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