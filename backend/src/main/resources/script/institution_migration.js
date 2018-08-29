const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

var positionMap = {};

function addValueToPosition(key, value) {
    positionMap[key] = positionMap[key] || [];
    positionMap[key].push(value);
}

function getDataFromTable(tableName) {
    let query = `SELECT * FROM ${tableName}`;
    return mssql.query(query);
}

function getDataByQuery(query) {
    return mssql.query(query);
}

function getPosition() {
    return mysql.query("SELECT * FROM position");
}

function getInstitution() {
    return mysql.query("SELECT * FROM institution");
}

function getPositionOnInstitution() {
    return mysql.query("SELECT * FROM position_on_institution");
}

function syncInstitution() {
    getPosition().then((rows) => {
        rows.forEach(row => {
            if (row.id != null && row.old_id != null) {
                addValueToPosition(row.old_id, row.id);
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataFromTable("Ali_tblInstitutionPosition").then((rows) => {
            rows.forEach(function(value){
                // mapping position_id
                var pId = positionMap[value['PositionId']];

                var query = `INSERT INTO position_on_institution (cycle_id, position_id, institution_id, desired) VALUES (${value['AppYearID']}, ${pId}, ${value['InstitutionId']}, ${value['ProposalsRequired']})`;
                mysql.query(query).then(console.log("Done"));
            });
            return Promise.resolve(null);
        }).then(response => {
            getInstitution().then((rows) => {
                rows.forEach(row => {
                    var institutionId = row.id;
                    var oldInstitutionId = row.old_institution_id;
                    let query = `UPDATE position_on_institution SET institution_id = ${institutionId} WHERE institution_id = ${oldInstitutionId}`;
                    mysql.query(query).then(/*console.log("Done")*/);
                });
                return Promise.resolve(null);
            }).then(response => {
                getPositionOnInstitution().then((rows) => {
                    rows.forEach(row => {
                        var cycleId = row.cycle_id;
                        var poiId = row.id;
                        let query = `INSERT INTO cycle_position_on_institution (cycle_id, position_on_institution_id) VALUES (${cycleId}, ${poiId})`;
                        mysql.query(query).then(/*console.log("Done")*/);
                    });
                });
            });
        });
    });
}


syncInstitution();
