using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private MongoClient _client = null;
        private IMongoCollection<BsonDocument> _col = null;

        public QuestionRepository(MongoClient client)
        {
            _client = client;
            _col = _client.GetDatabase("dbBot").GetCollection<BsonDocument>("Question");
        }

        public async Task Create(string question, int position)
        {
            var doc = new BsonDocument()
            {
                {"Description", question },
                {"Position", position },
            };

            await _col.InsertOneAsync(doc);
        }

        public Question Get()
        {
            throw new NotImplementedException();
        }

        public Question Get(BsonElement filter)
        {
            throw new NotImplementedException();
        }
    }
}
