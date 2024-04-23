using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace GamePlanner1Logics
{
    public class GameLogic
    {

        static string connectionString = @"Data Source=DESKTOP-TGVPSNC\TESTDB;Initial Catalog=gameplanner1;Integrated Security=True";
        

        public bool AddGame(Game g)
        {
            bool isSuccessful = false;

            // Check if game name already exists and delete if needed
            if (IsGameNameExists(g.GameName))
            {
                DeleteData(g.GameName);
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert into games table
                    string gamesInsertQuery = "INSERT INTO games(gameName, noOfTeams) VALUES (@GameName, @participantNumber)";
                    using (SqlCommand command = new SqlCommand(gamesInsertQuery, con))
                    {
                        command.Parameters.AddWithValue("@GameName", g.GameName);
                        command.Parameters.AddWithValue("@participantNumber", g.NoOfTeams);
                        int gamesRowsAffected = command.ExecuteNonQuery();
                        isSuccessful = gamesRowsAffected > 0;
                    }

                    // Insert into teams table
                    string teamsQuery = "INSERT INTO teams(gameName, teamName, NoOfParticipants) VALUES (@GameName, @teamName, @NoOfParticipants)";
                    using (SqlCommand command = new SqlCommand(teamsQuery, con))
                    {
                        foreach (var team in g.Teams)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@GameName", g.GameName);
                            command.Parameters.AddWithValue("@teamName", team.TeamName);
                            command.Parameters.AddWithValue("@NoOfParticipants", team.NoOfParticipants);
                            int teamsRowsAffected = command.ExecuteNonQuery();
                            if (teamsRowsAffected == 0)
                            {
                                isSuccessful = false;
                                Console.WriteLine("Failed to insert team: " + team.TeamName);
                                break;
                            }

                            // Insert into participants table for each team
                            string participantsQuery = "INSERT INTO participants(gameName, teamName, participantName) VALUES (@GameName, @TeamName, @ParticipantName)";
                            using (SqlCommand command1 = new SqlCommand(participantsQuery, con))
                            {
                                foreach (var participant in team.Participants)
                                {
                                    command1.Parameters.Clear();
                                    command1.Parameters.AddWithValue("@GameName", g.GameName);
                                    command1.Parameters.AddWithValue("@TeamName", team.TeamName);
                                    command1.Parameters.AddWithValue("@ParticipantName", participant);
                                    int participantsRowsAffected = command1.ExecuteNonQuery();
                                    if (participantsRowsAffected == 0)
                                    {
                                        isSuccessful = false;
                                        Console.WriteLine("Failed to insert participant: " + participant);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e) { }
            return isSuccessful;
        }

        public static void DeleteData(string gameName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    
                    string query = "delete from Matches where gameName=@GameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@GameName", gameName);
                        command.ExecuteNonQuery();
                    }
                    string query1 = "delete from Participants where gameName=@GameName";
                    using (SqlCommand command = new SqlCommand(query1, con))
                    {
                        command.Parameters.AddWithValue("@GameName", gameName);
                        command.ExecuteNonQuery();
                    }
                    string query3 = "delete from teams where gameName=@GameName";
                    using (SqlCommand command = new SqlCommand(query3, con))
                    {
                        command.Parameters.AddWithValue("@GameName", gameName);
                        command.ExecuteNonQuery();

                    }
                    string query2 = "delete from games where gameName=@GameName";
                    using (SqlCommand command = new SqlCommand(query2, con))
                    {
                        command.Parameters.AddWithValue("@GameName", gameName);
                        command.ExecuteNonQuery();

                    }
                    
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured {e.Message}");
            }
        }
        public void SaveGameDraw(List<Matches> matches)
        {
            //List<Matches> matches = matches;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "insert into matches(gameName,team1,team2) values(@gameName,@player1,@player2)";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        for (int i = 0; i < matches.Count; i++)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@gameName", matches[i].GameName);
                            command.Parameters.AddWithValue("@player1", matches[i].Team1);
                            command.Parameters.AddWithValue("@player2", matches[i].Team2);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.${e.Message}");
            }
        }
        public List<string> GetTeamsData(string gameName)
        {

            List<string> teamsdata = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select teamName from teams where gameName=@gameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@gameName", gameName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                teamsdata.Add(reader["teamName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return teamsdata;
        }
        public List<Matches> ViewGameDraw(string GameName)
        {
            List<Matches> matches = new List<Matches>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select GameName,team1,team2 from matches where GameName=@gameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@gameName", GameName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Matches m = new Matches();
                                m.GameName = reader["gameName"].ToString();
                                m.Team1 = reader["Team1"].ToString();
                                m.Team2 = reader["Team2"].ToString();
                                matches.Add(m);
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return matches;
        }
        public void DeleteDraw(string gameName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "delete from matches  where gamename=@gameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@gameName", gameName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
        }
        public static bool IsGameNameExists(string GameName)
        {
            int gameCount = 0;
            //string connectionString = @"Data Source=DESKTOP-TGVPSNC\TESTDB;Initial Catalog=gamepalnner1;Integrated Security=True";
            string query = "select count(gameId) from games where gameName=@GameName";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@GameName", GameName); // Changed parameter name
                        gameCount = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return gameCount > 0;
        }

        public List<Matches> ViewAllGameDraws()
        {
            List<Matches> games = new List<Matches>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select gameName, Team1,Team2 from Matches order by gameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Matches m = new Matches();
                                m.GameName = reader["gameName"].ToString();
                                m.Team1 = reader["Team1"].ToString();
                                m.Team2 = reader["Team2"].ToString();
                                games.Add(m);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return games;
        }
        public List<string> GetGameNames()
        {
            List<string> gamenames = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select gamename from games;";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string gamename = reader["gamename"].ToString();
                                gamenames.Add(gamename);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return gamenames;
        }

        
        public List<Matches> GenerateDraw(List<string> teams, string GameName)
        {
            List<Matches> match = new List<Matches>();
            Random random = new Random();
            int playerCount = teams.Count;
            teams = teams.OrderBy(x => random.Next()).ToList();
            if (playerCount % 2 != 0)
            {
                teams.Add("Next Round");
            }


            for (int i = 0; i < playerCount; i = i + 2)
            {

                Matches m = new Matches();
                m.GameName = GameName;
                m.Team1 = teams[i];
                m.Team2 = teams[i + 1];
                match.Add(m);
            }

            return match;
        }
        public bool IsGameDrawExists(string GameName)
        {
            int count = 0;
            try
            {
                string connectionString = @"Data Source=DESKTOP-TGVPSNC\TESTDB;Initial Catalog=gamepalnner;Integrated Security=True";
                string query = "select count(gameName) from matches where gameName=@gameName";
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    con.Open();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@gameName", GameName);
                        count = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Error Occured .{e.Message}");
            }
            return count > 0;
        }
        public List<string> ViewPlayers(string teamName)
        {
            List<string> players = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select participantName from participants where teamName=@teamName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@teamName", teamName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Name = reader["participantName"].ToString();
                                players.Add(Name);
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return players;
        }
        public List<string> ViewTeams(string GameName)
        {
            List<string> teams = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "select teamName from teams where GameName=@gameName";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@gameName", GameName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Name = reader["teamName"].ToString();
                                teams.Add(Name);
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                GamePlannerDataManager.PrintMessage($"Exception occured.{e.Message}");
            }
            return teams;
        }
    }
}
