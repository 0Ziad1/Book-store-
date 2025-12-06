document.getElementById("loginForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch("http://localhost:5288/api/user/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            email: email,
            password: password
        })
    });

    const result = await response.json();

    if (response.ok) {
        document.getElementById("message").innerText = "Login Success!";
        window.location.href = "index.html";
    } else {
        document.getElementById("message").innerText = result.message || "Login failed";
    }
});
