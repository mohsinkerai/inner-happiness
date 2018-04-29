'use strict';

const mssql = require('mssql');

var databaseConfig = {
    user: "sa",
    password: "admin",
    server: "ALIAHAD-B",
    database: "AliAppointments",
    parseJSON: true
};

var pool = null;

function getPool() {
    if (pool === null) {
        pool = new mssql.Connection(databaseConfig);
    }
    return pool;
}

exports.end = function () {
    if (pool !== null) {
        pool.close();
        pool = null;
    }
};

exports.query = (queryString) => {
    return new Promise((resolve, reject) => {
            getPool().connect(function(err){
            if(err) {
                console.log(err);
                reject(err);
            } else {
                let request = new mssql.Request(getPool());
                request.query(queryString, function(err, recordSet) {
                    if(err) {
                        reject(err);
                    } else {
                        resolve(recordSet);
                    }
                });
            }
        });
});
}
