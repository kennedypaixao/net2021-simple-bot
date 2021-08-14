using SimpleBotCore.Bot;
using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBot : BotDialog
    {
        IUserProfileRepository _userProfile;
        IQuestionMongoRepository _questionMongoRepository;
        IQuestionSqlRepository _questionSQLRepository;

        public SimpleBot(IUserProfileRepository userProfile, IQuestionMongoRepository questionMongoRepository, IQuestionSqlRepository questionSqlRepository)
        {
            _userProfile = userProfile;
            _questionMongoRepository = questionMongoRepository;
            _questionSQLRepository = questionSqlRepository;
        }

        protected async override Task BotConversation()
        {
            SimpleUser user = _userProfile.TryLoadUser(UserId);

            // Create a user if it is null
            if (user == null)
            {
                user = _userProfile.Create(new SimpleUser(UserId));
            }

            await WriteAsync("Boa noite!");

            if (user.Nome != null && user.Idade != 0 && user.Cor != null)
            {
                await WriteAsync(
                    $"{user.Nome}, de {user.Idade} anos, " +
                    $"vejo que cadastrou sua cor preferida como {user.Cor}");
            }


            if (user.Nome == null)
            {
                await WriteAsync("Qual o seu nome?");

                user.Nome = await ReadAsync();

                _userProfile.AtualizaNome(UserId, user.Nome);
            }

            if (user.Idade == 0)
            {
                await WriteAsync("Qual a sua idade?");

                user.Idade = Int32.Parse(await ReadAsync());

                _userProfile.AtualizaIdade(UserId, user.Idade);
            }

            if (user.Cor == null)
            {
                await WriteAsync("Qual a sua cor preferida?");

                user.Cor = await ReadAsync();

                _userProfile.AtualizaCor(UserId, user.Cor);
            }

            await WriteAsync($"{user.Nome}, bem vindo ao Oraculo. Você tem direito a 3 perguntas");

            for (int i = 0; i < 3; i++)
            {
                string texto = await ReadAsync();

                if (texto.EndsWith("?"))
                {
                    await WriteAsync("Processando...");

                    await _questionMongoRepository.Create(texto, i);
                    await _questionSQLRepository.Create(texto, i);

                    // SQL DE CRIAÇÃO DA TABELA UTILIZADA
                    // CREATE TABLE dbo.Question(
                    //    ID bigint NOT NULL IDENTITY(1, 1),
                    //    Description nvarchar(MAX) NOT NULL,
                    //    Position nchar(10) NOT NULL,
                    //    CONSTRAINT PK_Question Primary Key(ID)
                    //)

                    await WriteAsync($"Pergunta salva com sucesso.");

                    if (i < 3)
                        await WriteAsync($"Qual sua próxima pergunta?");
                }
                else
                {
                    await WriteAsync("Você disse: " + texto);
                }
            }
        }
    }
}
