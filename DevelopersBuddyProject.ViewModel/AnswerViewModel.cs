using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.ViewModel
{
    public class AnswerViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int VotesCount { get; set; }
        public int CurrentUserVoteType { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }

    }
}
