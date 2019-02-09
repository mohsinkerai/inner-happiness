jQuery.fn.exists = function () { return this.length > 0; };

$(document).ready(function () {
    Initialize();
});

function InitializeTypeAhead(id, name, prefetchJson, remoteUrl, positionId, url) {
    try {
        //var json = JSON.parse(prefetchJson.replace(/&quot;/g, '"'));;
        //var localData = $.map(json, function (el) { return el });

        var dataSource = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            //prefetch: remoteUrl,
            //local: localData,
            remote: {
                url: remoteUrl + "/%QUERY",
                wildcard: '%QUERY',
                cache: false
            }
        });

        $("#" + id).typeahead(null, {
            name: name,
            hint: true,
            highlight: true,
            minLength: 3,
            source: dataSource
        });

        $("#" + id).bind('typeahead:select', function (ev, suggestion) {
            mApp.block("#nominations-table-" + positionId, {});
            $.ajax({
                type: "POST",
                url: url,
                data: { "id": positionId, "personId": suggestion },
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "html",
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    mApp.unblock("#nominations-table-" + positionId, {});
                    alert("Request: " +
                        xmlHttpRequest.toString() +
                        "\n\nStatus: " +
                        textStatus +
                        "\n\nError: " +
                        errorThrown);
                },
                success: function (result) {
                    if (result.length !== 4) {
                        $("#nominations-table-" + positionId).html(result);
                        InitializeNominationDataTableLite("nominations-" + positionId, "Nominations");
                        $("#nominations-" + positionId).css("min-height", "0px");
                        $("#person-lookup-" + positionId).val('').trigger('change');
                    }
                    mApp.unblock("#nominations-table-" + positionId, {});
                }
            });
        });
    }
    catch (err) { }
}

function Initialize() {
    $(".input-validation-error").parents(".form-group").addClass("has-danger");
    $(".m-select2")
        .each(function () {
            var placeholder = $(this).attr("placeholder");
            $(this)
                .select2({
                    placeholder: placeholder,
                    allowClear: true
                });
        });
    $(".m-select2-multiple")
        .each(function () {
            var placeholder = $(this).attr("placeholder");
            $(this)
                .select2({
                    placeholder: placeholder,
                    tags: true,
                    allowClear: true
                });
        });
    $(".preserve-order").on("select2:select", function (evt) {
        var element = evt.params.data.element;
        var $element = $(element);

        $element.detach();
        $(this).append($element);
        $(this).trigger("change");
    });
    $(".date-picker").datepicker({
        todayHighlight: true,

        //orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
		},
		format: "dd/mm/yyyy",
        autoclose: true
    });

    $(".date-picker-year-only").datepicker({
        todayHighlight: true,
        //orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        },
        format: "yyyy",
        minViewMode: "years",
        autoclose: true
    });
    $(".date-picker-month-year-only").datepicker({
        todayHighlight: true,
        //orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        },
        format: "mm-yyyy",
        startView: "years",
        minViewMode: "months",
        autoclose: true,
        onClose: function (dateText, inst) {
            $(this).datepicker("setDate", new Date(inst.selectedYear, inst.selectedMonth, 1));
        }
    });

    $(document).on('focus', '.select2.select2-container', function (e) {
        // only open on original attempt - close focus event should not fire open
        if (e.originalEvent && $(this).find(".select2-selection--single").length > 0) {
            $(this).siblings('select:enabled').select2('open');
        }
    });

    //$(document).keydown(function (e) {
    //    // Listening tab button.
    //    if (e.which == 9) {
    //        tabPressed = true;
    //    }
    //});

    //$(document).on('focus', '.select2', function () {
    //    if (tabPressed) {
    //        tabPressed = false;
    //        $(this).siblings('select').select2('open');
    //    }
    //});
}

function IsEmpty(data) {
    if (typeof (data) == 'number' || typeof (data) == 'boolean') {
        return false;
    }
    if (typeof (data) == 'undefined' || data === null) {
        return true;
    }
    if (typeof (data.length) != 'undefined') {
        return data.length == 0;
    }
    var count = 0;
    for (var i in data) {
        if (data.hasOwnProperty(i)) {
            count++;
        }
    }
    return count == 0;
}

function toggleNotMe() {
    $("#toggleNotMe").hide();
    $("#loginTitle").html("Login To Your Account");
    $("#CustomerCode").val("");
    $("#RememberMe").attr("checked", false);
    deleteCookie("RememberMe");
    deleteCookie("Company");
}

var deleteCookie = function (name) {
    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:01 GMT;";
};

function LanguageListEdit(id, language, read, write, speak) {
    $("#Language").val(language).trigger('change');
    $("#Read").val(read).trigger('change');
    $("#Write").val(write).trigger('change');
    $("#Speak").val(speak).trigger('change');
    $("#language-id").val(id);

    $("#language-row-" + id).addClass("m-datatable__row--hover");
}

function EmploymentListEdit(id, nameOfOrganization, category, designation, location, employmentEmailAddress, employmentTelephone, typeOfBusiness, natureOfBusiness, natureOfBusinessOther, employmentStartDate, employmentEndDate, endDateForDisplay) {
    $("#NameOfOrganization").val(nameOfOrganization).trigger('change');
    $("#EmploymentCategory").val(category).trigger('change');
    $("#Designation").val(designation).trigger('change');
    $("#Location").val(location).trigger('change');
    $("#EmploymentEmailAddress").val(employmentEmailAddress).trigger('change');
    $("#EmploymentTelephone").val(employmentTelephone).trigger('change');
    $("#TypeOfBusiness").val(typeOfBusiness).trigger('change');
    $("#NatureOfBusiness").val(natureOfBusiness).trigger('change');
	$("#NatureOfBusinessOther").val(natureOfBusinessOther).trigger('change');	
	$("#EmploymentStartDate").val(moment(employmentStartDate).format("MM-YYYY")).trigger('change');
	$("#EmploymentEndDate").val(moment(employmentEndDate).format("MM-YYYY")).trigger('change');
	$("#employment-id").val(id);

	if (endDateForDisplay == "") {
		$("#IsContinued").prop("checked", true);
		$("#EmploymentEndDate").prop("disabled", true);
	} else {
		$("#IsContinued").prop("checked", false);
		$("#EmploymentEndDate").prop("disabled", false);
	}

    $("#employment-row-" + id).addClass("m-datatable__row--hover");
}

function FamilyInformationListEdit(id, relativeCnic, relativeSalutation, relativeFirstName, relativeFathersName, relativeFamilyName, relativeJamatiTitle, relativeDateOfBirth, relativeRelation) {
    $("#RelativeCnic").val(relativeCnic).trigger('change');
    $("#RelativeSalutation").val(relativeSalutation).trigger('change');
    $("#RelativeFirstName").val(relativeFirstName).trigger('change');
    $("#RelativeFathersName").val(relativeFathersName).trigger('change');
    $("#RelativeFamilyName").val(relativeFamilyName).trigger('change');
    $("#RelativeJamatiTitle").val(relativeJamatiTitle).trigger('change');
    $("#RelativeDateOfBirth").val(relativeDateOfBirth).trigger('change');
    $("#RelativeRelation").val(relativeRelation).trigger('change');
	$("#family-information-id").val(id);
	
    $("#family-relation-row-" + id).addClass("m-datatable__row--hover");
}

