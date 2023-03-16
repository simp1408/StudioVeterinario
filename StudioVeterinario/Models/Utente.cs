namespace StudioVeterinario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utente")]
    public partial class Utente
    {
        [Key]
        
        public int ID_utente { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Pwd { get; set; }

       
        public string Ruolo { get; set; }
    }
}
