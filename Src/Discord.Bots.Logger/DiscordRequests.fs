module Discord.Bots.Logger.DiscordRequests

open System.Net.Http
open System.Net.Http.Json

let [<Literal>] DiscordApiHost = ""

type WebhookName      = private WebhookName      of string
type WebhookAvatar    = private WebhookAvatar    of string
type WebhookChannelId = private WebhookChannelId of string

type CreateWebhookError =
| Permissions // WIP
| LostConnection
| Other

type CreateWebhook = WebhookChannelId -> WebhookName -> WebhookAvatar option -> HttpClient -> Result<Unit, CreateWebhookError> Async

let createWebhook: CreateWebhook = fun channelId name avatar httpClient -> async {
    use content = JsonContent.Create {| name = name; avatar = avatar |}
    let! response = httpClient.PostAsync($"{DiscordApiHost}/channels/{channelId}/webhooks", content) |> Async.AwaitTask

    match response with
    | x when x.IsSuccessStatusCode ->
        return Ok ()
    | _ ->
        return Error CreateWebhookError.Other
}

/// https://discord.com/developers/docs/resources/channel#create-message
let createMessage() = ()