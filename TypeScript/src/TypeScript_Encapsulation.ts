/**
 * OOP封裝 Strategy Pattern - Strategy Class
 */
interface Strategy {
    execute(x: number, y: number): number;
}
class Context {
    constructor(private strategy: Strategy) { }

    request(x: number, y: number): number {
        return this.strategy.execute(x, y);
    }
}
let context = new Context(new class implements Strategy {
    execute(x: number, y: number) {
        return x + y;
    }
});
let result: number = context.request(1, 1);
console.log(result);

/**
 * OOP封裝 Strategy Pattern - Strategy Function
 */
interface Strategy2 {
    (x: number, y: number): number;
}
let context2 = (strategy: Strategy2) => (x: number, y: number) => strategy(x, y);
let request = context2((x, y) => x + y);
let result11: number = request(1, 1);
console.log(result11);