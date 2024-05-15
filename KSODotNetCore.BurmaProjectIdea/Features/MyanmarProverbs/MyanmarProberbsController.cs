using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace KSODotNetCore.BurmaProjectIdea.Features.MyanmarProverbs;

[Route("api/[controller]")]
[ApiController]
public class MyanmarProberbsController : ControllerBase
{
    private async Task<Tbl_MmProverbs> GetDataAsync()
    {
        string jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
        var model = JsonConvert.DeserializeObject<Tbl_MmProverbs>(jsonStr);
        return model!;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var model = await GetDataAsync();
        return Ok(model.Tbl_MMProverbsTitle);
    }

    [HttpGet("{titleName}")]
    public async Task<IActionResult> Get(string titleName)
    {
        var model = await GetDataAsync();
        var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
        if(item is null)
        {
            return NotFound("no data found");
        }
        var titleId = item.TitleId;
        var result = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId);

        List<Tbl_MmproverbsHead> list = result.Select(x => new Tbl_MmproverbsHead
        {
            ProverbId = x.ProverbId,
            ProverbName = x.ProverbName,
            TitleId = x.TitleId,

        }).ToList();
        return Ok(list);
    }

    [HttpGet("{titleId}/{proverbId}")]
    public async Task<IActionResult> Get(int titleId, int proverbId)
    {
        var model = await GetDataAsync();
        var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);
        return Ok(item);
    }
}


public class Tbl_MmProverbs
{
    public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_MmproverbsDetails[] Tbl_MMProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_MmproverbsDetails
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class Tbl_MmproverbsHead
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
}