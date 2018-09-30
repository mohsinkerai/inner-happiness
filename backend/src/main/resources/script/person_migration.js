const mysql = require('./mysql-service');
const mssql = require('./mssql-service');


var languageMap = {};
var cityMap = {};
var jamatiTitleMap = {};
var salutationMap = {};
var jamatKhanaMap = {};
var occupationTypeMap = {};
var maritalStatusMap = {};
addValueToMaritalStatus('SNG', 1);
addValueToMaritalStatus('WDW', 2);
addValueToMaritalStatus('MRD', 3);
addValueToMaritalStatus('DIV', 4);
addValueToMaritalStatus('SEP', 5);

function addValueToCity(key, value) {
    //if the list is already created for the "key", then uses it
    //else creates new list for the "key" to store multiple values in it.
    cityMap[key] = cityMap[key] || [];
    cityMap[key].push(value);
}

function addValueToJamatiTitle(key, value) {
    //if the list is already created for the "key", then uses it
    //else creates new list for the "key" to store multiple values in it.
    jamatiTitleMap[key] = jamatiTitleMap[key] || [];
    jamatiTitleMap[key].push(value);
}

function addValueToMaritalStatus(key, value) {
    //if the list is already created for the "key", then uses it
    //else creates new list for the "key" to store multiple values in it.
    maritalStatusMap[key] = maritalStatusMap[key] || [];
    maritalStatusMap[key].push(value);
}

function addValueToSalutation(key, value) {
    //if the list is already created for the "key", then uses it
    //else creates new list for the "key" to store multiple values in it.
    salutationMap[key] = salutationMap[key] || [];
    salutationMap[key].push(value);
}

function addValueToJamatKhana(key, value) {
    //if the list is already created for the "key", then uses it
    //else creates new list for the "key" to store multiple values in it.
    jamatKhanaMap[key] = jamatKhanaMap[key] || [];
    jamatKhanaMap[key].push(value);
}

function getCity() {
    return mysql.query("SELECT * FROM city");
}

function getCountry() {
    return mysql.query("SELECT * FROM country");
}

function getEducationalInstitution() {
    return mysql.query("SELECT * FROM educational_institution");
}

function getEducationalDegree() {
    return mysql.query("SELECT * FROM educational_degree");
}

function getAreaOfStudy() {
    return mysql.query("SELECT * FROM area_of_study");
}

function getLanguage() {
    return mysql.query("SELECT * FROM language");
}

function getJamatiTitle() {
    return mysql.query("SELECT * FROM jamati_title");
}

function getSalutation() {
    return mysql.query("SELECT * FROM salutation");
}

function getJamatKhana() {
    return mysql.query("SELECT * FROM level WHERE level_type_id = 4");
}

function getPerson() {
    return mysql.query("SELECT * FROM person");
}

function getOccupationType() {
    return mysql.query("SELECT * FROM occupation");
}

function getBusinessType() {
    return mysql.query("SELECT * FROM business_type");
}

