// Carga los datos de la empresa al ingresar rut
$('#WorkOrder_RutEmpresa').change(function () {
    // Rut de la Empresa
    var rutEmpresa = document.getElementById("WorkOrder_RutEmpresa").value;

    // Carga los datos de la empresa al ingresar rut empresa
    $.ajax({
        type: "GET",
        url: "/Tecnico/WorkOrders/DatosEmp",
        data: { "rut": rutEmpresa },
        dataType: "json",
        success: function (response) {
            if (response.datos[0] == null) {
                vaciaCajas();
                Swal.fire({
                    position: 'center',
                    title: 'ERROR!',
                    text: 'No existen datos de la empresa',
                    type: 'error',
                    showConfirmButton: false,
                    timer: 1750
                });
            }

            // Llena los valores
            $('#idEmpresa').val(response.datos[0].id);
            $('#WorkOrder_NombreEmpresa').val(response.datos[0].razonSocial);
            $('#WorkOrder_CorreoElectronico').val(response.datos[0].correoElectronico);

            console.log($('#idEmpresa').val());

            Swal.fire({
                position: 'center',
                title: 'Exito!',
                text: 'Datos cargados correctamente',
                type: 'success',
                showConfirmButton: false,
                timer: 1750
            });
        }
    });
});

// Carga los datos de la empresa al ingresar razon social
$('#WorkOrder_NombreEmpresa').change(function () {
    // Rut de la Empresa
    var nomEmpresa = document.getElementById("WorkOrder_NombreEmpresa").value;

    // Ajax Call para la empresa y sus datos correspondientes
    $.ajax({
        type: "GET",
        url: "/Tecnico/WorkOrders/DatosNomEmp",
        data: { "nomEmp": nomEmpresa },
        dataType: "json",
        success: function (response) {
            if (response.datos[0] == null) {
                vaciaCajas();
                Swal.fire({
                    position: 'center',
                    title: 'ERROR!',
                    text: 'No existen datos de la empresa',
                    type: 'error',
                    showConfirmButton: false,
                    timer: 1750
                });
            }

            // Llena los valores
            $('#idEmpresa').val(response.datos[0].id);
            $('#WorkOrder_RutEmpresa').val(response.datos[0].rutEmpresa);
            $('#WorkOrder_CorreoElectronico').val(response.datos[0].correoElectronico);

            //console.log($('#idEmpresa').val());

            Swal.fire({
                position: 'center',
                title: 'Exito!',
                text: 'Datos cargados correctamente',
                type: 'success',
                showConfirmButton: false,
                timer: 1750
            });
        }
    });
});