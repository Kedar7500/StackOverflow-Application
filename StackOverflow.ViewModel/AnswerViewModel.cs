﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.ViewModel
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }

        public string AnswerText { get; set;}

        public DateTime AnswerDateAndTime { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public int VotesCount { get; set; }

        public virtual UserViewModel User { get; set; }

        public virtual List<VoteViewModel> Votes { get; set; }

        public int CurrentUserVoteType { get; set; }
    }
}
