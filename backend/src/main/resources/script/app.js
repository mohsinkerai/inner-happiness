const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

function getDataFromTable(tableName) {
    let query = `SELECT * FROM ${tableName}`;
    return mssql.query(query);
}

function getDataByQuery(query) {
    return mssql.query(query);
}

function getDateTime(dateTime) {
    if(dateTime == null || dateTime == '' || dateTime == 'null') {
        return null;
    }
    return (new Date(dateTime).toISOString().slice(0, 19).replace('T', ' '));
}

function syncCity() {
    getDataFromTable("Ali_tblCity")
        .then((rows) => {
            rows.forEach(function(value){
            let countryId = `${value['CountryCode']}`.replace(/^0+/, '');
            let query = `Insert INTO city (name, country_id, old_id) VALUES ("${value['Descr']}", ${countryId}, "${value['CityCode']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done City");
    });
}

function syncCountry() {
    getDataFromTable("Ali_tblCountry")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO country (name, code) VALUES ("${value['Descr']}","${value['ShortDescr']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Country");
    });
}

function syncJamatiTitle() {
    getDataFromTable("Ali_tblTitle")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO jamati_title (name, gender, old_id) VALUES ("${value['Descr']}","${value['Gender']}", ${value['TitleId']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Jamati title");
    });
}

function syncSalutation() {
    getDataByQuery("SELECT DISTINCT(SalutationId) FROM Ali_tblPerson WHERE SalutationId IS NOT NULL AND SalutationId != '' ORDER BY SalutationId ASC")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO salutation (name) VALUES ("${value['SalutationId']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Salutation");
    });
}

function syncEducationalDegree() {
    getDataByQuery("SELECT DISTINCT(Descr) FROM Ali_tblDegree WHERE Descr IS NOT NULL AND Descr != '' ORDER BY Descr ASC")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO educational_degree (name) VALUES ("${value['Descr']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Educational Degree");
    });
}

function syncLanguage() {
    getDataByQuery("SELECT DISTINCT Language FROM Ali_tblLanguage ORDER BY Language ASC")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO language (name) VALUES ("${value['Language']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Language");
    });
}

function syncVoluntaryInstitution() {
    getDataFromTable("Ali_tblInstitution")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO voluntary_institution (name) VALUES ("${value['Descr']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Voluntary Institution");
    });
}

function syncAreaOfStudy() {
    getDataFromTable("Ali_tblMajorStudyArea")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO area_of_study (name) VALUES ("${value['Descr']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Done Major Study Area");
    });
}

function syncBusinessType() {
    getDataByQuery("SELECT distinct(BusinessType) FROM Ali_tblOccupation WHERE BusinessType IS NOT NULL AND BusinessType != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO business_type (name) VALUES ("${value['BusinessType']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Business Type");
    });
}

function syncBusinessNature() {
    getDataByQuery("SELECT distinct(BusinessNature) FROM Ali_tblOccupation WHERE BusinessNature IS NOT NULL AND BusinessNature != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO business_nature (name) VALUES ("${value['BusinessNature']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Business Nature");
    });
}

function syncProfessionalMembership() {
    getDataByQuery("SELECT DISTINCT InstitutionName FROM Ali_tblProfessionalMembership WHERE InstitutionName IS NOT NULL AND InstitutionName != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO professional_membership (name) VALUES (TRIM("${value['InstitutionName']}"))`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Professional Membership");
    });
}

function syncFieldOfInterest() {
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Any Institution", "ANY")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Concilation & Arbitration Board", "CAB")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Education Services", "EDS")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Economic Planning Board", "EPB")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Focus Humanitarian Assitance", "FOC")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Grants & Review Board", "GRB")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Health Service", "HLS")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Islmailia Charitable Trust", "ICP")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Tariqah & Religious Education Board", "ITB")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Planning and Building Services", "PBS")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Social Welfare Board", "SWB")').then(console.log("Done"));
    mysql.query('INSERT INTO field_of_interest (name, short_name) VALUES ("Youth and Sports Board", "YSB")').then(console.log("Done"));
}

