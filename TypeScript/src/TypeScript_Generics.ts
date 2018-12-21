/**
 * Identity Function
 */
function echo<T>(arg: T): T {
    console.log(arg);
    return arg;
}
let output1 : string = echo<string>('Sam');
let output2 : number = echo<number>(1);

/**
 * Generic Interface
 */
const myEcho: <T>(arg: T) => T = echo;
myEcho('123');
const myEcho2: {<T>(arg: T): T} = echo;
myEcho2('456');

//型別由Function決定
interface IEcho {
    <T>(arg: T): T;
}
//型別由Interface決定
interface IEcho2<T> {
    (arg: T): T;
}
const myEcho3: IEcho = echo;
myEcho3('789');
const myEcho4: IEcho2<string> = echo;
myEcho4('abc');

/**
 * Generic Class
 */
class GenericNumber<T> {
    zeroValue: T;
    add: (x: T, y: T) => T;
}
let myGenericNumber = new GenericNumber<number>();
myGenericNumber.zeroValue = 0;
myGenericNumber.add = function(x, y) { 
    console.log(x + y);
    return x + y; 
};
myGenericNumber.add(1, 2);

/**
 * Generic Constraint - Extends Interface
 * 執行完再回傳Interface
 */
interface Lengthwise {
    length: number;
}
function loggingIdentity<T extends Lengthwise>(arg: T): T {
    console.log(arg.length);
    return arg;
}
loggingIdentity({ length: 10, value: 2});

/**
 * Generic Constraint - Extends KeyOf
 */
function getProperty<T, K extends keyof T>(obj: T, key: K) {
    console.log(obj[key]);
    return obj[key];
}
let x = { a: 1, b: 2, c: 3, f: 4 };
getProperty(x, 'a');

/**
 * new(): T
 */
function create<T>(c: new() => T): T {
    return new c();
}

/**
 * 實際應用
 */
class Order {
    Ordernumber: number;
}
abstract class AbstractRepository<T> {
    protected model: T;

    find(id: number, columne: ['*']): T {
        return this.model;
    }
    all(columns = ['*']): T[] {
        return new Array<T>();
    }
    create(data: T): number {
        return 1;
     }
    update(data: T): boolean {
        return true;
     }
    delete(id: number): boolean {
        return true;
    }
}
class OrderRepository extends AbstractRepository<Order> {
    constructor(protected order: Order) {
        super();
    }
}