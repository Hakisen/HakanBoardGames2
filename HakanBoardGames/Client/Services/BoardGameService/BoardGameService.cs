using HakanBoardGames.Shared.Models;
using MudBlazor;
using System.Net.Http.Json;

namespace HakanBoardGames.Client.Services.BoardGameService
{
    public class BoardGameService : IBoardGameService
    {
        public event Action? BoardGamesChanged;
        private HttpClient http { get; }
        public BoardGameService(HttpClient Http)
        {
            http = Http;
        }

        public List<BGBoardGame> BoardGames { get; set; } = new List<BGBoardGame>();
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public bool ShowMudProgressCircular { get; set; } = false;
        public bool ShowMudAlert { get; set; } = false;
        public Severity MudBlazorSeverity { get; set; } = Severity.Info;

        public async Task GetBoardGames()
        {
            ShowMudAlert = true;
            MudBlazorSeverity = Severity.Info;
            Message = "Loading boardgames...";
            ShowMudProgressCircular = true;
            BoardGamesChanged!.Invoke();
            var response = await http.GetFromJsonAsync<ServiceResponse<List<BGBoardGame>>>($"api/boardgame");
            if (response != null && response.Data != null)
            {
                BoardGames = response.Data;
                Message = response.Message;
                Success = response.Success;
            }
            if (BoardGames.Count == 0)
            {
                Success = false;
                Message = "No BoardGames were found.";
            }
            ShowMudAlert = true;
            if (response!.Success)
            {
                MudBlazorSeverity = Severity.Success;
            }
            else
            {
                MudBlazorSeverity = Severity.Error;
            }
            ShowMudProgressCircular = false;
            BoardGamesChanged!.Invoke();
        }

        public async Task DownlodAndSaveBoardGame()
        {
            ShowMudAlert = true;
            MudBlazorSeverity = Severity.Info;
            Message = "Please wait...";
            ShowMudProgressCircular = true;
            BoardGamesChanged!.Invoke();
            var response = await http.GetFromJsonAsync<ServiceResponse<List<BGBoardGame>>>("api/boardgame/DownloadAndSaveBoardGames");
            if (response != null && response.Data != null)
            {
                BoardGames = response.Data;
                Message = response.Message;
                Success = response.Success;
            }
            if (response!.Success && BoardGames.Count == 0)
            {
                Success = false;
                Message = "No BoardGames were found.";
            }
            ShowMudAlert = true;
            if (response!.Success)
            {
                MudBlazorSeverity = Severity.Success;
            }
            else
            {
                MudBlazorSeverity = Severity.Error;
            }
            ShowMudProgressCircular = false;
            BoardGamesChanged!.Invoke();
        }

        public async Task DeleteBoardGame(Guid id)
        {
            ShowMudAlert = true;
            MudBlazorSeverity = Severity.Info;
            Message = $"Please wait, deleting...";
            ShowMudProgressCircular = true;
            BoardGamesChanged!.Invoke();
            var response = await http.DeleteAsync($"api/boardgame/DeleteBoardGame/{id}");
            if (response != null)
            {
                //TO DO
                Message = string.Empty;
            }
            ShowMudProgressCircular = false;
            await GetBoardGames();
            BoardGamesChanged!.Invoke();
        }
    }
}
