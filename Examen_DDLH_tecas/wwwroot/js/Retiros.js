function VerificaAccion() {
    var cantidad = $('#cantidadAManiaobrar').val();
    let saldo = $('#txtSaldo').text();
    let saldonumero = parseFloat(saldo.substring(1));
    if (cantidad <= saldonumero) {
        console.log("Puede retirar");
        $('#botonS').show();
        return true;
    }
    console.warn("No se puede hacer el movimiento");
    alert("No se puede retirar : " + cantidad+"\n"+"Verifique la cantidad y vuelva a intentar");
    $('#botonS').hide();
    return false;
}
function pulsar(e) {
    if (e.keyCode === 13 && !e.shiftKey) {
        e.preventDefault();        
     
    }
}