var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var dockerComposeYml = Environment.GetEnvironmentVariable("docker-compose-yml");

app.MapGet(
    "/{repository}",
    (string repository) => File.WriteAllText(
        $"/commands/{Guid.NewGuid()}.txt",
        $"docker-compose -f {dockerComposeYml} up -d  --force-recreate {repository}"));

app.Run();