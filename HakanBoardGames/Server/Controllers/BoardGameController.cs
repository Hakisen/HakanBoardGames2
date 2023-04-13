using HakanBoardGames.Server.Services.BoardGameService;
using HakanBoardGames.Shared.BoardGameModels;
using HakanBoardGames.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HakanBoardGames.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private IBoardGameService boardGameService { get; }
        public BoardGameController(IBoardGameService BoardGameService)
        {
            boardGameService = BoardGameService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<BGBoardGame>>>> GetBoardGames()
        {
            var result = await boardGameService.GetBoardGames();
            return Ok(result);
        }

        [HttpDelete("deleteboardgame/{id:guid}")]
        public async Task<ActionResult<ServiceResponse<List<BGBoardGame>>>> DeleteBoardGame(Guid id)
        {
            var result = await boardGameService.DeleteBoardGame(id);
            return Ok(result);
        }

        [HttpGet("DownloadAndSaveBoardGames")]
        public async Task<ActionResult<ServiceResponse<List<BGBoardGame>>>> DownloadAndSaveBoardGames()
        {
            var result = await boardGameService.DownloadAndSaveBoardGames();
            return Ok(result);
        }
    }
}
