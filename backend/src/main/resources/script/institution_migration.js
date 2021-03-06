const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

var positionMap = {};
var institutionMap = {};
var personMap = {};

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

function getPerson() {
    return mysql.query("SELECT * FROM person");
}

function getInstitutionLevel() {
    return mysql.query("select i.id as 'institution_id', l.old_id as 'old_institution_id' from institution i join level l on i.level_id = l.id");
}

function getInstitution() {
    return mysql.query("SELECT * FROM institution");
}

function getPersonCPI() {
    return mysql.query("SELECT * FROM person_cpi");
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
                mysql.query(query).then(/*console.log("Done")*/);
            });
            return Promise.resolve(null);
        }).then(response => {
            console.log('COMPLETED');
        });
    });
}

function syncPositionOnInstitution() {
    getInstitution().then((rows) => {
        rows.forEach(row => {
            if (row.id != null && row.old_institution_id != null) {
                institutionMap[row.old_institution_id] = row.id;
            }
        })
        return Promise.resolve(null);

    }).then(response => {
        getPositionOnInstitution().then((rows) => {
            rows.forEach(row => {
                var id = row.id;
                var oldInstitutionId = row.institution_id;
                var institutionId = (institutionMap[oldInstitutionId] == undefined) ? null : institutionMap[oldInstitutionId];
                let query = `UPDATE position_on_institution SET institution_id = ${institutionId} WHERE institution_id = ${oldInstitutionId} AND id = ${id}`;
                mysql.query(query).then(/*console.log("Done")*/);

                var cycleId = row.cycle_id;
                var poiId = row.id;
                query = `INSERT INTO cycle_position_on_institution (cycle_id, position_on_institution_id) VALUES (${cycleId}, ${poiId})`;
                mysql.query(query).then(/*console.log("Done")*/);
            });
            return Promise.resolve(null);

        }).then(response => {
            console.log('COMPLETED');
        });
    });
}

