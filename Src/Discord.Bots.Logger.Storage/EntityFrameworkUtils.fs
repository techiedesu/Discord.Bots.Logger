module Discord.Bots.Logger.Storage.EntityFrameworkUtils

open System.Linq
open Microsoft.EntityFrameworkCore
open FSharp.Control.Tasks

module Option =
    let inline ofBool value = if value then Some () else None

let (^) f x = f(x)

let inline (|Null|_|) (value: 'a) = obj.ReferenceEquals(value, null) |> Option.ofBool

type IQueryable<'T> with
    member this.TrySingleAsync (predicate: 'T -> bool) = task {
        match! this.SingleOrDefaultAsync predicate with
        | Null ->
            return None
        | value ->
            return Some value
    }