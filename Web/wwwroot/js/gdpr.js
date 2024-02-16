﻿class GDPR {
    constructor() {
        this.bindEvents()

        if (this.getConsentState() !== consentStates.Accepted)
            this.showGDPR()
    }

    consentStates = {
        Accepted: "accepted",
        Rejected: "rejected"
    }

    bindEvents() {
        let buttonAccept = document.querySelector('.gdpr-consent__button--accept')
        buttonAccept.addEventListener('click', () => {
            this.setConsentState(consentStates.Accepted)
            this.hideGDPR()
        })
        let buttonReject = document.querySelector('.gdpr-consent__button--reject')
        buttonReject.addEventListener('click', () => {
            this.setConsentState(consentStates.Rejected)
            this.hideGDPR()
        })
    }

    getConsentState() {
        let stateString = localStorage.getItem('gdpr-consent-choice')
        return stateString !== null ? JSON.parse(stateString) : null
    }

    setConsentState(state) {
        const hour = 60 * 60 * 1000
        let datetime = (new Date() + hour).toLocaleString("nl-NL", { timeZone: "UTC" }); //UTC+1
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

    setCookie(cname, cvalue, exdays = 365) {
        if (getConsentState() == consentStates.Accepted)
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }
}

const gdpr = new GDPR()