function syncMaritalStatus() {
    getDataByQuery("SELECT DISTINCT MaritalStatus FROM Ali_tblPerson WHERE MaritalStatus IS NOT NULL AND MaritalStatus != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO marital_status (name) VALUES ("${value['MaritalStatus']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Marital Status");
    });
}

function syncOccupation() {
    getDataByQuery("SELECT DISTINCT(Occupation) FROM Ali_tblOccupation WHERE Occupation IS NOT NULL AND Occupation != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO occupation (name) VALUES ("${value['Occupation']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Occupation");
    });
}

function syncEducationalInstitution() {
    getDataByQuery("SELECT DISTINCT(Descr) FROM Ali_tblAcademicInstitution WHERE Descr IS NOT NULL AND Descr != '' ORDER BY Descr ASC")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO educational_institution (name) VALUES ("${value['Descr']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Educational Institution");
    });
}

function syncPosition() {
    getDataFromTable("Ali_tblPosition")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO \`position\` (name, old_id) VALUES ("${value['Descr']}", ${value['PositionId']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Position");
    });
}

function syncCycle() {
    getDataFromTable("Ali_tblAppointmentYear")
        .then((rows) => {
            rows.forEach(function(value){
            let startDate = getDateTime(`${value['StartDate']}`);
            let endDate = getDateTime(`${value['EndDate']}`);
            let query = `Insert INTO cycle (name, start_date, end_date) VALUES ('', '${startDate}', '${endDate}')`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Cycle");
    });
}

function syncSecularStudyLevel() {
    getDataByQuery("SELECT DISTINCT EducationLevel FROM Ali_tblPerson WHERE EducationLevel IS NOT NULL AND EducationLevel != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO secular_study_level (name) VALUES ("${value['EducationLevel']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Education Level");
    });
}

function syncSkills() {
    getDataByQuery("SELECT DISTINCT SkillsGot FROM Ali_tblPerson WHERE SkillsGot IS NOT NULL AND SkillsGot != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO skill (name) VALUES ("${value['SkillsGot']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Skills");
    });
}

function syncPublicServiceInstitution() {
    getDataByQuery("SELECT DISTINCT InstitutionName FROM Ali_tblPublicService WHERE InstitutionName IS NOT NULL AND InstitutionName != ''")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO public_service_institution (name) VALUES ("${value['InstitutionName']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Institution Name");
    });
}

function syncTempRegion() {
    getDataByQuery("SELECT DISTINCT r.RegionId, r.Descr AS RegionName, r.ShortDescr, c.Descr AS CountryName FROM Ali_tblRegion r JOIN Ali_tblCountry c ON r.CountryCode = c.CountryCode")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO temp_region (temp_id, name, name_descr, country) VALUES ("${value['RegionId']}", "${value['RegionName']}", "${value['ShortDescr']}", "${value['CountryName']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Temporary Table Region");
    });
}

function syncRegionalCouncil() {
    getDataByQuery("SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'REG' ORDER BY RegionId")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id) VALUES (2, "${value['ShortDescr']}", "${value['Descr']}", 1)`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level");
    });
}

