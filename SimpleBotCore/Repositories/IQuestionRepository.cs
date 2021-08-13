using MongoDB.Bson;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IQuestionRepository
    {
        Question Get();
        Question Get(BsonElement filter);
        Task Create(string question, int position); 
    }
}
