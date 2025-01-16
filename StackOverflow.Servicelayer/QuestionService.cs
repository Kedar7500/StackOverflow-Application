using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using StackOverflow.DomainModels;
using StackOverflow.Repositories;
using StackOverflow.ViewModel;

namespace StackOverflow.Servicelayer
{
    public interface IQuestionService
    {
        void InsertQuestion(QuestionViewModel qvm);
        void UpdateQuestionDetails(QuestionViewModel qvm);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<NewQuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionId(int qid, int userId);

    }

    public class QuestionService
    {
        IQuestionsRepository qr;

        public QuestionService()
        {
            qr = new QuestionsRepository();
        }

        public void InsertQuestion(QuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<QuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);
        }

        public void UpdateQuestionDetails(QuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<QuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid, value);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCount(qid,value);
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid,value);
        }

        public void DeleteQuestion(int qid)
        {
            qr.DeleteQuestion(qid);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> questions = qr.GetQuestions();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> q = mapper.Map<List<Question>, List<QuestionViewModel>>(questions);
            return q;
        }

        public QuestionViewModel GetQuestionByQuestionId(int qid, int userId = 0)
        {
            Question q = qr.GetQuestionByQId(qid).FirstOrDefault();
            QuestionViewModel questionViewModel = null;

            if(q != null){

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });

                IMapper mapper = config.CreateMapper();
                questionViewModel = mapper.Map<Question, QuestionViewModel>(q);

                foreach(var item in questionViewModel.Answers)
                {
                    item.CurrentUserVoteType = 0;

                    VoteViewModel vote = item.Votes.Where(temp => temp.UserId == userId).FirstOrDefault();
                    if(vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }

            return questionViewModel;
        }

    }
}
