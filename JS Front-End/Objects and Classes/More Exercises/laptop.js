class Laptop {
    constructor(info, quality) {
        this.info = info;
        this.quality = quality;
        this.isOn = false;
    }

    turnOn() {
        this.isOn = true;
        this.quality--;
    }

    turnOff() {
        this.isOn = false;
        this.quality--;
    }

    showInfo() {
        return JSON.stringify(this.info);
    }

    get price() {
        return 800 - this.info.age * 2 + this.quality * 0.5;
    }
}

// Test 1:
// let info = { producer: "Dell", age: 2, brand: "XPS" };
// let laptop = new Laptop(info, 10);
// laptop.turnOn();
// console.log(laptop.showInfo());
// laptop.turnOff();
// console.log(laptop.quality);
// laptop.turnOn();
// console.log(laptop.isOn);
// console.log(laptop.price);

// Test 2:
// let info = { producer: "Lenovo", age: 1, brand: "Legion" };
// let laptop = new Laptop(info, 10);
// laptop.turnOn();
// console.log(laptop.showInfo());
// laptop.turnOff();
// laptop.turnOn();
// laptop.turnOff();
// console.log(laptop.isOn);
