const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

function getDataFromTable(tableName) {
    let query = `SELECT * FROM ${tableName}`;
    return mssql.query(query);
}

function syncCity() {
    getDataFromTable("Ali_tblCity")
    .then((rows) => {
        rows.forEach(function(value){
            let query = `Insert INTO city (name) VALUES ("${value['Descr']}")`;
            mysql.query(query).then(console.log("Done"));
        });
    });
}

function syncCountry() {
    getDataFromTable("Ali_tblCountry")
    .then((rows) => {
        rows.forEach(function(value){
            let query = `Insert INTO country (name, code) VALUES ("${value['Descr']}","${value['ShortDescr']}")`;
            mysql.query(query).then(console.log("Done"));
        });
    });
}

syncCity();
syncCountry();