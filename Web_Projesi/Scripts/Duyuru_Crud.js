//Dokuman hazır olduğunda loadData işlemini gerçekleştir.
$(document).ready(function () {
    loadData();
});

//Duyuru verilerinin görüntülenmesi işlemi
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
                html += '<td><a href="#" data-target="#myModal" data-toggle="modal" onclick="return getbyID(' + item.Duyuru_Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Duyuru_Id + ')">Delete</a></td>';
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

function Update() {
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
        url: "/LoggedKoordinator/Update",
        data: JSON.stringify(duyuruObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Duyuru_Id').val("");
            $('#Duyuru_Basligi').val("");
            $('#Duyuru_Icerigi').val("");

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Duyuru ID
function getbyID(duyuru_Id) {
    $('#Duyuru_Basligi').css('border-color', 'lightgrey');
    $('#Duyuru_Icerigi').css('border-color', 'lightgrey');
    $.ajax({
        url: "/LoggedKoordinator/GetbyID/" + duyuru_Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log("Duyuru ID: " + result.Duyuru_Id);
            console.log("Duyuru_Basligi: " + result.Duyuru_Basligi);
            console.log("Duyuru_Icerigi " + result.Duyuru_Icerigi);
            $('#Duyuru_Id').val(result.Duyuru_Id);
            $('#Duyuru_Basligi').val(result.Duyuru_Basligi);
            $('#Duyuru_Icerigi').val(result.Duyuru_Icerigi);
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            console.log(333333);
            alert(errormessage.responseText);
        }
    });
    return false;
}



//function for deleting duyuru's record
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