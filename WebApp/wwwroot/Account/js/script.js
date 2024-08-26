const API_URL = 'https://localhost:7153/api';

async function registerUser() {
    const userName = document.getElementById('userName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const phoneNumber = document.getElementById('phoneNumber').value;
    const dateOfBirth = document.getElementById('dateOfBirth').value;

    try {
        const response = await fetch(`${API_URL}/Account/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ userName, email, password, phoneNumber, dateOfBirth })
        });

        const result = await response.json(); 

        if (response.ok && result.success) {
            alert('User registered successfully!');
            window.location.href = 'Login.html'; 
        } else {
            const errorMessage = result.error || result.message || 'Registration failed';
            alert(`Error: ${errorMessage}`);
        }
    } catch (error) {
        alert('Network error, please try again later');
        alert('Network Error:', error);
    }
}


async function loginUser() {
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const response = await fetch(`${API_URL}/Account/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
    });
       const result = await response.json(); 
    if (response.ok && result.success) {
        alert('Login successful!');
        window.location.href = '../Order/CreateOrder.html';
    }
    else {
        const errorMessage = result.error || result.message || 'Login failed';
        alert(`Error: ${errorMessage}`);
    }
}



