/// <reference path="../js/gdpr.js" />

// mock the elements that are manipulated
const gdprSectionDOM = document.createElement('section')
gdprSectionDOM.classList.add("gdpr-consent")
gdprSectionDOM.classList.add("hide")

const buttonYes = document.createElement('button');
buttonYes.classList.add("gdpr-consent__button--accept")

const buttonNo = document.createElement('button');
buttonNo.classList.add("gdpr-consent__button--reject")

var HTMLElements = { ".gdpr-consent": gdprSectionDOM, ".gdpr-consent__button--accept": buttonYes, ".gdpr-consent__button--reject" : buttonNo };
document.querySelector = jasmine.createSpy('HTML Element').and.callFake(function (className) {
    return HTMLElements[className]
})

const gdpr = new GDPR()

describe("the gdpr state", function () {
    const gdprLocalStorageKey = "gdpr-consent-choice"

    beforeEach(function () {
        localStorage.removeItem(gdprLocalStorageKey)
        document.querySelector(`.gdpr-consent`).classList.add('hide')
        document.querySelector(`.gdpr-consent`).classList.remove('show')
    })

    it("is default empty", function () {
        let currentStorageValue = localStorage.getItem(gdprLocalStorageKey)

        expect(currentStorageValue).toBe(null)
    })

    it("upon setting first time, it updates localstorage", function () {
        gdpr.setConsentState(consentStates.Accepted)
        let currentStorageValue = localStorage.getItem(gdprLocalStorageKey)

        expect(currentStorageValue).not.toBe(null)
    })

    it("after setting first time, get returns the state", function () {
        gdpr.setConsentState(consentStates.Accepted)
        let currentState = gdpr.getConsentState()

        expect(currentState).not.toBe(null)
    })

    it("upon setting to accepted, in localstorage contains information accepted", function () {
        gdpr.setConsentState(consentStates.Accepted)
        let currentState = gdpr.getConsentState()

        expect(currentState).toBe(consentStates.Accepted)
    })

    it("upon setting to rejected, in localstorage contains information rejected", function () {
        gdpr.setConsentState(consentStates.Rejected)
        let currentState = gdpr.getConsentState()

        expect(currentState).toBe(consentStates.Rejected)
    })

    it("upon hiding, confirm hidden", function () {
        gdpr.hideGDPR()

        expect(document.querySelector(".gdpr-consent").classList.contains("hide")).toBe(true)
        expect(document.querySelector(".gdpr-consent").classList.contains("show")).toBe(false)
    })

    it("upon show, confirm shown", function () {
        gdpr.showGDPR()

        expect(document.querySelector(".gdpr-consent").classList.contains("show")).toBe(true)
        expect(document.querySelector(".gdpr-consent").classList.contains("hide")).toBe(false)
    })
})