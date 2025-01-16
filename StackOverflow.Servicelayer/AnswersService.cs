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
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void DeleteAnswer(int aid);
        void UpdateAnswerVotesCount(int aid, int uid, int value );
        List<AnswerViewModel> GetAnswersByQuestionId(int qid);
        AnswerViewModel GetAnswerByAnswerId(int AsnwerId);

    }

    public class AnswersService
    {
        IAnswersRepository ar;

        public AnswersService()
        {
            ar = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);
            ar.InsertAnswer(a);
        }

        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);
            ar.UpdateAnswer(a);
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswersVoteCount(aid,uid,value);
        }

        public void DeleteAnswer(int aid)
        {
            ar.DeleteAnswer(aid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionId(int qid)
        {
            List<Answer> answers = ar.GetAnswersByQuestionId(qid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<AnswerViewModel> ans = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers);

            return ans;
        }

        public AnswerViewModel GetAnswerByAnswerId(int AnswerId)
        {
            Answer ans = ar.GetAnswersByAnswerId(AnswerId).FirstOrDefault();
            AnswerViewModel a = null;

            if(ans != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                a = mapper.Map<Answer, AnswerViewModel>(ans);
            }
            return a;
        }
    }
}
