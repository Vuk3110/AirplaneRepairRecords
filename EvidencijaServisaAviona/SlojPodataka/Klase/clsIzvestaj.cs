using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Klase { 

// Class: Izvestaj - Prati podatke o izveštajima servisa.

//# Responsibility:
//- Prati podatke o izveštaju(IDIzvestaja, IDServisa, JMBGZaposlenog, Opis).
//- Omogućava pristup i modifikaciju podataka o izveštaju.

//// Collaboration:
//- Sa klasom Servis(veza sa IDServisa).
//- Sa klasom Zaposleni(veza sa JMBGZaposlenog).

    [Table("IZVESTAJ")]
    public class clsIzvestaj
    {
        //polja
        [Key]
        private int _idIzvestaja;

        [Required]
        private int _idServisa;

        [Required]
        [RegularExpression(@"^[0-9]{13}$")]
        private string _jmbgZaposlenog;

        [Required]
        private string _opis;
        //property
        public int IdIzvestaja { get => _idIzvestaja; set => _idIzvestaja = value; }
        public int IdServisa { get => _idServisa; set => _idServisa = value; }
        public string JmbgZaposlenog { get => _jmbgZaposlenog; set => _jmbgZaposlenog = value; }
        public string Opis { get => _opis; set => _opis = value; }
    }
}
