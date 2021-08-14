using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IQuestionSqlRepository
    {
        Task Create(string question, int position);
    }
}
