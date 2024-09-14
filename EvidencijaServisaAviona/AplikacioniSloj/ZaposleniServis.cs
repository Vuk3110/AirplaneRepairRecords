using System.Data;
using SlojPodataka.Interfejsi;
using DomenskiSloj;
using SlojPodataka;

namespace AplikacioniSloj
{
    public class ZaposleniServis
    {
        private readonly IZaposleniRepo _repo;
        private readonly clsPoslovnaPravila _pravila;

        public ZaposleniServis(IZaposleniRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _pravila = poslovnaPravila;
            _repo = repo;
        }

        public DataSet DajSveZaposlene()
        {
            return _repo.DajSveZaposlene();
        }

        public DataSet DajZaposlenogPoPrezimenu(string prezime)
        {
            return _repo.DajZaposlenogPoPrezimenu(prezime);
        }

        public (bool,string) NoviZaposleni(clsZaposleni objNoviZaposleni)
        {
            if (_pravila.PostojiZaposleniSaJmbg(objNoviZaposleni.Jmbg))
            {
                return (false, "Zaposleni sa unetim JMBG-om već postoji!");
            }
            else
            {
                if (_repo.NoviZaposleni(objNoviZaposleni))
                {
                    return (true, "Zaposleni je uspešno dodat!");
                }
                else
                    return (false, "Zaposleni nije dodat!");
            }
        }


        public (bool, string) ObrisiZaposlenog(string jmbg)
        {
            if(_repo.ObrisiZaposlenog(jmbg))
            {
                return (true, "Zaposleni je uspešno obrisan");
            }
            return (false, "Zaposleni nije obrisan");
        }

        

        public (bool, string) IzmeniZaposlenog(string stariJMBG, clsZaposleni objNoviZaposleni)
        {
            if( _repo.IzmeniZaposlenog(stariJMBG, objNoviZaposleni))
            {
                return (true, "Zaposleni je uspešno izmenjen");
            }
            return (false, "Zaposleni nije izmenjen");
        }

        public clsZaposleni PronadjiPoEmail(string email)
        {
            return _repo.PronadjiPoEmail(email);
        }
    }
}
