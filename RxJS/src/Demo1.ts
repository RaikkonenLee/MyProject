import { forkJoin, zip, combineLatest, Subject } from "rxjs";
import { withLatestFrom, take, first } from "rxjs/operators";

//part 1
type Color = 'white' | 'green' | 'red' | 'blue';
type Logo = 'fish' | 'dog' | 'bird' | 'cow';

//part 2
const color$ = new Subject<Color>();
const logo$ = new Subject<Logo>();

//part 3
//zip 會等待所有Observable都執行完才會觸發subscribe，不管有多少個Observable都可以
// zip(color$, logo$)
//     .subscribe(([color, logo]) => console.log(`${color} shirt with ${logo}`));

//combineLatest 會取得每一個Observable的最後一筆結果，都要有結果才會觸發subscribe
// combineLatest(color$, logo$)
//     .subscribe(([color, logo]) => console.log(`${color} shirt with ${logo}`));

//withLatestFrom 會等兩個Observable都有執行，並且要等前面的執行才會觸發subscribe
// color$.pipe(
//   withLatestFrom(logo$)  
// ).subscribe(([color, logo]) => console.log(`${color} shirt with ${logo}`));

//forkJoin 會等兩個Observable都有complete動作才會觸發subscribe，那要直接手動complete可以使用take或first的方式
// forkJoin(color$, logo$)
//     .subscribe(([color, logo]) => console.log(`${color} shirt with ${logo}`));

//使用take或first的方式
const firstColor$ = color$.pipe(take(1));
const firstLogo$ = logo$.pipe(first());

forkJoin(firstColor$, firstLogo$)
    .subscribe(([color, logo]) => console.log(`${color} shirt with ${logo}`));

//part 4
color$.next('white');
logo$.next('fish');

color$.next('green');
logo$.next('dog');

color$.next('red');
logo$.next('bird');

color$.next('blue');

//part 5
// forkJoin沒有手動停止就要下complete
// color$.complete();
// logo$.complete();