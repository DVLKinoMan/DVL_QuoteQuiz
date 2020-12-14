using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVL_QuoteQuiz.Domain.Models
{
    public class User
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

        //todo password
        //public byte[] PasswordHash { get; set; }

        //todo check if email is valid somehow
        [Required]
        [Column(Order = 3)]
        [MaxLength(254)]
        public string Email { get; set; } = default!;

        [Column(Order = 4)]
        public Gender? Gender { get; set; }

        //todo: want default value as false
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(Order = 5)] public bool IsAdmin { get; set; }

        //todo: want default value as false
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(Order = 6)] public bool IsDisabled { get; set; }

        //todo: default value datetime.now
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdatedDateTime { get; set; }

        public IList<UserAnsweredQuote>? AnsweredQuotes { get; set; } = default!;
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
