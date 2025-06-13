const moviesDiv = document.getElementById("movies");

async function fetchMovies() {
    try {
        const response = await fetch("/api/movies/random");
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.statusText}`);
        }
        const movies = await response.json();
        moviesDiv.innerHTML = ""; 

        movies.forEach((movie) => {
            const movieDiv = document.createElement("div");
            movieDiv.className = "movie";

            const imageUrl = movie.imageUrl || ""; 
            const title = movie.title || "Unknown Title";

            movieDiv.innerHTML = `
                <img src="${imageUrl}" alt="${title}" />
                <p>${title}</p>
            `;
            moviesDiv.appendChild(movieDiv);
        });
    } catch (error) {
        console.error("Error fetching movies:", error);

        moviesDiv.innerHTML = `
            <p class="error-message">Could not load movies. Please try again later.</p>
        `;
    }
}

fetchMovies();