function ProfessionalTrainingListEdit(id, training, institution, country, month, year, date) {
    $("#ProfesisonalTraining").val(training).trigger('change');
    $("#ProfessionalTrainingInstitution").val(institution).trigger('change');
    $("#ProfessionalTrainingCountry").val(country).trigger('change');
    //$("#ProfessionalTrainingMonth").val(month).trigger('change');
    //$("#ProfessionalTrainingYear").val(year).trigger('change');
	$("#ProfessionalTrainingDate").val(moment(month).format("MM-YYYY")).trigger('change');
    $("#professional-training-id").val(id);

    $("#professional-training-row-" + id).addClass("m-datatable__row--hover");
}

function AkdnTrainingListEdit(id, training, country, month, year, date) {
    $("#AkdnTraining").val(training).trigger('change');
    $("#AkdnTrainingCountry").val(country).trigger('change');
    //$("#AkdnTrainingMonth").val(month).trigger('change');
    //$("#AkdnTrainingYear").val(year).trigger('change');
	$("#AkdnTrainingDate").val(moment(date).format("MM-YYYY")).trigger('change');
    $("#akdn-training-id").val(id);

    $("#akdn-training-row-" + id).addClass("m-datatable__row--hover");
}

function VoluntaryCommunityListEdit(id, institution, fromYear, toYear, position, cycle) {
    if (cycle !== "") {
        $($(".is-imamat-appointee")[0]).prop("checked", true).trigger('change');
        $("#VoluntaryCommunityCycle").val(cycle).trigger('change');
    } else {
        $($(".is-imamat-appointee")[1]).prop("checked", true).trigger('change');
        $("#VoluntaryCommunityFromYear").val(fromYear).trigger('change');
        $("#VoluntaryCommunityToYear").val(toYear).trigger('change');
    }
    $("#VoluntaryCommunityInstitution").val(institution).trigger('change');
    $("#VoluntaryCommunityPosition").val(position).trigger('change');
    $("#voluntary-community-id").val(id);

    $("#voluntary-community-row-" + id).addClass("m-datatable__row--hover");
}

function VoluntaryPublicListEdit(id, institution, fromYear, toYear, position) {
    $("#VoluntaryPublicInstitution").val(institution).trigger('change');
    $("#VoluntaryPublicFromYear").val(fromYear).trigger('change');
    $("#VoluntaryPublicToYear").val(toYear).trigger('change');
    $("#VoluntaryPublicPosition").val(position).trigger('change');
    $("#voluntary-public-id").val(id);

    $("#voluntary-public-row-" + id).addClass("m-datatable__row--hover");
}

function EducationListEdit(id, institution, countryOfStudy, fromYear, toYear, nameOfDegree, majorAreaOfStudy) {
    $("#Institution").val(institution).trigger('change');
	$("#CountryOfStudy").val(countryOfStudy).trigger('change');
	$("#FromYear").val(fromYear).trigger('change');
	$("#ToYear").val(toYear).trigger('change');
	$("#NameOfDegree").val(nameOfDegree).trigger('change');
	$("#MajorAreaOfStudy").val(majorAreaOfStudy).trigger('change');
    $("#education-id").val(id);

    $("#education-row-" + id).addClass("m-datatable__row--hover");
}

