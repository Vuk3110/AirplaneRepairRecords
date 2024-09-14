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
    public class clsIzvestajRepo : IIzvestajRepo
    {
       
        private string _stringKonekcije;

        public clsIzvestajRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajIzvestajIzPogleda()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajIzvestajIzPogleda", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajIzvestajPoPrezimenuZaposlenog(string Prezime)
        {

            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajIzvestajPoPrezimenuZaposlenog", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = Prezime;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }
        public DataSet DajIzvestajPoProizvodjacu(string Proizvodjac)
        {

            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajIzvestajPoProizvodjacu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar).Value = Proizvodjac;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

       

        public void KreirajIzvestajIZatvoriServis(int IDServisa, string JMBGZaposlenog, string Opis)
        {
            using (SqlConnection Veza = new SqlConnection(_stringKonekcije))
            {
                Veza.Open();
                using (SqlTransaction transakcija = Veza.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand Komanda = new SqlCommand("KreirajIzvestajIZatvoriServis", Veza, transakcija))
                        {
                            Komanda.CommandType = CommandType.StoredProcedure;
                            Komanda.Parameters.Add("@IDServisa", SqlDbType.Int).Value = IDServisa;
                            Komanda.Parameters.Add("@JMBGZaposlenog", SqlDbType.NVarChar).Value = JMBGZaposlenog;
                            Komanda.Parameters.Add("@Opis", SqlDbType.NVarChar).Value = Opis;
                            Komanda.ExecuteNonQuery();
                        }
                        transakcija.Commit();
                    }
                    catch
                    {
                        transakcija.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataSet DajIzvestajPoId(int IDIzvestaja)
        {

            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajIzvestajPoId", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDIzvestaja", SqlDbType.NVarChar).Value = IDIzvestaja;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public void KreirajPotrosnjuDela(int[] delovi, int[] kolicine, int idServis)
        {
            if (delovi.Length != kolicine.Length)
            {
                throw new ArgumentException("Broj delova mora biti isti kao broj količina.");
            }

            // Check available parts in the database
            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlTransaction transakcija = veza.BeginTransaction())
                {
                    try
                    {
                        // Check if there are enough parts in stock
                        for (int i = 0; i < delovi.Length; i++)
                        {
                            using (SqlCommand komanda = new SqlCommand("SELECT Stanje FROM Deo WHERE Id = @IdDeo", veza, transakcija))
                            {
                                komanda.Parameters.AddWithValue("@IdDeo", delovi[i]);
                                int stanje = (int)komanda.ExecuteScalar();
                                if (stanje < kolicine[i])
                                {
                                    throw new InvalidOperationException($"Nema dovoljno na stanju za deo sa ID {delovi[i]}. Dostupno: {stanje}, Traženo: {kolicine[i]}.");
                                }
                            }
                        }

                        // Create DataTable to represent Table-Valued Parameter
                        var table = new DataTable();
                        table.Columns.Add("IdDeo", typeof(int));
                        table.Columns.Add("PotrosenoKomada", typeof(int));

                        for (int i = 0; i < delovi.Length; i++)
                        {
                            table.Rows.Add(delovi[i], kolicine[i]);
                        }

                        using (SqlCommand komanda = new SqlCommand("dbo.KreirajPotrosnjuDela", veza, transakcija))
                        {
                            komanda.CommandType = CommandType.StoredProcedure;
                            komanda.Parameters.AddWithValue("@PotrosnjaDela", table).SqlDbType = SqlDbType.Structured;
                            komanda.Parameters.AddWithValue("@IdServis", idServis);
                            komanda.ExecuteNonQuery();
                        }
                        transakcija.Commit();
                    }
                    catch
                    {
                        transakcija.Rollback();
                        throw;
                    }
                }
            }
        }







    }
}
