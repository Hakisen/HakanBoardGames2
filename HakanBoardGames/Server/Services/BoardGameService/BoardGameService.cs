using HakanBoardGames.Shared.BoardGameModels;
using HakanBoardGames.Shared.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazorVisualDNA.Server.Data;
using System.Text.Json;

namespace HakanBoardGames.Server.Services.BoardGameService
{
    public class BoardGameService : IBoardGameService
    {
        private DataContext context { get; }
        private HttpClient http { get; }

        public BoardGameService(DataContext Context, HttpClient Http)
        {
            context = Context;
            http = Http;
        }

        public async Task<ServiceResponse<List<BGBoardGame>>> GetBoardGames()
        {
            var boardgames = await context.BoardGames!
                .Include(h => h.Players)
                .Include(h => h.Categories)
                .Include(h => h.Creators)
                .AsNoTracking().ToListAsync().ConfigureAwait(false);

            return new ServiceResponse<List<BGBoardGame>>
            {
                Data = boardgames,
                Message = $"BoardGames were loaded."
            };
        }

        public async Task<ServiceResponse<List<BGBoardGame>>> DeleteBoardGame(Guid id)
        {
            var boardgames = new List<BGBoardGame>();
            BGBoardGame? dbBGBoardGame = await context.BoardGames!.FindAsync(id);
            //BGBoardGame? dbBGBoardGame = await context.BoardGames!.FirstOrDefaultAsync(g => g.BGBoardGameDBId == id);
            if (dbBGBoardGame == null)
            {
                // TO DO
                //boardgames = await context.BoardGames!
                //       .Include(h => h.Players)
                //       .Include(h => h.Categories)
                //       .Include(h => h.Creators)
                //       .AsNoTracking().ToListAsync().ConfigureAwait(false);
                return new ServiceResponse<List<BGBoardGame>>
                {
                    Success = false,
                    Data = boardgames,
                    Message = "BGBoardGame was not found."
                };
            }

            context.BoardGames!.Remove(dbBGBoardGame);
            var tt = await context.SaveChangesAsync();
            var r = tt;
            // TO DO
            //boardgames = await context.BoardGames!
            //           .Include(h => h.Players)
            //           .Include(h => h.Categories)
            //           .Include(h => h.Creators)
            //           .AsNoTracking().ToListAsync().ConfigureAwait(false);
            return new ServiceResponse<List<BGBoardGame>>
            {
                Data = boardgames,
                Message = "BGBoardGame was deleted."
            };
        }

