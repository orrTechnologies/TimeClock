$(document).ready(function () {

  //  $('#employee-table').DataTable();

    $('#deleteCustomerModal').on('show.bs.modal', function (e) {

        console.log("opened");
    });
    
    $('#delete-employee-confirm-button').click(function (e) {
        var modal = $('#deleteCustomerModal');
        console.log(modal.relatedTarget.dataset.employee);

        var employeeId = e.relatedTarget.dataset.employee;
        var options = { id: employeeId };

        $.ajax({
            type: "POST",
            url: "Delete",
            data: JSON.stringify(options),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                modal.modal('hide');
            }
        });
    });
});