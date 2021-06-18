// Write your JavaScript code.
function GetModalInformeProduccion() {
    var table = $("#informe").DataTable({
       
        pageLength: 25,
        destroy: true,
        dom: 'Blfrtip',
        buttons: [],
        ajax: {
            url: '/Guest/Home/GetInformeProduccion',
            type: 'GET',
            dataType: 'json'
        },
        "columns": [         
            { "data": "nombreTecnico" },
            { "data": "cliente" },
            { "data": "tiempoTranscurridoEnMinutos" },
            { "data": "cantidadTicketsPendientes" },
          
        ],
      
    });


    $('#modalInformeProduccion').modal('show');
}
function GetFichaCliente(value) {

    var rut = value;

    if (rut == "") {
        Swal.fire("App Soporte", "Rut no puede ir vacío", "error");
    }
    else if (rut.length > 11 || rut.length < 9) {
        Swal.fire("App Soporte", "Número de caracteres incorrectos en campo Rut", "error");
    }
    else {

        $.ajax({
            url: '/Tecnico/Tickets/GetFichaCliente',
            dataType: 'json',
            type: 'GET',
            data: { 'rutCliente': rut },
            success: function (response) {


                if (response !== "") {
                    $("#rut").val(response.rutEmpresa);
                    $("#rs").val(response.razonSocial);
                    $("#fantasia").val(response.nombreFantasia);
                    $("#correoE").val(response.correoElectronico);
                    $("#block").val(response.isBlocked);
                    $("#direccion").val(response.direccion);
                    $("#comuna").val(response.comuna);
                    $("#ciudad").val(response.ciudad);


                    var myPath = '/Admin/Empresas/Details/' + response.id,
                        link = document.getElementById('js_link');
                    link.href = myPath;

                    $('#modalCliente').modal('show');

                }

            }
        });

    }

};







/*== VALIDADOR SWAL DE TICKETS ==*/
function ValidaTicket()
{
    //== Validaciones
    var razonSocial = document.getElementById("razonSocial").value;
    var problema = document.getElementById("problema").value;

    if (razonSocial === "") {
        Swal.fire("Error!", "Razón social no puede estar vacío!", "error");
        return false;
    }

    if (problema === "") {
        Swal.fire("Error!", "Problema no puede estar vacío!", "error");
        return false;
    }
}

/*== VALIDADOR DE RUT ==*/
function valRut(box) {
    var rut = document.getElementById(box).value;
    suma = 0;
    mul = 2;i = 0;

    for (i = rut.length - 3; i >= 0; i--) {
        suma = suma + parseInt(rut.charAt(i)) * mul;
        mul = mul == 7 ? 2 : mul + 1;
    }

    var dvr = '' + (11 - suma % 11);

    if (dvr == '10') dvr = 'K'; else if (dvr == '11') dvr = '0';
    {
        if (rut.charAt(rut.length - 2) != "-" || rut.charAt(rut.length - 1).toUpperCase() != dvr) {
            rut.IsValid = false;
            document.getElementById(box).value = "";
            Swal.fire("Error", "Rut con formato incorrecto. ", "error");

        }
        else {
            rut.IsValid = true;
        }
    }
}

/* VALIDADOR DE RUT V.2 */
function checkRut(rut) {

    // Despejar Puntos
    var valor = rut.value.replace('.', '');

    // Despejar Guión
    valor = valor.replace('-', '');

    // Aislar Cuerpo y Dígito Verificador
    cuerpo = valor.slice(0, -1);
    dv = valor.slice(-1).toUpperCase();

    // Formatear RUN
    rut.value = cuerpo + '-' + dv

    // Si no cumple con el mínimo ej. (n.nnn.nnn)
    if (cuerpo.length < 7) { rut.setCustomValidity("RUT Incompleto"); return false; }

    // Calcular Dígito Verificador
    suma = 0;
    multiplo = 2;

    // Para cada dígito del Cuerpo
    for (i = 1; i <= cuerpo.length; i++) {

        // Obtener su Producto con el Múltiplo Correspondiente
        index = multiplo * valor.charAt(cuerpo.length - i);

        // Sumar al Contador General
        suma = suma + index;

        // Consolidar Múltiplo dentro del rango [2,7]
        if (multiplo < 7) { multiplo = multiplo + 1; } else { multiplo = 2; }

    }

    // Calcular Dígito Verificador en base al Módulo 11
    dvEsperado = 11 - (suma % 11);

    // Casos Especiales (0 y K)
    dv = (dv == 'K') ? 10 : dv;
    dv = (dv == 0) ? 11 : dv;

    // Validar que el Cuerpo coincide con su Dígito Verificador
    if (dvEsperado != dv) { rut.setCustomValidity("RUT Inválido"); return false; }

    // Si todo sale bien, eliminar errores (decretar que es válido)
    rut.setCustomValidity('');
}

