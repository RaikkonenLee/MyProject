/** 
 * 以=>取代Anonymous Function
*/
let hello1 = function(firstName: string, lastName: string): string {
    return `Hello ${firstName} ${lastName}`;
};
let hello2 = 
    (firstName: string, lastName: string): string => `Hello ${firstName} ${lastName}`;

let helloMessage: string = hello2('Sam', 'Xiao');
console.log(helloMessage);

/** 
 * 以=>取代Callback 
*/
const angular = [
    'Kevin',
    'Jeff',
    'Jimmy'
];
const angularLength1: number[] = angular.map(function(person: string) {
    return person.length;
});
const angularLength2: number[] = angular.map((person: string) => person.length );
console.log(angularLength2);

/** 
 * Anonymous Function & this
*/
let Foo = {
    name: 'Sam',
    handleMessage: function(message: string, callback: any) {
        callback(message);
    },
    receive: function() {
        this.handleMessage('Hello,', (message: any) => {
            console.log(`${message} ${this.name}`);
        });
    }
};
Foo.receive();
