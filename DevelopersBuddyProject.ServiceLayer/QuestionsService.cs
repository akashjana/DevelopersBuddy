using AutoMapper;
using DevelopersBuddyProject.DomainModel;
using DevelopersBuddyProject.Repositories;
using DevelopersBuddyProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qstnViewModel);
        void UpdateQuestionDetails(EditQuestionViewModel qstnViewModel);
        void UpdateQuestionVotesCount(int qstnId, int value);
        void UpdateQuestionAnswersCount(int qstnId, int value);
        void UpdateQuestionViewsCount(int qstnId, int value);
        void DeleteQuestion(int qstnId);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionId(int qstnId, int userId);
    }
    public class QuestionsService : IQuestionsService
    {
        readonly IQuestionsRepository questionsRepository;
        public QuestionsService()
        {
            questionsRepository = new QuestionsRepository();
        }
        public void DeleteQuestion(int qstnId)
        {
            questionsRepository.DeleteQuestion(qstnId);
        }

        public QuestionViewModel GetQuestionByQuestionId(int qstnId, int userId = 0)
        {
            Question question = questionsRepository
                .GetQuestionByQuestionId(qstnId)
                .FirstOrDefault();
            QuestionViewModel questionViewModel = null;
            if (question != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                questionViewModel = mapper.Map<Question, QuestionViewModel>(question);
                foreach(var item in questionViewModel.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(x => x.UserId == userId).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return questionViewModel;
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questions = questionsRepository.GetQuestions();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> questionsViewModel = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return questionsViewModel;
        }

        public void InsertQuestion(NewQuestionViewModel qstnViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewQuestionViewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<NewQuestionViewModel, Question>(qstnViewModel);
            questionsRepository.InsertQuestion(question);
        }

        public void UpdateQuestionAnswersCount(int qstnId, int value)
        {
            questionsRepository.UpdateQuestionAnswersCount(qstnId, value);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel qstnViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditQuestionViewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question question = mapper.Map<EditQuestionViewModel, Question>(qstnViewModel);
            questionsRepository.UpdateQuestionDetails(question);
        }

        public void UpdateQuestionViewsCount(int qstnId, int value)
        {
            questionsRepository.UpdateQuestionViewsCount(qstnId, value);
        }

        public void UpdateQuestionVotesCount(int qstnId, int value)
        {
            questionsRepository.UpdateQuestionVotesCount(qstnId, value);
        }
    }
}
