document.getElementById('loginForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    try {
        const response = await fetch('http://localhost:5233/Account/login', { 
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `username=${username}&password=${password}`
        });

        if (response.ok) {
            window.location.href = 'admin.html';
        } else {
            alert('Invalid username or password');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});
