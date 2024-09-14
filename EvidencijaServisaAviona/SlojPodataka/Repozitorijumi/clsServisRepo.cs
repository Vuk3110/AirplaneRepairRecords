using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;

namespace SlojPodataka.Repozitorijumi
{
    public class clsServisRepo : IServisRepo
    {
        
        private string _stringKonekcije;

        
        public clsServisRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSveNezavrseneServise()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveNezavrseneServise", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajSveServise()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveServise", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }



        public bool NoviServis(clsServis objNoviServis)
        {
           
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviServis", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDAviona", SqlDbType.Int).Value = objNoviServis.IdAviona;
            Komanda.Parameters.Add("@DatumServisa", SqlDbType.Date).Value = objNoviServis.DatumSerivsa;
            Komanda.Parameters.Add("@OpisKvara", SqlDbType.NVarChar).Value = objNoviServis.OpisKvara;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

          
            return (proveraUnosa > 0);
        }

        public DataSet DajServisPoId(int idServisa)
        {
            DataSet servis = new DataSet();

            SqlConnection veza = new SqlConnection(_stringKonekcije);
            veza.Open();
            SqlCommand komanda = new SqlCommand("DajServisPoId", veza);
            komanda.CommandType = CommandType.StoredProcedure;
            komanda.Parameters.Add("@IDServisa", SqlDbType.Int).Value = idServisa;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(servis);
            veza.Close();
            veza.Dispose();

            return servis;
        }


    

        public void PromeniStatusTiketaNaOtvoren(int IDServisa)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                using (SqlCommand Komanda = new SqlCommand("PromeniStatusTiketaNaOtvoren", Veza))
                {
                    Komanda.CommandType = CommandType.StoredProcedure;
                    Komanda.Parameters.Add("@IDServisa", SqlDbType.Int).Value = IDServisa;
                    Komanda.ExecuteNonQuery();
                }
            }
        }

        public bool ZaposleniImaPreuzetServis(string JMBG)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                using (SqlCommand Komanda = new SqlCommand("ProveriDaLiZaposleniImaServis", Veza))
                {
                    Komanda.CommandType = CommandType.StoredProcedure;
                    Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = JMBG;
                    int count = (int)Komanda.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool PostaviPreuzetOdStrane(string JMBG, int idServisa)
        {
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            SqlCommand Komanda = new SqlCommand("PostaviPreuzetOdStraneZaServis", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = JMBG;
            Komanda.Parameters.Add("@IDServisa", SqlDbType.Int).Value = idServisa;

            Veza.Open();
            int rezultat = Komanda.ExecuteNonQuery();
            Veza.Close();

            return rezultat > 0;
        }

        public DataSet DajPreuzeteServise(string JMBG)
        {
            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            SqlDataAdapter Adapter = new SqlDataAdapter("DajPreuzeteServise", Veza);
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            Adapter.SelectCommand.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = JMBG;
            DataSet DataSet = new DataSet();
            Adapter.Fill(DataSet);
            return DataSet;
        }

        public DataSet DajSveDelove()
        {
            DataSet dsDelovi = new DataSet();

            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlCommand komanda = new SqlCommand("DajSveDelove", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = komanda;
                    da.Fill(dsDelovi);
                }
            }

            return dsDelovi;
        }

        public DataSet DajPotrosnjuDelovaPoIdServisa(int idServis)
        {
            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                using (SqlCommand komanda = new SqlCommand("DajPotrosnjuDelovaPoIdServisa", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    komanda.Parameters.Add(new SqlParameter("@IdServis", idServis));

                    SqlDataAdapter adapter = new SqlDataAdapter(komanda);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }

        public DataSet DajSvePotrosnjeDelova()
        {
            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                using (SqlCommand komanda = new SqlCommand("DajSvePotrosnjeDelova", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }


    }
}
