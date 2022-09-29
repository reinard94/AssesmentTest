$(document).ready(function () {
    var table = null;
    bindDatatable();
    $('#ajaxReload').on('click', function () {
        table.ajax.reload(null, false);
    })

    $('#ajaxReloadReset').on('click', function () {
        table.ajax.reload(null, true);
    }) 
});

function bindDatatable() {
    table = $('#tblUser').DataTable({
        "processing": true,
        "serverSide": true,
        "bLengthChange": false,
        "ordering": false,
        "paging": true,
        "searching": false,
        "responsive": true,
        "ajax": {
            url: "/Account/GetUsers", type: "post"
        },
        "columns": [
            { "data": "id" },
            { "data": "email" },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Detail" onclick="return GetById(' + meta.row + ')" >Detail</button>';
                }
            }
        ]
    });
}

function GetById(nummber) {
    var id = table.row(nummber).data().id;
    $.ajax({
        url: "/Account/DetailUser/",
        data: { id: id }
    }).then((result) => {
        $('#id').val(result.id);
        $('#name').val(result.name);
        $('#firstName').val(result.firstName);
        $('#lastName').val(result.lastName);
        $("#avatar").attr("src", result.avatar);
        $('#exampleModal').modal('show');
    });
}