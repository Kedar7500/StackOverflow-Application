﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DomainModels
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        public string QuestionName { get; set; }

        public DateTime QuestionDateAndTime { get; set; }

        public int UserID { get; set; }

        public int CategoryID { get; set; }

        public int VotesCount { get; set; }

        public int AnswerCount { get; set; }

        public int ViewsCount { get; set; }

        [ForeignKey("UserID")]
        public virtual User UserType { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
