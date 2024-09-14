using System.Data;

namespace SlojPodataka.Interfejsi
{
    public interface IAvionRepo
    {
        DataSet DajAvionePoProizvodjacu(string proizvodjac);
        DataSet DajSveAvione();

        public DataSet DajSveProizvodjace();
        public string DajProizvodjacaPoAvionu(int idAviona);

        public bool DodajAvion(clsAvion objNoviAvion);

        public bool ObrisiAvion(int IdAviona);

        public bool PostojiAvionSaId(int idAviona);
    }
}