function syncJamatKhana() {
    getDataByQuery("SELECT j.Population, j.Closed, j.OldCode, i.Descr, i.ShortDescr FROM Ali_tblJamatkhana j JOIN Ali_tblInstitution i ON j.InstitutionId = i.InstitutionId")
        .then((rows) => {
            rows.forEach(function(value){
            let query = `Insert INTO jamatkhana (name, short_name, population, is_closed, old_code) VALUES ("${value['Descr']}", "${value['ShortDescr']}", ${value['Population']}, ${value['Closed']}, "${value['OldCode']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Jamatkhana");
    });
}

function syncRegion() {
    getDataFromTable("Ali_tblRegion")
        .then((rows) => {
            rows.forEach(function(value){
            var countryId = `${value['CountryCode']}`.replace(/^0+/, '');
            let query = `Insert INTO region (old_id, name, short_name, country_id) VALUES ("${value['RegionId']}", "${value['Descr']}", "${value['ShortDescr']}", ${countryId})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Region");
    });
}

function syncLocalCouncil() {
    getDataByQuery("SELECT * FROM Ali_tblLocalCouncil WHERE Descr != '' AND Descr != 'Non Existent' ORDER BY LocalCouncilId")
        .then((rows) => {
            rows.forEach(function(value){
            var cityId = (`${value['CityCode']}`.replace(/^0+/, ''));
            cityId = cityId != '' ? cityId : '0';
            var oldRegionId = `${value['RegionId']}`;
            mysql.query(`SELECT * FROM region WHERE old_id = '${oldRegionId}'`).then((rows) => {
                rows.forEach(function(v){
                    let query = `Insert INTO local_council (old_id, name, short_name, city_id, is_closed, region_id) VALUES ("${value['LocalCouncilId']}", "${value['Descr']}", "${value['ShortDescr']}", ${cityId}, ${value['Closed']}, ${v['id']})`;
                    mysql.query(query).then(/*console.log("Done")*/);
                })
            });
        });
        console.log("Synced Local Council For All Regions");
    });
}

//******************** LEVEL Migration *************************************************//

function syncLevelNC() {
    getDataByQuery("SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'NAT'")
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, old_id, old_code, is_closed) VALUES (1, "${value['ShortDescr']}", "${value['Descr']}", ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 2");
    });
}

function syncLevelRC() {
    getDataByQuery("SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'REG' AND ShortDescr LIKE 'RC%'")
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed) VALUES (2, "${value['ShortDescr']}", "${value['Descr']}", 8, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 3");
    });
}

function syncLevelLC() {
    getDataByQuery("SELECT reg.InstitutionId AS 'RegionalCouncilId', lcl.* FROM Ali_tblInstitution reg JOIN Ali_tblInstitution lcl ON reg.RegionId = lcl.RegionId WHERE reg.Jurisdiction = 'REG' AND reg.ShortDescr LIKE 'RC%' AND lcl.Jurisdiction = 'LCL' AND lcl.ShortDescr LIKE 'LC%'")
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed) VALUES (3, "${value['ShortDescr']}", "${value['Descr']}", ${value['RegionalCouncilId']}, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 4");
    });
}

function syncLevelLCParent() {
    mysql.query(`SELECT id, old_id FROM level WHERE level_type_id = 2`).then((rows) => {
        rows.forEach(function(value){
            mysql.query(`UPDATE level SET level_parent_id = "${value['id']}" WHERE level_parent_id = "${value['old_id']}"`)
                .then(/*console.log("done")*/);
        });
        console.log("Synced Level 5");
    });
}

function syncLevelJK() {
    getDataByQuery("SELECT lcl.InstitutionId AS 'LCId', jkh.* FROM Ali_tblInstitution lcl JOIN Ali_tblInstitution jkh ON lcl.LocalCouncilId = jkh.LocalCouncilId WHERE lcl.Jurisdiction = 'LCL' AND jkh.Jurisdiction = 'JKH' AND lcl.ShortDescr LIKE 'LC%' ORDER BY jkh.LocalCouncilId")
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? '' : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed) VALUES (4, "${value['ShortDescr']}", "${value['Descr']}", ${value['LCId']}, ${value['InstitutionId']}, "${oldCode}", ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 6")
    });
}

function syncLevelJKParent() {
    mysql.query(`SELECT id, old_id FROM level WHERE level_type_id = 3`).then((rows) => {
        rows.forEach(function(value){
            mysql.query(`UPDATE level SET level_parent_id = "${value['id']}" WHERE level_parent_id = "${value['old_id']}"`)
                .then(/*console.log("done")*/);
        });
        console.log("Synced Level 7")
    });
}

