using System.Data;

namespace SlojPodataka.Interfejsi
{
    public interface IServisRepo
    {
       
        public DataSet DajSveServise();
        DataSet DajSveNezavrseneServise();
        bool NoviServis(clsServis objNoviServis);
       

        public DataSet DajServisPoId(int idServisa);
        public void PromeniStatusTiketaNaOtvoren(int IDServisa);
        public bool ZaposleniImaPreuzetServis(string JMBG);

        public bool PostaviPreuzetOdStrane(string JMBG, int idServisa);

        public DataSet DajPreuzeteServise(string JMBG);

        public DataSet DajSveDelove();

        public DataSet DajPotrosnjuDelovaPoIdServisa(int idServis);
        public DataSet DajSvePotrosnjeDelova();

    }
}