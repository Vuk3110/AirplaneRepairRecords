using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Data.SqlClient;

namespace SlojPodataka.Repozitorijumi
{
    public class clsZaposleniRepo : IZaposleniRepo
    {
     
        private string _stringKonekcije;

      
        public clsZaposleniRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

 
        public DataSet DajSveZaposlene()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveZaposlene", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }


        
        public DataSet DajZaposlenogPoPrezimenu(string Prezime)
        {

            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajZaposlenogPoPrezimenu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = Prezime;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool PostojiZaposleniSaJmbg(string jmbg)
        {
            bool postoji = false;

            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlCommand komanda = new SqlCommand("ProveriJMBG", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    komanda.Parameters.Add("@Jmbg", SqlDbType.NVarChar).Value = jmbg;

                    
                    int brojRedova = (int)komanda.ExecuteScalar();
                    if (brojRedova > 0)
                    {
                        postoji = true;
                    }
                }
            }

            return postoji;
        }

        public bool NoviZaposleni(clsZaposleni objNoviZaposleni)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviZaposleni", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = objNoviZaposleni.Jmbg;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviZaposleni.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviZaposleni.Prezime;
            Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = objNoviZaposleni.Email;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviZaposleni.Lozinka;
            Komanda.Parameters.Add("@Ovlascenje", SqlDbType.Int).Value = objNoviZaposleni.Ovlascenje;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

           
            return (proveraUnosa > 0);
        }

        public bool ObrisiZaposlenog(string JMBG)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("ObrisiZaposlenog", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = JMBG;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);

        }


        public bool IzmeniZaposlenog(string StariJMBG, clsZaposleni objNoviZaposleni)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();

            SqlCommand Komanda = new SqlCommand("IzmeniZaposlenog", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@StariJMBG", SqlDbType.NVarChar).Value = StariJMBG;
            Komanda.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = objNoviZaposleni.Jmbg;
            Komanda.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = objNoviZaposleni.Ime;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = objNoviZaposleni.Prezime;
            Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = objNoviZaposleni.Email;
            Komanda.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = objNoviZaposleni.Lozinka;
            Komanda.Parameters.Add("@Ovlascenje", SqlDbType.Int).Value = objNoviZaposleni.Ovlascenje;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            return (proveraUnosa > 0);
        }


        public clsZaposleni PronadjiPoEmail(string email)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {

                Veza.Open();
                SqlCommand Komanda = new SqlCommand("PronadjiZaposlenogPoEmailu", Veza);
                Komanda.CommandType = CommandType.StoredProcedure;
                Komanda.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                using (SqlDataReader Reader = Komanda.ExecuteReader())
                {
                    if (Reader.Read())
                    {
                        return MapirajRedUObjekat(Reader);
                    }
                    else
                    {
                        return null; 
                    }
                }
              
            }

        }


        private clsZaposleni MapirajRedUObjekat(SqlDataReader reader)
        {
            return new clsZaposleni
            {
                Jmbg = reader["JMBG"] != DBNull.Value ? reader["JMBG"].ToString() : string.Empty,
                Ime = reader["Ime"] != DBNull.Value ? reader["Ime"].ToString() : string.Empty,
                Prezime = reader["Prezime"] != DBNull.Value ? reader["Prezime"].ToString() : string.Empty,
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                Lozinka = reader["Lozinka"] != DBNull.Value ? reader["Lozinka"].ToString() : string.Empty,
                Ovlascenje = reader["Ovlascenje"] != DBNull.Value ? Convert.ToInt32(reader["Ovlascenje"]) : 0,
                TipZaposlenog = reader["TipZaposlenog"] != DBNull.Value ? Convert.ToInt32(reader["TipZaposlenog"]) : 0
            };
        }


    }
}