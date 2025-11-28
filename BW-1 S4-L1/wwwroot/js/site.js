
window.addEventListener("scroll", function () {
    const navbar = document.querySelector(".navbar");

    if (window.scrollY > 20) {
        navbar.style.backgroundColor = "#D8AB00";
    } else {
        navbar.style.backgroundColor = "";
    }
});