function syncLevelRegionalITREB() {
    getDataByQuery(`SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'REG' AND ShortDescr LIKE 'ITREB%'`)
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed) VALUES (2, "${value['ShortDescr']}", "${value['Descr']}", 3, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 8")
    });
}

function syncLevelLocalITREB() {
    getDataByQuery(`SELECT reg.InstitutionId AS 'RItrebId', lcl.* FROM Ali_tblInstitution reg JOIN Ali_tblInstitution lcl ON reg.RegionId = lcl.RegionId WHERE reg.Jurisdiction = 'REG' AND reg.ShortDescr LIKE 'ITREB%' AND lcl.Jurisdiction = 'LCL' AND lcl.ShortDescr LIKE 'ITREB%' ORDER BY lcl.RegionId`)
        .then((rows) => {
            rows.forEach(function(value){
            let oldCode = value['OldCode'];
            oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
            let query = `Insert INTO level (level_type_id, name, full_name, level_parent_id, old_id, old_code, is_closed) VALUES (3, "${value['ShortDescr']}", "${value['Descr']}", ${value['RItrebId']}, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Synced Level 9")
    });
}

function syncLevelLocalITREBParent() {
    mysql.query(`SELECT id, old_id FROM level WHERE level_type_id = 2 AND name LIKE 'ITREB%'`).then((rows) => {
        rows.forEach(function(value){
        mysql.query(`UPDATE level SET level_parent_id = "${value['id']}" WHERE level_parent_id = "${value['old_id']}"`)
            .then(/*console.log("done")*/);
    });
    console.log("Synced Level 10")
});
}

function syncLevelRegionalCAB() {
    getDataByQuery(`SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'REG' AND ShortDescr LIKE 'CAB%'`)
        .then((rows) => {
        rows.forEach(function(value){
        let oldCode = value['OldCode'];
        oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
        let query = `Insert INTO level (level_type_id, name, full_name, code_eo, code_nc, level_parent_id, old_id, old_code, is_closed) VALUES (2, "${value['ShortDescr']}", "${value['Descr']}", '', '', 1, ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
        mysql.query(query).then(/*console.log("Done")*/);
    });
    console.log("Synced Level 11")
});
}

function syncLevelGRS() {
    getDataByQuery("SELECT * FROM Ali_tblInstitution WHERE Jurisdiction = 'GRS'")
        .then((rows) => {
        rows.forEach(function(value){
        let oldCode = value['OldCode'];
        oldCode = (oldCode == null || oldCode == '' || oldCode == ' ') ? null : oldCode;
        let query = `Insert INTO level (level_type_id, name, full_name, code_eo, code_nc, old_id, old_code, is_closed) VALUES (5, "${value['ShortDescr']}", "${value['Descr']}", '', '', ${value['InstitutionId']}, ${oldCode}, ${value['Closed']})`;
        mysql.query(query).then(/*console.log("Done")*/);
    });
    console.log("Synced Level 12")
});
}


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//syncCity();
//syncCountry();
//syncJamatiTitle();
//syncLanguage();
//syncSalutation();
//syncMaritalStatus();
//syncCycle();
//syncOccupation();
//syncAreaOfStudy();
//syncBusinessNature();
//syncBusinessType();
//syncEducationalDegree();
//syncEducationalInstitution();
//syncFieldOfInterest();
//syncProfessionalMembership();
//syncPublicServiceInstitution();
//syncSecularStudyLevel();
//syncVoluntaryInstitution();
syncPosition();


//syncJamatKhana();
//syncLocalCouncil();
//syncRegion();
//syncRegionalCouncil();
//syncSkills();
//syncTempRegion();
//syncLevelNC();
//syncLevelRC();
//syncLevelLC();
//syncLevelLCParent();
//syncLevelJK();
//syncLevelJKParent();
//syncLevelRegionalITREB();
//syncLevelLocalITREB();
//syncLevelLocalITREBParent();
//syncLevelRegionalCAB();
//syncLevelGRS();

