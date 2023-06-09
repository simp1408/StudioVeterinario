namespace StudioVeterinario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipologiaAnimale")]
    public partial class TipologiaAnimale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipologiaAnimale()
        {
            Animale = new HashSet<Animale>();
        }

        [Key]
        public int ID_TipologiaAnimale { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Nome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animale> Animale { get; set; }
    }
}
