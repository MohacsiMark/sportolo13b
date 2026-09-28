using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sportolo13b.Models;
using sportolo13b.Models.DTOs;
using MySqlConnector;

namespace sportolo13b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=sportolo13b;";

        [HttpGet]
        public object GetSportolok()    
        {
            List<Eredmeny> eredmeny = new List<Eredmeny>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM eredmeny";

            var cmd = new MySqlCommand(sql, connection);

            var data = cmd.ExecuteReader();

            while(data.Read())
            {
                eredmeny.Add(new Eredmeny
                {
                    Id = data.GetInt32("id"),
                    Competition = data.GetString("competition"),
                    Description = data.GetString("description"),
                    ResultTime =data.GetDateTime("resultTime"),
                    UpdateTime = data.GetDateTime("updateTime"),
                    SportoloId = data.GetInt32("sportoloId")
                });
            }
            connection.Close();

            return eredmeny;
        }

        [HttpGet("{id:int}")]
        public object GetEredmenyId(int id)
        {
            List<Eredmeny> eredmeny = new List<Eredmeny>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"SELECT * FROM `eredmeny` WHERE `id`=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read()==true)
            {
                eredmeny.Add(new Eredmeny
                {
                    Id = datareader.GetInt32("id"),
                    Competition = datareader.GetString("competition"),
                    Description = datareader.GetString("description"),
                    ResultTime = datareader.GetDateTime("resultTime"),
                    UpdateTime = datareader.GetDateTime("updateTime"),
                    SportoloId = datareader.GetInt32("sportoloId")
                });
            }
            connection.Close();

            return eredmeny;
        }

        [HttpPost]

        public object PostEredmeny([FromBody] PostEredmenyDTO postEredmenyDTO)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"INSERT INTO `eredmeny`(`competition`, `description`, `resultTime`, `updateTime`, `sportoloId`) VALUES (@competition, @description, @resultTime, @updateTime, @sportoloId)";
            
            var cmd = new MySqlCommand(sql, connection);
            
            cmd.Parameters.AddWithValue("@competition", postEredmenyDTO.Competition);
            cmd.Parameters.AddWithValue("@description", postEredmenyDTO.Description);
            cmd.Parameters.AddWithValue("@resultTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@updateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@sportoloId", postEredmenyDTO.SportoloId);
            
            cmd.ExecuteNonQuery();

            connection.Close();

            return new { message = "Sikeres felvétel.", result = postEredmenyDTO };
        }

        [HttpPut]

        public object PutEredmeny([FromQuery] int id,PutEredmenyDTO putEredmenyDTO)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"UPDATE `eredmeny` SET `competition`=@competition, `description`=@description, `updateTime`=@updateTime, `sportoloId`=@sportoloId WHERE Id=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@competition", putEredmenyDTO.Competition);
            cmd.Parameters.AddWithValue("@description", putEredmenyDTO.Description);
            cmd.Parameters.AddWithValue("@updateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@sportoloId", putEredmenyDTO.SportoloId);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new { message = "Sikeres módosítás.", result = putEredmenyDTO };
        }

        [HttpDelete]
        public object DeleteEredmeny(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"DELETE FROM `eredmeny` WHERE Id=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new { message = "Sikeres törlés.", result = "" };
        }
    }
}
