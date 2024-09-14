using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IZaposleniRepo _repoZaposleni;
        private IServisRepo _repoServis;
        private IAvionRepo _repoAvion;

        public clsPoslovnaPravila(IZaposleniRepo repoZaposleni, IServisRepo repoServis, IAvionRepo repoAvion)
        {
            _repoZaposleni = repoZaposleni;
            _repoServis = repoServis;
            _repoAvion = repoAvion;
        }

        public bool PostojiZaposleniSaJmbg(string jmbg)
        {
            return _repoZaposleni.PostojiZaposleniSaJmbg(jmbg);
        }

        public bool PostojiAvionSaId(int idAviona)
        {
            return _repoAvion.PostojiAvionSaId(idAviona);
        }

        public bool ZaposleniImaOvlascenje(string JMBG, int IDServisa)
        {
            DataSet zaposleniPodaci = _repoZaposleni.DajSveZaposlene();
            DataRow zaposleni = zaposleniPodaci.Tables[0].AsEnumerable()
                .FirstOrDefault(row => row.Field<string>("JMBG") == JMBG);
            if (zaposleni == null)
            {
                throw new Exception("Zaposleni nije pronađen.");
            }

            string ovlascenjeIDProizvodjaca = zaposleni.Field<string>("Ovlascenje");

            DataSet servisPodaci = _repoServis.DajSveServise();
            DataRow servis = servisPodaci.Tables[0].AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("IDServisa") == IDServisa);
            if (servis == null)
            {
                throw new Exception("Servis nije pronađen.");
            }

            int idAviona = servis.Field<int>("IDAviona");
            string idProizvodjacaAviona =(_repoAvion.DajProizvodjacaPoAvionu(idAviona)).ToString();

            return ovlascenjeIDProizvodjaca == idProizvodjacaAviona;
        }

        public bool ZaposleniMozeDaPreuzmeServis(string JMBG)
        {
            if (_repoServis.ZaposleniImaPreuzetServis(JMBG))
            {
                return false; 
            }
            else return true;
        }
    }
}
