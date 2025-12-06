document.getElementById("registerForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const name = document.getElementById("name").value;
    const phone = document.getElementById("phone").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("http://localhost:5288/api/user/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name: name,
            phoneNumber: phone,
            email: email,
            password: password
        })
    });

    const result = await response.json();

    if (response.ok) {
        document.getElementById("message").innerText = "Registered Successfully!";
        // Redirect to Login Page
        window.location.href = "login.html";
    } else {
        document.getElementById("message").innerText = result.message || "Registration failed";
    }
});
