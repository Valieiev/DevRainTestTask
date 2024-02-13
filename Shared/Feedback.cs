using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevRain.Shared
{
    public class Feedback
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "Feedback")]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Sentiment")]
        public string Sentiment { get; set; }

        [Display(Name = "Positive Score")]
        public decimal? PositiveScore { get; set; }

        [Display(Name = "Negative Score")]
        public decimal? NegativeScore { get; set; }

        [Display(Name = "Neutral Score")]
        public decimal? NeutralScore { get; set; }
    }
}
