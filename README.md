# SoLaWille
## A game about starting your own community supported agriculture farm




#### The first version was created by Lukas Rehberg, Emma Seeger and Matthias Richter as a Project for the Introduction to Unity course 2021 at UOS
#### The second version was created by Emma Seeger, Matthias Richter, Marlena Napp and Annika Theisen as a Project for the Advanced Experiment Design in Unity course 2021 at UOS

## Second version updates:


Link to a Let's Play video showing off the added features of the game: https://youtu.be/8cEOzEIHc8s  

Link to builds of the game for windows: https://1drv.ms/u/s!AiYro-6-3hvN-1KGqy8_rHuvA4gE?e=xghhbO

Since this is an update to a project we submitted for the "Introduction to Unity" course we wrote an update report to differentiate and explain all the features we added for the advanced experiment design course. Additionally to those major additions we also fixed bugs in the old version and made some more minor adjustments:

### Technical Update Report:
#### Autonomous NPCs
We created a system to include autonomous NPCs in our game. Our system implements a finite state machine. Each NPC in the game has a NPC script that inherits from the abstract NPC Base Class and controlls the initialization of the npc/states, the animations, and keeps track of the current state of the NPC. There exist NPC scripts for Sheep NPCs and Citizens which implement specific behavious and animations. We decided to implement the possible states as components. There exists an abstract NPCState Baseclass that each specific state inherits from. Central to each state is that it implements a state action that is executed if the NPC is in the state and a state transition that defines the conditions for transitioning from the state into other states. This way all the states an NPC has define a transition graph. We tried to build the states as modular as possible such that the game designer can pick a subset of them and give them to a new npc class without having to adapt each state. The states we implemented are:
- Idle State: All states transition into idle state if they are "done".
  - There is a specific Idle state for the citzen (Idle Citizen) and the sheep (wander state).
- Avoid State: In this state the NPC avoids a certain game object. All states have the option to transition into the avoid state if the NPC has this state component and objects to avoid are assigned.
- Follow State: The NPC follows a target object at some distance.
- Perform Task State: The NPC performs a set of tasks
  - NPC needs a Task Controller that manages the set of tasks
  - Task Objects need the NPCTask script as a component.
- Meet NPC State: The NPC meets another NPC, stops for an interaction, and continues after some time
- Meet Player State: The NPC greets the player and stops for an interaction
For details of the system refer to our scripts in the "Agents" folder.

#### Player & NPC Animations
We added animations of certain actions for both the Player and the NPCs. Added animations are:
 - Weeding
 - Harvesting
 - Watering
 - Digging
 - Planting
 - Pick Up
 - Working (NPC only)
 - Greeting (NPC only)
 - Walking (NPC only)
 
We also added an animated Ocean

Additional Assets used for animation: 
- RPG Character Mecanim Animation Pack FREE
- LowPolyWater_Pack
- Mixamo Magic Pack
- Supercyan Character Pack Free Sample


#### Dialouge & Quest System
We developted a Dialouge System that enables the Player to talk to an NPC (or other Gameobject)
We developed a Quest System. Through a Dialouge the Player can receive, accept and complete a Quest. 

#### Start Screen & Pausing/Quitting the Game
We added a Startscreen and the possibility to pause and quit the game

#### Minimap
We added a Minimap to the UI to give the Player a better overview of their position in the game.

#### World building
Finally we extended our game world to incorporate all of our added features and changes. We added a Village and a City with autonomous NPCs, created a simple collection question, and added some NPCs that talk to the player and give them some information about the game. Everything is build such that it can be scaled up  and new NPCs, Quests and Dialouges can be added easily.

Additional Assets used for World building:
- LowPolyBrickHouses

## First version 
Link to a Let's Play video showing off the features of the game: https://youtu.be/B4_037opUOE  

Link to builds of the game for windows, mac and linux: https://1drv.ms/u/s!AiYro-6-3hvN8E1WXnbM1NNAxvzT?e=mV5Wxb



## Assets used:
 - Low Poly Game Kit: Inspiration for parts of the Game Logic, Player character and animation, terrain and some tree prefabs
 
 - Low Poly Nature - Free Vegetation
 - Classic Acoustic Guitar
 - Low Poly Farm Lite
 - Root Vegetables
