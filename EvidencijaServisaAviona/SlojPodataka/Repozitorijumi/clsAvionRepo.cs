using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using SlojPodataka.Klase;

namespace SlojPodataka.Repozitorijumi
{
    public class clsAvionRepo : IAvionRepo
    {
       
        private string _stringKonekcije;

        
        public clsAvionRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

      
        public DataSet DajSveAvione()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajAvioneIzPogleda", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajSveProizvodjace()
        {
            DataSet dsPodaci = new DataSet();

            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                SqlCommand Komanda = new SqlCommand("DajSveProizvodjace", Veza); 
                Komanda.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = Komanda;
                da.Fill(dsPodaci);
            }

            return dsPodaci;
        }




        public DataSet DajAvionePoProizvodjacu(string proizvodjac)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajAvionePoProizvodjacu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Naziv", SqlDbType.NVarChar).Value = proizvodjac;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public string DajProizvodjacaPoAvionu(int idAviona)
        {
            string idProizvodjaca = "";
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                SqlCommand Komanda = new SqlCommand("DajProizvodjacaPoAvionu", Veza)
                {
                    CommandType = CommandType.StoredProcedure
                };
                Komanda.Parameters.Add("@IDAviona", SqlDbType.Int).Value = idAviona;
                SqlDataReader reader = Komanda.ExecuteReader();
                if (reader.Read())
                {
                    idProizvodjaca = reader.GetString(0); 
                }
            }
            return idProizvodjaca;
        }

        public bool DodajAvion(clsAvion objNoviAvion)
        
            
            {
                int proveraUnosa = 0;

                using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
                {
                    Veza.Open();
                    using (SqlCommand Komanda = new SqlCommand("DodajAvion", Veza))
                    {
                        Komanda.CommandType = CommandType.StoredProcedure;
                        Komanda.Parameters.Add("@IDProizvodjaca", SqlDbType.Int).Value = objNoviAvion.IdProizvodjaca;
                        Komanda.Parameters.Add("@Model", SqlDbType.NVarChar, 30).Value = objNoviAvion.Model;
                        Komanda.Parameters.Add("@DatumProizvodnje", SqlDbType.Date).Value = objNoviAvion.DatumProizvodnje;


                    proveraUnosa = Komanda.ExecuteNonQuery();
                    Veza.Close();
                    Veza.Dispose();
                }
                }

                return (proveraUnosa > 0);
            }

        public bool ObrisiAvion(int IdAviona)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("ObrisiAvion", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDAviona", SqlDbType.NVarChar).Value = IdAviona;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);

        }

        public bool PostojiAvionSaId(int idAviona)
        {
            bool postoji = false;

            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlCommand komanda = new SqlCommand("ProveriIDAviona", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    komanda.Parameters.Add("@IDAviona", SqlDbType.Int).Value = idAviona;

                    int brojRedova = (int)komanda.ExecuteScalar();
                    if (brojRedova > 0)
                    {
                        postoji = true;
                    }
                }
            }

            return postoji;
        }




    }
}


