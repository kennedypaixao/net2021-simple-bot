using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class QuestionMongoRepository : IQuestionMongoRepository
    {
        private MongoClient _client = null;
        private IMongoCollection<BsonDocument> _col = null;

        public QuestionMongoRepository(MongoClient client)
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

    }
}
