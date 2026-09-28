using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using sportolo13b.Models;
using sportolo13b.Models.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sportolo13b.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportolokController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=sportolo13b;";

        [HttpGet("NameEmailId")]

        public object GetSportolokNameEmailId(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT `name`,`email` FROM `sportolo` WHERE `id`=@id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read()==true)
            {
                Sportolok sportolok=new Sportolok()
                {
                    Name = datareader.GetString("name"),
                    Email = datareader.GetString("email")
                };
                if(sportolok != null)
                {
                    data = new
                    {
                        result="Sikeres lekérés", sportolok.Name,
                        sportolok.Email
                    };
                }
            }
            connection.Close();

            return data;
        }

        [HttpGet("Minden")]

        public object GetSportolokMinden(int id)
        {
            List<Eredmeny> eredmenyek = new List<Eredmeny>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT sportolo.name, eredmeny.competition, eredmeny.description FROM sportolo INNER JOIN eredmeny ON sportolo.id = eredmeny.sportoloId WHERE sportolo.id = @id";

            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            string sportoloNev = "";

            while (datareader.Read())
            {
                sportoloNev = datareader.GetString("name");

                Eredmeny eredmeny = new Eredmeny()
                {
                    Competition = datareader.GetString("competition"),
                    Description = datareader.GetString("description")
                };

                eredmenyek.Add(eredmeny);
            }

            connection.Close();

            return new
            {
                Name = sportoloNev,
                Eredmenyek = eredmenyek
            };
        }

        [HttpGet("EredmenyekSzama")]

        public object GetEredmenyekSzama()
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT COUNT(*) FROM eredmeny";

            var cmd = new MySqlCommand(sql, connection);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read() == true)
            {
                int darab = datareader.GetInt32(0);

                data = new
                {
                    result = "Sikeres lekérés",
                    EredmenyekSzama = darab
                };
            }

            connection.Close();

            return data;
        }

        [HttpGet("SportoloEredmenyekSzama")]

        public object GetSportoloEredmenyeinekSzama(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT COUNT(*) FROM eredmeny WHERE sportoloId = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read() == true)
            {
                int darab = datareader.GetInt32(0);

                data = new
                {
                    result = "Sikeres lekérés",
                    EredmenyekSzama = darab
                };
            }

            connection.Close();

            return data;
        }

    }
}
