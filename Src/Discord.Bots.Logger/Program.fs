open System

type UnixTime = private UnixTime of int64
type User = private User of string
type Channel = private Channel of string
type ChatEventMessage = private ChatEventMessage of string

type ChatEvent =
| UserConnectedToVoiceChannel of User * Channel * UnixTime
| UserDisconnectedFromVoiceChannel of User * Channel * UnixTime

type CreateMessage = ChatEvent -> ChatEventMessage

let createMessage: CreateMessage = fun chatEvent ->
    Unchecked.defaultof<ChatEventMessage>

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = from "F#" // Call the function
    printfn "Hello world %s" message
    0 // return an integer exit code