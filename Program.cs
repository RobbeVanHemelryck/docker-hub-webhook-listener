using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var dockerComposeYml = Environment.GetEnvironmentVariable("docker-compose-yml");

app.MapGet("/{repository}", (string repository) =>
{
    var cmd = new Process();
    cmd.StartInfo.FileName = "cmd.exe";
    cmd.StartInfo.RedirectStandardInput = true;
    cmd.StartInfo.RedirectStandardOutput = true;
    cmd.StartInfo.UseShellExecute = false;
    cmd.StartInfo.CreateNoWindow = true;
    cmd.Start();

    cmd.StandardInput.WriteLine($"docker-compose -f {dockerComposeYml} up -d  --force-recreate {repository}");
    cmd.StandardInput.Flush();
    cmd.StandardInput.Close();
    cmd.WaitForExit();
    Console.WriteLine(cmd.StandardOutput.ReadToEnd());
    return repository;
});

//test
app.Run();