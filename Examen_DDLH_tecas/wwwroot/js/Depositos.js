function validarDeposito() {
    var cantidad = $('#cantidadAManiaobrar').val();
    if (cantidad < 0) {
        console.warn("no se puede hacer este movimiento");
        alert('No se puede depositar' + cantidad+'\n'+'Corriga y vuleva a intentar');
        $('#BotonS').hide();
        return false;
    }
    console.log("Puede hacer el movimiento");
    $('#BotonS').show();
    return true;
}

function pulsar(e) {
    if (e.keyCode === 13 && !e.shiftKey) {
        e.preventDefault();
    }
}