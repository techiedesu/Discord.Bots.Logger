module Discord.Bots.Logger.DiscordTypes

/// I don't give a fuck what does this name mean
type Snowflake = private Snowflake of string

type ChannelType =
| GuildText = 0
| DM = 1
| GuildVoice = 2
| GROUP_DM = 3
| GUILD_CATEGORY = 4
| GUILD_NEWS = 5
| GUILD_STORE = 6
| GUILD_NEWS_THREAD = 10
| GUILD_PUBLIC_THREAD = 11
| GUILD_PRIVATE_THREAD = 12
| GUILD_STAGE_VOICE = 13

/// TODO: Add https://discord.com/developers/docs/resources/channel#channel-object
type Channel =
    { id: Snowflake
      ``type``: ChannelType
      guildId: int option
      position: int option }

type WebhookType =
/// Incoming Webhooks can post messages to channels with a generated token
| Incoming = 1
/// Channel Follower Webhooks are internal webhooks used with Channel Following to post new messages into channels
| ChannelFollower = 2
/// Application webhooks are webhooks used with Interactions
| Application = 3

/// TODO: Add https://discord.com/developers/docs/resources/webhook#webhook-object
type Webhook =
    { id: Snowflake
      ``type``: WebhookType }