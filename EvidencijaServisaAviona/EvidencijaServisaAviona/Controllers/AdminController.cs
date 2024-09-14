using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;
using System.Reflection;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly ZaposleniServis _zaposleniServis;
        private readonly AvionServis _avionServis;
        private readonly ServisServis _servisServis;
        private readonly IzvestajServis _izvestajServis;

        public AdminController(ZaposleniServis zaposleniServis, AvionServis avionServis, ServisServis servisServis, IzvestajServis izvestajServis)
        {
            _zaposleniServis = zaposleniServis;
            _avionServis = avionServis;
            _servisServis = servisServis;
            _izvestajServis = izvestajServis;
        }

        public IActionResult AdminPocetna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledZaposlenih(string prezime)
        {
            DataSet rezultat;



            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _zaposleniServis.DajZaposlenogPoPrezimenu(prezime);
            }
            else
            {
                rezultat = _zaposleniServis.DajSveZaposlene();
            }

            return View(rezultat);
        }


        [HttpGet]
        public IActionResult AdminPregledAviona(string proizvodjac)
        {
            DataSet rezultat;
            ViewBag.SviProizvodjaci = _avionServis.DajSveProizvodjace().Tables[0];


            if (string.IsNullOrEmpty(proizvodjac))
            {

                rezultat = _avionServis.DajSveAvione();
            }
            else
            {

                rezultat = _avionServis.DajAvionePoProizvodjacu(proizvodjac);
            }

            return View(rezultat);
        }

        [HttpGet]
        public IActionResult AdminDodajeAvion()
        {
            ViewBag.Proizvodjaci = _avionServis.DajSveProizvodjace().Tables[0];
            return View();
        }

        [HttpPost]
        public IActionResult AdminDodajeAvion(clsAvion noviAvion)
        {
            var (success, message) = _avionServis.DodajAvion(noviAvion);

            if(success)
            {
                TempData["Message"] = message;
            }
            else
            {
                TempData["Message"] = message;
            }
            ViewBag.Proizvodjaci = _avionServis.DajSveProizvodjace().Tables[0];
            return RedirectToAction("AdminPregledAviona");
        }



        public IActionResult AdminPregledServisa()
        {
            DataSet rezultat = _servisServis.DajSveServise();
            DataView dv = rezultat.Tables[0].DefaultView;
            dv.Sort = "IDServisa DESC"; 
            var sortiraniRezultat = dv.ToTable();
            rezultat.Tables.RemoveAt(0);
            rezultat.Tables.Add(sortiraniRezultat);

            return View(rezultat);
        }

        public IActionResult AdminPregledServisaDetalji(int id)
        {
         
            var servisDetalji = _servisServis.DajServisPoId(id);
            return View(servisDetalji);
        }

        public ActionResult AdminFormaZaServis()
        {
            return View("AdminDodajeServis");
        }



        [HttpPost]
        public IActionResult AdminDodajeServis(clsServis model, string action)
        {
            var (success, message) = _servisServis.NoviServis(model);

            if (success)
            {
                TempData["Message"] = message;
            }
            else
            {
                TempData["Message"] = message;
            }

            return RedirectToAction("AdminPregledServisa");
        }
        public IActionResult AdminFormaZaZaposlenog()
        {
            return View("AdminDodajeZaposlenog");
        }
        [HttpPost]
        public IActionResult AdminDodajeZaposlenog(clsZaposleni model)
        {
            var (success, message) = _zaposleniServis.NoviZaposleni(model);

            
                if (success)
                {
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Message"] = message;
                }

             
                return RedirectToAction("AdminPregledZaposlenih");
            }

            
        




        [HttpPost]
        public IActionResult AdminBriseZaposlenog(string JMBG)
        {
            var (success, message) = _zaposleniServis.ObrisiZaposlenog(JMBG);


    
        if (success)
        {
            TempData["Message"] = message;
        }
        else
        {
            TempData["Message"] = message;
        }
        return RedirectToAction("AdminPregledZaposlenih");
        }

        [HttpPost]
        public IActionResult AdminBriseAvion(int IDAviona)
        {
            var (success,message) = _avionServis.ObrisiAvion(IDAviona);

            if(success)
            { 
                TempData["Message"] = message;
            }
            else
            {
                TempData["Message"] = message;
            }

            return RedirectToAction("AdminPregledAviona");
        }
        [HttpGet]
        public IActionResult AdminPregledIzvestaja(string akcija, string filter)
        {
            DataSet rezultat;
            if (string.IsNullOrEmpty(akcija) || string.IsNullOrEmpty(filter))
            {
                rezultat = _izvestajServis.DajIzvestajIzPogleda();
                return View(rezultat);
            }
            else if (akcija == "prezime")
            {
                rezultat = _izvestajServis.DajIzvestajPoPrezimenuZaposlenog(filter);
                return View(rezultat);
            }
            else if (akcija == "proizvodjac")
            {
                rezultat = _izvestajServis.DajIzvestajPoProizvodjacu(filter);
                return View(rezultat);
            }
            else
            {
                rezultat = _izvestajServis.DajIzvestajIzPogleda();
                return View(rezultat);
            }
            
        }
        




        public IActionResult AdminPregledZaposlenogDetalji()
        {
            return View();
        }

       

        [HttpPost]
        public IActionResult IzmeniZaposlenog(string? email, string? action)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (action == "izmeni")
                {
                    clsZaposleni zaposleni = _zaposleniServis.PronadjiPoEmail(email);
                    return View("AdminPregledZaposlenogDetalji", zaposleni);
                }
               
            }

       
            return RedirectToAction("AdminPregledZaposlenih");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(string action, clsZaposleni model, string StariJMBG)
        {
            if (action == "izmeni")
            {
                string stariJMBG = StariJMBG;

                var (success, message) = _zaposleniServis.IzmeniZaposlenog(stariJMBG, model);


                if (success)
                {
                    TempData["Message"] = message;
                }
                else
                {
                    TempData["Message"] = message;
                }

                return RedirectToAction("AdminPregledZaposlenih"); 
            }

            return View(); 
        }


        public IActionResult AdminPregledIzvestajaDetalji(int? IDIzvestaja)
        {
            DataSet ds = new DataSet();

            if (IDIzvestaja.HasValue)
            {
                // Preuzmi jedan izveštaj
                var izvestaj = _izvestajServis.DajIzvestajPoId(IDIzvestaja.Value);
                var idServis = izvestaj.Tables[0].AsEnumerable().FirstOrDefault()?["IDServisa"];

                // Preuzmi potrošnju delova za taj idServis
                var potrosnjaDelova = _servisServis.DajPotrosnjuDelovaPoIdServisa((int)idServis);

                // Postavi imena tabela pre dodavanja
                DataTable dtIzvestaj = izvestaj.Tables[0].Copy();
                dtIzvestaj.TableName = "Izvestaj";
                ds.Tables.Add(dtIzvestaj);

                DataTable dtPotrosnjaDelova = potrosnjaDelova.Tables[0].Copy();
                dtPotrosnjaDelova.TableName = "PotrosnjaDelova";
                ds.Tables.Add(dtPotrosnjaDelova);
            }
            else
            {
                // Preuzmi sve izveštaje i sve potrošnje delova
                var sviIzvestaji = _izvestajServis.DajIzvestajIzPogleda();
                var sviPotrosnjaDelova = _servisServis.DajSvePotrosnjeDelova();

                // Kreiraj DataTable za potrošnje delova povezanih sa izveštajima
                DataTable dtPotrosnjaDelova = new DataTable();
               
                dtPotrosnjaDelova.Columns.Add("IDIzvestaja", typeof(int));
                dtPotrosnjaDelova.Columns.Add("IdServis", typeof(int));
                dtPotrosnjaDelova.Columns.Add("IdDeo", typeof(int));
                dtPotrosnjaDelova.Columns.Add("NazivDeo", typeof(string));
                dtPotrosnjaDelova.Columns.Add("PotrosenoKomada", typeof(int));

                // Kreiraj mapu IDServisa na listu potrošnji delova
                var izvestajiList = sviIzvestaji.Tables[0].AsEnumerable().ToList();
                
                var potrosnjaDelovaList = sviPotrosnjaDelova.Tables[0].AsEnumerable().ToList();

                // Poveži potrošnje delova sa izveštajima
                foreach (var izvestajRow in izvestajiList)
                {
                    int idServis = izvestajRow.Field<int>("IDServisa");

                    var odgovarajucaPotrosnja = potrosnjaDelovaList
                        .Where(row => row.Field<int>("IdServis") == idServis)
                        .ToList();

                    foreach (var potrosnjaRow in odgovarajucaPotrosnja)
                    {
                        dtPotrosnjaDelova.Rows.Add(
                            izvestajRow.Field<int>("IDIzvestaja"),
                            potrosnjaRow.Field<int>("IdServis"),
                            potrosnjaRow.Field<int>("IdDeo"),
                            potrosnjaRow.Field<string>("NazivDeo"),
                            potrosnjaRow.Field<int>("PotrosenoKomada")
                        ); 
                    }
                }

                // Dodaj podatke u DataSet
                DataTable dtIzvestaji = sviIzvestaji.Tables[0].Copy();
                dtIzvestaji.TableName = "Izvestaj";
                ds.Tables.Add(dtIzvestaji);

                // Dodaj tabelu potrošnje delova
                dtPotrosnjaDelova.TableName = "PotrosnjaDelova";
                ds.Tables.Add(dtPotrosnjaDelova);
            }

            return View(ds);
        }






    }
}
