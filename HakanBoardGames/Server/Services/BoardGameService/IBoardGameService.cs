using HakanBoardGames.Shared.BoardGameModels;
using HakanBoardGames.Shared.Models;

namespace HakanBoardGames.Server.Services.BoardGameService
{
    public interface IBoardGameService
    {
        Task<ServiceResponse<List<BGBoardGame>>> GetBoardGames();
        Task<ServiceResponse<List<BGBoardGame>>> DeleteBoardGame(Guid id);
        Task<ServiceResponse<List<BGBoardGame>>> DownloadAndSaveBoardGames();

    }
}
