using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Trello.Models;

namespace Trello.Controllers;

public class CarteController : Controller
{

    public static ProjetContext context = new();

    public CarteController()
    {
        Console.WriteLine("Carte ok!");
        
    }


    [HttpGet]
    public IActionResult Add() {
        return View();
    }

    [HttpPost]
    public IActionResult Add(string titre, string description, int idListe) 
    {
        Liste tmpListe = context.Listes.Single(l => l.IdListe == idListe);
        Carte tmpCarte = new Carte(titre, description, tmpListe);
        context.Cartes.Add(tmpCarte);
        context.SaveChanges();
        return RedirectToAction("Liste", "Home");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Carte editCard = context.Cartes.Single(c => c.IdCarte == id);
        return View(editCard);
    }

    [HttpPost]
    public IActionResult Edit(Carte formResult)
    {
        Carte editCard = context.Cartes.Single(c => c.IdCarte == formResult.IdCarte);
        Liste tmpListe = context.Listes.Single(l => l.IdListe == formResult.IdListe);
        
        editCard.Titre = formResult.Titre;
        editCard.Description = formResult.Description;
        editCard.IdListe = formResult.IdListe;
        editCard.IdListeNavigation = tmpListe;
        context.SaveChanges();
        return RedirectToAction("Liste", "Home");
    }

    // public IActionResult Delete(int id)
    // {
    //     Carte cardToRemove = context.Cartes.Single(c => c.IdCarte == id);
    //     context.Cartes.Remove(cardToRemove);
    //     context.SaveChanges();
    //     return RedirectToAction("Liste", "Home");
    // }

}
