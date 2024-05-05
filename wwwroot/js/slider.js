let currentSlide = 0;
const slides = document.querySelectorAll('.slide');

function showSlide(index) {
    if (index < 0) {
        index = slides.length - 1;
    } else if (index >= slides.length) {
        index = 0;
    }
    for (let i = 0; i < slides.length; i++) {
        if (i === index) {
            slides[i].style.display = 'block';
        } else {
            slides[i].style.display = 'none';
        }
    }
    currentSlide = index;
}

function nextSlide() {
    showSlide(currentSlide + 1);
}

function prevSlide() {
    showSlide(currentSlide - 1);
}

// Show the first slide initially
showSlide(0);