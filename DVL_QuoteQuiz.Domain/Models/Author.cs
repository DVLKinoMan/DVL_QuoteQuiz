using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class Author
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Column(Order = 2)]
        public string Name { get; set; }
    }
}
