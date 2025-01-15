using StackOverflow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer ans);
        void UpdateAnswer(Answer ans);
        void UpdateAnswersVoteCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionId(int qid);
        List<Answer> GetAnswersByAnswerId(int ansId);
    }
    
    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDB db;
        IQuestionsRepository qr;
        IVotesRepository vr;

        public AnswersRepository()
        {
            db = new StackOverflowDB();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void InsertAnswer(Answer ans)
        {
            db.Answers.Add(ans);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(ans.QuestionId, 1);
        }

        public void UpdateAnswer(Answer ans)
        {
            Answer answer = db.Answers.Where(temp => temp.AnswerID == ans.AnswerID).FirstOrDefault();

            if(answer != null)
            {
                answer.AnswerText = ans.AnswerText;
                db.SaveChanges();
            }
        }
        public void UpdateAnswersVoteCount(int aid, int uid, int value)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();

            if(ans != null)
            {
                ans.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionId, value);
                vr.UpdateVote(aid, uid, value);
            }
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).First();

            if (ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.AnswerID, -1);
            }
        }

        public List<Answer> GetAnswersByQuestionId(int qid)
        {
           List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == qid).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return ans;
        }

        public List<Answer> GetAnswersByAnswerId(int ansId)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == ansId).ToList();
            return ans;
        }

    }
}
