using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunUI.SysModels;

namespace RunUI.Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpPost]
        public async Task<object> List()
        {

            var qg = new QueryGenertator<TSysTest>(Request.Body);
            var where = await qg.GetExpression();
            var (page, size) = await qg.GetPageInfo();

            var rows = await Orm.Select<TSysTest>()
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
            var st = await Orm.Select<TSysTest>().Where(i => i.Sex == AppEnum.Sex.女).FirstAsync();
            return st.Name;
        }

        [HttpGet]
        public async Task<string> Insert()
        {
            var s = RangeHelper.Range(1, 200).Select(i => new TSysTest
            {
                Id = OrderIdHelper.ObjecId(),
                Name = $"name_{i}",
                TenantId = 1,
                Sex = (AppEnum.Sex)(i % 3),
                IsUsed = i % 3 == 0,
                HtmlContent = "<br>",
                Lng = RandomHelper.NextDecimal(0,180),
                Lat = RandomHelper.NextDecimal(0, 90),
                Money = RandomHelper.Next(0,200),
                Price = RandomHelper.Next(0, 100),
            });
            await Orm.Delete<TSysTest>().Where(i=>true).ExecuteAffrowsAsync();
            await Orm.Insert(s).ExecuteAffrowsAsync();
            return "OK";
        }
    }
}
