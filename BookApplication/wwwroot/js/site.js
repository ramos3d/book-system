// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function confirmDelete(bookId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // Trigger form submission
            document.getElementById('deleteForm').submit();
        }
    });
}

function validatePasswords() {
    var password = document.getElementById('password').value;
    var confirmPassword = document.getElementById('confirm-password').value;
    var resultElement = document.getElementById('result');

    if (password !== confirmPassword) {
        resultElement.textContent = "Please make sure both passwords are the same.";
        return false;
    }
    resultElement.textContent = ""; 
    return true;
}