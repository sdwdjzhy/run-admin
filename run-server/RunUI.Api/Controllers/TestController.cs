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
            var st = await Orm.Select<TSysTest>().Where(i => i.Money >= 11).FirstAsync();
            return st.Name;
        }

        //[HttpGet]
        //public async Task<string> Insert()
        //{
        //    var s = RangeHelper.Range(1, 200).Select(i => new TSysTest
        //    {
        //        Id = OrderIdHelper.ObjecId(),
        //        Name = $"name_{i}",
        //        TenantId = 1,
        //        Sex = (AppEnum.Sex)(i % 3),
        //        IsUsed = i % 3 == 0,
        //        HtmlContent = "<br>",
        //        Lng = 116.431002m,
        //        Lat = 39.909401m,
        //        Money = 10,
        //        Price = 20,
        //    });

        //    await Orm.Insert(s).ExecuteAffrowsAsync();
        //    return "OK";
        //}
    }
}
