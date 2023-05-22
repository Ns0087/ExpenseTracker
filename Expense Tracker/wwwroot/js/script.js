const passwordInput = document.getElementById('Password');
let confirmPasswordInput = document.getElementById('ConfirmPassword');

passwordInput.addEventListener('input', validatePasswords);
confirmPasswordInput.addEventListener('input', validatePasswords);

function validatePasswords() {
    const password = passwordInput.value;
    const confirmPassword = confirmPasswordInput.value;
    let registerButton = document.getElementById("register");

    if (password === confirmPassword && password.length > 0) {
        registerButton.disabled = false;
        confirmPasswordInput.classList.remove("notMatched");
    } else {
        registerButton.disabled = true;
        confirmPasswordInput.classList.add("notMatched");
    }
}