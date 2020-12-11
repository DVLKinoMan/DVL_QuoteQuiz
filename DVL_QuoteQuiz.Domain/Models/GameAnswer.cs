using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class GameAnswer
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [Required]
        [Column(Order = 3)]
        public int QuoteAnswerId { get; set; }

        [ForeignKey("QuoteAnswerId")]
        public QuoteAnswer QuoteAnswer { get; set; }
    }
}
