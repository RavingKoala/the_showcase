using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
public class GameController : Controller {
    // GET: GameController
    [HttpGet]
    public ActionResult Index(string id) {
        return View();
    }

    // GET: GameController/DoMove/5 {body}
    [HttpPost]
    public ActionResult DoMove(int id) {
        return View();
    }

    // GET: GameController/Create
    [HttpGet]
    public ActionResult Create() {
        return View();
    }

    // POST: GameController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }

    // GET: GameController/Edit/5
    public ActionResult Edit(int id) {
        return View();
    }

    // POST: GameController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }

    // GET: GameController/Delete/5
    public ActionResult Delete(int id) {
        return View();
    }

    // POST: GameController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection) {
        try {
            return RedirectToAction(nameof(Index));
        } catch {
            return View();
        }
    }

}
