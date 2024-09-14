using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka;

public class NalogController : Controller
{
    private readonly ZaposleniServis _zaposleniServis;

    public NalogController(ZaposleniServis zaposleniServis)
    {
        _zaposleniServis = zaposleniServis;
    }

   
    [HttpGet]
    public ActionResult Prijava()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Prijava(PrijavaModel model)
    {
        if (ModelState.IsValid)
        {
            
            var prijavljeniZaposleni = _zaposleniServis.PronadjiPoEmail(model.Email);

            if (prijavljeniZaposleni != null)
            {
          
                if (prijavljeniZaposleni.Lozinka == model.Lozinka)
                {
                
                    HttpContext.Session.SetString("JMBG", prijavljeniZaposleni.Jmbg);
                    HttpContext.Session.SetString("Ime", prijavljeniZaposleni.Ime);
                    HttpContext.Session.SetString("Prezime", prijavljeniZaposleni.Prezime);
                    HttpContext.Session.SetString("Email", prijavljeniZaposleni.Email);
                    HttpContext.Session.SetString("Lozinka", prijavljeniZaposleni.Lozinka);
                    HttpContext.Session.SetInt32("Ovlascenje", prijavljeniZaposleni.Ovlascenje);
                    HttpContext.Session.SetInt32("TipZaposlenog", prijavljeniZaposleni.TipZaposlenog);

               
                    if (prijavljeniZaposleni.TipZaposlenog == 1)
                    {
                        return RedirectToAction("AdminPocetna", "Admin");
                    }
                    else if (prijavljeniZaposleni.TipZaposlenog == 2)
                    {
                        return RedirectToAction("ZaposleniPocetna", "Zaposleni");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError(string.Empty, "Pogrešna lozinka");
                }
            }
            else
            {
                
                ModelState.AddModelError(string.Empty, "Zaposleni ne postoji!");
            }
        }

   
        return View(model);
    }

}
