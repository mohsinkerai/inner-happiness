const mysql = require('./mysql-service');
const mssql = require('./mssql-service');

function executeMigration() {
    getDataFromTable("Ali_tblCity").then((rows) => {
        console.log("Migrating City - Total Records: ", rows.length);
        rows.forEach(function(value){
            let countryId = `${value['CountryCode']}`.replace(/^0+/, '');
            let query = `Insert INTO city (name, country_id, old_id) VALUES ("${value['Descr']}", ${countryId}, "${value['CityCode']}")`;
            mysql.query(query).then(/*console.log("Done")*/);
        });
        console.log("Migrated City");
        return Promise.resolve(null);

    }).then(response => {
        getDataFromTable("Ali_tblCountry").then((rows) => {
            console.log("Migrating Country - Total Records: ", rows.length);
            rows.forEach(function(value){
                let query = `Insert INTO country (name, code) VALUES ("${value['Descr']}","${value['ShortDescr']}")`;
                mysql.query(query).then(/*console.log("Done")*/);
            });
            console.log("Migrated Country");
            return Promise.resolve(null);

        }).then(response => {
            getDataByQuery("SELECT DISTINCT(SalutationId) FROM Ali_tblPerson WHERE SalutationId IS NOT NULL AND SalutationId != '' ORDER BY SalutationId ASC").then((rows) => {
                console.log("Migrating Salutation - Total Records: ", rows.length);
                rows.forEach(function(value){
                    let query = `Insert INTO salutation (name) VALUES ("${value['SalutationId']}")`;
                    mysql.query(query).then(/*console.log("Done")*/);
                });
                console.log("Migrated Salutation");
                return Promise.resolve(null);

            }).then(response => {
                console.log("Migrating FieldOfInterest - Total Records: 12");
                let query = 'INSERT INTO field_of_interest (name, short_name) VALUES ("Any Institution", "ANY"),("Concilation & Arbitration Board", "CAB"),("Education Services", "EDS"),("Economic Planning Board", "EPB"),("Focus Humanitarian Assitance", "FOC"),("Grants & Review Board", "GRB"),("Health Service", "HLS"),("Islmailia Charitable Trust", "ICP"),("Tariqah & Religious Education Board", "ITB"),("Planning and Building Services", "PBS"),("Social Welfare Board", "SWB"),("Youth and Sports Board", "YSB")';
                mysql.query(query).then(/*console.log("Done")*/);
                console.log("Migrated FieldOfInterest");
                return Promise.resolve(null);

            }).then(response => {
                getDataByQuery("SELECT DISTINCT MaritalStatus FROM Ali_tblPerson WHERE MaritalStatus IS NOT NULL AND MaritalStatus != ''").then((rows) => {
                    console.log("Migrating MaritalStatus - Total Records: ", rows.length);
                    rows.forEach(function(value){
                        let query = `Insert INTO marital_status (name) VALUES ("${value['MaritalStatus']}")`;
                        mysql.query(query).then(/*console.log("Done")*/);
                    });
                    console.log("Migrated MaritalStatus");
                    return Promise.resolve(null);

                }).then(response => {
                    getDataFromTable("Ali_tblAppointmentYear").then((rows) => {
                        console.log("Migrating Cycle - Total Records: ", rows.length);
                        rows.forEach(function(value){
                            let startDate = getDateTime(`${value['StartDate']}`);
                            let endDate = getDateTime(`${value['EndDate']}`);
                            let query = `Insert INTO cycle (name, start_date, end_date) VALUES ('', '${startDate}', '${endDate}')`;
                            mysql.query(query).then(/*console.log("Done")*/);
                        });
                        console.log("Migrated Cycle");
                        return Promise.resolve(null);

                    }).then(response => {
                        console.log("Migrating LevelType - Total Records: 5");
                        let query = 'INSERT INTO level_type (name) VALUES ("NAT"), ("REG"), ("LCL"), ("JKH"), ("GRS")';
                        mysql.query(query).then(/*console.log("Done")*/);
                        console.log("Migrated LevelType");
                        return Promise.resolve(null);

                    }).then(response => {
                        getDataFromTable("Ali_tblTitle").then((rows) => {
                            console.log("Migrating JamatiTitle - Total Records: ", rows.length);
                            rows.forEach(function(value){
                                let query = `Insert INTO jamati_title (name, gender, old_id) VALUES ("${value['Descr']}","${value['Gender']}", ${value['TitleId']})`;
                                mysql.query(query).then(/*console.log("Done")*/);
                            });
                            console.log("Migrated JamatiTitle");
                            return Promise.resolve(null);

                        }).then(response => {
                            getDataByQuery("SELECT DISTINCT(Descr) FROM Ali_tblDegree WHERE Descr IS NOT NULL AND Descr != '' ORDER BY Descr ASC").then((rows) => {
                                console.log("Migrating EducationalDegree - Total Records: ", rows.length);
                                rows.forEach(function(value){
                                    let query = `Insert INTO educational_degree (name) VALUES ("${value['Descr']}")`;
                                    mysql.query(query).then(/*console.log("Done")*/);
                                });
                                console.log("Migrated EducationalDegree");
                                return Promise.resolve(null);

                            }).then(response => {
                                getDataByQuery("SELECT DISTINCT Language FROM Ali_tblLanguage ORDER BY Language ASC").then((rows) => {
                                    console.log("Migrating Language - Total Records: ", rows.length);
                                    rows.forEach(function(value){
                                        let query = `Insert INTO language (name) VALUES ("${value['Language']}")`;
                                        mysql.query(query).then(/*console.log("Done")*/);
                                    });
                                    console.log("Migrated Language");
                                    return Promise.resolve(null);

                                }).then(response => {
                                    getDataFromTable("Ali_tblMajorStudyArea").then((rows) => {
                                        console.log("Migrating AreaOfStudy - Total Records: ", rows.length);
                                        rows.forEach(function(value){
                                            let query = `Insert INTO area_of_study (name) VALUES ("${value['Descr']}")`;
                                            mysql.query(query).then(/*console.log("Done")*/);
                                        });
                                        console.log("Migrated AreaOfStudy");
                                        return Promise.resolve(null);

                                    }).then(response => {
                                        getDataByQuery("SELECT distinct(BusinessType) FROM Ali_tblOccupation WHERE BusinessType IS NOT NULL AND BusinessType != ''").then((rows) => {
                                            console.log("Migrating BusinessType - Total Records: ", rows.length);
                                            rows.forEach(function(value){
                                                let query = `Insert INTO business_type (name) VALUES ("${value['BusinessType']}")`;
                                                mysql.query(query).then(/*console.log("Done")*/);
                                            });
                                            console.log("Migrated BusinessType");
                                            return Promise.resolve(null);

                                        }).then(response => {
                                            getDataByQuery("SELECT distinct(BusinessNature) FROM Ali_tblOccupation WHERE BusinessNature IS NOT NULL AND BusinessNature != ''").then((rows) => {
                                                console.log("Migrating BusinessNature - Total Records: ", rows.length);
                                                rows.forEach(function(value){
                                                    let query = `Insert INTO business_nature (name) VALUES ("${value['BusinessNature']}")`;
                                                    mysql.query(query).then(/*console.log("Done")*/);
                                                });
                                                console.log("Migrated BusinessNature");
                                                return Promise.resolve(null);

                                            }).then(response => {
                                                getDataByQuery("SELECT DISTINCT InstitutionName FROM Ali_tblProfessionalMembership WHERE InstitutionName IS NOT NULL AND InstitutionName != ''").then((rows) => {
                                                    console.log("Migrating ProfessionalMembership - Total Records: ", rows.length);
                                                    rows.forEach(function(value){
                                                        let query = `Insert INTO professional_membership (name) VALUES (TRIM("${value['InstitutionName']}"))`;
                                                        mysql.query(query).then(/*console.log("Done")*/);
                                                    });
                                                    console.log("Migrated ProfessionalMembership");
                                                    return Promise.resolve(null);

                                                }).then(response => {
                                                    getDataByQuery("SELECT DISTINCT(Occupation) FROM Ali_tblOccupation WHERE Occupation IS NOT NULL AND Occupation != ''").then((rows) => {
                                                        console.log("Migrating Occupation - Total Records: ", rows.length);
                                                        rows.forEach(function(value){
                                                            let query = `Insert INTO occupation (name) VALUES ("${value['Occupation']}")`;
                                                            mysql.query(query).then(/*console.log("Done")*/);
                                                        });
                                                        console.log("Migrated Occupation");
                                                        return Promise.resolve(null);

                                                    }).then(response => {
                                                        getDataByQuery("SELECT DISTINCT(Descr) FROM Ali_tblAcademicInstitution WHERE Descr IS NOT NULL AND Descr != '' ORDER BY Descr ASC").then((rows) => {
                                                            console.log("Migrating EducationalInstitution - Total Records: ", rows.length);
                                                            rows.forEach(function(value){
                                                                let query = `Insert INTO educational_institution (name) VALUES ("${value['Descr']}")`;
                                                                mysql.query(query).then(/*console.log("Done")*/);
                                                            });
                                                            console.log("Migrated EducationalInstitution");
                                                            return Promise.resolve(null);

                                                        }).then(response => {
                                                            getDataFromTable("Ali_tblPosition").then((rows) => {
                                                                console.log("Migrating Position - Total Records: ", rows.length);
                                                                rows.forEach(function(value){
                                                                    let query = `Insert INTO \`position\` (name, old_id) VALUES ("${value['Descr']}", ${value['PositionId']})`;
                                                                    mysql.query(query).then(/*console.log("Done")*/);
                                                                });
                                                                console.log("Migrated Position");
                                                                return Promise.resolve(null);

                                                            }).then(response => {
                                                                getDataByQuery("SELECT DISTINCT EducationLevel FROM Ali_tblPerson WHERE EducationLevel IS NOT NULL AND EducationLevel != ''").then((rows) => {
                                                                    console.log("Migrating SecularStudyLevel - Total Records: ", rows.length);
                                                                    rows.forEach(function(value){
                                                                        let query = `Insert INTO secular_study_level (name) VALUES ("${value['EducationLevel']}")`;
                                                                        mysql.query(query).then(/*console.log("Done")*/);
                                                                    });
                                                                    console.log("Migrated SecularStudyLevel");
                                                                    return Promise.resolve(null);

                                                                }).then(response => {
                                                                    getDataByQuery("SELECT DISTINCT InstitutionName FROM Ali_tblPublicService WHERE InstitutionName IS NOT NULL AND InstitutionName != ''").then((rows) => {
                                                                        console.log("Migrating PublicServiceInstitution - Total Records: ", rows.length);
                                                                        rows.forEach(function(value){
                                                                            let query = `Insert INTO public_service_institution (name) VALUES ("${value['InstitutionName']}")`;
                                                                            mysql.query(query).then(/*console.log("Done")*/);
                                                                        });
                                                                        console.log("Migrated PublicServiceInstitution");
                                                                        return Promise.resolve(null);

                                                                    });
                                                                })
                                                            })
                                                        })
                                                    })
                                                })
                                            })
                                        })
                                    })
                                })
                            })
                        })
                    })
                })
            })
        })
    })
}

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



executeMigration();
