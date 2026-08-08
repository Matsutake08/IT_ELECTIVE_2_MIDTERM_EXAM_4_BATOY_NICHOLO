using ComputerLaboratoryUsageMonitoringSystem.Models;
using ComputerLaboratoryUsageMonitoringSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerLaboratoryUsageMonitoringSystem.Controllers;

[Authorize]
public class LaboratorySessionController : Controller
{
    private readonly LaboratorySessionRepository _repository;

    public LaboratorySessionController(LaboratorySessionRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index(string? search)
    {
        ViewBag.Search = search;
        return View(_repository.GetAll(search));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(LaboratorySession session)
    {
        if (!ModelState.IsValid)
        {
            return View(session);
        }

        _repository.Add(session);
        TempData["Message"] = "Laboratory session registered.";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        LaboratorySession? session = _repository.GetById(id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(LaboratorySession session)
    {
        if (!ModelState.IsValid)
        {
            return View(session);
        }

        _repository.Update(session);
        TempData["Message"] = "Laboratory session updated.";
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        LaboratorySession? session = _repository.GetById(id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    public IActionResult TimeOut(int id)
    {
        LaboratorySession? session = _repository.GetById(id);
        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult TimeOutConfirmed(int id)
    {
        _repository.RecordTimeOut(id);
        TempData["Message"] = "Time out recorded.";
        return RedirectToAction("Index");
    }
}
