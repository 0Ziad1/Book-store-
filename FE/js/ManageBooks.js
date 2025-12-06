document.addEventListener("DOMContentLoaded", function () {
    loadAllBooks();
    setupAddBookBtn();
    setupSearchBar();
});

// -----------------------------
// Load all books from API
// -----------------------------
async function loadAllBooks() {
    try {
        const response = await fetch("http://localhost:5288/api/book/getAll");
        if (!response.ok) throw new Error("Failed to fetch books");

        const books = await response.json();
        const container = document.getElementById("bookContainer");
        container.innerHTML = ""; // clear existing cards

        books.forEach(book => createBookCard(book, false)); // existing book
    } catch (error) {
        console.error(error);
    }
}

// -----------------------------
// Create a card (existing or new)
// isNew = false for existing, true for new
// -----------------------------
function createBookCard(book, isNew) {
    const bookContainer = document.getElementById("bookContainer");
    const colDiv = document.createElement("div");
    colDiv.className = "col-md-4";

    const cardDiv = document.createElement("div");
    cardDiv.className = "card h-100 shadow-sm";

    // For existing books, store the ID
    if (!isNew) cardDiv.dataset.bookId = book.id;

    cardDiv.innerHTML = `
        <div class="card-body">
            <h5 class="card-title" contenteditable="true">${book.title}</h5>
            <p><strong>ISBN:</strong> <span class="isbn" contenteditable="${isNew ? "true" : "false"}">${book.isbn}</span></p>
            <p><strong>Author(s):</strong> <span class="authors" contenteditable="true">${book.authors}</span></p>
            <p><strong>Publisher:</strong> <span class="publisher" contenteditable="true">${book.publisher}</span></p>
            <p><strong>Publication Year:</strong> <span class="year" contenteditable="true">${book.publicationYear}</span></p>
            <p><strong>Total Copies:</strong> <span class="totalCopies" contenteditable="true">${book.totalCopies}</span></p>
            <p><strong>Available Copies:</strong> <span class="availableCopies" contenteditable="true">${book.availableCopies}</span></p>
        </div>
        <div class="card-footer text-end">
            <button class="btn btn-sm btn-danger remove-btn">Remove</button>
            ${isNew ? `<button class="btn btn-sm btn-primary save-btn">Save</button>` : ''}
            <button class="btn btn-sm btn-success update-btn" style="display:${isNew ? 'none' : 'inline-block'};">Update</button>
        </div>
    `;

    colDiv.appendChild(cardDiv);
    bookContainer.appendChild(colDiv);

    // REMOVE BUTTON
    cardDiv.querySelector(".remove-btn").addEventListener("click", async () => {
        const bookId = cardDiv.dataset.bookId;
        if (bookId) {
            const response = await fetch(`http://localhost:5288/api/book/${bookId}`, { method: "DELETE" });
            if (!response.ok) return alert("Failed to remove book");
        }
        bookContainer.removeChild(colDiv);
        alert("Book removed!");
    });

    // SAVE BUTTON (only for new books)
    const saveBtn = cardDiv.querySelector(".save-btn");
    if (saveBtn) {
        saveBtn.addEventListener("click", async () => {
            const newBook = {
                isbn: cardDiv.querySelector(".isbn").innerText.trim(),
                title: cardDiv.querySelector(".card-title").innerText.trim(),
                authors: cardDiv.querySelector(".authors").innerText.trim(),
                publisher: cardDiv.querySelector(".publisher").innerText.trim(),
                publicationYear: parseInt(cardDiv.querySelector(".year").innerText),
                totalCopies: parseInt(cardDiv.querySelector(".totalCopies").innerText),
                availableCopies: parseInt(cardDiv.querySelector(".availableCopies").innerText)
            };

            const response = await fetch("http://localhost:5288/api/book/manageBooks", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(newBook)
            });

            const result = await response.json();

            if (response.ok) {
                alert("Book added!");
                cardDiv.dataset.bookId = result.book.id;

                // Switch buttons
                saveBtn.style.display = "none";
                cardDiv.querySelector(".update-btn").style.display = "inline-block";
                cardDiv.querySelector(".isbn").contentEditable = false;
            } else {
                alert(result.message);
            }
        });
    }

    // UPDATE BUTTON
    const updateBtn = cardDiv.querySelector(".update-btn");
    updateBtn.addEventListener("click", async () => {
        const bookId = cardDiv.dataset.bookId;
        if (!bookId) return alert("Book ID missing!");

        const updatedBook = {
            title: cardDiv.querySelector(".card-title").innerText.trim(),
            authors: cardDiv.querySelector(".authors").innerText.trim(),
            publisher: cardDiv.querySelector(".publisher").innerText.trim(),
            publicationYear: parseInt(cardDiv.querySelector(".year").innerText),
            totalCopies: parseInt(cardDiv.querySelector(".totalCopies").innerText),
            availableCopies: parseInt(cardDiv.querySelector(".availableCopies").innerText)
        };

        const response = await fetch(`http://localhost:5288/api/book/update/${bookId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(updatedBook)
        });

        if (response.ok) {
            alert("Book updated successfully!");
        } else {
            alert("Update failed.");
        }
    });
}

// -----------------------------
// Add new book button
// -----------------------------
function setupAddBookBtn() {
    const addBtn = document.getElementById("addBookBtn");
    addBtn.replaceWith(addBtn.cloneNode(true)); // remove previous listeners

    document.getElementById("addBookBtn").addEventListener("click", () => {
        const emptyBook = {
            isbn: "",
            title: "New Book",
            authors: "",
            publisher: "",
            publicationYear: new Date().getFullYear(),
            totalCopies: 0,
            availableCopies: 0
        };
        createBookCard(emptyBook, true); // true = new book
    });
}
function setupSearchBar() {
    const searchInput = document.getElementById("searchInput");
    searchInput.addEventListener("input", () => {
        const query = searchInput.value.toLowerCase();
        const allCards = document.querySelectorAll("#bookContainer .col-md-4");

        allCards.forEach(card => {
            const title = card.querySelector(".card-title").innerText.toLowerCase();
            if (title.includes(query)) {
                card.style.display = "block";
            } else {
                card.style.display = "none";
            }
        });
    });
}