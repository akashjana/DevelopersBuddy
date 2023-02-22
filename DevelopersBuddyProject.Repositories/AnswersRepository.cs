using DevelopersBuddyProject.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void UpdateAnswerVotesCount(int answerId, int userId, int value);
        void DeleteAnswer(int answerId);
        List<Answer> GetAnswerByQuestionId(int questionId);
        List<Answer> GetAnswerByAnswerId(int answerId);
    }
    public class AnswersRepository:IAnswersRepository
    {
        readonly DevelopersBuddyDatabaseDbContext db;
        IQuestionsRepository questionsRepository;
        IVotesRepository votesRepository;
        public AnswersRepository()
        {
            db = new DevelopersBuddyDatabaseDbContext();
            questionsRepository = new QuestionsRepository();
            votesRepository = new VotesRepository();
        }

        public void DeleteAnswer(int answerId)
        {
            Answer existingAnswer = db.Answers.Where(x => x.AnswerId == answerId).FirstOrDefault();
            if (existingAnswer != null)
            {
                db.Answers.Remove(existingAnswer);
                db.SaveChanges();
                questionsRepository.UpdateQuestionAnswersCount(existingAnswer.QuestionId, -1);
            }
        }

        public List<Answer> GetAnswerByAnswerId(int answerId)
        {
            List<Answer> answers = db.Answers.Where(x => x.AnswerId == answerId).ToList();
            return answers;
        }

        public List<Answer> GetAnswerByQuestionId(int questionId)
        {
            List<Answer> answers = db.Answers.Where(x => x.QuestionId == questionId).OrderByDescending(x => x.AnswerDateAndTime).ToList();
            return answers;
        }

        public void InsertAnswer(Answer answer)
        {
            db.Answers.Add(answer);
            db.SaveChanges();
            questionsRepository.UpdateQuestionAnswersCount(answer.QuestionId, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
            Answer existingAnswer = db.Answers.Where(x => x.AnswerId == answer.AnswerId).FirstOrDefault();
            if (existingAnswer != null)
            {
                existingAnswer.AnswerText = answer.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int answerId, int userId, int value)
        {
            Answer existingAnswer = db.Answers.Where(x => x.AnswerId == answerId).FirstOrDefault();
            if (existingAnswer != null)
            {
                existingAnswer.VotesCount +=value;
                db.SaveChanges();
                questionsRepository.UpdateQuestionVotesCount(existingAnswer.QuestionId, value);
                votesRepository.UpdateVote(answerId, userId, value);
            }
        }
    }
}