function LanguageListDelete(url, id, reOrderUrl) {
    mApp.block("#language-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#language-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#language-table").html(result);
                InitializeDataTableLiteWithRowReordering("language", "Languages", reOrderUrl);
                $("#Language").val('').trigger('change');
                $("#Read").val('').trigger('change');
                $("#Write").val('').trigger('change');
                $("#Speak").val('').trigger('change');
                $("#language-id").val('');
            }
            mApp.unblock("#language-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function EmploymentListDelete(url, id, reOrderUrl) {
    mApp.block("#employment-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#employment-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#employment-table").html(result);
                InitializeDataTableLiteWithRowReordering("employment", "Employments", reOrderUrl);
                $("#NameOfOrganization").val('').trigger('change');
                $("#Designation").val('').trigger('change');
                $("#Location").val('').trigger('change');
                $("#EmploymentEmailAddress").val('').trigger('change');
                $("#EmploymentTelephone").val('').trigger('change');
                $("#TypeOfBusiness").val('').trigger('change');
                $("#NatureOfBusiness").val('').trigger('change');
                $("#NatureOfBusinessOther").val('').trigger('change');
                $("#EmploymentStartDate").val('').trigger('change');
                $("#EmploymentEndDate").val('').trigger('change');
                $("#employment-id").val('');
            }
            mApp.unblock("#employment-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function FamilyInformationListDelete(url, id) {
    mApp.block("#family-relation-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#family-relation-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#family-relation-table").html(result);
                InitializeDataTableLite("family-relation", "Family Information");
                $("#RelativeCnic").val('').trigger('change');
                $("#RelativeSalutation").val('').trigger('change');
                $("#RelativeFirstName").val('').trigger('change');
                $("#RelativeFathersName").val('').trigger('change');
                $("#RelativeFamilyName").val('').trigger('change');
                $("#RelativeJamatiTitle").val('').trigger('change');
                $("#RelativeDateOfBirth").val('').trigger('change');
                $("#RelativeRelation").val('').trigger('change');
                $("#family-information-id").val('');
            }
            mApp.unblock("#family-relation-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function ProfessionalTrainingListDelete(url, id, reOrderUrl) {
    mApp.block("#professional-training-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#professional-training-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#professional-training-table").html(result);
                InitializeDataTableLiteWithRowReordering("professional-training", "Professional Trainings", reOrderUrl);
                $("#ProfesisonalTraining").val('').trigger('change');
                $("#ProfessionalTrainingInstitution").val('').trigger('change');
                $("#ProfessionalTrainingCountry").val('').trigger('change');
                $("#ProfessionalTrainingMonth").val('').trigger('change');
                $("#ProfessionalTrainingYear").val('').trigger('change');
                $("#professional-training-id").val('');
            }
            mApp.unblock("#professional-training-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function VoluntaryCommunityListDelete(url, id, reOrderUrl) {
    mApp.block("#voluntary-community-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#voluntary-community-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#voluntary-community-table").html(result);
                InitializeDataTableLiteWithRowReordering("voluntary-community", "Voluntary Community Service", reOrderUrl);
                $("#VoluntaryCommunityInstitution").val('').trigger('change');
                $("#VoluntaryCommunityFromYear").val('').trigger('change');
                $("#VoluntaryCommunityToYear").val('').trigger('change');
                $("#VoluntaryCommunityPosition").val('').trigger('change');
                $("#voluntary-community-id").val('');
            }
            mApp.unblock("#voluntary-community-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function VoluntaryPublicListDelete(url, id, reOrderUrl) {
    mApp.block("#voluntary-public-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#voluntary-public-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#voluntary-public-table").html(result);
                InitializeDataTableLiteWithRowReordering("voluntary-public", "Voluntary Public Service", reOrderUrl);
                $("#VoluntaryPublicInstitution").val('').trigger('change');
                $("#VoluntaryPublicFromYear").val('').trigger('change');
                $("#VoluntaryPublicToYear").val('').trigger('change');
                $("#VoluntaryPublicPosition").val('').trigger('change');
                $("#voluntary-public-id").val('');
            }
            mApp.unblock("#voluntary-public-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function EducationListDelete(url, id, reOrderUrl) {
    mApp.block("#education-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#education-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#education-table").html(result);
                InitializeDataTableLiteWithRowReordering("education", "Education", reOrderUrl);
                $("#Institution").val('').trigger('change');
                $("#CountryOfStudy").val('').trigger('change');
                $("#FromYear").val('').trigger('change');
                $("#ToYear").val('').trigger('change');
                $("#NameOfDegree").val('').trigger('change');
                $("#MajorAreaOfStudy").val('').trigger('change');
                $("#education-id").val('');
            }
            mApp.unblock("#education-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function AkdnTrainingListDelete(url, id, reOrderUrl) {
    mApp.block("#akdn-training-table", {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#akdn-training-table", {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#akdn-training-table").html(result);
                InitializeDataTableLiteWithRowReordering("akdn-training", "AKDN Trainings", reOrderUrl);
                $("#AkdnTraining").val('').trigger('change');
                $("#AkdnTrainingCountry").val('').trigger('change');
                $("#AkdnTrainingMonth").val('').trigger('change');
                $("#AkdnTrainingYear").val('').trigger('change');
                $("#akdn-training-id").val('');
            }
            mApp.unblock("#akdn-training-table", {});
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function LanguageListAdd(url, reOrderUrl) {
    mApp.block("#language-table", {});
    if ($("#Language").valid() && $("#Read").valid() && $("#Write").valid() && $("#Speak").valid()) {
        var languageId = $("#language-id").val();
        var language = $("#Language").val();
        var read = $("#Read").val();
        var write = $("#Write").val();
        var speak = $("#Speak").val();
        if (IsEmpty(languageId) &&
            IsEmpty(language) &&
            IsEmpty(read) &&
            IsEmpty(write) &&
            IsEmpty(speak)) {
            mApp.unblock("#language-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": languageId, "language": language, "read": read, "write": write, "speak": speak },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#language-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#language-table").html(result);
                    InitializeDataTableLiteWithRowReordering("language", "Languages", reOrderUrl);
                    $("#Language").val('').trigger('change');
                    $("#Read").val('').trigger('change');
                    $("#Write").val('').trigger('change');
                    $("#Speak").val('').trigger('change');
                    $("#language-id").val('');
                }
                mApp.unblock("#language-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function ProfessionalTrainingListAdd(url, reOrderUrl) {
    mApp.block("#professional-training-table", {});
    if ($("#ProfesisonalTraining").valid() && $("#ProfessionalTrainingInstitution").valid() && $("#ProfessionalTrainingCountry").valid() && $("#ProfessionalTrainingDate").valid()) {
        var trainingId = $("#professional-training-id").val();
        var training = $("#ProfesisonalTraining").val();
        var institution = $("#ProfessionalTrainingInstitution").val();
        var country = $("#ProfessionalTrainingCountry").val();
        //var month = $("#ProfessionalTrainingMonth").val();
        //var year = $("#ProfessionalTrainingYear").val();
        var date = $("#ProfessionalTrainingDate").val();
        if (IsEmpty(trainingId) &&
            IsEmpty(training) &&
            IsEmpty(institution) &&
            IsEmpty(country) &&
            IsEmpty(date)) {
            mApp.unblock("#professional-training-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": trainingId, "training": training, "institution": institution, "countryOfTarining": country, "date": date },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#professional-training-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#professional-training-table").html(result);
                    InitializeDataTableLiteWithRowReordering("professional-training", "Professional Trainings", reOrderUrl);
                    $("#ProfesisonalTraining").val('').trigger('change');
                    $("#ProfessionalTrainingInstitution").val('').trigger('change');
                    $("#ProfessionalTrainingCountry").val('').trigger('change');
                    //$("#ProfessionalTrainingMonth").val('').trigger('change');
                    //$("#ProfessionalTrainingYear").val('').trigger('change');
                    $("#ProfessionalTrainingDate").val('').trigger('change');
                    $("#professional-training-id").val('');
                }
                mApp.unblock("#professional-training-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function VoluntaryCommunityListAdd(url, reOrderUrl) {
    mApp.block("#voluntary-community-table", {});
    if ($("#VoluntaryCommunityInstitution").valid() && $("#VoluntaryCommunityFromYear").valid() && $("#VoluntaryCommunityFromYear").valid() && $("#VoluntaryCommunityToYear").valid() && $("#VoluntaryCommunityPosition").valid()) {
        var voluntaryCommunityId = $("#voluntary-community-id").val();
        var institution = $("#VoluntaryCommunityInstitution").val();
        var fromYear = $("#VoluntaryCommunityFromYear").val();
        var toYear = $("#VoluntaryCommunityToYear").val();
        var position = $("#VoluntaryCommunityPosition").val();
        var cycle = $("#VoluntaryCommunityCycle").val();
        var cycleName = $("#VoluntaryCommunityCycle :selected").text();
        var cycleToSend = cycle + "|" + cycleName;
        if (!$(".is-imamat-appointee")[0].checked) {
            cycleToSend = "";
        }
        if (IsEmpty(voluntaryCommunityId) &&
            IsEmpty(institution) &&
            IsEmpty(fromYear) &&
            IsEmpty(toYear) &&
            IsEmpty(position)) {
            mApp.unblock("#voluntary-community-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": voluntaryCommunityId, "institution": institution, "fromYear": fromYear, "toYear": toYear, "position": position, "cycle": cycleToSend },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#voluntary-community-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#voluntary-community-table").html(result);
                    InitializeDataTableLiteWithRowReordering("voluntary-community", "Voluntary Community Service", reOrderUrl);
                    $("#VoluntaryCommunityInstitution").val('').trigger('change');
                    $("#VoluntaryCommunityFromYear").val('').trigger('change');
                    $("#VoluntaryCommunityToYear").val('').trigger('change');
                    $("#VoluntaryCommunityPosition").val('').trigger('change');
                    $("#VoluntaryCommunityCycle").val('').trigger('change');
                    $("#voluntary-community-id").val('');
                }
                mApp.unblock("#voluntary-community-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function VoluntaryPublicListAdd(url, reOrderUrl) {
    mApp.block("#voluntary-public-table", {});
    if ($("#VoluntaryPublicInstitution").valid() && $("#VoluntaryPublicFromYear").valid() && $("#VoluntaryPublicFromYear").valid() && $("#VoluntaryPublicToYear").valid() && $("#VoluntaryPublicPosition").valid()) {
        var voluntaryPublicId = $("#voluntary-public-id").val();
        var institution = $("#VoluntaryPublicInstitution").val();
        var fromYear = $("#VoluntaryPublicFromYear").val();
        var toYear = $("#VoluntaryPublicToYear").val();
        var position = $("#VoluntaryPublicPosition").val();
        if (IsEmpty(voluntaryPublicId) &&
            IsEmpty(institution) &&
            IsEmpty(fromYear) &&
            IsEmpty(toYear) &&
            IsEmpty(position)) {
            mApp.unblock("#voluntary-public-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": voluntaryPublicId, "institution": institution, "fromYear": fromYear, "toYear": toYear, "position": position },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#voluntary-public-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#voluntary-public-table").html(result);
                    InitializeDataTableLiteWithRowReordering("voluntary-public", "Voluntary Public Service", reOrderUrl);
                    $("#VoluntaryPublicInstitution").val('').trigger('change');
                    $("#VoluntaryPublicFromYear").val('').trigger('change');
                    $("#VoluntaryPublicToYear").val('').trigger('change');
                    $("#VoluntaryPublicPosition").val('').trigger('change');
                    $("#voluntary-public-id").val('');
                }
                mApp.unblock("#voluntary-public-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function EducationListAdd(url, reOrderUrl) {
    mApp.block("#education-table", {});
    if ($("#Institution").valid() && $("#CountryOfStudy").valid() && $("#FromYear").valid() && $("#ToYear").valid() && $("#NameOfDegree").valid() && $("#MajorAreaOfStudy").valid()) {
        var educationId = $("#education-id").val();
        var institution = $("#Institution").val();
        var countryOfStudy = $("#CountryOfStudy").val();
        var fromYear = $("#FromYear").val();
		var toYear = $("#ToYear").val();
        var nameOfDegree = $("#NameOfDegree").val();
        var majorAreaOfStudy = $("#MajorAreaOfStudy").val();
        if (IsEmpty(educationId) &&
            IsEmpty(institution) &&
            IsEmpty(countryOfStudy) &&
            IsEmpty(fromYear) &&
            IsEmpty(toYear) &&
            IsEmpty(nameOfDegree) &&
            IsEmpty(majorAreaOfStudy)) {
            mApp.unblock("#education-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": educationId, "institution": institution, "countryOfStudy": countryOfStudy, "fromYear": fromYear, "toYear": toYear, "nameOfDegree": nameOfDegree, "majorAreaOfStudy": majorAreaOfStudy },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#education-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#education-table").html(result);
                    InitializeDataTableLiteWithRowReordering("education", "Education", reOrderUrl);
                    $("#Institution").val('').trigger('change');
                    $("#CountryOfStudy").val('').trigger('change');
                    $("#FromYear").val('').trigger('change');
                    $("#ToYear").val('').trigger('change');
                    $("#NameOfDegree").val('').trigger('change');
                    $("#MajorAreaOfStudy").val('').trigger('change');
                    $("#education-id").val('');
                }
                mApp.unblock("#education-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function EmploymentListAdd(url, reOrderUrl) {
    mApp.block("#employment-table", {});
    if ($("#NameOfOrganization").valid() && $("#EmploymentCategory").valid() && $("#Designation").valid() && $("#Location").valid() && $("#EmploymentEmailAddress").valid() && $("#EmploymentTelephone").valid() && $("#TypeOfBusiness").valid() && $("#NatureOfBusiness").valid() && $("#NatureOfBusinessOther").valid() && $("#EmploymentStartDate").valid() && $("#EmploymentEndDate").valid()) {
        var id = $("#employment-id").val();
        var name = $("#NameOfOrganization").val();
        var category = $("input[name='EmploymentCategory']:checked")[0].defaultValue;
        var designation = $("#Designation").val();
        var location = $("#Location").val();
        var email = $("#EmploymentEmailAddress").val();
        var phone = $("#EmploymentTelephone").val();
        var type = $("#TypeOfBusiness").val();
        var nature = $("#NatureOfBusiness").val();
        var other = $("#NatureOfBusinessOther").val();
        var start = $("#EmploymentStartDate").val();
		var end = $("#EmploymentEndDate").val();

		var isContine = document.getElementById("IsContinued").checked

		if (isContine) {
			end = null;
		}

        if (IsEmpty(id) &&
            IsEmpty(name) &&
            IsEmpty(category) &&
            IsEmpty(designation) &&
            IsEmpty(location) &&
            IsEmpty(email) &&
            IsEmpty(phone) &&
            IsEmpty(type) &&
            IsEmpty(nature) &&
            IsEmpty(other) &&
            IsEmpty(start) &&
            IsEmpty(end)) {
            mApp.unblock("#employment-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": id, "nameOfOrganization": name, "category": category, "designation": designation, "location": location, "employmentEmailAddress": email, "employmentTelephone": phone, "typeOfBusiness": type, "natureOfBusiness": nature, "NatureOfBusinessOther": other, "employmentStartDate": start, "employmentEndDate": end },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#employment-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#employment-table").html(result);
                    InitializeDataTableLiteWithRowReordering("employment", "Employments", reOrderUrl);
                    $("#NameOfOrganization").val('').trigger('change');
                    $("#EmploymentCategory").val('').trigger('change');
                    $("#Designation").val('').trigger('change');
                    $("#Location").val('').trigger('change');
                    $("#EmploymentEmailAddress").val('').trigger('change');
                    $("#EmploymentTelephone").val('').trigger('change');
                    $("#TypeOfBusiness").val('').trigger('change');
                    $("#NatureOfBusiness").val('').trigger('change');
                    $("#NatureOfBusinessOther").val('').trigger('change');
                    $("#EmploymentStartDate").val('').trigger('change');
                    $("#EmploymentEndDate").val('').trigger('change');
                    $("#employment-id").val('');
                }
                mApp.unblock("#employment-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function FamilyInformationListAdd(url) {
    mApp.block("#family-relation-table", {});
    if ($("#RelativeCnic").valid() && $("#RelativeSalutation").valid() && $("#RelativeFirstName").valid() && $("#RelativeFathersName").valid() && $("#RelativeFamilyName").valid() && $("#RelativeJamatiTitle").valid() && $("#RelativeDateOfBirth").valid() && $("#RelativeRelation").valid()) {
        var id = $("#family-information-id").val();
        var relativeCnic = $("#RelativeCnic").val();
        var relativeSalutation = $("#RelativeSalutation").val();
        var relativeFirstName = $("#RelativeFirstName").val();
        var relativeFathersName = $("#RelativeFathersName").val();
        var relativeFamilyName = $("#RelativeFamilyName").val();
        var relativeJamatiTitle = $("#RelativeJamatiTitle").val();
        var relativeDateOfBirth = $("#RelativeDateOfBirth").val();
        var relativeRelation = $("#RelativeRelation").val();
        var personId = $("#RelativePersonId").val();
        var cycle = $("#RelativeCycle").val();
        var position = $("#RelativePosition").val();
        var institution = $("#RelativeInstitution").val();
        if (IsEmpty(id) &&
            IsEmpty(relativeCnic) &&
            IsEmpty(relativeSalutation) &&
            IsEmpty(relativeFirstName) &&
            IsEmpty(relativeFathersName) &&
            IsEmpty(relativeFamilyName) &&
            IsEmpty(relativeJamatiTitle) &&
            IsEmpty(relativeDateOfBirth) &&
            IsEmpty(relativeRelation) &&
            IsEmpty(personId) &&
            IsEmpty(cycle) &&
            IsEmpty(position) &&
            IsEmpty(institution)) {
            mApp.unblock("#family-relation-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": id, "relativeCnic": relativeCnic, "relativeSalutation": relativeSalutation, "relativeFirstName": relativeFirstName, "relativeFathersName": relativeFathersName, "relativeFamilyName": relativeFamilyName, "relativeJamatiTitle": relativeJamatiTitle, "relativeDateOfBirth": relativeDateOfBirth, "relativeRelation": relativeRelation, "personId": personId, "relativeCycle": cycle, "relativeInstitution": institution, "relativePosition": position },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#family-relation-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#family-relation-table").html(result);
                    InitializeDataTableLite("family-relation", "Family Information");
                    $("#RelativeCnic").val('').trigger('change');
                    $("#RelativeSalutation").val('').trigger('change');
                    $("#RelativeFirstName").val('').trigger('change');
                    $("#RelativeFathersName").val('').trigger('change');
                    $("#RelativeFamilyName").val('').trigger('change');
                    $("#RelativeJamatiTitle").val('').trigger('change');
                    $("#RelativeDateOfBirth").val('').trigger('change');
                    $("#RelativeRelation").val('').trigger('change');
                    $("#family-information-id").val('').trigger('change');
                    $("#RelativeFormNumber").val('').trigger('change');
                    $("#RelativeCycle").val('').trigger('change');
                    $("#RelativeInstitution").val('').trigger('change');
                    $("#RelativePosition").val('').trigger('change');
                }
                mApp.unblock("#family-relation-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function AkdnTrainingListAdd(url, reOrderUrl) {
    mApp.block("#akdn-training-table", {});
    if ($("#AkdnTraining").valid() && $("#AkdnTrainingCountry").valid() && $("#AkdnTrainingDate").valid()) {
        var trainingId = $("#akdn-training-id").val();
        var training = $("#AkdnTraining").val();
        var country = $("#AkdnTrainingCountry").val();
        //var month = $("#AkdnTrainingMonth").val();
        //var year = $("#AkdnTrainingYear").val();
        var date = $("#AkdnTrainingDate").val();
        if (IsEmpty(trainingId) &&
            IsEmpty(training) &&
            IsEmpty(country) &&
            IsEmpty(date)) {
            mApp.unblock("#akdn-training-table", {});
            alert("Please enter at least one value to proceed.");
        }
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": trainingId, "training": training, "countryOfTarining": country, "date": date },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("#akdn-training-table", {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                if (result.length !== 4) {
                    $("#akdn-training-table").html(result);
                    InitializeDataTableLiteWithRowReordering("akdn-training", "AKDN Trainings", reOrderUrl);
                    $("#AkdnTraining").val('').trigger('change');
                    $("#AkdnTrainingCountry").val('').trigger('change');
                    $("#AkdnTrainingDate").val('').trigger('change');
                    $("#akdn-training-id").val('');
                }
                mApp.unblock("#akdn-training-table", {});
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function InitializeDataTableLite(id, title) {
    var e;
    (e = $("#" + id)).DataTable({
        responsive: true,
        paging: false,
        info: false,
        filter: false
    });
}

function InitializeDataTableLiteWithRowReordering(id, title, url) {
    var table = $("#" + id).DataTable({
        responsive: true,
        paging: false,
        info: false,
        filter: false,
        rowReorder: true,
        order: [
            [0, "asc"]
        ]
    });

    table.on("row-reorder", function (e, diff, edit) {
        mApp.block("#" + id + "-table", {});

        if (diff.length !== 0) {
            var primary = table.row(diff[0].node).data()[1];
            var primaryId = diff[0].node.getAttribute("id")
                .substring(diff[0].node.getAttribute("id").lastIndexOf("row") + 4);
            var primaryPosition = diff[0].newData;
            var secondary = table.row(diff[1].node).data()[1];
            var secondaryId = diff[1].node.getAttribute("id")
                .substring(diff[1].node.getAttribute("id").lastIndexOf("row") + 4);
            var secondaryPosition = diff[1].newData;

            $.ajax({
                type: "POST",
                url: url,
                data: {
                    "primaryId": primaryId,
                    "primaryPosition": primaryPosition,
                    "secondaryId": secondaryId,
                    "secondaryPosition": secondaryPosition
                },
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "html",
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    mApp.unblock("#" + id + "-table", {});
                    alert("Request: " +
                        xmlHttpRequest.toString() +
                        "\n\nStatus: " +
                        textStatus +
                        "\n\nError: " +
                        errorThrown);
                },
                success: function(result) {
                    if (result.length !== 4) {
                        $("#" + id + "-table").html(result);
                        InitializeDataTableLiteWithRowReordering(id, title, url);
                        $("#" + id).css("min-height", "0px");
                    }
                    mApp.unblock("#" + id + "-table", {});
                }
            });
        }
    });
}

function InitializeDataTable(id, title) {


    var datatable = $("#" + id).mDatatable({
        // datasource definition
        data: {
            //type: 'remote',
            //source: {
            //    read: {
            //        // sample GET method
            //        method: 'GET',
            //        url: 'http://keenthemes.com/metronic/preview/inc/api/datatables/demos/default.php',
            //        map: function (raw) {
            //            // sample data mapping
            //            var dataSet = raw;
            //            if (typeof raw.data !== 'undefined') {
            //                dataSet = raw.data;
            //            }
            //            return dataSet;
            //        },
            //    },
            //},
            //pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        },

        // layout definition
        layout: {
            theme: "default", // datatable theme
            class: "", // custom wrapper class
            scroll: true, // enable/disable datatable scroll both horizontal and vertical when needed.
            footer: false // display/hide footer
        },

        // column sorting
        sortable: true,

        pagination: true,

        toolbar: {
            // toolbar items
            items: {
                // pagination
                pagination: {
                    // page size select
                    pageSizeSelect: [10, 20, 30, 50, 100],
                },
            },
        },

        search: {
            input: $("#generalSearch")
        }

        // columns definition
        //       columns: [
        //           {
        //               field: 'RecordID',
        //               title: '#',
        //               sortable: false, // disable sort for this column
        //               width: 40,
        //               selector: false,
        //               textAlign: 'center',
        //           }, {
        //               field: 'OrderID',
        //               title: 'Order ID',
        //               // sortable: 'asc', // default sort
        //               filterable: false, // disable or enable filtering
        //               width: 150,
        //               // basic templating support for column rendering,
        //               template: '{{OrderID}} - {{ShipCountry}}',
        //           }, {
        //               field: 'ShipCountry',
        //               title: 'Ship Country',
        //               width: 150,
        //               template: function (row) {
        //                   // callback function support for column rendering
        //                   return row.ShipCountry + ' - ' + row.ShipCity;
        //               },
        //           }, {
        //               field: 'ShipCity',
        //               title: 'Ship City',
        //           }, {
        //               field: 'Currency',
        //               title: 'Currency',
        //               width: 100,
        //           }, {
        //               field: 'ShipDate',
        //               title: 'Ship Date',
        //               sortable: 'asc',
        //               type: 'date',
        //               format: 'MM/DD/YYYY',
        //           }, {
        //               field: 'Latitude',
        //               title: 'Latitude',
        //               type: 'number',
        //           }, {
        //               field: 'Status',
        //               title: 'Status',
        //               // callback function support for column rendering
        //               template: function (row) {
        //                   var status = {
        //                       1: { 'title': 'Pending', 'class': 'm-badge--brand' },
        //                       2: { 'title': 'Delivered', 'class': ' m-badge--metal' },
        //                       3: { 'title': 'Canceled', 'class': ' m-badge--primary' },
        //                       4: { 'title': 'Success', 'class': ' m-badge--success' },
        //                       5: { 'title': 'Info', 'class': ' m-badge--info' },
        //                       6: { 'title': 'Danger', 'class': ' m-badge--danger' },
        //                       7: { 'title': 'Warning', 'class': ' m-badge--warning' },
        //                   };
        //                   return '<span class="m-badge ' + status[row.Status].class + ' m-badge--wide">' + status[row.Status].title + '</span>';
        //               },
        //           }, {
        //               field: 'Type',
        //               title: 'Type',
        //               // callback function support for column rendering
        //               template: function (row) {
        //                   var status = {
        //                       1: { 'title': 'Online', 'state': 'danger' },
        //                       2: { 'title': 'Retail', 'state': 'primary' },
        //                       3: { 'title': 'Direct', 'state': 'accent' },
        //                   };
        //                   return '<span class="m-badge m-badge--' + status[row.Type].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + status[row.Type].state + '">' +
        //                       status[row.Type].title + '</span>';
        //               },
        //           }, {
        //               field: 'Actions',
        //               width: 110,
        //               title: 'Actions',
        //               sortable: false,
        //               overflow: 'visible',
        //               template: function (row, index, datatable) {
        //                   var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
        //                   return '\
        //	<div class="dropdown ' + dropup + '">\
        //		<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
        //                           <i class="la la-ellipsis-h"></i>\
        //                       </a>\
        //	  	<div class="dropdown-menu dropdown-menu-right">\
        //	    	<a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\
        //	    	<a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\
        //	    	<a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\
        //	  	</div>\
        //	</div>\
        //	<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Edit details">\
        //		<i class="la la-edit"></i>\
        //	</a>\
        //	<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\
        //		<i class="la la-trash"></i>\
        //	</a>\
        //';
        //               },
        //           }],
    });

    var query = datatable.getDataSourceQuery();

    $("#m_form_status").on("change", function () {
        // shortcode to datatable.getDataSourceParam('query');
        var query = datatable.getDataSourceQuery();
        query.Status = $(this).val().toLowerCase();
        // shortcode to datatable.setDataSourceParam('query', query);
        datatable.setDataSourceQuery(query);
        datatable.load();
    }).val(typeof query.Status !== "undefined" ? query.Status : "");

    $("#m_form_type").on("change", function () {
        // shortcode to datatable.getDataSourceParam('query');
        var query = datatable.getDataSourceQuery();
        query.Type = $(this).val().toLowerCase();
        // shortcode to datatable.setDataSourceParam('query', query);
        datatable.setDataSourceQuery(query);
        datatable.load();
    }).val(typeof query.Type !== "undefined" ? query.Type : "");

    $("#m_form_status, #m_form_type").selectpicker();
}

function InitializeServerSideAdministratorDataTable(id, title, url) {
    var table = $("#" + id);

    var fixedHeaderOffset = 0;
    if (App.getViewPort().width < App.getResponsiveBreakpoint("md")) {
        if ($(".page-header").hasClass("page-header-fixed-mobile")) {
            fixedHeaderOffset = $(".page-header").outerHeight(true);
        }
    } else if ($(".page-header").hasClass("navbar-fixed-top")) {
        fixedHeaderOffset = $(".page-header").outerHeight(true);
    }

    var oTable = table.dataTable({
        // Internationalisation. For more info refer to http://datatables.net/manual/i18n
        "language": {
            "aria": {
                "sortAscending": ": activate to sort column ascending",
                "sortDescending": ": activate to sort column descending"
            },
            "emptyTable": "No data available in table",
            "info": "Showing _START_ to _END_ of _TOTAL_ records",
            "infoEmpty": "No records found",
            "infoFiltered": "(filtered1 from _MAX_ total records)",
            "lengthMenu": "Show _MENU_",
            "search": "Search:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "previous": "Prev",
                "next": "Next",
                "last": "Last",
                "first": "First"
            }
        },
        //"bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.
        "lengthMenu": [
            [5, 10, 20, -1],
            [5, 10, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 10,
        "columnDefs": [
            {
                "targets": [10],
                "visible": false,
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [11],
                "visible": false,
                "searchable": false,
                "orderable": false
            },
            {
                "targets": [12],
                "visible": false,
                "searchable": false,
                "orderable": false
            }
        ],
        // setup rowreorder extension: http://datatables.net/extensions/fixedheader/
        fixedHeader: {
            header: true,
            headerOffset: fixedHeaderOffset
        },
        //"order": [
        //    [0, "asc"]
        //],
        "pagingType": "bootstrap_full_number",
        buttons: [
            {
                extend: "print",
                className: "btn default blue-stripe blue-stripe",
                exportOptions: {
                    columns: ":visible"
                }
            },
            {
                extend: "copy",
                className: "btn default blue-stripe",
                exportOptions: {
                    columns: ":visible"
                }
            },
            {
                extend: "pdf",
                className: "btn default blue-stripe",
                exportOptions: {
                    columns: ":visible"
                }
            },
            {
                extend: "excel",
                className: "btn default blue-stripe",
                exportOptions: {
                    columns: ":visible"
                }
            },
            {
                extend: "csv",
                className: "btn default blue-stripe",
                exportOptions: {
                    columns: ":visible"
                }
            },
            {
                extend: "colvis",
                className: "btn default blue-stripe",
                text: "Columns"
            }
        ],
        // setup responsive extension: http://datatables.net/extensions/responsive/
        //responsive: true,
        "dom":
            "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
        colReorder: true,
        processing: true,
        serverSide: true,
        ajax: {
            "url": url,
            "type": "POST"
        },
        //ajax: url,
        columns: [
            {
                "data": "formNumber"
            },
            {
                "data": "fullName"
            },
            {
                "data": "enteredOnForDisplay"
            },
            {
                "data": "enteredByUserForDisplay"
            },
            {
                "data": "submittedForVerificationOnForDisplay"
            },
            {
                "data": "sentForVerificationByUserForDisplay"
            },
            {
                "data": "verifiedOnForDisplay"
            },
            {
                "data": "approvedByUserForDisplay"
            },
            //{
            //    "data": "comments"
            //},
            {
                "data": "status"
            },
            {
                "data": "completionPercentage"
            },
            {
                "data": "isExported"
            },
            {
                "data": "exportedOnForDisplay"
            },
            {
                "data": "detailUrl"
            }
        ],
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData["formNumber"].includes("-A-")) {
                $(nRow).addClass("adult-row");
            } else {
                $(nRow).addClass("young-murid-row");
            }
            if (aData["status"] == 0) {
                $('td:eq(8)', nRow).html('<span class="label label-sm label-info"> New </span>');
            } else if (aData["status"] == 1) {
                $('td:eq(8)', nRow).html('<span class="label label-sm label-warning"> Pending </span>');
            } else if (aData["status"] == 2) {
                if (aData["isExported"] === true) {
                    $('td:eq(8)', nRow)
                        .html(
                            '<span class="label label-sm label-success"> Approved </span>&nbsp;<span class="label label-sm label-primary"> Exported to Exelare on ' +
                            aData["exportedOnForDisplay"] +
                            ' </span>');
                } else {
                    $('td:eq(8)', nRow).html('<span class="label label-sm label-success"> Approved </span>');
                }
            } else if (aData["status"] == 3) {
                $('td:eq(8)', nRow).html('<span class="label label-sm label-danger"> Rejected </span>');
            }
            $('td:eq(9)', nRow).html(aData["completionPercentage"] + ' %');
            $('td:eq(0)', nRow).html('<a href=' + aData["detailUrl"] + '>' + aData["formNumber"] + '</a>');
            // Bold the grade for all 'A' grade browsers
            //if (aData[4] == "A") {
            //    $('td:eq(4)', nRow).html('<b>A</b>'); // You can add your link here!!
            //    //like $('td:eq(4)', nRow).html( '<a href="'+ aData[4] +'">Click Here</b>' );
            //}
        },
        ordering: false,
        search: {
            'smart': true,
            'delay': 4000,
            'type': 'trailing' // 'blur', 'leading' could be other legal values
        }
    });
}

function InitializeServerSideDataTable(id, title, url) {
    var datatable = $("#" + id).mDatatable({
        processing: true,
        serverSide: true,
        ajax: {
            "url": url,
            "type": "POST"
        },
        layout: {
            theme: "default", // datatable theme
            class: "", // custom wrapper class
            scroll: true, // enable/disable datatable scroll both horizontal and vertical when needed.
            footer: false // display/hide footer
        },
        sortable: true,
        pagination: true,
        toolbar: {
            items: {
                pagination: {
                    pageSizeSelect: [10, 20, 30, 50, 100],
                }
            }
        },
        search: {
            input: $("#generalSearch"),
            smart: true,
            delay: 4000,
            type: "trailing"
        }
    });

    var query = datatable.getDataSourceQuery();

    $("#m_form_status").on("change", function () {
        // shortcode to datatable.getDataSourceParam('query');
        var query = datatable.getDataSourceQuery();
        query.Status = $(this).val().toLowerCase();
        // shortcode to datatable.setDataSourceParam('query', query);
        datatable.setDataSourceQuery(query);
        datatable.load();
    }).val(typeof query.Status !== "undefined" ? query.Status : "");

    $("#m_form_type").on("change", function () {
        // shortcode to datatable.getDataSourceParam('query');
        var query = datatable.getDataSourceQuery();
        query.Type = $(this).val().toLowerCase();
        // shortcode to datatable.setDataSourceParam('query', query);
        datatable.setDataSourceQuery(query);
        datatable.load();
    }).val(typeof query.Type !== "undefined" ? query.Type : "");

    $("#m_form_status, #m_form_type").selectpicker();
}

$(".delete-confirm").click(function () {
    swal({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: $(".delete-confirm").data("deleteurl"),
                data: { uid: $(".delete-confirm").data("uid") },
                type: "GET"
            })
                .done(function (data) {
                    if (data.success === true) {
                        swal({
                            title: "Deleted!",
                            text: "Record has been deleted!",
                            timer: 2000,
                            onOpen: function () {
                                swal.showLoading();
                            }
                        }).then(function (result) {
                            if (result.dismiss === "timer") {
                                window.location = $(".delete-confirm").data("redirecturl");
                            }
                        });
                        //swal("Deleted!", "User has been deleted!", "success");

                    }
                    else {
                        swal("Failure!", "The AJAX request failed!", "error");
                    }
                });
        }
    });
});

function ShowToastr(toastrType, message) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-center",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    if (toastrType === "success") {
        toastr.success(message);
    }
    else if (toastrType === "error") {
        toastr.error(message);
    }
    else if (toastrType === "info") {
        toastr.info(message);
    }
    else if (toastrType === "warning") {
        toastr.warning(message);
    }
}

function DeleteConfirm(uid, urlForDelete, urlToPost) {
    swal({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!"
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: urlForDelete,
                data: { uid: uid },
                type: "GET"
            })
                .done(function (data) {
                    if (data.success === true) {
                        swal({
                            title: "Deleted!",
                            text: "Record has been deleted!",
                            timer: 2000,
                            onOpen: function () {
                                swal.showLoading();
                            }
                        }).then(function (result) {
                            if (result.dismiss === "timer") {
                                window.location = urlToPost;
                            }
                        });
                        //swal("Deleted!", "User has been deleted!", "success");

                    }
                    else {
                        swal("Failure!", "The AJAX request failed!", "error");
                    }
                });
        }
    });
    return true;
}

function MakeCascadingDropDown(primaryDropdownClass, secondaryDropdownClass, url) {
    if ($("." + primaryDropdownClass).exists() && $("." + secondaryDropdownClass).exists()) {
        $("." + primaryDropdownClass)
            .change(function () {
                mApp.block("." + secondaryDropdownClass, {});
                var selectedPrimaryValue = $(this).val();
                LoadSecondaryDropDown(primaryDropdownClass, secondaryDropdownClass, selectedPrimaryValue, url, "");
            });
    }
}

function LoadSecondaryDropDown(primaryDropdownClass, secondaryDropdownClass, selectedPrimaryValue, url, selectedSecondaryValue) {
    if (selectedPrimaryValue === "" || selectedPrimaryValue === "0") {
        $("." + primaryDropdownClass).val([]);
        $("." + secondaryDropdownClass).empty().append("<option></option>").prop("disabled", true);
    } else {
        $.ajax({
            type: "GET",
            async: false,
            url: url,
            data: { "uid": selectedPrimaryValue },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                mApp.unblock("." + secondaryDropdownClass, {});
                alert("Request: " +
                    xmlHttpRequest.toString() +
                    "\n\nStatus: " +
                    textStatus +
                    "\n\nError: " +
                    errorThrown);
            },
            success: function (result) {
                $("." + secondaryDropdownClass)
                    .empty()
                    .append("<option></option>");
                $.each(result,
                    function (key, value) {
                        $("." + secondaryDropdownClass)
                            .append($("<option></option>")
                                .attr("value", value.value)
                                .text(value.text));
                    });
                $("." + secondaryDropdownClass).prop("disabled", false);
                if (selectedSecondaryValue !== "" || selectedSecondaryValue !== "0") {
                    $("." + secondaryDropdownClass).val(selectedSecondaryValue);
                }
                mApp.unblock("." + secondaryDropdownClass, {});
            }
        });
    }
}

function InitializePersonDataTable(id, title, url, personUrl, newPersonUrl, cnic) {
    var table = $("#" + id).DataTable({
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        filter: false,
        ajax: url,
        columns: [{
            data: "id"
        }, {
            data: "fullName"
        }, {
            data: "cnic"
        }, {
            data: "dateOfBirthForDisplay"
        }, {
            data: "Actions"
        }],
        columnDefs: [{
            targets: 4,
            title: "Actions",
            orderable: false,
            render: function (a, e, t, n) {
                return '<a href=' +
                    t["detailUrl"] +
                    ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="View Details"><i class="la la-eye"></i></a><a href=' +
                    t["editUrl"] +
                    ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="Edit Details"><i class="la la-edit"></i></a>';
            }
        }],
        colReorder: true,
        pagingType: "full_numbers",
        dom: "<'row'<'col-sm-6 text-left'B>>\n\t\t\t<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
        buttons: ["print", "copyHtml5", "excelHtml5", "csvHtml5", "pdfHtml5"],
        lengthMenu: [5, 10, 25, 50],
        pageLength: 10,
        language: {
            lengthMenu: "Display _MENU_"
        },
        order: [
            [0, "desc"]
        ]
    });

    table.on('xhr.dt',
        function (e, settings, json, xhr) {
			if (json.data.length === 1) {
				var id = json.data[0].id;
				window.location.href = personUrl + "/" + id;
			} else if (json.data.length === 0) {
				window.location.href = newPersonUrl + "/" + cnic;
			}
        });
}

function InitializeLitePersonDataTable(id, title, url) {
    var e;
    (e = $("#" + id)).DataTable({
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        filter: false,
        ajax: url,
        columns: [{
            data: "fullName"
        }, {
            data: "cnic"
        }, {
            data: "Actions"
        }],
        columnDefs: [{
            targets: 2,
            title: "Actions",
            orderable: false,
            render: function (a, e, t, n) {
                return '<a target="_blank" href=' +
                    t["detailUrl"] +
                    ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="Nominate"><i class="la la-bullseye"></i></a><a target="_blank" href=' +
                    t["detailUrl"] +
                    ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="View Details"><i class="la la-eye"></i></a><a target="_blank" href=' +
                    t["editUrl"] +
                    ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="Edit Details"><i class="la la-edit"></i></a>';
            }
        }],
        colReorder: true,
        pagingType: "full_numbers",
        dom: "<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
        //buttons: ["print", "copyHtml5", "excelHtml5", "csvHtml5", "pdfHtml5"],
        lengthMenu: [5, 10, 25, 50],
        pageLength: 10,
        language: {
            lengthMenu: "Display _MENU_"
        },
        order: [
            [0, "desc"]
        ]
    });
}

function InitializeInstitutionDataTable(id, title, url) {
    var e;
    (e = $("#" + id)).DataTable({
        responsive: true,
        searchDelay: 500,
        processing: true,
        serverSide: true,
        filter: false,
        ajax: url,
        columns: [{
            data: "positionName"
        }, {
            data: "fullName"
        }, {
            data: "Actions"
        }],
        columnDefs: [{
            targets: 2,
            title: "Actions",
            orderable: false,
            render: function (a, e, t, n) {
	            return '<a href=' +
		            t["detailUrl"] +
		            ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="View Details"><i class="la la-eye"></i></a><a target="_blank" href=' +
		            t["threePlusOneUrl"] +
		            ' class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only" title="Generate 3+1 Report"><i class="la la-sitemap"></i></a>';
            }
        }],
        colReorder: true,
        pagingType: "full_numbers",
        dom: "<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>\n\t\t\t<'row'<'col-sm-12'tr>>\n\t\t\t<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>",
        buttons: ["print", "copyHtml5", "excelHtml5", "csvHtml5", "pdfHtml5"],
        lengthMenu: [5, 10, 25, 50],
        pageLength: 10,
        language: {
            lengthMenu: "Display _MENU_"
        },
        order: [
            [0, "desc"]
        ]
    });
}

function InitializeNominationDataTableLite(id, title, url, positionId) {
    var table = $("#" + id).DataTable({
        responsive: true,
        paging: false,
        info: false,
        filter: false,
        rowReorder: true,
        order: [
            [0, "asc"]
        ]
        //"columnDefs": [
        //    {
        //        "targets": [0],
        //        "visible": false
        //    }
        //]
    });

    table.on("row-reorder", function (e, diff, edit) {
        mApp.block("#nominations-table-" + positionId, {});

        if (diff.length !== 0) {
            var primary = table.row(diff[0].node).data()[1];
            var primaryId = diff[0].node.getAttribute("id").substring(15);
            var primaryPosition = diff[0].newData;
            var secondary = table.row(diff[1].node).data()[1];
            var secondaryId = diff[1].node.getAttribute("id").substring(15);
            var secondaryPosition = diff[1].newData;

            $.ajax({
                type: "POST",
                url: url,
                data: {
                    "positionId": positionId,
                    "primaryId": primaryId,
                    "primaryPosition": primaryPosition,
                    "secondaryId": secondaryId,
                    "secondaryPosition": secondaryPosition
                },
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                dataType: "html",
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    mApp.unblock("#nominations-table-" + positionId, {});
                    alert("Request: " +
                        xmlHttpRequest.toString() +
                        "\n\nStatus: " +
                        textStatus +
                        "\n\nError: " +
                        errorThrown);
                },
                success: function(result) {
                    if (result.length !== 4) {
                        $("#nominations-table-" + positionId).html(result);
                        InitializeNominationDataTableLite("nominations-" + positionId, "Nominations");
                        $("#nominations-" + positionId).css("min-height", "0px");
                    }
                    mApp.unblock("#nominations-table-" + positionId, {});
                }
            });
        }
    });
}

function RemoveNomination(url, positionId, personId, personAppointmentId, seatId, id) {
    mApp.block("#nominations-table-" + positionId, {});
    $.ajax({
        type: "POST",
		url: url,
		data: { "positionId": positionId, "personId": personId, "personAppointmentId": personAppointmentId, "seatId": seatId, "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#nominations-table-" + positionId, {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.length !== 4) {
                $("#nominations-table-" + positionId).html(result);
                InitializeNominationDataTableLite("nominations-" + positionId, "Nominations");
                $("#nominations-" + positionId).css("min-height", "0px");
            }
            mApp.unblock("#nominations-table-" + positionId, {});
        }
    });
}

function Recommend(url, positionId, personId, personAppointmentId, seatId, id) {
    mApp.block("#nominations-table-" + positionId, {});
    $.ajax({
        type: "POST",
        url: url,
        data: { "positionId": positionId, "personId": personId, "personAppointmentId": personAppointmentId, "seatId": seatId, "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            mApp.unblock("#nominations-table-" + positionId, {});
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            if (result.startsWith('"Error - ')) {
	            alert(result);
            } else {
	            $("#nominations-table-" + positionId).html(result);
	            InitializeNominationDataTableLite("nominations-" + positionId, "Nominations");
	            $("#nominations-" + positionId).css("min-height", "0px");
            }
            mApp.unblock("#nominations-table-" + positionId, {});
        }
    });
}

function LoadDropDownViaAjax(dropDownClass, url, selectedValue, secondarySelectedValue, tertiarySelectedValue) {
    $("." + dropDownClass)
        .empty()
        .append("<option></option>");
    $.ajax({
        type: "GET",
        async: false,
        url: url,
        data: { "level": selectedValue, "subLevel": secondarySelectedValue, "type": tertiarySelectedValue },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            alert("Request: " +
                xmlHttpRequest.toString() +
                "\n\nStatus: " +
                textStatus +
                "\n\nError: " +
                errorThrown);
        },
        success: function (result) {
            $("." + dropDownClass)
                .empty()
                .append("<option></option>");
            $.each(result,
                function (key, value) {
                    $("." + dropDownClass)
                        .append($("<option></option>")
                            .attr("value", value.value)
                            .text(value.text));
                });
            $("." + dropDownClass).prop("disabled", false);
            $("." + dropDownClass).focus();
        }
    });
}

function CustomPersonValidation() {
    $(".formnumber").prop("disabled", false);
    if ($("#mainForm").valid()) {
        if (IsEmpty($("#Cnic").val()) && IsEmpty($("#PassportNumber").val())) {
            $(".formnumber").prop("disabled", true);
            alert("Either Cnic or Passport Number is required.");
            return false;
        } else {
            $("#mainForm").off("submit");
            $("#mainForm").submit();
        }
    } else {
        $('html, body').animate({
            scrollTop: ($('.field-validation-error').offset().top - 300)
        }, 2000);
        $(".formnumber").prop("disabled", true);
        return false;
    }
}

function InstitutionListAdd(url) {
	mApp.block("#institution-table", {});
		var institution = $("#InstitutionName").val();
		if (IsEmpty(institution)) {
			mApp.unblock("#institution-table", {});
			alert("Please enter at least one value to proceed.");
		}
		$.ajax({
			type: "POST",
			url: url,
			data: {"id":"", "institution": institution},
			contentType: "application/x-www-form-urlencoded; charset=utf-8",
			dataType: "html",
			error: function (xmlHttpRequest, textStatus, errorThrown) {
				mApp.unblock("#institution-table", {});
				alert("Request: " +
					xmlHttpRequest.toString() +
					"\n\nStatus: " +
					textStatus +
					"\n\nError: " +
					errorThrown);
			},
			success: function (result) {
				if (result.length !== 4) {
					$("#institution-table").html(result);
					$("#InstitutionName").val('').trigger('change');
				}
				mApp.unblock("#institution-table", {});
				//else {
				//    window.location.replace(window.loginUrl);
				//}
			}
	});
}

function InstitutionListDelete(url, id) {
	mApp.block("#institution-table", {});
	$.ajax({
		type: "POST",
		url: url,
		data: { "id": id },
		contentType: "application/x-www-form-urlencoded; charset=utf-8",
		dataType: "html",
		error: function (xmlHttpRequest, textStatus, errorThrown) {
			mApp.unblock("#institution-table", {});
			alert("Request: " +
				xmlHttpRequest.toString() +
				"\n\nStatus: " +
				textStatus +
				"\n\nError: " +
				errorThrown);
		},
		success: function (result) {
			if (result.length !== 4) {
				$("#institution-table").html(result);
				$("#InstitutionName").val('').trigger('change');
			}
			mApp.unblock("#institution-table", {});
		}
	});
}