        public async Task<ServiceResponse<List<BGBoardGame>>> DownloadAndSaveBoardGames()
        {
            ServiceResponse<List<BGBoardGame>> serviceResponse;

            JsonDocument jsonDocument;

            try
            {
                jsonDocument = await DownloadFileAsync();
                if (jsonDocument == null)
                {
                    throw new Exception($"JsonDocument is empty. Failure when downloading .json content from website!");
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<BGBoardGame>>
                {
                    Success = false,
                    Data = await context.BoardGames!.AsNoTracking().ToListAsync().ConfigureAwait(false),
                    Message = e.Message,
                };
            }

            try
            {
                serviceResponse = await ParseAndSaveJsonDocumentAsync(jsonDocument);
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<BGBoardGame>>
                {
                    Success = false,
                    Data = await context.BoardGames!.AsNoTracking().ToListAsync().ConfigureAwait(false),
                    Message = e.Message,
                };
            }

            return serviceResponse;
        }

        private async Task<JsonDocument> DownloadFileAsync()
        {
            JsonDocument jsonDocument;

            using (var streamReader = new StreamReader(@"boardgames123.json"))
            {
                jsonDocument = JsonDocument.Parse(await streamReader.ReadToEndAsync());
            }

            //DO NOT DELETE!!!!!!!!!!!!!!!!
            //using (http)
            //{
            //    http.BaseAddress = new Uri("https://www.familytreedna.com/public/y-dna-haplotree/");

            //    http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json"));
            //    http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    var url = $"http://case.senarion.com/boardgames";

            //    // add ApiKey 

            //    using (var response = await http.GetAsync(url))
            //    {
            //        using (var stream = await response.Content.ReadAsStreamAsync())
            //        {
            //            jsonDocument = await JsonDocument.ParseAsync(stream);
            //        }
            //    }
            //}
            //DO NOT DELETE!!!!!!!!!!!!!!!!
            return jsonDocument;
        }

        private async Task<ServiceResponse<List<BGBoardGame>>> ParseAndSaveJsonDocumentAsync(JsonDocument jsonDocument)
        {
            ServiceResponse<List<BGBoardGame>> serviceResponse = new ServiceResponse<List<BGBoardGame>>()
            {
                Success = false,
                Data = await context.BoardGames!.AsNoTracking().ToListAsync().ConfigureAwait(false),
                Message = $"Error has occurred. Data was not saved to database."
            };

            using (jsonDocument)
            {
                JsonElement rootElement = jsonDocument.RootElement;

                var boardGamesList = new List<BGBoardGame>();
                foreach (var jsonProperty in rootElement.EnumerateArray())
                {
                    var boardGame = new BGBoardGame();

                    if (jsonProperty.TryGetProperty("id", out JsonElement idElement))
                    {
                        boardGame.BoardGameId = idElement!.GetString()!;
                    }
                    else
                    {
                        throw new ArgumentNullException($"Boardgame id is missing.");
                    }

                    if (jsonProperty.TryGetProperty("name", out JsonElement nameElement))
                    {
                        boardGame.Name = nameElement!.GetString()!;
                    }
                    else
                    {
                        throw new ArgumentNullException($"Boardgame name is missing.");
                    }

                    if (jsonProperty.TryGetProperty("released", out JsonElement releasedElement))
                    {
                        boardGame.Releasd = releasedElement!.GetString()!;
                    }

                    if (jsonProperty.TryGetProperty("tagline", out JsonElement taglineElement))
                    {
                        boardGame.Tagline = taglineElement!.GetString()!;
                    }

                    if (jsonProperty.TryGetProperty("age", out JsonElement ageElement))
                    {
                        boardGame.Age = ageElement.GetInt32();
                    }

                    var creatorsList = new List<BGCreator>();
                    if (jsonProperty.TryGetProperty("creators", out JsonElement creatorsElement))
                    {
                        foreach (var creatorJsonElement in creatorsElement.EnumerateArray())
                        {
                            var creatorValue = creatorJsonElement.GetString();
                            var creator = new BGCreator
                            {
                                FullName = creatorValue!,
                                BoardGameId = boardGame.Id,
                                BoardGame = boardGame,
                            };
                            creatorsList.Add(creator);
                        }
                    }

                    var categoriesList = new List<BGCategory>();
                    if (jsonProperty.TryGetProperty("categories", out JsonElement categoriesElement))
                    {
                        foreach (var categoryJsonElement in categoriesElement.EnumerateArray())
                        {
                            var categoryValue = categoryJsonElement.GetString();
                            var category = new BGCategory
                            {
                                Category = categoryValue!,
                                BoardGameId = boardGame.Id,
                                BoardGame = boardGame,
                            };
                            categoriesList.Add(category);
                        }
                    }

                    var playersList = new List<BGPlayer>();
                    if (jsonProperty.TryGetProperty("players", out JsonElement playersElement))
                    {
                        var player = new BGPlayer
                        {
                            BoardGameId = boardGame.Id,
                            BoardGame = boardGame,
                        };

                        if (playersElement.TryGetProperty("min", out JsonElement minElement))
                        {
                            player.Min = minElement.GetInt32();
                        }

                        if (playersElement.TryGetProperty("max", out JsonElement maxElement))
                        {
                            player.Max = minElement.GetInt32();
                        }
                        playersList.Add(player);
                    }

                    boardGame.Players!.AddRange(playersList);
                    boardGame.Creators!.AddRange(creatorsList);
                    boardGame.Categories!.AddRange(categoriesList);

                    boardGame.SavedToDatabaseDate = DateTime.Now;

                    boardGamesList.Add(boardGame);

                    context.BoardGames!.Add(boardGame);
                };

                var savedRecordsCount = await context.SaveChangesAsync();

                if (savedRecordsCount > 0)
                {
                    var boardgames = await context.BoardGames!
                        .Include(h => h.Players)
                        .Include(h => h.Categories)
                        .Include(h => h.Creators)
                        .AsNoTracking().ToListAsync().ConfigureAwait(false);

                    serviceResponse.Success = true;
                    serviceResponse.Data = boardgames;
                    serviceResponse.Message = $"Success. BoardGames were saved to database.";
                }
            }
            return serviceResponse;
        }
    }
}