function getBusinessNature() {
    return mysql.query("SELECT * FROM business_nature");
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

function convertImage(image) {
    console.log('START');
    if(image == null || image == '' || image == undefined) {
        console.log('NULL');
        return null;
    }

    var bytes = [], str;

    for(var i=2; i< image.length-1; i+=2){
        bytes.push(parseInt(image.toString().substr(i, 2), 16));
    }

    var s = String.fromCharCode.apply(String, bytes);
    var buffer = new Buffer(s.toString(), 'binary');
    str = buffer.toString('base64');
    console.log('END');
    return str;
}

function syncPerson() {
    getCity().then((rows) => {
        rows.forEach(row => {
            if (row.id != null && row.old_id != null) {
                addValueToCity(row.old_id, row.id);
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getJamatiTitle().then((rows) => {
            rows.forEach(row => {
                if (row.id != null && row.old_id != null) {
                    addValueToJamatiTitle(row.old_id, row.id);
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            getSalutation().then((rows) => {
                rows.forEach(row => {
                    if (row.id != null && row.name != null) {
                        addValueToSalutation(row.name, row.id);
                    }
                });
                return Promise.resolve(null);
            }).then(response => {
                getJamatKhana().then((rows) => {
                    rows.forEach(row => {
                        if (row.old_id != null && row.id != null) {
                            addValueToJamatKhana(row.old_id, row.id);
                        }
                    });
                    return Promise.resolve(null);
                }).then(response => {
                    getDataByQuery("SELECT j.InstitutionId AS 'jkInstitutionId', p.* FROM Ali_tblPerson p LEFT JOIN Ali_tblJamatkhana j ON p.JKId = j.JKId").then((rows) => {
                        rows.forEach(function(value){
                            // mapping city_id
                            var cityCode = value['CityCode'];
                            var cityId = (cityCode == null || cityCode == '' || cityCode == ' ') ? null : cityMap[cityCode];

                            // mapping title_id
                            var titleId = value['TitleId'];
                            var jamatiTitleId = (titleId == null || titleId == '' || titleId == ' ') ? null : jamatiTitleMap[titleId];
                            jamatiTitleId = (jamatiTitleId == undefined) ? null : jamatiTitleId;

                            // mapping marital_status
                            var maritalStatus = value['MaritalStatus'];
                            var maritalStatusId = (maritalStatus == null || maritalStatus == '' || maritalStatus == ' ') ? null : maritalStatusMap[maritalStatus];

                            // mapping salutation_id
                            var salutation = value['SalutationId'];
                            var salutationId = (salutation == null || salutation == '' || salutation == ' ') ? null : salutationMap[salutation];

                            // mapping jamatkhana_id
                            //var jkInstitutionId = value['jkInstitutionId'];
                            //var jkLevelId = (jkInstitutionId == null || jkInstitutionId == '' || jkInstitutionId == ' ') ? 0 : jamatKhanaMap[jkInstitutionId];
                            //console.log('jkInstitutionId: ', jkInstitutionId, ' jkLevelId: ', jkLevelId);

                            // parsing birth_date
                            var birthDate = getDateTime(`${value['BirthDate']}`);
                            birthDate = birthDate == null ? null : `"${birthDate}"`;

                            // parsing death_date
                            var deathDate = getDateTime(value['DeathDate']);
                            deathDate = deathDate == null ? null : `"${deathDate}"`;

                            // parsing gender
                            var gender = value['Gender'];
                            gender = gender == 'M' ? 0 : 1;

                            // parsing hours_per_week
                            var hoursPerWeek = value['TimeCommitment'];
                            hoursPerWeek = (hoursPerWeek == null || hoursPerWeek == '' || hoursPerWeek == 'null') ? null : hoursPerWeek;

                            // parsing address
                            var address = value['Address'];
                            address = (address == null) ? '' : address.split('\"').join('\'');

                            // parsing skills
                            var skills = value['SkillsGot'];
                            skills = (skills == null) ? '' : skills.trim();
                            if(skills == '') {
                                skills = '{"skills":[]}';
                            } else {
                                var s = skills.split(',');
                                var skillsList = [];
                                s.forEach(function(value) {
                                    value = value.replace(/\s{2,}/g, ' ')
                                    value = value.replace(/["']+/g, "");
                                    skillsList.push(value.trim());
                                });
                                str = JSON.stringify(skillsList);
                                skills = '{"skills":' + str.trim() + '}';
                            }

                            // parsing old_code
                            var oldCode = value['OldCode'];
                            oldCode = (oldCode == null || oldCode == '' || oldCode == 'null') ? null : oldCode;

                            // parsing old_code
                            var relocateLocation = value['RelocationAddress'];
                            relocateLocation = (relocateLocation == null || relocateLocation == '' || relocateLocation == 'null') ? null : "\"" + relocateLocation + "\"";

                            let image = value['PhotoImage'];
                            image = convertImage(image);

                            var query = `INSERT INTO person (old_id, cnic, old_cnic, city, salutation, first_name, fathers_name, family_name, jamati_title, date_of_birth, residential_address, residence_telephone, mobile_phone, email_address, marital_status, regional_council, local_council, jamatkhana, relocate_location, highest_level_of_study, highest_level_of_study_other, hours_per_week, full_name, nc_form_no, eo_form_no, old_code, death_cause, death_date, gender, plan_to_relocate, skills, image) VALUES (${value['PersonId']}, "${value['CNIC']}", "${value['OLDNIC']}", ${cityId}, ${salutationId}, "${value['FirstName']}", "${value['MiddleName']}", "${value['LastName']}", ${jamatiTitleId}, ${birthDate}, "${address}", "${value['Phone']}", "${value['Mobile']}", "${value['EmailId']}", ${maritalStatusId}, 0, 0, 0, ${relocateLocation}, "${value['EducationLevel']}", "${value['OtherEducation']}", ${hoursPerWeek}, "${value['FullName']}", ${value['NCFormNo']}, ${value['EOFormNo']}, ${oldCode}, "${value['DeathCause']}", ${deathDate}, ${gender}, 0, '${skills}', '${image}')`;
                            mysql.query(query).then(console.log("Done"));
                        });
                    });
                });
            });
        });
    });
}

var personLanguageMap = {};
var proficiencyMap = {};
proficiencyMap['N'] = 1;    //Native
proficiencyMap['E'] = 2;    //Excellent
proficiencyMap['G'] = 3;    //Good
proficiencyMap['F'] = 4;    //Fair
proficiencyMap['X'] = 5;    //Not Available

function syncPersonLanguage() {
    getLanguage().then((rows) => {
        rows.forEach(row => {
            if (row.name != null && row.id != null) {
                languageMap[row.name] = row.id;
                //addValueToLanguage(row.name, row.id);
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataByQuery("SELECT * FROM Ali_tblLanguage ORDER BY PersonId").then((rows) => {
            rows.forEach(function(value) {
                let personId = value['PersonId'];
                let language = value['Language'];
                let languageId = languageMap[language];

                let readSkill = value['ReadSkill'];
                readSkill = (readSkill == null || readSkill == '') ? 'X' : readSkill;
                readSkill = proficiencyMap[readSkill];

                let writeSkill = value['WriteSkill'];
                writeSkill = (writeSkill == null || writeSkill == '') ? 'X' : writeSkill;
                writeSkill = proficiencyMap[writeSkill];

                let speakSkill = value['SpeakSkill'];
                speakSkill = (speakSkill == null || speakSkill == '') ? 'X' : speakSkill;
                speakSkill = proficiencyMap[speakSkill];

                var tempMap = {};
                tempMap['languageProficiencyId'] = language;
                tempMap['language'] = languageId;
                tempMap['write'] = writeSkill;
                tempMap['read'] = readSkill;
                tempMap['speak'] = speakSkill;

                var personLanguageList = (personLanguageMap[personId] == undefined) ? [] : personLanguageMap[personId];
                personLanguageList.push(tempMap);

                personLanguageMap[personId] = personLanguageList;
            });
            return Promise.resolve(null);
        }).then(response => {
            let personIds = Object.keys(personLanguageMap);
            personIds.reduce((promiseChain, id) =>
                promiseChain.then(() => {
                    var languages = JSON.stringify(personLanguageMap[id]);
                    var query = `UPDATE person SET language_proficiencies = '${languages}' WHERE old_id = ${id}`;
                    mysql.query(query).then(console.log("Done"));
                }),
                Promise.resolve()
            );
            //personIds.forEach(function(id) {
            //    var languages = JSON.stringify(personLanguageMap[id]);
            //    var query = `UPDATE person SET language_proficiencies = '${languages}' WHERE old_id = ${id}`;
            //    mysql.query(query).then(console.log("Done"));
            //})
        });
    });
}

var personPMMap = {};

function syncPersonProfessionalMembership() {
    getDataByQuery("SELECT * FROM Ali_tblProfessionalMembership WHERE InstitutionName IS NOT NULL ORDER BY PersonId").then((rows) => {
        rows.forEach(function(value) {
            let personId = value['PersonId'];
            let institutionName = value['InstitutionName'];
            institutionName = institutionName.replace("'","''");
            var personPMList = (personPMMap[personId] == undefined) ? [] : personPMMap[personId];
            personPMList.push(institutionName.trim());

            personPMMap[personId] = personPMList;
        });
        return Promise.resolve(null);
    }).then(response => {
        let personIds = Object.keys(personPMMap);
        console.log(personIds.length);
        personIds.forEach(function(id) {
            var professionalMemberships = JSON.stringify(personPMMap[id]);
            professionalMemberships = '{"professionalMemberships":' + professionalMemberships + '}';
            var query = `UPDATE person SET professional_memberships = '${professionalMemberships}' WHERE old_id = ${id}`;
            mysql.query(query).then(console.log("Done"));
        })
    });
}

var personMap = {};
var relationMap = {};
relationMap['FTH'] = 1;
relationMap['MTH'] = 2;
relationMap['BRO'] = 3;
relationMap['SIS'] = 4;
relationMap['SON'] = 5;
relationMap['DTR'] = 6;
relationMap['HSB'] = 7;
relationMap['WIF'] = 8;

function syncPersonRelationPerson() {
    getPerson().then((rows) => {
        rows.forEach(row => {
            if (row.id != null && row.old_id != null) {
                personMap[row.old_id] = row.id;
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataFromTable("Ali_tblRelative").then((rows) => {
            rows.forEach(function(value) {
                let personId = value['PersonId'];
                personId = (personMap[personId] == undefined) ? null : personMap[personId];
                let relativePersonId = value['RelativePersonId'];
                relativePersonId = (personMap[relativePersonId] == undefined) ? null : personMap[relativePersonId];
                let relationId = relationMap[value['Relationship']];

                if(personId != null && relativePersonId != null) {
                    var query = `INSERT INTO person_relation_person (first_person_id, second_person_id, relation_id) VALUES (${personId}, ${relativePersonId}, ${relationId})`;
                    mysql.query(query).then(/*console.log("Done")*/);
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            console.log('COMPLETED');
        });
    });
}

/*
 String educationId
 long institution
 long countryOfStudy
 long nameOfDegree
 int fromYear
 int toYear
 String majorAreaOfStudy
 */

var educationalDegreeMap = {};
var educationalInstitutionMap = {};
var areaOfStudyMap = {};
var countryMap = {};
var personEducationMap = {};

function syncPersonEducation() {
    getAreaOfStudy().then((rows) => {
        rows.forEach(row => {
            if (row.old_id != null && row.id != null) {
                areaOfStudyMap[row.old_id] = row.id;
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getCountry().then((rows) => {
            rows.forEach(row => {
                if (row.code != null && row.id != null) {
                    countryMap[row.code] = row.id;
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            getEducationalInstitution().then((rows) => {
                rows.forEach(row => {
                    if (row.old_id != null && row.id != null) {
                        educationalInstitutionMap[row.old_id] = row.id;
                    }
                });
                return Promise.resolve(null);
            }).then(response => {
                getEducationalDegree().then((rows) => {
                    rows.forEach(row => {
                        if (row.old_id != null && row.id != null) {
                            educationalDegreeMap[row.old_id] = row.id;
                        }
                    });
                    return Promise.resolve(null);
                }).then(response => {
                    getDataFromTable("Ali_tblEducation").then((rows) => {
                        rows.forEach(function(value) {
                            let personId = value['PersonId'];
                            let educationId = value['EducationId'];
                            let educationalInstitutionId = value['AcademicInstitutionId'];
                            educationalInstitutionId = educationalInstitutionMap[educationalInstitutionId];
                            educationalInstitutionId = (educationalInstitutionId == undefined) ? null : educationalInstitutionId;

                            let degreeId = value['DegreeId'];
                            degreeId = educationalDegreeMap[degreeId];
                            degreeId = (degreeId == undefined) ? null : degreeId;

                            let fromYear = value['FromYear'];
                            let toYear = value['ToYear'];

                            let countryCode = ['CountryCode'];
                            countryCode = countryMap[countryCode];
                            countryCode = (countryCode == undefined) ? null : countryCode;

                            let majorAreaOfStudyId = value['MajorStudyAreaId'];
                            majorAreaOfStudyId = areaOfStudyMap[majorAreaOfStudyId];
                            majorAreaOfStudyId = (majorAreaOfStudyId == undefined) ? null : majorAreaOfStudyId;

                            var tempMap = {};
                            tempMap['educationId'] = educationId;
                            tempMap['institution'] = educationalInstitutionId;
                            tempMap['countryOfStudy'] = countryCode;
                            tempMap['nameOfDegree'] = degreeId;
                            tempMap['fromYear'] = fromYear;
                            tempMap['toYear'] = toYear;
                            tempMap['majorAreaOfStudy'] = majorAreaOfStudyId;

                            var personEducationList = (personEducationMap[personId] == undefined) ? [] : personEducationMap[personId];
                            personEducationList.push(tempMap);

                            personEducationMap[personId] = personEducationList;
                        });
                        return Promise.resolve(null);
                    }).then(response => {
                        let personIds = Object.keys(personEducationMap);
                        personIds.reduce((promiseChain, id) =>
                            promiseChain.then(() => {
                                var educations = JSON.stringify(personEducationMap[id]);
                                var query = `UPDATE person SET educations = '${educations}' WHERE old_id = ${id}`;
                                mysql.query(query).then(/*console.log("Done")*/);
                            }),
                            Promise.resolve()
                        );
                    }).then(response => {
                        console.log('COMPLETED');
                    });
                });
            });
        });
    });
}

function syncOccupation() {
    getOccupationType().then((rows) => {
        rows.forEach(row => {
            if (row.name != null && row.id != null) {
                occupationTypeMap[row.name] = row.id;
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getDataFromTable('Ali_tblOccupation').then((rows) => {
            rows.forEach(function(value) {
                var personId = value['PersonId'];
                var occupation = value['Occupation'];
                occupation = (occupation == null || occupation == '') ? null : (occupationTypeMap[occupation.toString().toUpperCase()] == undefined) ? null : occupationTypeMap[occupation.toString().toUpperCase()];
                var companyName = value['CompanyName'];
                companyName = (companyName == null || companyName == '') ? '' : companyName.replace(/["']+/g, "");

                var query = `UPDATE person SET occupation_type = ${occupation}, occupation_others = '${companyName}' WHERE old_id = ${personId}`;
                mysql.query(query).then(/*console.log("Done")*/);
            });
            return Promise.resolve(null);
        }).then(response => {
            console.log('COMPLETED');
        });
    })
}

var businessTypeMap = {};
var businessNatureMap = {};
var personEmploymentMap = {};

function syncPersonEmployment() {
    getBusinessType().then((rows) => {
        rows.forEach(row => {
            if (row.name != null && row.id != null) {
                businessTypeMap[row.name] = row.id;
            }
        });
        return Promise.resolve(null);
    }).then(response => {
        getBusinessNature().then((rows) => {
            rows.forEach(row => {
                if (row.name != null && row.id != null) {
                    businessNatureMap[row.name] = row.id;
                }
            });
            return Promise.resolve(null);
        }).then(response => {
            getDataByQuery("select d.Descr, o.* from Ali_tblOccupation o join My_tblDesignation d on o.DesignationId = d.DesignationId").then((rows) => {
                rows.forEach(function(value) {
                    let personId = value['PersonId'];
                    let organization = value['CompanyName'];
                    organization = (organization == null) ? null : organization.replace(/["']+/g, "").trim();

                    let phoneNumber = value['Phone'];
                    phoneNumber = (phoneNumber == null) ? null : phoneNumber.replace(/["']+/g, "").trim();

                    let designation = value['Descr'];
                    designation = (designation == null) ? null : designation.replace(/["']+/g, "").trim();

                    let email = value['OfficeEmailId'];
                    email = (email == null) ? null : email.replace(/["']+/g, "").trim();

                    let address = value['Address'];
                    address = (address == null) ? null : address.replace(/["'\\]+/g, "").trim();
                    let businessType = value['BusinessType'];

                    businessType = businessTypeMap[businessType];
                    businessType = (businessType == undefined) ? null : businessType;

                    let businessNature = value['BusinessNature'];
                    businessNature = businessNatureMap[businessNature];
                    businessNature = (businessNature == undefined) ? null : businessNature;

                    var tempMap = {};
                    tempMap['employmentId'] = '';
                    tempMap['nameOfOrganization'] = organization;
                    tempMap['designation'] = designation;
                    tempMap['location'] = address;
                    tempMap['employmentEmailAddress'] = email;
                    tempMap['employmentTelephone'] = phoneNumber;
                    tempMap['businessType'] = businessType;
                    tempMap['businessNature'] = businessNature;

                    var personEmploymentList = (personEmploymentMap[personId] == undefined) ? [] : personEmploymentMap[personId];
                    personEmploymentList.push(tempMap);

                    personEmploymentMap[personId] = personEmploymentList;
                });
                return Promise.resolve(null);
            }).then(response => {
                let personIds = Object.keys(personEmploymentMap);
                personIds.reduce((promiseChain, id) =>
                    promiseChain.then(() => {
                        var employments = JSON.stringify(personEmploymentMap[id]);
                        var query = `UPDATE person SET employments = '${employments}' WHERE old_id = ${id}`;
                        mysql.query(query).then(/*console.log("Done")*/);
                    }),
                    Promise.resolve()
                );
            }).then(response => {
                console.log('COMPLETED');
            });
        });
    });
}

function syncImage() {
    getDataFromTable("Ali_tblPerson").then((rows) => {
        rows.forEach(function (value) {
            var image = value['PhotoImage'];

            if(image == null || image == '' || image == undefined) {
                image = null;
            } else {
                var bytes = [], str;

                for(var i=2; i< image.length-1; i+=2){
                    bytes.push(parseInt(image.toString().substr(i, 2), 16));
                }

                var s = String.fromCharCode.apply(String, bytes);
                var buffer = new Buffer(s.toString(), 'binary');
                str = buffer.toString('base64');
            }

            console.log(str);
        })
    }).then(response => {
        console.log('COMPLETED');
    })
}

//syncPerson();
//syncPersonLanguage();
//syncPersonProfessionalMembership();
//syncPersonRelationPerson();
//syncPersonEducation();
//syncOccupation();
//syncPersonEmployment();
syncImage();
