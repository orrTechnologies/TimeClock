$(document).ready(function() {

    //  $('#employee-table').DataTable();


        $('#deleteCustomerModal').on('show.bs.modal', function(e) {
            $(this).attr.employee = e.relatedTarget.dataset.employee;
            console.log("opened");
        });

        $('#delete-employee-confirm-button').on('click', function(e) {
            var modal = $('#deleteCustomerModal');

            var employeeId = modal.attr.employee;
            var options = { id: employeeId };

            $.ajax({
                type: "POST",
                url: "Delete",
                data: JSON.stringify(options),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    modal.modal('hide');
                    console.log(msg);
                },
                error: function(msg) {
                    console.log(msg);
                }
            });
        });

        $("#deleteCustomerModal").on('hidden.bs.modal', function() {
            $(this).data('bs.modal', null);
        });

});