using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Trello.Models;

namespace Trello.Controllers;

public class ListeController : Controller
{

    public static ProjetContext context = new();

    public ListeController()
    {
        Console.WriteLine("Liste ok!");
        
    }

    [HttpGet]
    public IActionResult Add() {
        return View();
    }

    [HttpPost]
    public IActionResult Add(string name, int projectId) 
    {
        Projet tmpProjet = context.Projets.Single(p => p.IdProjet == projectId);
        Liste tmpListe = new Liste(name, tmpProjet);
        context.Listes.Add(tmpListe);
        context.SaveChanges();
        return RedirectToAction("Liste", "Home");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Liste editList = context.Listes.Single(l => l.IdListe == id);
        return View(editList);
    }

    [HttpPost]
    public IActionResult Edit(Liste formResult)
    {
        Liste editList = context.Listes.Single(l => l.IdListe == formResult.IdListe);
        editList.Nom = formResult.Nom;
        editList.IdProjet = formResult.IdProjet;
        context.SaveChanges();
        return RedirectToAction("Liste", "Home");
    }

    // public IActionResult Delete(int id)
    // {
    //     Liste listToRemove = context.Listes.Single(l => l.IdListe == id);
    //     context.Listes.Remove(listToRemove);
    //     context.SaveChanges();
    //     return RedirectToAction("Liste", "Home");
    // }

}
