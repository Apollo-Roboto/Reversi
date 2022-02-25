# Reversi Bot Notes



```txt
0   1   2   3   4   5   6   7

8

16

24

32

40

48

56
```



```txt

  (1,0)        Right: j % 8 != 0
 (-1,0)         Left: (j+1) % 8 != 0
  (0,1)          Top: j > 0
 (0,-1)         Down: j < 64
  (1,1)     TopRight: j > 0 && j % 8 != 0
 (-1,1)      TopLeft: j > 0 && (j+1) % 8 != 0
(-1,-1)     DownLeft: j < 64 && (j+1) % 8 != 0
 (1,-1)    DownRight: j < 64 && j % 8 != 0


incrementation = x + -(y*8)
init = i + incrementation


out = False

if(x == 1)
	out = (j % 8 != 0)
else if (x == -1)
	out = ((j+1) % 8 != 0)

if(y == 1)
	out = (j > 0)
else if(y == -1)
	out = (j < 64)

```









































# Where I left off
Was doing the 8 direction validation, only looking for bound right now
There's this PositionScore model that needs to be returned from the `GetPossibleMoves`


# Some Links
http://samsoft.org.uk/reversi/





























create a clone() for the board









## Strategies

Corner are important and the bot should aim for

Try to limit the opponent's possible moves

Start by not taking too much cells for a good start

MultiThreading