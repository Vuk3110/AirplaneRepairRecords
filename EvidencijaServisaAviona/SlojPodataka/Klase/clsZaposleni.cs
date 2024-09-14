using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace SlojPodataka
{
//    // Class: Zaposleni - Prati podatke o zaposlenima.

//# Responsibility:
//- Prati podatke o zaposlenom(JMBG, Ime, Prezime, Email, Lozinka, Ovlascenje, TipZaposlenog).
//- Omogućava pristup i modifikaciju podataka o zaposlenom.

//// Collaboration:
//- Sa klasom Servis(veza sa servisima koje zaposleni preuzima).
//- Sa klasom Izvestaj(veza sa izveštajima koje zaposleni kreira).

    [Table("ZAPOSLENI")]
    public class clsZaposleni
    {
        //  polja
        private string _jmbg;
        private string _ime;
        private string _prezime;
        private string _email;
        private string _lozinka;
        private int _ovlascenje;
        private int _tipZaposlenog;

        //property sa validacijom
        [Key]
        [Required(ErrorMessage = "JMBG je obavezan.")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "JMBG mora sadrzati 13 cifara")]
        public string Jmbg
        {
            get => _jmbg;
            set => _jmbg = value;
        }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(20, ErrorMessage = "Ime može sadržati maksimalno 20 karaktera.")]
        public string Ime
        {
            get => _ime;
            set => _ime = value;
        }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(40, ErrorMessage = "Prezime može sadržati maksimalno 40 karaktera.")]
        public string Prezime
        {
            get => _prezime;
            set => _prezime = value;
        }

        [Required(ErrorMessage = "E-mail je obavezan.")]
        [StringLength(50, ErrorMessage = "E-mail može sadržati maksimalno 50 karaktera.")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}$", ErrorMessage = "Format e-maila je nepravilan")]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        [StringLength(20, ErrorMessage = "Lozinka može sadržati maksimalno 20 karaktera.")]
        public string Lozinka
        {
            get => _lozinka;
            set => _lozinka = value;
        }

        [Required(ErrorMessage = "Ovlascenje je obavezno.")]
        public int Ovlascenje
        {
            get => _ovlascenje;
            set => _ovlascenje = value;
        }

        
        public int TipZaposlenog
        {
            get => _tipZaposlenog;
            set => _tipZaposlenog = value;
        }
    }
}
