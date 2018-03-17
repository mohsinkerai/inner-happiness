jQuery.fn.exists = function () { return this.length > 0; };

$(document).ready(function () {
    $(".input-validation-error").parents(".form-group").addClass("has-danger");
    $(".m-select2")
        .each(function () {
            var placeholder = $(this).attr("placeholder");
            $(this)
                .select2({
                    placeholder: placeholder
                });
        });
    $(".m-select2-multiple")
        .each(function () {
            var placeholder = $(this).attr("placeholder");
            $(this)
                .select2({
                    placeholder: placeholder,
                    tags: true
                });
        });
    $(".date-picker").datepicker({
        todayHighlight: true,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    });
});

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

    $("#language-row-" + id).addClass("selected");
}

function ProfessionalTrainingListEdit(id, training, institution, country, month, year) {
    $("#ProfesisonalTraining").val(training).trigger('change');
    $("#ProfessionalTrainingInstitution").val(institution).trigger('change');
    $("#ProfessionalTrainingCountry").val(country).trigger('change');
    $("#ProfessionalTrainingMonth").val(month).trigger('change');
    $("#ProfessionalTrainingYear").val(year).trigger('change');
    $("#professional-training-id").val(id);

    $("#professional-training-row-" + id).addClass("selected");
}

function AkdnTrainingListEdit(id, training, country, month, year) {
    $("#AkdnTraining").val(training).trigger('change');
    $("#AkdnTrainingCountry").val(country).trigger('change');
    $("#AkdnTrainingMonth").val(month).trigger('change');
    $("#AkdnTrainingYear").val(year).trigger('change');
    $("#akdn-training-id").val(id);

    $("#akdn-training-row-" + id).addClass("selected");
}

function EducationListEdit(id, institution, countryOfStudy, fromYear, toYear, nameOfDegree, majorAreaOfStudy) {
    $("#Institution").val(institution).trigger('change');
    $("#CountryOfStudy").val(countryOfStudy).trigger('change');
    $("#FromYear").val(fromYear).trigger('change');
    $("#ToYear").val(toYear).trigger('change');
    $("#NameOfDegree").val(nameOfDegree).trigger('change');
    $("#MajorAreaOfStudy").val(majorAreaOfStudy).trigger('change');
    $("#education-id").val(id);

    $("#education-row-" + id).addClass("selected");
}

function LanguageListDelete(url, id) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                InitializeDataTableLite("language", "Languages");
                $("#Language").val('').trigger('change');
                $("#Read").val('').trigger('change');
                $("#Write").val('').trigger('change');
                $("#Speak").val('').trigger('change');
                $("#language-id").val('');
            }
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function ProfessionalTrainingListDelete(url, id) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                InitializeDataTableLite("professional-training", "Professional Trainings");
                $("#ProfesisonalTraining").val('').trigger('change');
                $("#ProfessionalTrainingInstitution").val('').trigger('change');
                $("#ProfessionalTrainingCountry").val('').trigger('change');
                $("#ProfessionalTrainingMonth").val('').trigger('change');
                $("#ProfessionalTrainingYear").val('').trigger('change');
                $("#professional-training-id").val('');
            }
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function EducationListDelete(url, id) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                InitializeDataTableLite("education", "Education");
                $("#Institution").val('').trigger('change');
                $("#CountryOfStudy").val('').trigger('change');
                $("#FromYear").val('').trigger('change');
                $("#ToYear").val('').trigger('change');
                $("#NameOfDegree").val('').trigger('change');
                $("#MajorAreaOfStudy").val('').trigger('change');
                $("#education-id").val('');
            }
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function AkdnTrainingListDelete(url, id) {
    $.ajax({
        type: "POST",
        url: url,
        data: { "id": id },
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "html",
        error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                InitializeDataTableLite("akdn-training", "AKDN Trainings");
                $("#AkdnTraining").val('').trigger('change');
                $("#AkdnTrainingCountry").val('').trigger('change');
                $("#AkdnTrainingMonth").val('').trigger('change');
                $("#AkdnTrainingYear").val('').trigger('change');
                $("#akdn-training-id").val('');
            }
            //else {
            //    window.location.replace(window.loginUrl);
            //}
        }
    });
}

