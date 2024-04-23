using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GamePlanner1Logics;
using System.Text.RegularExpressions;
namespace GamePlanner1WebApplication.Controllers
{
    public class GamePlanner1Controller : ApiController
    {
        GameLogic gameLogic = new GameLogic();

        [HttpGet]
        [Route("api/GamePlanner1/IsGameNameExists/{GameName}")]
        public bool IsGameNameExists(string GameName)
        {
            return GameLogic.IsGameNameExists(GameName);
        }
        [HttpGet]
        [Route("api/GamePlanner1/GetMatches/{GameName}")]
        public List<Matches> GetMatches(string GameName)
        {
            return gameLogic.ViewGameDraw(GameName);
        }
        [HttpGet]
        [Route("api/GamePlanner1/Getplayers/{TeamName}")]
        public List<string> Getplayers(string TeamName)
        {
            return gameLogic.ViewPlayers(TeamName);
        }
        [HttpGet]
        [Route("api/GamePlanner1/GetTeams/{GameName}")]
        public List<string> GetTeams(string GameName)
        {
            return gameLogic.ViewTeams(GameName);
        }
        [HttpPost]
        [Route("api/GamePlanner1/PostGame")]
        public IHttpActionResult PostGame([FromBody] Game g1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!Regex.IsMatch(g1.GameName, "^[a-zA-Z].*$"))
            {
                return BadRequest("game name must start with alphabets");
            }
            if (gameLogic.AddGame(g1))
            {
                return Ok("Added Successfully");
            }
            else
            {
                return BadRequest("Failed to add game.");
            }
        }

        [HttpGet]

        [Route("api/GamePlanner1/GenerateDraw/{gameName}")]
        public IHttpActionResult GenerateDraw(string gameName)
        {
            List<string> players = gameLogic.GetTeamsData(gameName);
            if (players.Count > 0)
            {
                List<Matches> matches = gameLogic.GenerateDraw(players, gameName);
                return Ok(matches);
            }
            else
            {
                return BadRequest("game not found");
            }
        }
        [HttpPost]
        [Route("api/GamePlanner1/SaveDraw/{gameName}")]
        public IHttpActionResult SaveDraw(string gameName, [FromBody] List<Matches> matches)
        {
            if (matches == null || matches.Count == 0)
            {
                return BadRequest("Invalid draw data.");
            }
            foreach (var m in matches)
            {
                if (m.GameName != gameName)
                {
                    return BadRequest("GameName doesn't match in url");
                }

            }
            bool drawExists = gameLogic.IsGameDrawExists(gameName);
            if (drawExists)
            {
                gameLogic.DeleteDraw(gameName);
            }
            gameLogic.SaveGameDraw(matches);
            return Ok("Draw saved successfully.");
        }
        [HttpGet]
        [Route("api/GamePlanner1/GetAllGameDraws")]
        public IHttpActionResult GetAllGameDraws()
        {
            if (gameLogic.ViewAllGameDraws().Count > 0)
            {
                return Ok(gameLogic.ViewAllGameDraws());
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("api/GamePlanner1/GetAllGameNames")]
        public List<string> GetAllGameNames()
        {
            List<string> gameNames = gameLogic.GetGameNames();
            gameNames.RemoveAll(string.IsNullOrEmpty);
            return gameNames;
        }
    }
}
