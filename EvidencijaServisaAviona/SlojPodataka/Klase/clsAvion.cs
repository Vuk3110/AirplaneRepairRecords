using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{

// Class: Avion - Prati podatke o avionima.

//// Responsibility:
//- Prati podatke o avionu(ID, IDProizvodjaca, Model, DatumProizvodnje).
//- Omogućava pristup i modifikaciju podataka o avionu.

//// Collaboration:
//- Sa klasom Servis(veza sa IDAviona).


    [Table("AVION")]
    public class clsAvion
    {
        //polja
        [Key]
        private int _idAviona;

        [Required]
        private int _idProizvodjaca;

        [Required]
        [StringLength(30)]
        private string _model;

        [Required]
        
        private DateTime _datumProizvodnje;

        //property
        public int IdAviona { get => _idAviona; set => _idAviona = value; }
        public int IdProizvodjaca { get => _idProizvodjaca; set => _idProizvodjaca = value; }
        public string Model { get => _model; set => _model = value; }
        public DateTime DatumProizvodnje { get => _datumProizvodnje; set => _datumProizvodnje = value; }





    }
}
