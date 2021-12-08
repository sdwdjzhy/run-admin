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
            return st?.Name;

        }

        [HttpGet]
        public string Get2()
        {
            var obj = new TreeNode { Id = "xxx", Extra = new() { { "No", 11 }, { "title", "title_" } } };

            return obj.JsonSerialize();
        }
        [HttpGet]
        public List<TreeNode> Get1()
        {
            var list = new List<TreeNodeItem>();

            list.Add(new TreeNodeItem
            {
                Id = "1",
                Name = "name_1",
                ParentId = null,
                Weight = 1,
                Extra = new() { { "No", "no_1" } }
            });
            list.Add(new TreeNodeItem
            {
                Id = "2",
                Name = "name_2",
                ParentId = "",
                Weight = 2,
                Extra = new() { { "No", "no_2" } }
            });
            list.Add(new TreeNodeItem
            {
                Id = "3",
                Name = "name_1-3",
                ParentId = "1",
                Weight = 1,
                Extra = new() { { "No", "no_3" } }
            });

            var js = list.JsonSerialize();
            Console.WriteLine(js);
            return TreeNodeHelper.Make(list, null);
        }
        [HttpGet]
        public List<TreeNode<int>> Get3()
        {
            var list = new List<TreeNodeItem<int>>();

            list.Add(new TreeNodeItem<int>
            {
                Id = 1,
                Name = "name_1",
                ParentId = null,
                Weight = 1,
                Extra = new() { { "No", "no_1" } }
            });
            list.Add(new TreeNodeItem<int>
            {
                Id = 2,
                Name = "name_2",
                ParentId = null,
                Weight = 2,
                Extra = new() { { "No", "no_2" } }
            });
            list.Add(new TreeNodeItem<int>
            {
                Id = 3,
                Name = "name_1-3",
                ParentId = 1,
                Weight = 1,
                Extra = new() { { "No", "no_3" } }
            });
            var js = list.JsonSerialize();
            Console.WriteLine(js);
            return TreeNodeHelper.Make(list, null);
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
