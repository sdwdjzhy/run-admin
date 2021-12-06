using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunUI.SysModels;

namespace RunUI.Api.Controllers
{
    public class UserController : BaseCrudTenantController<TSysUser>
    {
        [HttpGet]
        public async Task<object> List(int page = 1, int size = 20)
        {

            var rows = await Orm.Select<TSysUser>()
                .Page(page, size)
                .Count(out var total)
                .ToListAsync();

            return new
            {
                rows,
                total
            };
        }

        [HttpGet]
        public async Task<object> Get()
        {

            var rows = await Orm.Select<TSysUser>().FirstAsync();


            return rows;
        }
        [HttpGet]
        public async Task<string> Insert()
        {
            var s = RangeHelper.Range(1, 200).Select(i => new TSysUser
            {
                Id = OrderIdHelper.ObjecId(),
                Name = $"name_{i}",
                Password = "123123",
                TenantId = "1",
            });

            await Orm.Insert(s).ExecuteAffrowsAsync();
            return "OK";
        }
    }
}
