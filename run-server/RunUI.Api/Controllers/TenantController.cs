using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunUI.SysModels;

namespace RunUI.Api.Controllers
{
    public class TenantController : BaseCrudController<TTenant>
    {

        [HttpGet]
        public async Task<object> Get()
        {
            var rows = await Repository.Where(i => i.Id == "123").FirstAsync();

            return rows;
        }
        [HttpGet]
        public async Task<string> Insert()
        {
            var s = RangeHelper.Range(1, 10).Select(i => new TTenant
            {
                Id = OrderIdHelper.ObjecId(),
                Name = $"name_{i}",
            });

            await Repository.InsertAsync(s);
            return "OK";
        }
    }
}
