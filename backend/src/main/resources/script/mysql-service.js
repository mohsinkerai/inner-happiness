'use strict';

const mysql = require('mysql');

var databaseConfig = {
    host: "localhost",
    database: "inner_satisfaction",
    user: "root",
    password: "",
    connectionLimit: 1,
    parseJSON: true
};

//var databaseConfig = {
//    host: "13.93.85.18",
//    database: "inner_satisfaction",
//    user: "amsal",
//    password: "hp",
//    connectionLimit: 1,
//    parseJSON: true
//};

var pool = null;

function getPool() {
    if (pool === null) {
        pool = mysql.createPool(databaseConfig);
    }
    return pool;
}

exports.end = function () {
    if (pool !== null) {
        pool.end();
        pool = null;
    }
};

exports.query = (queryString) => {
    return new Promise((resolve, reject) => {
        getPool().getConnection((err, connection) => {
            if (err) {
                console.error(err);
                reject(err);
            }
            else {
                connection.query(queryString, (err, rows, fields) => {
                    connection.release();
                    if (err) {
                        console.log("Error in query => " + queryString);
                        console.error(err);
                        reject(err);
                    } else {
                        resolve(rows);
                    }
                });
            }
        });
    });
};
