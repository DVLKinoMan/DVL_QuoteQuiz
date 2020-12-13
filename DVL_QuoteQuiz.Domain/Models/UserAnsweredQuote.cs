using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class UserAnsweredQuote
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public int QuoteId { get; set; }

        [ForeignKey("QuoteId")] public Quote Quote { get; set; } = default!;

        [Column(Order = 3)]
        public int? AnsweredAuthorId { get; set; }

        [ForeignKey("AnsweredAuthorId")] public Author? Author { get; set; }

        [Required]
        [Column(Order = 4)]
        public int UserId { get; set; }

        [ForeignKey("UserId")] public User User { get; set; } = default!;

        [Required] [Column(Order = 5)] public bool YesNoQuestion { get; set; } = true;

        [Required] [Column(Order = 6)] public bool AnsweredRight { get; set; } = false;

        [Column(Order = 7)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AnsweredDateTime { get; set; }
    }
}
