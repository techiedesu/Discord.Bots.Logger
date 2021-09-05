module Discord.Bots.Logger.WebhookActor

open System.Collections.Concurrent
open Discord.Bots.Logger.DiscordTypes

type AddWebhook = Webhook -> Unit
type RetryWebhook = Webhook -> Unit

type ProcessingError =
| Other

type ActorState =
| Active
| Stopped

let mutable state = ActorState.Stopped

let mailbox = ConcurrentQueue<Webhook>()
let webhookProcessor : (Webhook -> Result<Unit, ProcessingError>) = fun webhook ->
    match webhook.``type`` with
    | WebhookType.Incoming ->
        Ok ()
    | WebhookType.ChannelFollower ->
        Ok ()
    | WebhookType.Application ->
        Ok ()
    | _ ->
        Error ProcessingError.Other

let rec private retry webhook count =
    if count > 0 then
        try
            let result = webhookProcessor webhook
            match result with
            | Ok _ ->
                ()
            | Error _ ->
                retry webhook (count - 1)
        with
        | _ ->
            retry webhook (count - 1)

let start() =
    match mailbox.TryDequeue() with
    | true, webhook ->
        retry webhook 10
    | _ ->
        ()

let addMessage: AddWebhook = fun webhook ->
    match state with
    | ActorState.Stopped ->
        mailbox.Enqueue webhook
        start()
    | ActorState.Active ->
        mailbox.Enqueue webhook