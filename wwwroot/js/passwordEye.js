document.addEventListener("DOMContentLoaded", function () {
    function setupToggle(inputId, toggleId) {
        const input = document.getElementById(inputId);
        const toggle = document.getElementById(toggleId);
        if (input && toggle) {
            toggle.addEventListener("click", () => {
                const type = input.getAttribute("type") === "password" ? "text" : "password";
                input.setAttribute("type", type);
                toggle.classList.toggle("bi-eye");
                toggle.classList.toggle("bi-eye-slash");
            });
        }
    }
    setupToggle("passwordInput", "togglePassword");
    setupToggle("confirmPasswordInput", "toggleConfirmPassword");
});