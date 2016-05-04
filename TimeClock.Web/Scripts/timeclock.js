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


$(document).ready(function() {
    $('#userForm')
        .formValidation({
            framework: 'bootstrap',
            icon: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                name: {
                    validators: {
                        notEmpty: {
                            message: 'The full name is required'
                        },
                        regexp: {
                            regexp: /^[a-zA-Z\s]+$/,
                            message: 'The full name can only consist of alphabetical characters'
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: 'The email address is required'
                        },
                        emailAddress: {
                            message: 'The email address is not valid'
                        }
                    }
                },
                website: {
                    validators: {
                        notEmpty: {
                            message: 'The website address is required'
                        },
                        uri: {
                            allowEmptyProtocol: true,
                            message: 'The website address is not valid'
                        }
                    }
                }
            }
        })
        .on('success.form.fv', function(e) {
            // Save the form data via an Ajax request
            e.preventDefault();

            var $form = $(e.target),
                id    = $form.find('[name="id"]').val();

            // The url and method might be different in your application
            $.ajax({
                url: 'http://jsonplaceholder.typicode.com/users/' + id,
                method: 'PUT',
                data: $form.serialize()
            }).success(function(response) {
                // Get the cells
                var $button = $('button[data-id="' + response.id + '"]'),
                    $tr     = $button.closest('tr'),
                    $cells  = $tr.find('td');

                // Update the cell data
                $cells
                    .eq(1).html(response.name).end()
                    .eq(2).html(response.email).end()
                    .eq(3).html(response.website).end();

                // Hide the dialog
                $form.parents('.bootbox').modal('hide');

                // You can inform the user that the data is updated successfully
                // by highlighting the row or showing a message box
                bootbox.alert('The user profile is updated');
            });
        });

    $('.editButton').on('click', function() {
        // Get the record's ID via attribute
        var id = $(this).attr('data-id');

        $.ajax({
            url: 'http://jsonplaceholder.typicode.com/users/' + id,
            method: 'GET'
        }).success(function(response) {
            // Populate the form fields with the data returned from server
            $('#userForm')
                .find('[name="id"]').val(response.id).end()
                .find('[name="name"]').val(response.name).end()
                .find('[name="email"]').val(response.email).end()
                .find('[name="website"]').val(response.website).end();

            // Show the dialog
            bootbox
                .dialog({
                    title: 'Edit the user profile',
                    message: $('#userForm'),
                    show: false // We will show it manually later
                })
                .on('shown.bs.modal', function() {
                    $('#userForm')
                        .show()                             // Show the login form
                        .formValidation('resetForm'); // Reset form
                })
                .on('hide.bs.modal', function(e) {
                    // Bootbox will remove the modal (including the body which contains the login form)
                    // after hiding the modal
                    // Therefor, we need to backup the form
                    $('#userForm').hide().appendTo('body');
                })
                .modal('show');
        });
    });
});
