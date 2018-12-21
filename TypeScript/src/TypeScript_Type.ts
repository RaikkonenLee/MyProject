/**
 * 型別
 */
//boolean型別
let isDone: boolean = false;

//number型別，TS跟JS一樣只有浮點數
let decimal: number = 6;

//string型別
let firstName: string = "Sam";
let lastName: string = 'Xiao';
let sentance: string = `Hello, my name is ${firstName} ${lastName}`;

//陣列，第二種寫法是用泛型的寫法
let list1: number[] = [1, 2, 3];
let list2: Array<number> = [1, 2, 3];

//tuple型別
let tuple: [string, number];
tuple = ['hello', 10];

//enum型別
enum Gender {Male, Female};
enum Color {Red = 'a', Green = 'b', Blue = 'c'};
let boy: Gender = Gender.Male;
let green: Color = Color.Green;

//any
let notSure: any = 4;
notSure = 'maybe a string instead';
notSure = false;

//function 型別，沒有回傳就用void
function warnUser(): void {
    console.log('This is my warning message');
}

let message: any = warnUser();

//null&undefined型別
function sum(a: number, b: number | null | undefined) : number {
    return b === null || b === undefined ? a : a + b;
}
let sumNumber: number = sum(5, null);

function error(message: string): never {
    throw new Error(message);
}

//never型別，專門處理Exception和無窮迴圈(有點不知道要用在哪個地方)
function infiniteLoop(): never {
    while (true) {
        
    }
}

//轉型，建議使用第一種
let someValue: any = 'this is s string';
let strLength1: number = (<string>someValue).length;
let strLength2: number = (someValue as string).length;

//Array Destructing 解構陣列
let input = [1, 2];
let [first, second]: number[] = input;

//Object Destructing 解構物件
type C = { a: string, b: number };
function f({a, b}: C): void {
    console.log(a);
    console.log(b);
}
let o = {
    a: 'foo',
    b: 12
};
f(o);

//Spread 其餘參數
let firstSpread = [1, 2];
let secondSpread = [3, 4];
let bothPlus = [0, ...firstSpread, ...secondSpread, 5];
//
console.log(`${bothPlus}`);

/**
 * TypeScript 的 Function 
 */

 
