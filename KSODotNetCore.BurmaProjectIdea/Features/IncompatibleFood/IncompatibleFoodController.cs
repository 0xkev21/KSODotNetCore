using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSODotNetCore.RestApiWithNLayer.Features.IncompatibleFood
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncompatibleFoodController : ControllerBase
    {
        private async Task<InCompatibleFoodModel> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("incompatible-food.json");
            var model = JsonConvert.DeserializeObject<InCompatibleFoodModel>(jsonStr);
            return model;
        }

        [HttpGet("incompatiblefoods")]
        public async Task<IActionResult> GetAllFoods()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_IncompatibleFood);
        }

        [HttpGet("filter/{des}")]
        public async Task<IActionResult> GetFoodsByDes(string des)
        {
            var model = await GetDataAsync();
            var foodsByDes = model.Tbl_IncompatibleFood.Where(x => x.Description.Contains(des)).ToList();
            if (foodsByDes.Count == 0)
            {
                return NotFound("no data found");
            }
            return Ok(foodsByDes);
        }

        [HttpGet("incompatiblefoods/{id}")]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var model = await GetDataAsync();
            var food = model.Tbl_IncompatibleFood.FirstOrDefault(x => x.Id == id);
            if (food is null) return NotFound("no data found");
            return Ok(food);
        }
    }
}

public class InCompatibleFoodModel
{
    public Tbl_Incompatiblefood[] Tbl_IncompatibleFood { get; set; }
}

public class Tbl_Incompatiblefood
{
    public int Id { get; set; }
    public string FoodA { get; set; }
    public string FoodB { get; set; }
    public string Description { get; set; }
}