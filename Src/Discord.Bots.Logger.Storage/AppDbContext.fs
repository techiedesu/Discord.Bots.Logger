namespace Discord.Bots.Logger.Storage

open System.ComponentModel.DataAnnotations
open Microsoft.EntityFrameworkCore

[<CLIMutable>]
type LogChannel =
    { [<Required>] guildId: string
      [<Required>] channelId: string }

type AppDbContext(options : DbContextOptions<AppDbContext>) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable logChannels : DbSet<LogChannel>

    member this.LogChannels with get() = this.logChannels and set value = this.logChannels <- value