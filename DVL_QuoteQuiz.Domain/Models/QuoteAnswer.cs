using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class QuoteAnswer
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public int QuoteId { get; set; }

        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }

        [Required]
        [Column(Order = 3)]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [Column(Order = 4)]
        public bool IsRightAnswer { get; set; }
    }
}