function LanguageListAdd(url) {
    if ($("#Language").valid() && $("#Read").valid() && $("#Write").valid() && $("#Speak").valid()) {
        var languageId = $("#language-id").val();
        var language = $("#Language").val();
        var read = $("#Read").val();
        var write = $("#Write").val();
        var speak = $("#Speak").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": languageId, "language": language, "read": read, "write": write, "speak": speak },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                    InitializeDataTableLite("language", "Languages");
                    $("#Language").val('').trigger('change');
                    $("#Read").val('').trigger('change');
                    $("#Write").val('').trigger('change');
                    $("#Speak").val('').trigger('change');
                    $("#language-id").val('');
                }
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function ProfessionalTrainingListAdd(url) {
    if ($("#ProfesisonalTraining").valid() && $("#ProfessionalTrainingInstitution").valid() && $("#ProfessionalTrainingCountry").valid() && $("#ProfessionalTrainingMonth").valid() && $("#ProfessionalTrainingYear").valid()) {
        var trainingId = $("#professional-training-id").val();
        var training = $("#ProfesisonalTraining").val();
        var institution = $("#ProfessionalTrainingInstitution").val();
        var country = $("#ProfessionalTrainingCountry").val();
        var month = $("#ProfessionalTrainingMonth").val();
        var year = $("#ProfessionalTrainingYear").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": trainingId, "training": training, "institution": institution, "countryOfTarining": country, "month": month, "year": year },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                    InitializeDataTableLite("professional-training", "Professional Trainings");
                    $("#ProfesisonalTraining").val('').trigger('change');
                    $("#ProfessionalTrainingInstitution").val('').trigger('change');
                    $("#ProfessionalTrainingCountry").val('').trigger('change');
                    $("#ProfessionalTrainingMonth").val('').trigger('change');
                    $("#ProfessionalTrainingYear").val('').trigger('change');
                    $("#professional-training-id").val('');
                }
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function EducationListAdd(url) {
    if ($("#Institution").valid() && $("#CountryOfStudy").valid() && $("#FromYear").valid() && $("#ToYear").valid() && $("#NameOfDegree").valid() && $("#MajorAreaOfStudy").valid()) {
        var educationId = $("#education-id").val();
        var institution = $("#Institution").val();
        var countryOfStudy = $("#CountryOfStudy").val();
        var fromYear = $("#FromYear").val();
        var toYear = $("#FromYear").val();
        var nameOfDegree = $("#NameOfDegree").val();
        var majorAreaOfStudy = $("#MajorAreaOfStudy").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": educationId, "institution": institution, "countryOfStudy": countryOfStudy, "fromYear": fromYear, "toYear": toYear, "nameOfDegree": nameOfDegree, "majorAreaOfStudy": majorAreaOfStudy },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                    InitializeDataTableLite("education", "Education");
                    $("#Institution").val('').trigger('change');
                    $("#CountryOfStudy").val('').trigger('change');
                    $("#FromYear").val('').trigger('change');
                    $("#ToYear").val('').trigger('change');
                    $("#NameOfDegree").val('').trigger('change');
                    $("#MajorAreaOfStudy").val('').trigger('change');
                    $("#education-id").val('');
                }
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function AkdnTrainingListAdd(url) {
    if ($("#AkdnTraining").valid() && $("#AkdnTrainingCountry").valid() && $("#AkdnTrainingMonth").valid() && $("#AkdnTrainingYear").valid()) {
        var trainingId = $("#akdn-training-id").val();
        var training = $("#AkdnTraining").val();
        var country = $("#AkdnTrainingCountry").val();
        var month = $("#AkdnTrainingMonth").val();
        var year = $("#AkdnTrainingYear").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { "id": trainingId, "training": training, "countryOfTarining": country, "month": month, "year": year },
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "html",
            error: function (xmlHttpRequest, textStatus, errorThrown) {
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
                    InitializeDataTableLite("akdn-training", "AKDN Trainings");
                    $("#AkdnTraining").val('').trigger('change');
                    $("#AkdnTrainingCountry").val('').trigger('change');
                    $("#AkdnTrainingMonth").val('').trigger('change');
                    $("#AkdnTrainingYear").val('').trigger('change');
                    $("#akdn-training-id").val('');
                }
                //else {
                //    window.location.replace(window.loginUrl);
                //}
            }
        });
    }
}

function InitializeDataTableLite(id, title) {
    var datatable = $("#" + id).mDatatable({
        pagination: false
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
            serverSorting: true,
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
            input: $("#generalSearch"),
        },

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
        "positionClass": "toast-top-right",
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
            }
        });
    }
}
