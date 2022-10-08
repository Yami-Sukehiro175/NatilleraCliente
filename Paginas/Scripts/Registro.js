//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {

    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
    $("#btnInsertar").click(function () {
        ProcesarCliente("Insertar");
    });
    //Funcionalidad del botón Actualizar
    $("#btnActualizar").click(function () {
        ProcesarCliente("Actualizar");
    });
    //Funcionalidad del botón Eliminar
    $("#btnEliminar").click(function () {
        ProcesarCliente("Eliminar");
    });
    //Funcionalidad del botón Consultar
    $("#btnConsultar").click(function () {
        ProcesarCliente("Consultar");
    });
    LLenarComboCiudad();

    LLenarComboTipoCuenta();
    LLenarComboBanco();
    LlenarGridCliente();
});
function LlenarGridCliente() {
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Cliente",
        dataType: "json",
        data: null,
        success: function (grdCliente) {
            LlenarGridDatos(grdCliente, "#tblClientes");
        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html(ErrorSuper);
        }
    });
}
function LLenarComboCiudad() {
    var Opcion = { Comando: "Ciudad" }
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Ciudad",
        dataType: "json",
        data: null,
        success: function (cboCiudad) {
            console.log(cboCiudad);
            LlenarComboDatos(cboCiudad, "#cboCiudad");

        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html("Error AJAX");
        }
    });}
function LLenarComboTipoCuenta() {
    var Opcion = { Comando: "Ciudad" }
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/TipoCuenta",
        dataType: "json",
        data: null,
        success: function (cboCuenta) {
            console.log(cboCuenta);
            LlenarComboDatos(cboCuenta, "#cboCuenta");

        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html("Error AJAX");
        }
    });
}
function LLenarComboBanco() {
    var Opcion = { Comando: "Ciudad" }
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Banco",
        dataType: "json",
        data: null,
        success: function (cboBanco) {
            console.log(cboBanco);
            LlenarComboDatos(cboBanco, "#cboBanco");

        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html("Error AJAX");
        }
    });
}

function ProcesarCliente(Comando) {

    var Documento = $("#TxtIdCliente").val();
    var Nombre = $("#TxtNombreCliente").val();
    var FechaNacimiento = $("#TxtFechaNacimiento").val();
    var FechaIngreso = $("#TxtFechaIngreso").val();
    var Direccion = $("#TxtDireccion").val();
    var IdCiudad = $("#cboCiudad").val();
    var IdTipoCuenta = $("#cboCuenta").val();
    var NumeroCuenta = $("#TxtNumeroCuenta").val();
    var IdBanco = $("#cboBanco").val();
    var Telefono = $("#txtTelefono").val();
    var Celular = $("#txtCelular").val();
    var Correo = $("#txtCorreo").val();
    var Ocupacion = $("#txtOcupacion").val();
    if (FechaNacimiento == null || FechaNacimiento == '' || FechaNacimiento == 'undefined') {
        FechaNacimiento = '1900/01/01';
    }
    if (FechaIngreso == null || FechaIngreso == '' || FechaIngreso == 'undefined') {
        FechaIngreso = '1900/01/01';
    }
    if (Comando == 'Insertar') {
        if (Date.parse(FechaIngreso) <= Date.parse(FechaNacimiento)) {
            $("#dvMensaje2").addClass("alert alert-danger");
            $("#dvMensaje2").html("La fecha de Ingreso no puede ser menor ni igual a la fecha de Nacimiento");
            $("#TxtFechaRenuncia").val("");
            $("#TxtFechaRenuncia").focus();
            return;
            //La fecha final es menor que la inicial
        }
    }
    var DatosCliente = {
        Documento: Documento,
        Nombre: Nombre,
        FechaNacimiento: FechaNacimiento,
        FechaIngreso: FechaIngreso,
        Direccion: Direccion,
        IdCiudad: IdCiudad,
        IdTipoCuenta: IdTipoCuenta,
        NumeroCuenta: NumeroCuenta,
        IdBanco: IdBanco,
        Telefono: Telefono,
        Celular: Celular,
        Correo: Correo,
        Ocupacion: Ocupacion,
        Comando: Comando
    }
    console.log(DatosCliente);
    $("#dvMensaje").removeClass("alert alert-danger");
    $("#dvMensaje").html("");
    $("#dvMensaje2").removeClass("alert alert-danger");
    $("#dvMensaje2").html("");
    //Crear un objeto tipo json para enviarlo al servidor

    //Inicia el proceso de invocación de  la página del servidor con ajax
    $.ajax({
        type: "POST",
        url: "../Controladores/ControladorCliente.ashx",
        contentType: "json",
        data: JSON.stringify(DatosCliente),
        success: function (RptaCliente) {
            LlenarGridCliente();
            //Creo una variable con la respuesta de cliente, y la paso como 
            //objeto json
            var Clientes = JSON.parse(RptaCliente);
            console.log(Clientes);
            if (Clientes.Mensaje == "SI") {
                $("#dvMensaje").addClass("alert alert-success");
            }
            else {
                $("#dvMensaje").addClass("alert alert-danger");
            }
            $("#dvMensaje").html(Clientes.Error);
            if (Clientes.Comando == "Consultar") {
                //Se representa en los campos la informacion consultada
                //$("#txtDocumentoCliente").val(Bicicletas.DocumentoCliente);
                var Fecha1 = Clientes.FechaNacimiento.split('T')[0];
                var Fecha2 = Clientes.FechaIngreso.split('T')[0];
                $("#TxtIdCliente").val(Clientes.Documento);
                $("#TxtNombreCliente").val(Clientes.Nombre);
                $("#TxtFechaNacimiento").val(Fecha1);
                $("#TxtFechaIngreso").val(Fecha2);
                $("#TxtDireccion").val(Clientes.Direccion);
                $("#cboCiudad").val(Clientes.IdCiudad);
                $("#cboCuenta").val(Clientes.IdTipoCuenta);
                $("#TxtNumeroCuenta").val(Clientes.NumeroCuenta);
                $("#cboBanco").val(Clientes.IdBanco);
                $("#txtTelefono").val(Clientes.Telefono);
                $("#txtCelular").val(Clientes.Celular);
                $("#txtCorreo").val(Clientes.Correo);
                $("#txtOcupacion").val(Clientes.Ocupacion);
                Comando = "";

            }
            if (Clientes.Comando == "Eliminar") {
                $("#TxtIdCliente").val("");
                $("#TxtNombreCliente").val("");
                $("#TxtFechaNacimiento").val("");
                $("#TxtFechaIngreso").val("");
                $("#TxtDireccion").val("");
                $("#cboCiudad").val(0);
                $("#cboCuenta").val(0);
                $("#TxtNumeroCuenta").val("");
                $("#cboBanco").val(0);
                $("#txtTelefono").val("");
                $("#txtCelular").val("");
                $("#txtCorreo").val("");
                $("#txtOcupacion").val("");
            }
        },
        error: function (RptaError) {
            alert("Error: " + RptaError);
        }
    });
}