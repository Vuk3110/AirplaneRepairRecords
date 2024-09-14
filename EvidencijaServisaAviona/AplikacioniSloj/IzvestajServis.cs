using System.Data;
using SlojPodataka.Interfejsi;
using DomenskiSloj;

namespace AplikacioniSloj
{
    public class IzvestajServis
    {
        private readonly IIzvestajRepo _repo;

        public IzvestajServis(IIzvestajRepo repo)
        {
            _repo = repo;
        }



        public DataSet DajIzvestajIzPogleda()
        {
            return _repo.DajIzvestajIzPogleda();
        }

        public DataSet DajIzvestajPoId(int IDIzvestaja)
        {
           return _repo.DajIzvestajPoId(IDIzvestaja);
        }

        public DataSet DajIzvestajPoPrezimenuZaposlenog(string Prezime)
        {
            return _repo.DajIzvestajPoPrezimenuZaposlenog(Prezime);
        }

        public DataSet DajIzvestajPoProizvodjacu(string Proizvodjac)
        {
            return _repo.DajIzvestajPoProizvodjacu(Proizvodjac);
        }

        public string KreirajIzvestajIZatvoriServis(int IDServisa, string JMBGZaposlenog, string Opis)
        {
            _repo.KreirajIzvestajIZatvoriServis(IDServisa, JMBGZaposlenog, Opis);
            return ("Izveštaj je uspešno kreiran.");
        }


        public void KreirajPotrosnjuDela(int[] delovi, int[] kolicine, int idServis)
        {
            if (delovi.Length != kolicine.Length)
            {
                throw new ArgumentException("Broj delova mora biti isti kao broj količina.");
            }

            _repo.KreirajPotrosnjuDela(delovi, kolicine, idServis );
        }

    }
}
