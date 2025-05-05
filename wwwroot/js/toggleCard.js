const toggleCard = document.getElementById('toggleCard');
const toggleBack = document.getElementById('toggleBack');
const loginCard = document.getElementById('loginCard');
const registerCard = document.getElementById('registerCard');

toggleCard?.addEventListener('click', function (e) {
    e.preventDefault();
    loginCard.classList.add('d-none');
    registerCard.classList.remove('d-none');
});

toggleBack?.addEventListener('click', function (e) {
    e.preventDefault();
    registerCard.classList.add('d-none');
    loginCard.classList.remove('d-none');
});