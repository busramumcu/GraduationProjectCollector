
$(document).ready(function () {

    //Populate Contact
    LoadGorevs();

});


function LoadGorevs() {
    $('#update_panel').html('Loading Data...');

    $.ajax({
        url: '/home/GetGorevs',
        type: 'GET',
        dataType: 'json',
        success: function (d) {
            if (d.length > 0) {
                var $data = $('<table></table>').addClass('table table-responsive table-striped');
                var header = "<thead><tr><th>Contact Person</th><th>Contact No</th><th>Country</th><th>State</th><th></th></tr></thead>";
                $data.append(header);

                $.each(d, function (i, row) {
                    var $row = $('<tr/>');
                    $row.append($('<td/>').html(row.ContactPerson));
                    $row.append($('<td/>').html(row.ContactNo));
                    $row.append($('<td/>').html(row.CountryName));
                    $row.append($('<td/>').html(row.StateName));
                    $row.append($('<td/>').html("<a href='/LoggedDanisman/Save/" + row.ContactID + "' class='popup'>Düzenle</a>&nbsp;|&nbsp;<a href='/LoggedDanisman/Delete/" + row.ContactID + "' class='popup'>Sil</a>"));
                    $data.append($row);
                });

                $('#update_panel').html($data);
            }
            else {
                var $noData = $('<div/>').html('No Data Found!');
                $('#update_panel').html($noData);
            }
        },
        error: function () {
            alert('Error! Please try again.');
        }
    });

}