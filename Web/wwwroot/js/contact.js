document.querySelector('.form-contactpagina').addEventListener("submit", (e) => {
    // I'm not a fan of using jquery but this is needed because of the validation library
    if ($(".form-contactpagina").valid())
        document.querySelector(".spinner-container").classList.remove("hide");
});