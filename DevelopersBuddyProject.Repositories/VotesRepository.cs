using DevelopersBuddyProject.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersBuddyProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int answerId, int userId, int value);
    }
    public class VotesRepository:IVotesRepository
    {
        readonly DevelopersBuddyDatabaseDbContext db;
        public VotesRepository()
        {
            db = new DevelopersBuddyDatabaseDbContext();
        }

        public void UpdateVote(int answerId, int userId, int value)
        {
            int updateValue = 0;
            if (value > 0) updateValue = 1; 
            else if (value < 0) updateValue = -1;
            Vote existingVote = db.Votes.Where(x => x.AnswerId == answerId && x.UserId == userId).FirstOrDefault();
            if (existingVote != null)
            {
                existingVote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote()
                {
                    AnswerId = answerId,
                    UserId = userId,
                    VoteValue=updateValue
                };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }
    }
}
