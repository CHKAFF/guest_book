using GuestBook.DB;
using Ydb.Sdk;
using Ydb.Sdk.Table;
using Ydb.Sdk.Yc;

namespace GuestBook.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDB(this IServiceCollection services)
    {
        var saProvider = new ServiceAccountProvider(
            saFilePath: "creds.json", // Path to file with service account JSON info
            loggerFactory: new LoggerFactory());

        saProvider.Initialize().GetAwaiter().GetResult();
        var config = new DriverConfig(
            endpoint: "grpcs://ydb.serverless.yandexcloud.net:2135",
            database: "/ru-central1/b1gav6n61gmq06vv61u5/etno0pds55s3ttbulnk5",
            credentials: saProvider
        );

        var driver = new Driver(
            config: config,
            loggerFactory: new LoggerFactory()
        );

        driver.Initialize().GetAwaiter().GetResult(); // Make sure to await driver initialization
        return services
            .AddSingleton(new TableClient(driver, new TableClientConfig()))
            .AddSingleton<GuestBookDB>();
    }
}