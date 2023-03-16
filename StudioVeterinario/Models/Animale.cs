namespace StudioVeterinario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Animale")]
    public partial class Animale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animale()
        {
            Visita = new HashSet<Visita>();
        }

        [Key]
        public int ID_Animale { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Registrazione")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataRegistrazione { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Colore")]
        public string ColoreMantello { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Nascita")]
        public DateTime? DataNascita { get; set; }

        
        public bool Microchip { get; set; }

        [StringLength(50)]
        [Display(Name = "Nr.chip")]
        public string NumeroMicrochip { get; set; }

        [StringLength(50)]
        [Display(Name = "Proprietario")]
        public string NominativoProprietario { get; set; }

        public bool Smarrito { get; set; }

        [StringLength(50)]
        public string Foto { get; set; }
        [NotMapped()]
        public HttpPostedFileBase FileFoto { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Ricovero")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataInizioRicovero { get; set; }

        [Display(Name = "Tipo")]
        public int Id_TipologiaAnimale { get; set; }
        [Display(Name = "Tipo")]
        public virtual TipologiaAnimale TipologiaAnimale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
