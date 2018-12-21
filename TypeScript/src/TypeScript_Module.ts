/**
 *  Export Module 的用法，const、function、class、interface前面加上export即可傳到外部考使用
 */
//將Interface Export 出去
export interface StringValidator {
    isAcceptable(s: string): boolean;
}

//將Const Export 出去
export const numberRegexp = /^[0-9]+$/;

//將Class Export 出去
export class ZipCodeValidator implements StringValidator {
    isAcceptable(s: string) {
        return s.length === 5 && numberRegexp.test(s);
    }
}

//使用別名
class ZipCodeValidator2 implements StringValidator {
    isAcceptable(s: string) {
        return s.length === 5 && numberRegexp.test(s);
    }
}
export { ZipCodeValidator as mainValidator };

/**
 * Import 的用法
 */
//別名
import { ZipCodeValidator as ImportZipCodeValidator } from './TypeScript_Module';
let myValidator = new ImportZipCodeValidator();

import * as validator from './TypeScript_Module';
let myValidator2 = new validator.ZipCodeValidator();

/**
 * Default Export 一個Module只能有一個default export
 */
export default class ZipCodeValidator3 {
    static numberRegexp = /^[0-9]+$/;
    isAcceptable(s: string) {
        return s.length === 5 && ZipCodeValidator3.numberRegexp.test(s);
    }
}
import validator3 from './TypeScript_Module';
let myValidator3 = new validator3();

