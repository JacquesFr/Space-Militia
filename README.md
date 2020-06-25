# Senior_Design_Project_CS179

# Space Militia Game #

## Overview:

    Data Box
  
      Game:             Space Militia 
   
      Engineers:        Ashley McDaniel, Deven Fafard, Jacques Fracchia, Jerry Kuei, Michael Rojas
  
      Platform:        PC
  
      Lines of Code:  3,518         (written by us)
                    
                        +
  
                     139,287     (plugins + assets)
                    
                    = 142,805 lines

## Game Description 

**Levels**

  The game Space Militia is composed of two levels. 

  The First Level takes place in a rural setting, where the player must defend their village, find and board their ship. 
  To defend their village, the player must save their farm animals from aliens by putting the chickens in their pen. 
  
  The player will venture out of their village and fight off alien enemies while making their way to a couple buildings.
  Once reaching the buildings, the player must figure out a way to power and turn on the ship. 
  This can be completed by finding a power source to power the terminals and ping the ship. After the player completes ship launch tasks they will board the ship and fly into space. 

![Power the Space Ship Puzzle](https://i.imgur.com/L6MAzqV.png)

  The Second Level takes place aboard a space station. The player first finds themselves just outside of the loading dock where their ship landed. 
  The entire station has been invaded by aliens, the player will need to be shooting them throughout the entire level. Once they land they will follow down a corridor until they reach a terminal. 

  As a pre-puzzle the player will need to press [e] to activate the terminal opening the door into the first main puzzle. 
  The first main puzzle has three main rooms: an empty room with a locked door, an engine room with huge generators and capacitors, and a power room with two platforms similar to the ones in level 1. 
  The player will find a chicken they can use for one of the platforms, and in the engine room there is a battery that the player must place on the second platform. Once this is completed they will be presented with a message telling them that the power has been restored and to continue through the recently unlocked door.

![Alien](https://i.imgur.com/w6kP8X0.png)

  In the second puzzle, the player finds themselves stuck in a trash compactor. Audio is arranged for high reverb and ominous background ambience, alongside the sound of running engines and the distant echoes of trash being processed, giving the puzzle section a highly atmospheric presence. 
  There is trash placed around everywhere and a few enemies waiting for the player. There also is an exit door perched on an elevated platform the player cannot normally reach. Once the enemies have been cleared, the player may observe a button on the wall. 
  Pressing it will drop a gate that blocks the exit, and start to fill the room with water. Players may take note that a small heap of trash will block the gate from closing entirely, but not enough so the player can get underneath. This is hinting to the player to find a way to prop the gate open enough to pass under, and swim to the other side. 
  The player can then press the switch again to raise the gate and lower the water level. The player can then scour the nearby trash piles for a cube that can be picked up. 
  After the cube is in place  beneath the gate, the player will then press the button on the side of the wall, causing the trash compactor room to fill with water and dropping the gate, where the cube will block the gate from shutting entirely. 
  The player can then swim underneath the gate and swim upward pressing [space] to get onto the otherwise unreachable level platform into safety.
  
  The final portion of the spaceship does not have any puzzles, but instead has another three rooms the player must clear out. Following down the corridor the player  is first presented to living quarters filled with aliens. After completing this room the player then enters a console room filled with computers and a star chart animation. 
  
  Killing these enemies in the console room gets the player to low health, if the player has not taken time to rest and heal they should before continuing the level. The final room is a large two storied flight deck for the station. The player can either come into the room either from the second story balcony or the main level. 
  There are almost a dozen aliens the player must clear out. Once cleared the player is prompted with a dialogue stating that they have cleared out the station and a message stating â€˜youâ€™ve saved humanity, thank you hero!â€™. The player then has thirty seconds to explore the last room before the game ends.

**Gameplay**

  The game is a traditional first person shooter with puzzle solving elements. The player has access to a small arsenal of weapons in order to shoot (or melee strike) enemies. The player can pick up specific objects and press 'E' to solve puzzles. 
  The player is sectioned off in each level and must complete the puzzles in order to linearly progress through each section of the level.


  The controller scheme is as follows:
  
    Shoot Weapon:                left click
  
    Drop Item:                   right click
  
    Look:                        move mouse


    Move:                        [W], [A], [S] [D]
  
    Sprint:                      [Shift] 
  
    Swim Down:                   [Ctrl]
  
    Swim Up:                     [space]
  
    Pick Up Object / Interact:   [E]
  
    Crouch:                      [C]
  
    Select Weapon:               [1], [2], [3], [4], [5], [6]


## Implementation

**Game Engine**

  We opted to use Unity (version 2019.1.14f) as our game engine. We chose it because of its robust documentation and community support. It is also free to use for students and professionals who profit less than $100,000 annually, which made it a great choice.

**Scripting**

  Overall, we strived to follow best practices for game development - mainly event driven programming and separation of concerns. This means that the core game logic was facilitated by a centralized event manager (in our case GameManager). 
  Relevant objects such as the enemies, checkpoint objectives, and the player notify GameManager of changes to their states in an effort to keep core functionality out of Update(); polling for changes on every frame has the potential to be very expensive. 
  GameManager would in turn pass these notifications to relevant subsystem controllers. This was accomplished with a simple observer pattern implementation. Relevant events were declared in the NotificationType enum so multiple controllers could respond to the same event. 
  Scripts were separated between the directories: player scripts, enemy scripts, and world scripts. All scripts corresponding to the player and enemy went into their respective folder. The world directory was separated by levels, then separated by puzzle number. 
  That way we knew exactly where scripts were located for specific assets and checkpoints of the game.

![class diagram](https://i.imgur.com/IGefNF3.png)



**Tools**

  In addition to Unity, we used the Game development with Unity workload in order to use Visual Studio as our IDE. To track effort in a more modular fashion, we used Trello in addition to the spreadsheet that was provided. 

What planned tasks were not done?

* Unincorporated Planned Tasks

    * Saves after each checkpoint
  
    * Visible Enemy health bar
  
    * Visible Hit effect/particles from projectiles landing
  
    * Improve Environmental cues for objective
  
    * In-game objects that provide snippets of a story, when the Player interacts with the object
  
    * Bug: Gun can fire bullets before gun animation finishes if the Player clicks before animation finishes 
  
    * Bug: Arrows and Spears that get thrown or shot fall through the world when they should instead collide with the world. Their damage is very minimal.

  All other unincorporated tasks we did not have for the demonstration have been completed and included in the video gameplay below. We re-implemented the cutscene at the end of level 1 and displayed a UI that the level has been completed. 
  The transitions between the three puzzles have been smoothed out. This means that the rooms are now physically connected and the player just walks from one segment to the next. UI display has been added into the second level giving direction on what to do at each puzzle. 
  The final room now has a thirty second timer when the player wins the game they are prompted with a â€˜Youâ€™ve saved humanityâ€™ display and they can now wonder about the final room. Finally, a game title has been added: Space Militia!

**How did scrum work for us** **?**

  Scrum worked well for us because we were able to keep track of which tasks have been completed and which tasks have not been completed. It also allowed us to distribute the team work evenly, or as best as we could given everyone's different schedules.
  We would come up with all of the tasks and features we wanted to have after each sprint. The team would then pick which tasks they felt most comfortable completing. If a task was not completed by the due date, the team was very understanding and would allow the task to be completed next sprint. 
  If a team member needed help with any task then everyone would respond accordingly in the team discord. Towards the end the team was very excited about the game. 
  We ended up being able to have everything the TA was asking for, along with some extra features.  Scrum allowed us to stay on track to complete what we wanted in the game by the end of the quarter. 

**What would we do differently** **?**

  Because unlinked and lost assets during merging was a real issue during this project, one of our first priorities in the future would be ensuring a reliable and solid protocol for file sharing. This might be a carefully detailed process as to how we structure feature branches in Github, how we merge our branches, and how we make sure that upon merging that assets links are not lost. 
  For example, we had configured our Github repo to ignore .meta files on merging. This caused several issues with file linking and lost assets later on, and thus would be something to avoid in future practice. 
  We would also pay more attention to how we set up the repository before any work began so as not to delete previously completed work or lose object references.

**GamePlay Links** **:**

  https://www.youtube.com/watch?v=pZxdeIQI5Gk&feature=youtu.be

