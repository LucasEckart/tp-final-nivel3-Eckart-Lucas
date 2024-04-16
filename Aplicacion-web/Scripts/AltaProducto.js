


function validar() {
    const txtCodigo = document.getElementById("txtCodigo");
    const txtNombre = document.getElementById("txtNombre");
    const txtPrecio = document.getElementById("txtPrecio");

    txtCodigo.classList.remove("is-invalid");
    txtNombre.classList.remove("is-invalid");
    txtPrecio.classList.remove("is-invalid");

    let isValid = true; 

    if (txtCodigo.value.trim() === "") {
        txtCodigo.classList.add("is-invalid");
        isValid = false;

    }


    if (txtNombre.value.trim() === "") {
        txtNombre.classList.add("is-invalid");
        isValid = false;
    }


    if (txtPrecio.value.trim() === "") {
        txtPrecio.classList.add("is-invalid");
        isValid = false;
    }

    return isValid;
}


function limitarDescripcion() {
    var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value;
    if (descripcion.length > 150) {
        document.getElementById('<%= txtDescripcion.ClientID %>').value = descripcion.substring(0, 150);
    }
}