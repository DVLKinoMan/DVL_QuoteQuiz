using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class Quote
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Text { get; set; }

        [Required] [Column(Order = 3)] public bool IsDeleted { get; set; } = false;

        public ICollection<QuoteAnswer> QuoteAnswers { get; set; }
    }
}
