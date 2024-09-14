using System.Data;

namespace SlojPodataka.Interfejsi
{
    public interface IZaposleniRepo
    {
        DataSet DajSveZaposlene();
        DataSet DajZaposlenogPoPrezimenu(string Prezime);
        bool IzmeniZaposlenog(string StariJMBG, clsZaposleni objNoviZaposleni);
        bool NoviZaposleni(clsZaposleni objNoviZaposleni);
        bool ObrisiZaposlenog(string JMBG);
        clsZaposleni PronadjiPoEmail(string email);


        public bool PostojiZaposleniSaJmbg(string jmbg);
    }
}