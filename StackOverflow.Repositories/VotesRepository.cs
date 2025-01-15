using StackOverflow.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int aid, int uid, int value);
    }

    public class VotesRepository : IVotesRepository
    {
        StackOverflowDB db;

        public VotesRepository()
        {
            db = new StackOverflowDB();
        }
        public void UpdateVote(int aid, int uid, int value)
        {
            int updateValue;
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;

            Vote vote = db.Votes.Where(temp => temp.AnswerId == aid && temp.UserId == uid).FirstOrDefault();

            if(vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerId = aid, UserId = uid, VoteValue = value };
                db.Votes.Add(newVote);
            }

            db.SaveChanges();
        }
    }
}
