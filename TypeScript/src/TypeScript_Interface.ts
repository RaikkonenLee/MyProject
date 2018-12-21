/**
 *一般使用 Object的方式傳入
 */
interface LabelledValue {
    label: string;
    color?: string; //Optional Property
    readonly width: number;
}
function printLabel(labelledObj: LabelledValue ){
    console.log(labelledObj.label);
    console.log(labelledObj.width);
    //
    //Optional Property
    if (labelledObj.color !== undefined) {
        console.log(labelledObj.color)
    }
}
let myObj = { size: 10, label: 'Size 101 Object', width: 99, color: 'Red' };
printLabel(myObj);
//Type Assertion的方式傳入
printLabel(<LabelledValue>{ size: 10, label: 'Size 102 Object,', width: 999  });

/**
 * String Index Signature
 */
interface LabelledValue2 {
    label: string;
    [propName: string]: any;
}
function printLabel2(labelledObj: LabelledValue2) {
    console.log(labelledObj.label);
    console.log(labelledObj['size']);
}
printLabel2({ size: 10, label: 'Size 10 Object' });

/**
 * Index Interface
 */
interface CloudDictionary {
    [index: number]: string;
    [index: string]: number | string;
}
let clouds: CloudDictionary = {};
clouds[0] = 'aws';
clouds[1] = 'azure';
clouds[2] = 'gcp';
clouds['aws'] = 0;
clouds['azure'] = 1;
clouds['gcp'] = 1;
console.log(clouds[0]);
console.log(clouds[1]);
console.log(clouds[2]);
console.log(clouds['aws']);
console.log(clouds['azure']);
console.log(clouds['gcp']);


/**
 * Class Interface
 */
interface ClockInterface {
    currentTime: Date;
    setTime(d: Date): void;
}
class Clock implements ClockInterface {
    currentTime: Date;
    constructor(h: number, m: number) { }
    setTime(d: Date) {
        this.currentTime = d;
    }
}

/**
 * Constructor Interface
 */
interface ClockInterface2 {
    tick(): void;
}
interface ClockConstructor {
    new (hour: number, minute: number): ClockInterface2;
}
class Clock2 implements ClockInterface2 {
    currentTime: Date;
    constructor(h: number, m: number) { 
        console.log(`${h}, ${m}`);
    }
    tick(){
        console.log('beep beep');
    }
}
function createClock2(ctor: ClockConstructor, hour: number, minute: number): ClockInterface2 {
    return new ctor(hour, minute);
}
let clock = createClock2(Clock2, 12, 17);
clock.tick();

/**
 * Function Interface
 */
interface ILogistics {
    (weight: number): number;
}
class ShippingService {
    calculateFee(weight: number, logistics: ILogistics): number {
        return logistics(weight);
    }
}
const shippingService = new ShippingService();
const fee = shippingService.calculateFee(10, weight => 100 * weight + 10);
console.log(fee);

/**
 * Hybrid Interface
 */
interface Counter {
    (start: number): string;
    interval: number;
    reset(): void;
}
function getCounter(): Counter {
    let counter = <Counter>function(start: number) { console.log(start); };
    counter.interval = 123;
    counter.reset = function() {};
    return counter;
}
let c = getCounter();
c(10);
c.reset();
c.interval = 5.0;

/**
 * Inheritance
 */
interface Shape {
    color: string;
}
interface PenStroke {
    perWidth: number;
}
interface Square extends Shape, PenStroke {
    sideLength: number;
}
let square = <Square>{};
square.color = 'blue';
square.sideLength = 10;
square.perWidth = 5.0;

/**
 * Interface Extending Class
 */
class Control {
    private state: any;
}
interface SelectableControl extends Control {
    select(): void;
}
class Button extends Control implements SelectableControl {
    select() {}
}
class TextBox extends Control {

}

interface CloudSDK {
    putObject(container: string, blob: string, file: string): void;
    getObject(container: string, blob: string): void;
    deleteObject(container: string, blob: string): void;
}
class AWSSDK implements CloudSDK {
    putObject(container: string, blob: string, file: string) {}
    getObject(container: string, blob: string) {}
    deleteObject(container: string, blob: string) {}
}
class AzureSDK implements CloudSDK {
    putObject(container: string, blob: string, file: string) {}
    getObject(container: string, blob: string) {}
    deleteObject(container: string, blob: string) {}
}