/*== OBTIENE TODOS LOS DIAS DE UN MES SEGUN TIPO ==*/ 
function DiasDelMes(dia, idMes)
{
    //== Cambia los headers
    var d = new Date();
    var mesReal = idMes - 1;
    var getTot = daysInMonth(parseInt(mesReal), d.getFullYear());
    var days = new Array(); //Array de todos los dias

    //Validación de meses
    if (mesReal == 1) {
        getTot = 28;
    }

    if (mesReal == 3 || mesReal == 5 || mesReal == 8 || mesReal == 10) {
        getTot = 30;
    }

    if (mesReal == 0 || mesReal == 2 || mesReal == 4 || mesReal == 6 || mesReal == 7 || mesReal == 9 || mesReal == 11) {
        getTot = 31;
    }

    for (var i = 1; i <= getTot; i++) {
        var newDate = new Date(d.getFullYear(), parseInt(mesReal), i)
        if (newDate.getDay() == dia) {
            days.push(i);
        }
    }

    //Total de días de un mes
    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }

    //== Return array con todos los días del tipo seleccionado, del Mes seleccionado
    return days;
}

/* DEBUGUEA UN RESULTADO JSON */
function JSONdebug(json) {
    var strJSON = JSON.stringify(json, null, 2); // Deja el JSON en un formato leíble
    console.log(strJSON);
}

// Boton Excel, "Header" y Paginate Datatables
function orderDatatables() {
    // Remueve clases del boton de excel
    $("div.dt-buttons button").removeClass('dt-button');
    $("div.dt-buttons button").removeClass('buttons-excel');
    $("div.dt-buttons button").removeClass('buttons-html5');

    // Añade clase bootstrap, agrega icono y remueve el texto en span
    $("div.dt-buttons button").addClass('btn btn-outline-success col-sm-12 col-md-12 col-lg-12');
    $("div.dt-buttons button").append('<i class="fas fa-file-excel"></i>');
    $("div.dt-buttons button span").remove();

    // Añade clases boostrap al "header" de la datatables
    $("div.dataTables_wrapper div.dt-buttons").addClass('col-sm-12 col-md-12 col-lg-1 text-left');
    $("div.dataTables_wrapper div.dataTables_length").addClass('col-sm-12 col-md-12 col-lg-7');
    $("div.dataTables_wrapper div.dataTables_filter").addClass('col-sm-12 col-md-12 col-lg-4 ');

    // Añade clases boostrap al Paginate de la datatables
    $("div.dataTables_wrapper div.dataTables_info").addClass('col-sm-12 col-md-12 col-lg-6');
    $("div.dataTables_wrapper div.dataTables_paginate").addClass('col-sm-12 col-md-12 col-lg-6');

    // Añade clase bootstrap 'form-control' a inputs de datatable "Tickets Tecnicos"
    $("#tableTec thead tr th input").addClass('form-control');
}

$(document).ready(function () {
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });
});

// Validaciones para WorkOrders/Create
$(document).ready(function () {
    // Autocompletado para rut de empresa
    $("#WorkOrder_RutEmpresa").autocomplete({
        source: '/Tecnico/WorkOrders/SearchEmpRut'
    });

    // Autocompletado para nombre
    $("#WorkOrder_NombreEmpresa").autocomplete({
        source: '/Tecnico/WorkOrders/SearchEmpNom'
    });

    // ********** Start Bloqueo ingreso de letras **********//
    $('#WorkOrder_RutEmpresa').on('keypress', function (event) {
        var regex = new RegExp("^[0-9-]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $('#WorkOrder_HoraLlegada').on('keypress', function (event) {
        var regex = new RegExp("^[0-9:]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $('#WorkOrder_HoraSalida').on('keypress', function (event) {
        var regex = new RegExp("^[0-9:]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $('#rutCli').on('keypress', function (event) {
        var regex = new RegExp("^[0-9-]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
    // ********** End Bloqueo ingreso de letras **********//
});