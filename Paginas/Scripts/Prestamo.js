$(document).ready(function () {
    $("#btnInsertar").click(function () {
        ProcesarComando("POST");
    });
    $("#btnActualizar").click(function () {
        ProcesarComando("PUT");
    });
    $("#btnEliminar").click(function () {
        ProcesarComando("DELETE");
    });
    $("#btnConsultar").click(function () {
        ConsultarPrestamos();
    });
    llenarGridPrestamo();
    LLenarComboCliente();
});
function llenarGridPrestamo() {
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Campos?Id=1",
        dataType: "json",
        data: null,
        success: function (grdCampos) {
            LlenarGridDatos(grdCampos, "#tblPrestamo");
        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html(ErrorSuper);
        }
    });
}

function LLenarComboCliente() {
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Cliente?Combo=ComboCliente",
        dataType: "json",
        data: null,
        success: function (cboCliente) {
            console.log(cboCliente);
            LlenarComboDatos(cboCliente, "#cboCliente");

        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html("Error AJAX");
        }
    });
}
function ConsultarPrestamos() {
    var IDPrestamo = $("#txtIdPrestamo").val();
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Prestamo?PrestamoID=" + IDPrestamo,
        dataType: "json",
        data: null,
        success: function (RptaPrestamo) {
            if (RptaPrestamo == null) {
                $("#dvMensaje").addClass("alert alert-danger");
                $("#dvMensaje").html("No Hay Datos Asociados al Ahorro: " + IDPrestamo);
                $("#txtIdPrestamo").val("");
                $("#cboCliente").val("");
                $("#txtValorPrestado").val("");
                $("#txtFechaPrestamo").val("");
                $("#txtInteres").val("");
                $("#txtValorTotalPrestamo").val("");
            } else {
                console.log(RptaPrestamo);
                $("#txtIdPrestamo").val(RptaPrestamo.IDPrestamo);
                $("#cboCliente").val(RptaPrestamo.IDCliente);
                var Fechas = RptaPrestamo.FechaPrestamo.split('T')[0];
                $("#txtValorPrestado").val(RptaPrestamo.ValorPrestado);
                $("#txtFechaPrestamo").val(Fechas);
                $("#txtInteres").val(RptaPrestamo.InteresPor);
                $("#txtValorTotalPrestamo").val(RptaPrestamo.ValorTotalPrestamo);
                llenarCamposCliente();
                $("#dvMensaje").removeClass("alert alert-danger");
                $("#dvMensaje").html("");
            }
        },
        error: function (RptaError) {
            alert("Error: " + RptaError);
        }
    });
}
function llenarCamposCliente() {
    var DocumentoCliente = $("#cboCliente").val();
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Campos?Documento=" + DocumentoCliente,
        dataType: "json",
        data: null,
        success: function (RptaCamposCliente) {
            console.log(RptaCamposCliente[0].CampoA);
            $("#txtBanco").val(RptaCamposCliente[0].CampoB);
            $("#txtTipoCuenta").val(RptaCamposCliente[0].CampoA);
            $("#txtCuenta").val(RptaCamposCliente[0].CampoC);
        },
        error: function (ErrorSuper) {
            $("#dvMensaje").addClass("alert alert-warning");
            $("#dvMensaje").html("Error AJAX");
        }
    });
}


function ProcesarComando(Comando) {
    var IDPrestamo = $("#txtIdPrestamo").val();
    var IDCliente = $("#cboCliente").val();
    var FechaPrestamo = $("#txtFechaPrestamo").val();
    var ValorPrestado = $("#txtValorPrestado").val();
    var InteresPor = $("#txtInteres").val();
    var ValorTotalPrestamo = $("#txtValorTotalPrestamo").val();

    if (IDPrestamo == "") {
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("Ingrese el Numero del Prestamo");
        return;
    } else {
        $("#dvMensaje").removeClass("alert alert-danger");
        $("#dvMensaje").html("");
    }
    if (Comando == 'POST') {
        if (ValorPrestado == "") {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html("Ingrese el valor prestado");
            return;
        } else {
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").html("");
        }
        if (IDCliente == "" || IDCliente == null || IDCliente == 'undefined' || IDCliente == 0) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html("Seleccione el Cliente que Desea Ingresar su Prestamo");
            return;
        } else {
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").html("");
        }
        if (FechaPrestamo == null || FechaPrestamo == '' || FechaPrestamo == 'undefined') {
            FechaPrestamo = '1900/01/01';
        }
    }
    var DatosPrestamo = {
        IDPrestamo: IDPrestamo,
        IDCliente: IDCliente,
        FechaPrestamo: FechaPrestamo,
        ValorPrestado: ValorPrestado,
        InteresPor: InteresPor,
        ValorTotalPrestamo: ValorTotalPrestamo
    }
    console.log('Datos para enviar');
    console.log(DatosPrestamo);
    $.ajax({
        type: Comando,
        url: "http://localhost:50094/api/Prestamo",
        dataType: "json",
        data: DatosPrestamo,
        success: function (RptaPrestamo) {
            $("#dvMensaje").addClass("alert alert-success");
            $("#dvMensaje").html(RptaPrestamo);
            llenarGridPrestamo();
        },
        error: function (RptaError) {
            alert("Error: " + RptaError);
        }
    });
}