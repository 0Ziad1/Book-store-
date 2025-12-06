async function loadUsers() {
  const response = await fetch("https://localhost:7268/api/users");
  const data = await response.json();
  console.log(data);
}

loadUsers();
