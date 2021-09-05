module Discord.Bots.Logger.Storage.HttpApi

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open FSharp.Control.Tasks
open Discord.Bots.Logger.Storage.EntityFrameworkUtils

let personHandler (guildId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) -> task {
        let dbContext = ctx.GetService<AppDbContext>()
        let logChannels = dbContext.LogChannels.AsQueryable()

        match! logChannels.TrySingleAsync ^ fun channel -> channel.guildId = guildId with
        | Some guild ->
            return! json guild next ctx
        | None ->
            return! (RequestErrors.notFound (json {| message = "Guild not found" |})) next ctx
    }

let webApp =
    choose
        [ route "/ping" >=> text ""
          routef  "/log_channel/%s" personHandler ]

type Startup() =
    member this.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member this.Configure (app : IApplicationBuilder) (_env : IHostEnvironment) (_loggerFactory : ILoggerFactory) =
        app.UseGiraffe webApp