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
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel newAnsViewModel);
        void UpdateAnswer(EditAnswerViewModel edtAnsViewModel);
        void UpdateAnswerVotesCount(int answerId, int userId, int value);
        void DeleteAnswer(int answerId);
        List<AnswerViewModel> GetAnswerByQuestionId(int questionId);
        AnswerViewModel GetAnswerByAnswerId(int answerId);
    }
    public class AnswersService:IAnswersService
    {
        IAnswersRepository answersRepository;
        public AnswersService()
        {
            answersRepository = new AnswersRepository();
        }

        public void DeleteAnswer(int answerId)
        {
            answersRepository.DeleteAnswer(answerId);
        }

        public AnswerViewModel GetAnswerByAnswerId(int answerId)
        {
            Answer answer = answersRepository.GetAnswerByAnswerId(answerId).FirstOrDefault();
            AnswerViewModel answerViewModel = null;
            if (answer != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                answerViewModel = mapper.Map<Answer, AnswerViewModel>(answer);
            }
            return answerViewModel;
        }

        public List<AnswerViewModel> GetAnswerByQuestionId(int questionId)
        {
            List<Answer> answers = answersRepository.GetAnswerByQuestionId(questionId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> answerViewModel = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers);
            return answerViewModel;
        }

        public void InsertAnswer(NewAnswerViewModel newAnsViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewAnswerViewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<NewAnswerViewModel, Answer>(newAnsViewModel);
            answersRepository.InsertAnswer(answer);
        }

        public void UpdateAnswer(EditAnswerViewModel edtAnsViewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EditAnswerViewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<EditAnswerViewModel, Answer>(edtAnsViewModel);
            answersRepository.UpdateAnswer(answer);
        }

        public void UpdateAnswerVotesCount(int answerId, int userId, int value)
        {
            answersRepository.UpdateAnswerVotesCount(answerId, userId, value);
        }
    }
}
