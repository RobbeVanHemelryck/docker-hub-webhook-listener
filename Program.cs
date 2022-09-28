var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var dockerComposeYml = Environment.GetEnvironmentVariable("docker-compose-yml");

app.MapPost("/{repository}", (string repository) =>
{
    File.WriteAllText(
    $"/commands/{repository + " " + DateTime.Now.ToString("YYYY-MM-dd hh'u'mm")}.txt",
    $"docker pull taltiko/{repository} && docker-compose -f {dockerComposeYml} up -d  --force-recreate {repository}");
});

app.Run();