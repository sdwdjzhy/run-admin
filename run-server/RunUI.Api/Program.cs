
using Microsoft.Extensions.WebEncoders;
using NLog.Web;
using RunUI;
using RunUI.SysModels;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var config = NLogAspNetCoreExtensions.GetDefaultNLogAspNetCoreSetting();
var logger = NLogBuilder.ConfigureNLog(config).GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    AppConfigHelper.SetConfigurationRoot(builder.Configuration);
    builder.Logging.AddNLogWeb(config);
    builder.Host.UseNLog();

    builder.AddRunUI();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ITenantProvider, DefaultTenantProvider>();

    var connectionString = builder.Configuration.GetConnectionString("BaseConnectionString");
    IFreeSql fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.PostgreSQL, connectionString)
#if DEBUG
    .UseNoneCommandParameter(true) // ���ò�ʹ�� ������ ִ�� SQL ������㿪�����ԡ�
    .UseAutoSyncStructure(true) //�Զ�ͬ��ʵ��ṹ�����ݿ⣬FreeSql����ɨ����򼯣�ֻ��CRUDʱ�Ż����ɱ�
#endif
    .Build();
    builder.Services.AddSingleton<IFreeSql>(fsql);
    fsql.AddIndexes();

#if DEBUG
    fsql.Aop.CurdBefore += (s, e) =>
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("".PadRight(20, '*'));
        Console.WriteLine($"ִ�У���{e.Sql}��");
        Console.WriteLine("".PadRight(20, '*'));
        Console.ResetColor();
    };
#endif
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    //����Http Logging
    app.UseHttpLogging();

    app.UseHttpsRedirection();

    app.UseRunUI();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    //NLog: catch setup errors
    logger.Error(exception, "��Ϊ������ֹͣ����");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}