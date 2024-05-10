using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSODotNetCore.RestApiWithNLayer.Features.Snakes
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakesController : ControllerBase
    {
        private async Task<SnakesModel>GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("Snakes.json");
            var model = JsonConvert.DeserializeObject<SnakesModel>(jsonStr);
            return model;
        }

        [HttpGet("snakes")]
        public async Task<IActionResult> Snakes()
        {
            var model = await GetDataAsync();
            return Ok(model.snakes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Snake(int id)
        {
            var model = await GetDataAsync();
            var snake = model.snakes.FirstOrDefault(x => x.Id == id);
            if (snake is null) return NotFound("No data found");
            return Ok(snake);
        }
    }
}

public class SnakesModel
{
    public Snake[] snakes { get; set; }
}

public class Snake
{
    public int Id { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}