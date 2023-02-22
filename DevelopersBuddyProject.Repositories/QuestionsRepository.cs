using DevelopersBuddyProject.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        void UpdateQuestionVotesCount(int questionId, int value);
        void UpdateQuestionAnswersCount(int questionId, int value);
        void UpdateQuestionViewsCount(int questionId, int value);
        void DeleteQuestion(int questionId);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionId(int questionId);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        readonly DevelopersBuddyDatabaseDbContext db;
        public QuestionsRepository()
        {
            db = new DevelopersBuddyDatabaseDbContext();
        }
        public void DeleteQuestion(int questionId)
        {
            Question existingQuestion = db.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (existingQuestion != null)
            {
                db.Questions.Remove(existingQuestion);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestionByQuestionId(int questionId)
        {
            List<Question> questions = db.Questions.Where(x => x.QuestionId==questionId).ToList();
            return questions;
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = db.Questions.OrderByDescending(x => x.QuestionDateAndTime).ToList();
            return questions;
        }

        public void InsertQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int questionId, int value)
        {
            Question existingQuestion = db.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question existingQuestion = db.Questions.Where(x => x.QuestionId == question.QuestionId).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.QuestionName = question.QuestionName;
                existingQuestion.QuestionDateAndTime = question.QuestionDateAndTime;
                existingQuestion.CategoryId = question.CategoryId;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int questionId, int value)
        {
            Question existingQuestion = db.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int questionId, int value)
        {
            Question existingQuestion = db.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.VotesCount += value;
                db.SaveChanges();
            }
        }
    }
}
