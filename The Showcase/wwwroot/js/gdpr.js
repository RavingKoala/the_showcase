class GDPR {
    constructor() {
        this.bindEvents()

        if (this.getConsentState() === null)
            this.showGDPR()
    }

    bindEvents() {
        let buttonAccept = document.querySelector('.gdpr-consent__button--accept')
        buttonAccept.addEventListener('click', () => {
            this.setConsentState('accepted')
            this.hideGDPR()
        })
        let buttonReject = document.querySelector('.gdpr-consent__button--reject')
        buttonReject.addEventListener('click', () => {
            this.setConsentState('rejected')
            this.hideGDPR()
        })
    }

    getConsentState() {
        let StateString = localStorage.getItem('gdpr-consent-choice')
        return StateString !== null ? JSON.parse(StateString) : null
    }

    setConsentState(state) {
        const hour = 60 * 60 * 1000
        let datetime = (new Date() + hour).toLocaleString("nl-NL", { timeZone: "UTC" });
        let stateObject = { consent: state, datetime: datetime }
        localStorage.setItem('gdpr-consent-choice', JSON.stringify(stateObject));
    }

    hideGDPR() {
        document.querySelector(`.gdpr-consent`).classList.add('hide')
        document.querySelector(`.gdpr-consent`).classList.remove('show')
    }

    showGDPR() {
        document.querySelector(`.gdpr-consent`).classList.remove('hide')
        document.querySelector(`.gdpr-consent`).classList.add('show')
    }

    getCookie(cname) {
        let name = cname + "="
        let decodedCookie = decodeURIComponent(document.cookie)
        let ca = decodedCookie.split('')
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i]
            while (c.charAt(0) == ' ') {
                c = c.substring(1)
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length)
            }
        }
        return "";
    }

    setCookie(cname, value) {
        if (getConsentState() !== "accept") return;

        // TODO: add cookie implementation
    }
}

const gdpr = new GDPR()

