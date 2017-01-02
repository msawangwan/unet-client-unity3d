# unity unet client

* * *

## about 

- a unity3d http client
- for code see ../Assets/Script/

* * *

## game

* * *

- a multiplayer game for 2-players (maybe more players at a later date)
- the game is turn based
  - based on chess and
  - advanced war

* * *

## gameplay summary

* * *

- a player starts from the main menu
  - creates a name
  - chooses to either:
    - create a new session or
    - join an existing session
- once a session has two players:
  - player 1 (the host, for simplicity) chooses an HQ
  - player 2 then chooses an HQ
- for each turn a player has a specified amount of action points (AP)
  - any action the player takes, has a cost in AP
    - can move units from one node to another
    - can train units on nodes
    - can mount an attack on an enemy node
- a player is victorious once one victory condition is met:
  - by attrition (related to 'faith/willingness/someword' for a stat that ticks up or down)
  - by destroying the enemy hq

* * *

## implementation details

* * *

_todo: create seperate docs for each component of the system_

