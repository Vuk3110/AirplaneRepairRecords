using System.Data;
using SlojPodataka.Interfejsi;
using DomenskiSloj;
using SlojPodataka;

namespace AplikacioniSloj
{
    public class AvionServis
    {
        private readonly IAvionRepo _repo;

        public AvionServis(IAvionRepo repo)
        {
            _repo = repo;
           
        }
        
        public DataSet DajAvionePoProizvodjacu(string proizvodjac)
        {
            return _repo.DajAvionePoProizvodjacu(proizvodjac);
        }

        public DataSet DajSveAvione()
        {
            
            return _repo.DajSveAvione();
            
        }


        public DataSet DajSveProizvodjace()
        {
            return _repo.DajSveProizvodjace();
        }

        public (bool,string) DodajAvion(clsAvion objNoviAvion)
        {
            if (_repo.DodajAvion(objNoviAvion))
            {
                return (true, "Avion je uspešno dodat");
            }
            else
                return (false, "Avion nije dodat");
        }

        public (bool,string) ObrisiAvion(int idAviona)
        {
            if( _repo.ObrisiAvion(idAviona))
            {
                return (true, "Avion je uspešno obrisan");
            }
            else
                return (false, "Avion nije obrisan");
        }
    }
}
