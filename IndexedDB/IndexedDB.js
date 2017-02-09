var indexedDB = window.indexedDB || window.webkitIndexedDB || window.mozIndexedDB || window.msIndexedDB;
var IDBTransaction = window.IDBTransaction || window.webkitTransaction || window.msIDBTransaction || { READ_WRITE: "readwrite" };
var AccountDB;
//

function DBClass(tableName, columns) {
    this.TableName = tableName;
    this.Columns = columns;
    this.DBObject = undefined;
}

DBClass.prototype.CreateTable = function () {
    var that = this;
    var request = indexedDB.open(that.TableName);
    //
    request.onsuccess = function () {
        that.DBObject = request.result;
    };
    request.onerror = function (event) {
        console.log("IndexedDB error: " + event.target.errorCode);
    };
    request.onblocked = function (event) {
        console.log("IndexedDB on blocked");
        event.target.result.close();
    };
    request.onupgradeneeded = function (event) {
        var hasKey;
        var objectStore;
        //var tableName = that.TableName;
        //
        if (that.Columns) {
            hasKey = that.Columns.some(function (element, index, array) {
                if (element.Key) {
                    objectStore = event.currentTarget.result.createObjectStore(that.TableName, {
                        keyPath: element.ColumnName, autoIncrement: true
                    });
                    return true;
                } else {
                    return false;
                }
            });
            if (!hasKey) {
                objectStore = event.currentTarget.result.createObjectStore(that.TableName, {
                    keyPath: "SerialNumber", autoIncrement: true
                });
            }
            that.Columns.forEach(function (element, index, array) {
                if (!element.Key) {
                    objectStore.createIndex(element.ColumnName, element.ColumnName, { unique: element.Unique });
                }
            });
        }
    };
};

DBClass.prototype.AddData = function (columnsValueArray) {
    var transaction = this.DBObject.transaction(this.TableName, "readwrite");
    var objectStore = transaction.objectStore(this.TableName);
    var request;
    var columnsObject = {};
    columnsValueArray.forEach(function (element, index, array) {
        columnsObject[element.ColumnName] = element.Value;
    });
    request = objectStore.add(columnsObject);
    //
    request.onsuccess = function (event) {
        console.log("data save success!");
    };
    request.onerror = function (event) {
        console.log("data save error!");
    };
};

DBClass.prototype.UpdateData = function (columnsValueArray) {
    var transaction = this.DBObject.transaction(this.TableName, "readwrite");
    var objectStore = transaction.objectStore(this.TableName);
    var request;
    var columnsObject = {};
    columnsValueArray.forEach(function (element, index, array) {
        columnsObject[element.ColumnName] = element.Value;
    });
    request = objectStore.put(columnsObject);
    //
    request.onsuccess = function (event) {
        console.log("data update success!");
    };
    request.onerror = function (event) {
        console.log("data update error!");
    };
};

DBClass.prototype.DeleteData = function (keyValue) {
    var transaction = this.DBObject.transaction(this.TableName, "readwrite");
    var objectStore = transaction.objectStore(this.TableName);
    var request;
    request = objectStore.delete(keyValue);
    request.onsuccess = function (event) {
        console.log("data delete success!");
    };
    request.onerror = function (event) {
        console.log("data delete error!");
    };
};

DBClass.prototype.GetData = function (keyValue, getFunc) {
    var transaction = this.DBObject.transaction(this.TableName);
    var objectStore = transaction.objectStore(this.TableName);
    var request;
    request = objectStore.get(keyValue);
    objectStore.op
    //
    request.onsuccess = function (event) {
        if (getFunc) {
            getFunc(event.target.result);
        }
    };
    request.onerror = function (event) {
        console.log("data search error!");
    };
};

DBClass.prototype.GetAll = function (getFunc) {
    var transaction = this.DBObject.transaction(this.TableName);
    var objectStore = transaction.objectStore(this.TableName);
    var cursor = objectStore.openCursor();
    //
    cursor.onsuccess = function (event) {
        if (getFunc) {
            getFunc(event.target.result);
        }
    };
};

DBClass.prototype.DropTable = function () {
    if (this.DBObject) {
        this.DBObject.close();
    }
    var request = indexedDB.deleteDatabase(this.TableName);
    request.onsuccess = function () {
        console.log("Delete database successfully");
    };
    request.onerror = function () {
        console.log("Delete database error");
    };
    request.onblocked = function (event) {
        console.log("Database is on blocked, it's can't delete");
    };
};

var ColumnInfoClass = function (columnName, key, unique) {
    this.ColumnName = columnName;
    this.Key = key === undefined ? false : key;
    this.Unique = unique === undefined ? false : unique;
};

var ColumnValueClass = function (columnName, value) {
    this.ColumnName = columnName;
    this.Value = value;
};

function TestDB() {
    var columns = [];
    columns.push(new ColumnInfoClass("ID", true, true));
    columns.push(new ColumnInfoClass("Name"));
    columns.push(new ColumnInfoClass("Phone"));
    AccountDB = new DBClass("Account", columns);
    AccountDB.CreateTable();
}

function AddDB() {
    var values = [];
    values.push(new ColumnValueClass("ID", 1));
    values.push(new ColumnValueClass("Name", "測試新增"));
    values.push(new ColumnValueClass("Phone", "123456"));
    //
    AccountDB.AddData(values);
}

function UpdateDB() {
    var values = [];
    values.push(new ColumnValueClass("ID", 1));
    values.push(new ColumnValueClass("Name", "測試修改"));
    values.push(new ColumnValueClass("Phone", "654321"));
    //
    AccountDB.UpdateData(values);
}

function DeleteDB() {
    AccountDB.DeleteData(1);
}

function GetDB() {
    AccountDB.GetData(1, function (request) {
        if (request) {
            console.log(request.Name + "_" + request.Phone);
        } else {
            console.log("No Data!");
        }
    });
}

function GetAll() {
    AccountDB.GetAll(function (request) {
        if (request) {
            console.log(request.value.Name);
            request.continue();
        }
    });
}