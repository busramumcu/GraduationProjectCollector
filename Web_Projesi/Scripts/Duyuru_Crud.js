//Load Data in Table when documents is ready
$(document).ready(function () {
    loadData();
});
//Load Data function
function loadData() {
    $.ajax({
        url: "/LoggedKoordinator/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Duyuru_Id + '</td>';
                html += '<td>' + item.Duyuru_Basligi + '</td>';
                html += '<td>' + item.Duyuru_Icerigi + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.Duyuru_Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Duyuru_Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Add Data Function
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var duyuruObj = {
        Duyuru_Id: $('#Duyuru_Id').val(),
        Duyuru_Basligi: $('#Duyuru_Basligi').val(),
        Duyuru_Icerigi: $('#Duyuru_Icerigi').val()
    };
    $.ajax({
        url: "/LoggedKoordinator/Add",
        data: JSON.stringify(duyuruObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


////Function for getting the Data Based upon Employee ID
//function getbyID(EmpID) {
//    $('#Name').css('border-color', 'lightgrey');
//    $('#Age').css('border-color', 'lightgrey');
//    $('#State').css('border-color', 'lightgrey');
//    $('#Country').css('border-color', 'lightgrey');
//    $.ajax({
//        url: "/Home/getbyID/" + EmpID,
//        typr: "GET",
//        contentType: "application/json;charset=UTF-8",
//        dataType: "json",
//        success: function (result) {
//            $('#EmployeeID').val(result.EmployeeID);
//            $('#Name').val(result.Name);
//            $('#Age').val(result.Age);
//            $('#State').val(result.State);
//            $('#Country').val(result.Country);
//            $('#myModal').modal('show');
//            $('#btnUpdate').show();
//            $('#btnAdd').hide();
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//    return false;
//}
////function for updating employee's record
//function Update() {
//    var res = validate();
//    if (res == false) {
//        return false;
//    }
//    var empObj = {
//        EmployeeID: $('#EmployeeID').val(),
//        Name: $('#Name').val(),
//        Age: $('#Age').val(),
//        State: $('#State').val(),
//        Country: $('#Country').val(),
//    };
//    $.ajax({
//        url: "/Home/Update",
//        data: JSON.stringify(empObj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            loadData();
//            $('#myModal').modal('hide');
//            $('#EmployeeID').val("");
//            $('#Name').val("");
//            $('#Age').val("");
//            $('#State').val("");
//            $('#Country').val("");
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

//function for deleting employee's record
function Delele(ID) {
    var ans = confirm("Silmek İstediğinizden Emin Misiniz?");
    if (ans) {
        $.ajax({
            url: "/LoggedKoordinator/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#Duyuru_Id').val("");
    $('#Duyuru_Basligi').val("");
    $('#Duyuru_Icerigi').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Duyuru_Basligi').css('border-color', 'lightgrey');
    $('#Duyuru_Icerigi').css('border-color', 'lightgrey');
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Duyuru_Basligi').val().trim() == "") {
        $('#Duyuru_Basligi').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Duyuru_Basligi').css('border-color', 'lightgrey');
    }
    if ($('#Duyuru_Icerigi').val().trim() == "") {
        $('#Duyuru_Icerigi').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Duyuru_Icerigi').css('border-color', 'lightgrey');
    }
    return isValid;
}