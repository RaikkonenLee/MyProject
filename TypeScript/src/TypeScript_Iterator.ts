// interface Iterator<T>{
//     next(value?: any): IteratorResult<T>;
//     return?(value?: any): IteratorResult<T>;
//     throw?(e?: any): IteratorResult<T>;
// }

// interface IterableIterator<T> extends Iterator<T> {
//     [Symbol.iterator](): IterableIterator<T>;
// }

interface Student {
    Name: string;
}

export class School<T extends Student> implements IterableIterator<T> {
    private students: T[];
    private index= 0;
    //
    constructor() {
        this.students = [
          <T>{ Name: 'Sam' },
          <T>{ Name: 'Kevin' },
          <T>{ Name: 'Jessie' }
        ];
    }

    //使用IterableIterator的方式取出，透過next做回傳
    [Symbol.iterator](): IterableIterator<T> {
        return this;
    }

    next(): IteratorResult<T> {
        if (this.index < this.students.length) {
            return {
              done: false,
              value: this.students[this.index++]
            };
          }
        return {
            done: true,
            value: this.students[0]
        };
    }

    //使用generator的方式取出
    // *[Symbol.iterator]() {
    //     // for (const iter of this.students) {
    //     //     yield iter;
    //     // }
    //     //或
    //     yield* this.students;
    // }

}

const school = new School<Student>();

//console.log(school);
for (const student of school) {
    console.log(student.Name);
}

let sym = Symbol('My symbol');

console.log(Boolean(sym));

const obj: any = {};
let a: any = Symbol('a');
let b: any = Symbol('b');

obj[a] = 'Hello';
obj[b] = 'World';

const objectSymbols = Object.getOwnPropertySymbols(obj);
console.log(obj[a]);
