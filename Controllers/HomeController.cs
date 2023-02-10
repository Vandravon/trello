using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Trello.Models;

namespace Trello.Controllers;

public class HomeController : Controller
{

    public ProjetContext context = new();


    public HomeController()
    {
        Console.WriteLine("Home ok!");
        
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // FW Identity
    // Single tone
    // Session -> object qu'on ouvre et qui est avec nous
    // Créer une globale (variable où on a accès partout dans le code)

    [HttpPost]
    public IActionResult Login(string mail, string password)
    {
        using (var transaction = context.Database.BeginTransaction())
        try
        {
            Utilisateur userCheck = context.Utilisateurs.Single(u => u.AdresseEmail == mail);
            if (userCheck.MotDePasse == password)
            {
                Global.USERID = userCheck.IdUtilisateur;
                return RedirectToAction("Liste");
            }
            else
            {
                return RedirectToAction("Index");
            }
            transaction.Commit();
            Console.WriteLine("Login ok");
        }
        catch (Exception)
        {
            return RedirectToAction("Index");
        }

    }

    public IActionResult Liste()
    {
        // On part de l'utilisateur et on veut récupérer les listes qui lui sont associées
        // List<Projet> projets = new();
        // List<Liste> listes = new();

        // // On récupère les projets liés à l'utilisateur
        // Utilisateur user = context.Utilisateurs.Single(u => u.IdUtilisateur == Global.USERID);
        // List<ProjetUtilisateur> projetUtilisateurs = user.GetProjetUtilisateurs();

        // // On récupère les objets projets
        // foreach (ProjetUtilisateur projetUtilisateur in projetUtilisateurs)
        // {
        //     Projet tmp = context.Projets.Single(p => p.IdProjet == projetUtilisateur.IdProjet);
        //     projets.Add(tmp);
        // }

        // // On récupère les listes liées aux projets
        // foreach (Projet projet in projets)
        // {
        //     foreach (Liste liste in projet.IdListes)
        //     {
        //         Liste tmp = context.Listes.Single(l => l.IdListe == liste.IdListe);
        //         listes.Add(tmp);
        //     }
        // }


        // var utilisateur = context.Utilisateurs.Include( x => x.ProjetUtilisateurs)
        // .ThenInclude(x => x.Projets)
        // .ThenInclude(x => x.IdListes)
        // .ThenInclude(x => x.Cartes)
        // .First(x => x.IdUtilisateur == Global.USERID);
        // foreach (ProjetUtilisateur item in utilisateur.ProjetUtilisateurs)
        // {
        //     Console.WriteLine(item.projetNav.Nom + " " + item.projetNav.Description);
            
        // }

        Projet projet = context.Projets.Include( x => x.IdListes).ThenInclude(x => x.Cartes).First( x => x.IdProjet == 1);


        return View(projet);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
