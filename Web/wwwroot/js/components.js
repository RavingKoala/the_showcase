class EducationComponent extends HTMLElement {
    connectedCallback() {
        if (!this.hasValidParams()) {
            this.remove()
            return
        }

        this.construct()
    }

    hasValidParams() {
       return (typeof this.dataset.education == "string"
            || typeof this.dataset.period == "string"
            || typeof this.dataset.location == "string")
    }

    construct() {
        this.classList.add("profile-content__element")

        let header = document.createElement("div")
        header.className = "profile-content__element-header"

        let title = document.createElement("h4")
        title.textContent = this.dataset.education
        delete this.dataset.education
        title.style.fontWeight = "bold"
        title.style.fontSize = "18px"
        title.style.marginBottom = "2px"
        title.style.marginTop = "6px"
        header.appendChild(title)

        let period = document.createElement("p")
        period.textContent = this.dataset.period
        delete this.dataset.period
        period.style.fontSize = "12px"
        period.style.color = "#5c6166"
        window.matchMedia('only screen and (min-width: 800px)').addEventListener("change", (e) => {
             period.style.display = e.matches ? "flex" : "none"
        })
        period.style.display = window.outerWidth > 800 ? "flex" : "none"
        header.appendChild(period)


        this.appendChild(header);


        let institute = document.createElement("p")
        institute.textContent = this.dataset.location
        delete this.dataset.location
        institute.className = "profile-content__institute"
        institute.style.color = "rgba(39, 140, 39, 0.96)"
        institute.style.marginTop = "0"
        institute.style.fontSize = "14px"
        this.appendChild(institute)

    }
}
customElements.define('education-component', EducationComponent);
