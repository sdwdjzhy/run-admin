using FreeSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RunUI.SysModels;

namespace RunUI.Api.Controllers
{
    public class TestController : BaseCrudTenantController<TSysTest>
    {

        [HttpPost]
        public async Task<object> List()
        {

            var qg = new QueryHelper<TSysTest>(Request.Body);
            var where = await qg.GetExpression();
            var (page, size) = await qg.GetPageInfo();

            var rows = await Repository
                .Where(where)
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
        public async Task<string> Get()
        {
            Console.WriteLine(Server.MapPath("~/upload/"));
            var st = await Repository.Where(i => i.Sex == AppEnum.Sex.女).FirstAsync();
            return st.Name;
        }

        [HttpGet]
        public async Task<string> Insert()
        {
            var s = RangeHelper.Range(1, 200).Select(i => new TSysTest
            {
                Id = OrderIdHelper.ObjecId(),
                Name = $"name_{i}",
                TenantId = RandomHelper.Next(1, 10).ToString(),
                Sex = (AppEnum.Sex)RandomHelper.Next(0, 3),
                HtmlContent = "<br>",
                Lng = RandomHelper.NextDecimal(0, 180),
                Lat = RandomHelper.NextDecimal(0, 90),
                Money = RandomHelper.Next(0, 200),
                Price = RandomHelper.Next(0, 100),
            });
            await Orm.Delete<TSysTest>().Where(i => true).ExecuteAffrowsAsync();
            await Orm.Insert(s).ExecuteAffrowsAsync();
            return "OK";
        }
    }
}
