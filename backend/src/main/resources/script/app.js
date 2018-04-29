const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

function getDataFromTable(tableName) {
    let query = `SELECT * FROM ${tableName}`;
    return mssql.query(query);
}

function getDataByQuery(query) {
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

function syncTitle() {
    getDataFromTable("Ali_tblTitle")
        .then((rows) => {
        rows.forEach(function(value){
        let query = `Insert INTO jamati_titles (title, gender) VALUES ("${value['Descr']}","${value['Gender']}")`;
        mysql.query(query).then(console.log("Done"));
    });
});
}

function syncSalutation() {
    getDataByQuery("SELECT DISTINCT(SalutationId) FROM Ali_tblPerson WHERE SalutationId IS NOT NULL AND SalutationId != '' ORDER BY SalutationId ASC")
        .then((rows) => {
        rows.forEach(function(value){
        let query = `Insert INTO salutation (salutation) VALUES ("${value['SalutationId']}")`;
        mysql.query(query).then(console.log("Done"));
    });
});
}

function syncDegree() {
    getDataByQuery("SELECT DISTINCT(Descr) FROM Ali_tblDegree WHERE Descr IS NOT NULL AND Descr != '' ORDER BY Descr ASC")
        .then((rows) => {
        rows.forEach(function(value){
        let query = `Insert INTO educational_degree (name) VALUES ("${value['Descr']}")`;
        mysql.query(query).then(console.log("Done"));
    });
});
}

function syncLanguage() {
    getDataByQuery("SELECT DISTINCT Language FROM Ali_tblLanguage ORDER BY Language ASC")
        .then((rows) => {
        rows.forEach(function(value){
        let query = `Insert INTO language (name) VALUES ("${value['Language']}")`;
        mysql.query(query).then(console.log("Done"));
    });
});
}

//syncCity();
//syncCountry();
//syncTitle();
//syncSalutation();
//syncDegree();
//syncLanguage();

console.log("Migration Completed");