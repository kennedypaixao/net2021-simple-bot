using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IQuestionMongoRepository
    {
        Task Create(string question, int position); 
    }
}
