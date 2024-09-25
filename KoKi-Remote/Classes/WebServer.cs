using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text.Json;

namespace KoKi_Remote
{
    internal static partial class Program
    {
        private static void StartWebServer()
        {
            Version apiVersion = new(1, 0, 0);

            WebApplication app = WebApplication.Create(Array.Empty<string>());
            app.Urls.Add($"http://0.0.0.0:{Properties.Settings.Default.Port}"); // set port
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "PWA")) // host static files from directory "PWA"
            });

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
                List<string> routes = new List<string>();
                TypeInfo typeInfo = typeof(WebApplication).GetTypeInfo();
                FieldInfo fieldInfo = typeInfo.DeclaredFields.Where((FieldInfo e) => e.Name == "_dataSources").FirstOrDefault();
                List<EndpointDataSource> endpointDataSources = (List<EndpointDataSource>)fieldInfo.GetValue(app);
                foreach (EndpointDataSource endpointDataSource in endpointDataSources)
                {
                    foreach (Endpoint endpoint in endpointDataSource.Endpoints)
                    {
                        // TODO: add HTTP request type at end of route
                        string text = new string(((RoutePattern)endpoint.GetType().GetProperty("RoutePattern").GetValue(endpoint, null)).RawText);
                        if (text.StartsWith("/api/"))
                        {
                            routes.Add(text);
                        }
                    }
                }
                return routes;
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
            app.MapGet("/api/windows", () =>
            {
                var windowList = new List<object>();
                foreach (Process process in Process.GetProcesses())
                {
                    if (String.IsNullOrEmpty(process.MainWindowTitle)) continue;
                    windowList.Add(new
                    {
                        ProcessId = process.Id,
                        ProcessName = process.ProcessName,
                        WindowTitle = process.MainWindowTitle
                    });
                }
                return windowList;
            });
            app.MapGet("/api/windows/{id:int}/activate", (int id) =>
            {
                try
                {
                    Process process = Process.GetProcessById(id);
                    if (process.MainWindowHandle == IntPtr.Zero) return;
                    SwitchToThisWindow(process.MainWindowHandle, true);
                }
                catch (ArgumentException) { }
            });
            app.MapGet("/api/windows/{id:int}/icon", (int id) =>
            {
                try
                {
                    Process process = Process.GetProcessById(id);
                    if (process == null) return Results.NotFound("Process ID not found.");
                    if (process.MainModule == null) return Results.NotFound("Process main module not found.");
                    Icon? icon = Icon.ExtractAssociatedIcon(process.MainModule.FileName);
                    if (icon == null) return Results.NotFound("Application icon not found.");
                    using (var memoryStream = new MemoryStream())
                    {
                        icon.ToBitmap().Save(memoryStream, ImageFormat.Png);
                        return Results.File(memoryStream.ToArray(), contentType: "image/png");
                    }
                }
                catch (ArgumentException) { }
                return Results.NotFound("Image not found.");
            });

            app.RunAsync();
#if DEBUG
            Console.WriteLine($"Web server is running on port {Properties.Settings.Default.Port}");
#endif
        }
    }
}