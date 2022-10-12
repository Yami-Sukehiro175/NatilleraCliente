//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {
    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
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
        ConsultarBicicletas();
    });
    //LlenarGridBicicleta();
    llenarGridAhorro();
    LLenarComboCliente();
});
function llenarGridAhorro() {
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Campos",
        dataType: "json",
        data: null,
        success: function (grdCampos) {
            LlenarGridDatos(grdCampos, "#tblAhorros");
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
function ConsultarBicicletas() {
    var IDAhorro = $("#txtIdAhorro").val();
    $.ajax({
        type: "GET",
        url: "http://localhost:50094/api/Ahorrar?AhorroID=" + IDAhorro,
        dataType: "json",
        data: null,
        success: function (RptaAhorro) {
            if (RptaAhorro == null) {
                $("#dvMensaje").addClass("alert alert-danger");
                $("#dvMensaje").html("No Hay Datos Asociados al Documento: " + IDAhorro);
                $("#txtIdAhorro").val("");
                $("#cboCliente").val("");
                $("#txtFechaAhorro").val("");
                $("#txtCantidadAhorro").val("");
            } else {
                //$("#txtDocumentoCliente").val(RptaSuper.Nombre);
                $("#txtIdAhorro").val(RptaAhorro.IDAhorro);
                $("#cboCliente").val(RptaAhorro.IDCliente);
                var Fechas = RptaAhorro.FechaAhorro.split('T')[0];
                $("#txtFechaAhorro").val(Fechas);
                $("#txtCantidadAhorro").val(RptaAhorro.ValorAhorrado);


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
        url: "http://localhost:50094/api/Campos?Documento="+DocumentoCliente,
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
    var IDAhorro = $("#txtIdAhorro").val();
    var IDCliente = $("#cboCliente").val();
    var FechaAhorro = $("#txtFechaAhorro").val();
    var ValorAhorrado = $("#txtCantidadAhorro").val();
    if (IDAhorro == "") {
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("Ingrese el Numero del Ahorro");
        return;
    } else {
        $("#dvMensaje").removeClass("alert alert-danger");
        $("#dvMensaje").html("");
    }
    if (Comando == 'POST') {
        if (ValorAhorrado == "") {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html("Ingrese la cantidad que desea ahorrar");
            return;
        } else {
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").html("");
        }
        if (IDCliente == "" || IDCliente == null || IDCliente == 'undefined' || IDCliente == 0) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html("Seleccione el Cliente que Desea Ingresar su ahorrro");
            return;
        } else {
            $("#dvMensaje").removeClass("alert alert-danger");
            $("#dvMensaje").html("");
        }
        if (FechaAhorro == null || FechaAhorro == '' || FechaAhorro == 'undefined') {
            FechaAhorro = '1900/01/01';
        }
    }
    var DatosAhorro = {
        IDAhorro: IDAhorro,
        IDCliente: IDCliente,
        FechaAhorro: FechaAhorro,
        ValorAhorrado: ValorAhorrado
    }
    console.log('Datos para enviar');
    console.log(DatosAhorro);
    $.ajax({
        type: Comando,
        url: "http://localhost:50094/api/Ahorrar",
        dataType: "json",
        data:DatosAhorro,
        success: function (RptaAhorro) {
            $("#dvMensaje").addClass("alert alert-success");
            $("#dvMensaje").html(RptaAhorro);
            llenarGridAhorro();
        },
        error: function (RptaError) {
            alert("Error: " + RptaError);
        }
    });

}