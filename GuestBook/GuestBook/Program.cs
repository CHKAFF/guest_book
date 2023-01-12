using GuestBook;

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>()
                .UseUrls("http://localhost:8080/");
        });
}

    
