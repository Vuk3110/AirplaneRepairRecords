using System.Data;
using SlojPodataka.Interfejsi;
using DomenskiSloj;
using SlojPodataka;

namespace AplikacioniSloj
{
    public class ServisServis
    {
        private readonly IServisRepo _repo;

        private readonly clsPoslovnaPravila _poslovnaPravila;

        public ServisServis(IServisRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;

        }



        public DataSet DajSveServise()
        {
            return _repo.DajSveServise();
        }
        public DataSet DajSveNezavrseneServise()
        {
            return _repo.DajSveNezavrseneServise();
        }

        public DataSet DajServisPoId(int id)
        {
            return _repo.DajServisPoId(id);
        }

        public (bool, string) NoviServis(clsServis objNoviServis)
        {
            if (!_poslovnaPravila.PostojiAvionSaId(objNoviServis.IdAviona))
            {
                return (false, "Avion sa unetim ID-jem ne postoji");
            }
            else
            {

                if (_repo.NoviServis(objNoviServis))
                {
                    return (true, "Servis je uspešno dodat!");
                }
                else
                    return (false, "Servis nije dodat!");
            }
        }

        public (bool success, string message) PromeniStatusTiketaNaOtvoren(string JMBG, int IDServisa)
        {
            if (_poslovnaPravila.ZaposleniImaOvlascenje(JMBG, IDServisa))
            {
                if (_poslovnaPravila.ZaposleniMozeDaPreuzmeServis(JMBG))
                {
                    _repo.PromeniStatusTiketaNaOtvoren(IDServisa);
                    return (true, "Servis uspešno preuzet.");
                }
                else
                {
                    return (false, "Zaposleni već ima preuzet servis.");
                }
            }
            else
            {
                return (false, "Zaposleni nema ovlašćenje za ovaj servis.");
            }
        }




        public bool PostaviPreuzetOdStrane(string JMBG, int idServisa)
        {
            return _repo.PostaviPreuzetOdStrane(JMBG, idServisa);
        }

        public DataSet DajPreuzeteServise(string JMBG)
        {
            return _repo.DajPreuzeteServise(JMBG);
        }

        public DataSet DajSveDelove()
        {
            return _repo.DajSveDelove();
        }

        public DataSet DajPotrosnjuDelovaPoIdServisa(int idServisa)
        {
            return _repo.DajPotrosnjuDelovaPoIdServisa(idServisa);
        }

        public DataSet DajSvePotrosnjeDelova()
        {
            return _repo.DajSvePotrosnjeDelova();
        }
    }
}
