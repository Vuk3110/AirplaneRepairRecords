using System.Data;

namespace SlojPodataka.Interfejsi
{
    public interface IIzvestajRepo
    {
       
        DataSet DajIzvestajIzPogleda();
        DataSet DajIzvestajPoPrezimenuZaposlenog(string Prezime);
        DataSet DajIzvestajPoProizvodjacu(string Proizvodjac);
        void KreirajIzvestajIZatvoriServis(int IDServisa, string JMBGZaposlenog, string Opis);
        public void KreirajPotrosnjuDela(int[] delovi, int[] kolicine, int idServis);



        public DataSet DajIzvestajPoId(int IDIzvestaja);
    }
}