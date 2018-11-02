/**  Demo 1
import { Observable} from 'rxjs';
var observable = Observable.create(
    (observer:any) => {
        observer.next('Hello World!');
        observer.next('Hello Again!');
        observer.complete();
        observer.next('Bye');
    }
);
observable.subscribe(
    (x:any) => logItem(x),
    (error: any) => logItem('Error: ' + error),
    () => logItem('Completed')
);
function logItem(val:any) {
    var node = document.createElement("li");
    var textnode = document.createTextNode(val);
    node.appendChild(textnode);
    document.getElementById("list").appendChild(node);
}
 */
/** Demo 2
 
*/
import { Observable, from, fromEvent, interval, of, BehaviorSubject, combineLatest } from 'rxjs';
import { map, mergeMap, switchMap, filter, toArray, tap, take, merge, debounce, debounceTime } from 'rxjs/operators';

var wasteAPI$: Observable<Waste[]> = DataService();
var wastes$: Observable<Waste[]>;
var keyword$ = fromEvent(document.getElementById("textinput"), "keyup");
var textinput: any = document.getElementById("textinput");
//
wastes$ = keyword$.pipe(
    debounceTime(500),
    mergeMap((keyword: KeyboardEvent) => wasteAPI$.pipe(
            switchMap(((data: Waste[]) => data)),
            filter(hasKeyword(textinput.value)),
            toArray()
        )
    )
);
wastes$.subscribe(
    (result: Waste[]) => logItem(result));

let hasKeyword = (keyword: string) => {
    console.log(keyword);
    return (waste: Waste) => waste.RegistrationNo.indexOf(keyword) > -1;
}

function logItem(val: Waste[]) {
    document.getElementById("list").innerHTML = "";
    let node, textnode;
    val.forEach((element: Waste) => {
        node = document.createElement("li");
        textnode = document.createTextNode(element.OrgName);
        node.appendChild(textnode);
        document.getElementById("list").appendChild(node);
    });
}

function DataService() {
    return from(fetch('data.json'))
        .pipe(
            mergeMap(function(data: Response) {
                if (data && data.ok) {
                    return from(data.clone().json()).pipe(
                        map((data: any) => <Waste[]>data.result.records)
                    )    
                }
            })
        );
}
interface Waste {
    County: string;
    OrgType: string;
    Grade: string;
    OrgName: string;
    RegistrationNo: string;
    OrgAddress: string;
    TreatMethod: string;
    GrantDeadline: string;
    OrgTel: string;
    OperatingAddress: string;
  }
  //Demo 2