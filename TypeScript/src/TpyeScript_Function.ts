//function裡的參數定義型別，也可以定義function回傳的型別
function add(x: number, y: number) {
    return x + y;
}
let myAdd = function(x: number, y: number): number { return x + y; };
console.log(myAdd(2, 3));

//Optional Parameters
function buildName(firstName: string, lastName?: string) {
    return `${firstName} ${lastName}`;
}
let result1: string = buildName('Bob', 'Adams');
let result2: string = buildName('Bob');
console.log(`${result1} ${result2}`);

//Default Paramster
function buildName2(firstName: string, lastName: string = "Smith") {
    return `${firstName} ${lastName}`;
}
let result3: string = buildName2('bob');
let result4: string = buildName2('bob', 'Adams');
console.log(`${result3} ${result4}`);

//Rest Parameter
function buildName3(firstName: string, ...restOfName: string[]) {
    return `${firstName} ${restOfName.join(' ')}`;
}
let employeeName: string = buildName3('Kevin', 'Jeff', 'Jimmy');
console.log(employeeName);

