using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.ViewModel
{
    public class QuestionViewModel
    {
        [Required]
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCount { get; set; }

        public UserViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }
        public virtual List<AnswerViewModel> Answers { get; set; }
    }
}
