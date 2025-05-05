
document.addEventListener("DOMContentLoaded", function () {
    const password = document.getElementById("passwordInput");
    const confirm = document.getElementById("ConfirmPassword");

    function validatePasswordMatch() {
        if (password.value !== confirm.value) {
            confirm.setCustomValidity("Las contraseñas no coinciden");
        } else {
            confirm.setCustomValidity("");
        }
    }

    password.addEventListener("input", validatePasswordMatch);
    confirm.addEventListener("input", validatePasswordMatch);
});

