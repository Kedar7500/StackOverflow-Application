﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.ViewModel
{
    public class EditAnswerViewModel
    {
        [Required]
        public int AnswerId { get; set;}

        [Required]
        public string AnswerText { get; set;}

        [Required]
        public DateTime AnswerDateAndTime { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int QuestionId { get; set;}

        [Required]
        public int VotesCount { get; set; }

        [Required]
        public virtual QuestionViewModel Question { get; set; }


    }
}
