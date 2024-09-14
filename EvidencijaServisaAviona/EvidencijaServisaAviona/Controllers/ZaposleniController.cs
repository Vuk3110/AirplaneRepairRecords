using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using AplikacioniSloj;
using SlojPodataka;
using System.Data;
using DomenskiSloj;

namespace PrezentacioniSloj.Controllers
{
    public class ZaposleniController : Controller
    {

        private readonly ZaposleniServis _zaposleniServis;
        private readonly IzvestajServis _izvestajServis;
        private readonly ServisServis _servisServis;
        private readonly AvionServis _avionServis;
        private readonly clsPoslovnaPravila _poslovnaPravila;

        public ZaposleniController(ZaposleniServis zaposleniServis, IzvestajServis izvestajServis, ServisServis servisServis, AvionServis avionServis, clsPoslovnaPravila poslovnaPravila )
        {
            _zaposleniServis = zaposleniServis;
            _izvestajServis = izvestajServis;
            _servisServis = servisServis;
            _avionServis = avionServis;
            _poslovnaPravila = poslovnaPravila;
        }
        public IActionResult ZaposleniPocetna()
        {
            var JMBG = HttpContext.Session.GetString("JMBG");
            DataSet rezultat;
            rezultat = _servisServis.DajSveNezavrseneServise();

           
            DataView dv = rezultat.Tables[0].DefaultView;
            dv.Sort = "IDServisa DESC"; 
            var sortiraniRezultat = dv.ToTable();
            rezultat.Tables.RemoveAt(0);
            rezultat.Tables.Add(sortiraniRezultat);


            DataSet preuzetiServisi = _servisServis.DajPreuzeteServise(JMBG);
            ViewBag.PreuzetiServisi = preuzetiServisi;
            return View(rezultat);
          
        }

        [HttpPost]
        public IActionResult IzaberiServis(int idServisa)
        {
            var JMBG = HttpContext.Session.GetString("JMBG");

            if (idServisa == 0)
            {
                TempData["Message"] = "Nevažeći ID servisa.";
                return RedirectToAction("ZaposleniPocetna");
            }

            var (success, message) = _servisServis.PromeniStatusTiketaNaOtvoren(JMBG, idServisa);

            if (success)
            {
                _servisServis.PostaviPreuzetOdStrane(JMBG, idServisa);
                TempData["Message"] = message;
            }
            else
            {
                TempData["Message"] = message;
            }

            return RedirectToAction("ZaposleniPocetna");
        }


        public IActionResult PrikaziServis(int idServisa)
        {
            
                var servis = _servisServis.DajServisPoId(idServisa);
            var delovi = _servisServis.DajSveDelove();
            ViewBag.Delovi = delovi;
            return View("ZaposleniServisDetalji", servis);
           
        }

        [HttpPost]
        public IActionResult DodajIzvestaj(int idServisa, string opis, int[] delovi, int[] kolicina)
        {
            var JMBG = HttpContext.Session.GetString("JMBG");
            var message = _izvestajServis.KreirajIzvestajIZatvoriServis(idServisa, JMBG, opis);

            _izvestajServis.KreirajPotrosnjuDela(delovi, kolicina, idServisa);

            return RedirectToAction("ZaposleniPocetna", TempData["Message"] = message);
        }





    }
}
