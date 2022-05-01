# ReversiBot

Play reversi against a bot, or with two players, or watch the bot fight!
It chooses it's next move by looking recusivly through the board to find the best move.

## Usage

### Normal game (default to one player and one bot)
```powershell
dotnet run --project .\ReversiBot\ReversiBot.csproj
```

### Two player game
```powershell
dotnet run --project .\ReversiBot\ReversiBot.csproj --bot 0
```

### Watch the bot fight itself
```powershell
dotnet run --project .\ReversiBot\ReversiBot.csproj --bot 2
```

## Preview
```txt
Possible moves: A1 D1 C2 D2 F2 G3 B6 C6 D7

       B 12 • 9  W
   ╭─────────────────╮
1  │ • • • • • • • • │
2  │ • B • • B • • • │
3  │ • • B • B B • • │
4  │ • W W W B B W W │
5  │ • • B B B B • • │
6  │ • • • W B W • • │
7  │ • • • • • W • • │
8  │ • • • • • W • • │
   ╰─────────────────╯
     A B C D E F G H

WHITE Move:
```