function syncPersonInstitution_Nominated() {
    getDataFromTable("Ali_tblAppointee").then((rows) => {
        rows.forEach(function(value){
            var query = `INSERT INTO person_cpi (person_id, institution_id, position_id, cycle_id, is_appointed, is_recommended) VALUES (${value['PersonId']}, ${value['InstitutionId']}, ${value['PositionId']}, ${value['AppYearId']}, 1, 0)`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        return Promise.resolve(null);
    }).then(response => {
        getInstitutionLevel().then((rows) => {
            rows.forEach(row => {
                if (row.institution_id != null && row.old_institution_id != null) {
                    institutionMap[row.old_institution_id] = row.institution_id;
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            getPosition().then((rows) => {
                rows.forEach(row => {
                    if (row.id != null && row.old_id != null) {
                        positionMap[row.old_id] = row.id;
                    }
                });
                return Promise.resolve(null);
            }).then(response => {
                getPerson().then((rows) => {
                    rows.forEach(row => {
                        if (row.id != null && row.old_id != null) {
                            personMap[row.old_id] = row.id;
                        }
                    });
                    return Promise.resolve(null);
                }).then(response => {
                    //console.log('institutionMap: ', Object.keys(institutionMap).length, 'positionMap: ', Object.keys(positionMap).length, 'personMap: ', Object.keys(personMap).length);
                    getPersonCPI().then((rows) => {
                        var str = "[";
                        rows.forEach(row => {
                            var id = row.id;
                            var oldPositionId = row.position_id;
                            var positionId = (positionMap[oldPositionId] == undefined) ? null : positionMap[oldPositionId];
                            var oldPersonId = row.person_id;
                            var personId = (personMap[oldPersonId] == undefined) ? null : personMap[oldPersonId];
                            var oldInstitutionId = row.institution_id;
                            var institutionId = (institutionMap[oldInstitutionId] == undefined) ? null : institutionMap[oldInstitutionId];

                            if(personId != null) {
                                let query = `UPDATE person_cpi SET position_id = ${positionId} WHERE position_id = ${oldPositionId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);

                                query = `UPDATE person_cpi SET person_id = ${personId} WHERE person_id = ${oldPersonId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);

                                query = `UPDATE person_cpi SET institution_id = ${institutionId} WHERE institution_id = ${oldInstitutionId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);
                            } else {
                                str = str + oldPersonId + ', ';
                            }
                        });
                        str = str + ']';
                        console.log(str);
                        return Promise.resolve(null);
                    }).then(response => {
                        console.log("COMPLETED");
                    })
                })
            })
        })
    })
}

function syncPersonInstitution_Recommended() {
    getDataFromTable("Ali_tblRecommendation").then((rows) => {
        rows.forEach(function(value){
            var isRecommended = value['Recommended'] == 1 ? true : false;
            var query = `INSERT INTO person_cpi (person_id, institution_id, position_id, cycle_id, is_recommended) VALUES (${value['PersonId']}, ${value['InstitutionId']}, ${value['PositionId']}, ${value['AppYearID']}, ${isRecommended})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        return Promise.resolve(null);
    }).then(response => {
        getInstitutionLevel().then((rows) => {
            rows.forEach(row => {
                if (row.institution_id != null && row.old_institution_id != null) {
                    institutionMap[row.old_institution_id] = row.institution_id;
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            getPosition().then((rows) => {
                rows.forEach(row => {
                    if (row.id != null && row.old_id != null) {
                        positionMap[row.old_id] = row.id;
                    }
                });
                return Promise.resolve(null);
            }).then(response => {
                getPerson().then((rows) => {
                    rows.forEach(row => {
                        if (row.id != null && row.old_id != null) {
                            personMap[row.old_id] = row.id;
                        }
                    });
                    return Promise.resolve(null);
                }).then(response => {
                    //console.log('institutionMap: ', Object.keys(institutionMap).length, 'positionMap: ', Object.keys(positionMap).length, 'personMap: ', Object.keys(personMap).length);
                    getPersonCPI().then((rows) => {
                        var str = "[";
                        rows.forEach(row => {
                            var id = row.id;
                            var oldPositionId = row.position_id;
                            var positionId = (positionMap[oldPositionId] == undefined) ? null : positionMap[oldPositionId];
                            var oldPersonId = row.person_id;
                            var personId = (personMap[oldPersonId] == undefined) ? null : personMap[oldPersonId];
                            var oldInstitutionId = row.institution_id;
                            var institutionId = (institutionMap[oldInstitutionId] == undefined) ? null : institutionMap[oldInstitutionId];

                            if(personId != null) {
                                let query = `UPDATE person_cpi SET position_id = ${positionId} WHERE position_id = ${oldPositionId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);

                                query = `UPDATE person_cpi SET person_id = ${personId} WHERE person_id = ${oldPersonId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);

                                query = `UPDATE person_cpi SET institution_id = ${institutionId} WHERE institution_id = ${oldInstitutionId} AND id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);
                            } else {
                                str = str + oldPersonId + ', ';
                            }
                        });
                        str = str + ']';
                        console.log(str);
                        return Promise.resolve(null);
                    }).then(response => {
                        console.log("COMPLETED");
                    })
                })
            })
        })
    })
}

function syncPersonCPI() {
    getPersonCPI().then((rows) => {
        rows.forEach(row => {
            var institution_id = row.institution_id;
            var cycle_id = row.cycle_id;
            var position_id = row.position_id;
            let query = `select cpoi.id as 'cpoi_id' from cycle_position_on_institution cpoi join position_on_institution poi on cpoi.position_on_institution_id = poi.id where cpoi.cycle_id = ${cycle_id} and poi.institution_id = ${institution_id} and poi.position_id = ${position_id}`;
            mysql.query(query).then((rows) => {
                rows.forEach(row => {
                    var cpoi_id = row.cpoi_id;
                    let query = `UPDATE person_cpi SET cpi_id = ${cpoi_id} WHERE institution_id = ${institution_id} and cycle_id = ${cycle_id} and position_id = ${position_id}`;
                    mysql.query(query).then(/*console.log("Done")*/);
                });
            });
        });
        return Promise.resolve(null);
    }).then(response => {
        console.log("COMPLETED");
    })
}

//syncInstitution();
//syncPositionOnInstitution();
//syncPersonInstitution_Recommended();
//syncPersonInstitution_Nominated();
//syncPersonCPI();
