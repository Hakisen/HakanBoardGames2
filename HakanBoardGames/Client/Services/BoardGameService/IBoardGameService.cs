using MudBlazor;

namespace HakanBoardGames.Client.Services.BoardGameService
{
    public interface IBoardGameService
    {
        List<BGBoardGame> BoardGames { get; set; }

        string Message { get; set; }
        bool Success { get; set; }
        bool ShowMudProgressCircular { get; set; }
        bool ShowMudAlert { get; set; }
        Severity MudBlazorSeverity { get; set; }

        Task GetBoardGames();
        Task DownlodAndSaveBoardGame();

        Task DeleteBoardGame(Guid id);

        event Action BoardGamesChanged;
    }
}
