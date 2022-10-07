//Código para garantizar que se ejecute el código cuando termine de cargar la página
$(document).ready(function () {
    var f = new Date();
    var FechaSTR = f.getFullYear() + "-" + (f.getMonth() + 1) + "-" + f.getDate();
    console.log(FechaSTR);
    $("#TxtFechaliquidacion").val(FechaSTR);
    //Defino la funcionalidad de la página
    //Funcionalidad del botón "Registrar"
    $("#btnInsertar").click(function () {
        ProcesarLiquidacion("Insertar");
    });
    //Funcionalidad del botón Actualizar
    $("#btnActualizar").click(function () {
        ProcesarLiquidacion("Actualizar");
    });
    //Funcionalidad del botón Eliminar
    $("#btnEliminar").click(function () {
        ProcesarLiquidacion("Eliminar");
    });
    //Funcionalidad del botón Consultar
    $("#btnConsultar").click(function () {
        ProcesarLiquidacion("Consultar");
    });
});

function ProcesarLiquidacion(Comando) {
    
    var Documento = $("#TxtIdEmpleado").val();
    var Nombre = $("#TxtNombreEmpleado").val();
    var FechaNacimiento = $("#TxtFechaNacimiento").val();
    var FechaIngreso = $("#TxtFechaIngreso").val();
    var FechaRenuncia = $("#TxtFechaRenuncia").val();
    var FechaLiquidacion = $("#TxtFechaliquidacion").val();
    var Direccion = $("#TxtDireccion").val();
    var IdCiudad = $("#cboCiudad").val();
    var Telefono = $("#txtTelefono").val();
    var Celular = $("#txtCelular").val();
    var Correo = $("#txtCorreo").val();
    var Ocupacion = $("#txtOcupacion").val();
    var DiasTrabajados;
    var Salario = $("#txtSalario").val();
  
    if (Salario < 0) {
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("El salario no puede ser cero");
        $("#txtSalario").val("");
        $("#txtSalario").focus();
        return;
    }
    if (DiasTrabajados < 0) {
        $("#dvMensaje2").addClass("alert alert-danger");
        $("#dvMensaje2").html("La cantidad de dias trabajados no puede ser cero. Revise la fecha de Ingreso y Renuncia");
        $("#txtDiasTrabajados").val("");
        $("#txtDiasTrabajados").focus();
        return;
    }
    $("#dvMensaje").removeClass("alert alert-danger");
    $("#dvMensaje").html("");
    $("#dvMensaje2").removeClass("alert alert-danger");
    $("#dvMensaje2").html("");
    if (FechaNacimiento == null || FechaNacimiento == '' || FechaNacimiento == 'undefined') {
        FechaNacimiento = '1900/01/01';
    }
    if (FechaIngreso == null || FechaIngreso == '' || FechaIngreso == 'undefined') {
        FechaIngreso = '1900/01/01';
    }
    if (FechaRenuncia == null || FechaRenuncia == '' || FechaRenuncia == 'undefined') {
        FechaRenuncia = '1900/01/01';
    }
    if (FechaLiquidacion == null || FechaLiquidacion == '' || FechaLiquidacion == 'undefined') {
        FechaLiquidacion = '1900/01/01';
    }
    if (Date.parse(FechaRenuncia) < Date.parse(FechaIngreso)) {
        $("#dvMensaje2").addClass("alert alert-danger");
        $("#dvMensaje2").html("La fecha de Renuncia no puede ser menor que la fecha de Ingreso");
        $("#TxtFechaRenuncia").val("");
        $("#TxtFechaRenuncia").focus();
        return;
        //La fecha final es menor que la inicial
    } else if (Date.parse(FechaRenuncia) == Date.parse(FechaIngreso)) {
        DiasTrabajados = 1;
        //La fecha Final es mayor...
    }
    else {
        var fechaInicio = new Date(FechaIngreso).getTime();
        var fechaFin = new Date(FechaRenuncia).getTime();

        DiasTrabajados = fechaFin - fechaInicio;
        DiasTrabajados = DiasTrabajados / (1000 * 60 * 60 * 24);

        console.log(DiasTrabajados / (1000 * 60 * 60 * 24));
    }
    //var DiasTrabajados =
        $("#txtDiasTrabajados").val(DiasTrabajados);
    var DatosLiquidacion = {
        Documento: Documento,
        Nombre: Nombre,
        FechaNacimiento: FechaNacimiento,
        FechaIngreso: FechaIngreso,
        FechaRenuncia: FechaRenuncia,
        FechaLiquidacion: FechaLiquidacion,
        Direccion: Direccion,
        IdCiudad: IdCiudad,
        Telefono: Telefono,
        Celular: Celular,
        Correo: Correo,
        Ocupacion: Ocupacion,
        DiasTrabajados: DiasTrabajados,
        Salario: Salario,
        Comando:Comando
    }
    console.log(DatosLiquidacion);

    $("#dvMensaje").removeClass("alert alert-danger");
    $("#dvMensaje").html("");
    $("#dvMensaje2").removeClass("alert alert-danger");
    $("#dvMensaje2").html("");
    if (Salario == "") {
        Salario = 0;
        $("#txtSalario").val(Salario);
    }
    if (DiasTrabajados == "") {
        DiasTrabajados = 0;
        $("#txtDiasTrabajados").val(DiasTrabajados);
    }

    //Crear un objeto tipo json para enviarlo al servidor

    //Inicia el proceso de invocación de  la página del servidor con ajax
    $.ajax({
        type: "POST",
        url: "../Controladores/ControladorLiquidacion.ashx",
        contentType: "json",
        data: JSON.stringify(DatosLiquidacion),
        success: function (RptaVentaBicicletas) {
            //Creo una variable con la respuesta de cliente, y la paso como 
            //objeto json

            var Bicicletas = JSON.parse(RptaVentaBicicletas);
            console.log(Bicicletas);
            if (Bicicletas.Mensaje == "SI") {
                $("#dvMensaje").addClass("alert alert-success");
            }
            else {
                $("#dvMensaje").addClass("alert alert-danger");
            }
            $("#dvMensaje").html(Bicicletas.Error);
            if (Bicicletas.Comando == "Consultar") {
                //Se representa en los campos la informacion consultada
                //$("#txtDocumentoCliente").val(Bicicletas.DocumentoCliente);
                $("#txtNombreCliente").val(Bicicletas.NombreCliente);
                $("#TxtCantidadBicicletas").val(Bicicletas.CantidadBicicletas);
                $("#TxtPrecioUnitario").val(Bicicletas.ValorUnitario);
                $("#txtValorDescuentoAntes").val(Bicicletas.ValorDescuentoAntes);
                $("#TxtTotalPagar").val(Bicicletas.PrecioTotal);
                $("#TxtDescuento").val(Bicicletas.ValorDescuento);
                Comando = "";

            }
            if (Bicicletas.Comando == "Eliminar") {
                $("#TxtDescuento").val("");
                $("#TxtTotalPagar").val("");
                $("#txtValorDescuentoAntes").val("");
                $("#TxtPrecioUnitario").val("");
                $("#TxtCantidadBicicletas").val("");
                $("#txtNombreCliente").val("");
            }
        },
        error: function (RptaError) {
            alert("Error: " + RptaError);
        }
    });
}