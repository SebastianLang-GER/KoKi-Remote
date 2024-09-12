using KoKi_Remote;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using System.Reflection;
using System.Text.Json;

namespace KoKi_Remote
{
    internal static class Program
    {
        private static List<MultimediaPlayer> players = GetMultimediaPlayers();
        private static int playerIndex = 0;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool firstAppInstance = true;

            // single instance application
            using (new Mutex(initiallyOwned: true, "KoKi-Remote", out firstAppInstance))
            {
                if (firstAppInstance)
                {
                    StartWebServer();
                    ApplicationConfiguration.Initialize();
                    Application.Run(new TrayIconApp());
                }
            }
        }

        private static void StartWebServer()
        {
            Version apiVersion = new Version(1, 0, 0);

            WebApplication app = WebApplication.Create(Array.Empty<string>());
            app.Urls.Add($"http://0.0.0.0:{KoKi_Remote.Properties.Settings.Default.Port}"); // set port
            app.UseStaticFiles(); // host static files from directory "wwwroot"

            // redirections
            app.MapGet("/", (HttpResponse response) =>
            {
                response.Redirect("/index.html", permanent: true);
            });
            app.MapGet("/favicon.ico", (HttpResponse response) =>
            {
                response.Redirect("/images/logo.svg", permanent: true);
            });

            // API
            app.MapGet("/api", () =>
            {
                List<string> list = new List<string>();
                TypeInfo typeInfo = typeof(WebApplication).GetTypeInfo();
                FieldInfo fieldInfo = typeInfo.DeclaredFields.Where((FieldInfo e) => e.Name == "_dataSources").FirstOrDefault();
                List<EndpointDataSource> list2 = (List<EndpointDataSource>)fieldInfo.GetValue(app);
                foreach (EndpointDataSource current in list2)
                {
                    foreach (Endpoint current2 in current.Endpoints)
                    {
                        // TODO: add HTTP request type at end of route
                        string text = new string(((RoutePattern)current2.GetType().GetProperty("RoutePattern").GetValue(current2, null)).RawText);
                        if (text.StartsWith("/api/"))
                        {
                            list.Add(text);
                        }
                    }
                }
                return list;
            });
            app.MapGet("/api/version", () => new
            {
                version = new
                {
                    api = apiVersion.ToString(3),
                    backend = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3)
                }
            });
            app.MapGet("/api/ping", () => Results.Ok());
            app.MapGet("/api/player", () => new
            {
                Player = playerIndex + 1
            });
            app.MapPatch("/api/player", async (HttpRequest request) =>
            {
                request.EnableBuffering();
                if (!request.HasJsonContentType())
                {
                    return Results.BadRequest("Invalid content.");
                }
                if (!(await JsonDocument.ParseAsync(request.Body)).RootElement.TryGetProperty("player", out var player))
                {
                    return Results.BadRequest("Missing property \"player\".");
                }
                if (!player.TryGetInt32(out var value))
                {
                    return Results.BadRequest("Invalid value.");
                }
                if (value < 1 || value > players.Count)
                {
                    return Results.BadRequest("Player ID out of range.");
                }
                playerIndex = value - 1;
                return Results.Ok();
            });
            app.MapGet("/api/player/command/next", () =>
            {
                if (playerIndex < 0 || playerIndex > players.Count - 1)
                {
                    throw new IndexOutOfRangeException("The selected player could not be found.");
                }
                players[playerIndex].SendCommand(MultimediaPlayer.ControlCommandOptions.Next);
            });
            app.MapGet("/api/player/command/play-pause", () =>
            {
                if (playerIndex < 0 || playerIndex > players.Count - 1)
                {
                    throw new IndexOutOfRangeException("The selected player could not be found.");
                }
                players[playerIndex].SendCommand(MultimediaPlayer.ControlCommandOptions.PlayPause);
            });
            app.MapGet("/api/player/command/previous", () =>
            {
                if (playerIndex < 0 || playerIndex > players.Count - 1)
                {
                    throw new IndexOutOfRangeException("The selected player could not be found.");
                }
                players[playerIndex].SendCommand(MultimediaPlayer.ControlCommandOptions.Previous);
            });
            app.MapGet("/api/player/command/stop", () =>
            {
                if (playerIndex < 0 || playerIndex > players.Count - 1)
                {
                    throw new IndexOutOfRangeException("The selected player could not be found.");
                }
                players[playerIndex].SendCommand(MultimediaPlayer.ControlCommandOptions.Stop);
            });
            app.MapGet("/api/players", () => JsonSerializer.SerializeToDocument(players));
            app.MapGet("/api/players/{id:int}", (int id) =>
            {
                if (id < 1 || id > players.Count)
                {
                    Results.NotFound("Player ID out of range.");
                }
                Results.Ok(JsonSerializer.SerializeToDocument(players[id - 1]));
            });

            app.RunAsync();
            Console.WriteLine($"Web server is running on port {KoKi_Remote.Properties.Settings.Default.Port}");
        }

        private static List<MultimediaPlayer> GetMultimediaPlayers()
        {
            return new List<MultimediaPlayer>
            {
                new MultimediaPlayer
                {
                    Name = "VLC Media Player",
                    Logo = "/images/vlc.svg",
                    ProcessName = "vlc",
                    WindowTitle = "VLC media player",
                    Commands = new MultimediaPlayer.ControlCommands
                    {
                        PlayPause = " ",
                        Stop = "s",
                        Previous = "p",
                        Next = "n"
                    }
                },
                new MultimediaPlayer
                {
                    Name = "easyDCP Player+",
                    Logo = "/images/easydcp.webp",
                    ProcessName = "easyDCP Player+",
                    WindowTitle = "easyDCP Player+",
                    Commands = new MultimediaPlayer.ControlCommands
                    {
                        PlayPause = " ",
                        Stop = "s",
                        Previous = "p",
                        Next = "n"
                    }
                }
            };
        }
    }
}