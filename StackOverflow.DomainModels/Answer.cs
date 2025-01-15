using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DomainModels
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }

        public string AnswerText { get; set; }

        public DateTime AnswerDateAndTime { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public int VotesCount { get; set; }

        [ForeignKey("UserId")]
        public virtual User Users { get; set; }

        public virtual List<Vote> Votes { get; set; }
    }
}
