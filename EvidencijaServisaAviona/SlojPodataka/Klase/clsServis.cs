using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
//    // Class: Servis - Prati podatke o servisima aviona.

//# Responsibility:
//- Prati podatke o servisu(IDServisa, IDAviona, DatumServisa, Status, OpisKvara).
//- Omogućava pristup i modifikaciju podataka o servisu.

//// Collaboration:
//- Sa klasom Avion(veza sa IDAviona).
//- Sa klasom Izvestaj(veza sa IDServisa).
//- Sa klasom Zaposleni(veza sa Zaposlenim koji preuzima servis).

    [Table("SERVIS")]
    public class clsServis
    {

        //polja
        [Key]
        private int _idServisa;

        [Required]
        private int _idAviona;

        [Required(ErrorMessage = "Datum servisa je obavezan.")]
        private DateTime _datumSerivsa;

        [Required]
        private int _status;

        [StringLength(500, ErrorMessage = "Opis kvara može sadržati maksimalno 500 karaktera.")]
        private string _opisKvara;
        //property
        public int IdServisa { get => _idServisa; set => _idServisa = value; }
        public int IdAviona { get => _idAviona; set => _idAviona = value; }
        public DateTime DatumSerivsa { get; set ; } = DateTime.Today;
        public int Status { get => _status; set => _status = value; }
        public string OpisKvara { get => _opisKvara; set => _opisKvara = value; }
    }
}
