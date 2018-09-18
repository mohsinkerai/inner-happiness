const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

function getDataFromTable(tableName) {
    let query = `SELECT * FROM ${tableName}`;
    return mssql.query(query);
}

function getDataByQuery(query) {
    return mssql.query(query);
}

function getRCs() {
    return mysql.query("SELECT * FROM level WHERE level_type_id = 2");
}

function getLCs() {
    return mysql.query("SELECT * FROM level WHERE level_type_id = 3 AND full_name LIKE '%Council%'");
}

function getLevel() {
    return mysql.query("SELECT * FROM level");
}

function syncLevelType() {
    mysql.query('INSERT INTO level_type (name) VALUES ("NAT")').then(console.log("Done"));
    mysql.query('INSERT INTO level_type (name) VALUES ("REG")').then(console.log("Done"));
    mysql.query('INSERT INTO level_type (name) VALUES ("LCL")').then(console.log("Done"));
    mysql.query('INSERT INTO level_type (name) VALUES ("JKH")').then(console.log("Done"));
    mysql.query('INSERT INTO level_type (name) VALUES ("GRS")').then(console.log("Done"));
}

//******************** LEVEL Migration *************************************************//

function syncLevelNC() {
    getDataByQuery("SELECT * FROM My_tblInstitution WHERE Jurisdiction = 'NAT'")
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, old_id, old_code, is_closed, region) VALUES (1, "${value['ShortDescr']}", "${value['Descr']}", ${value['InstitutionId']}, ${oldCode}, ${value['Closed']}, "${value['RegionId']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("NAT Migrated");
    });
}

function syncLevelRC() {
    getDataByQuery("SELECT * FROM My_tblInstitution WHERE Jurisdiction = 'REG'")
        .then((rows) => {
        rows.forEach(function(value){
            let oldCode = value['OldCode'];
            let parentId;
            let name = value['ShortDescr'];
            if(name.toString().includes('RC')) {
                parentId = 8;
            } else if(name.toString().includes('ITREB')) {
                parentId = 3;
            } else if(name.toString().includes('CAB')) {
                parentId = 1;
            }
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed, region) VALUES (2, "${value['ShortDescr']}", "${value['Descr']}", ${parentId}, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']}, "${value['RegionId']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("REG Migrated");
    });
}

var councilMap = {};
var itrebMap = {};
var cabMap = {};

function syncLevelLC() {
    getRCs().then((rows) => {
        rows.forEach(row => {
            if (row.name != null && row.id != null && row.region != null) {
                if(row.name.toString().includes('RC')) {
                    councilMap[row.region] = row.id;
                } else if(row.name.toString().includes('ITREB')) {
                    itrebMap[row.region] = row.id;
                } else if(row.name.toString().includes('CAB')) {
                    cabMap[row.region] = row.id;
                }
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataByQuery("select * from My_tblInstitution where Jurisdiction = 'LCL'")
            .then((rows) => {
            rows.forEach(function(value){
                let name = value['Descr'];
                let region = value['RegionId'];
                let parentId;
                if(name.toString().includes('Council')) {
                    parentId = (councilMap[region] == undefined) ? null : councilMap[region];
                } else if(name.toString().includes('ITREB')) {
                    parentId = (itrebMap[region] == undefined) ? null : itrebMap[region];
                } else if(name.toString().includes('CAB')) {
                    parentId = (cabMap[region] == undefined) ? null : cabMap[region];
                } else {
                    parentId = null;
                }

                let oldCode = value['OldCode'];
                oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
                let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed, region, local_coucil_id) VALUES (3, "${value['ShortDescr']}", "${value['Descr']}", ${parentId}, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']}, "${region}", "${value['LocalCouncilId']}")`;
                mysql.query(query).then(/*console.log("Done")*/);
            });
            console.log("LCL Migrated");
        });
    });
}

var lcMap = {};

function syncLevelJk() {
    getLCs().then((rows) => {
        rows.forEach(row => {
            if (row.id != null && row.local_coucil_id != null) {
                lcMap[row.local_coucil_id] = row.id;
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataByQuery("SELECT * FROM My_tblInstitution WHERE Jurisdiction = 'JKH'")
            .then((rows) => {
                rows.forEach(function(value){
                let localCouncil = value['LocalCouncilId'];
                let parentId = (lcMap[localCouncil] == undefined) ? null : lcMap[localCouncil];
                let oldCode = value['OldCode'];
                oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
                let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed, region, local_coucil_id) VALUES (4, "${value['ShortDescr']}", "${value['Descr']}", ${parentId}, ${value['InstitutionId']}, "${oldCode}", ${value['Closed']}, "${value['RegionId']}", "${localCouncil}")`;
                mysql.query(query).then(/*console.log("Done")*/);
            });
            console.log("JKH Migrated");
        });
    });
}

function syncLevelGRS() {
    getDataByQuery("SELECT * FROM My_tblInstitution WHERE Jurisdiction = 'GRS'")
        .then((rows) => {
        rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, code_eo, code_nc, old_id, old_code, is_closed) VALUES (5, "${value['ShortDescr']}", "${value['Descr']}", '', '', ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("GRS Migrated")
    });
}

function syncLevelInstitution() {
    getLevel().then((rows) => {
        rows.forEach(function(value){
            mysql.query(`INSERT INTO institution (name, level_id, old_institution_id) VALUES ("${value['name']}", ${value['id']}, ${value['old_id']})`)
                .then(/*console.log("done")*/);
        });
        console.log("Synced Level Institution")
    });
}

//syncLevelNC();
//syncLevelRC();
//syncLevelLC();
//syncLevelJk();
//syncLevelGRS();

//syncLevelInstitution();