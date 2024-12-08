## Devlog Entry 1 ( Team Formation )

### Introducing the team

- Tools Lead: Ezra Frary
- Engine Lead: Damon Phan
- Design Co-Leads: Anais Montes & Shazer Rizzo Varela

\* each role is expected to act as described on the Team Formation Document!

### Tools and materials

\- Primary engine : Our team will be completing this project in the latest version of **Unity** (2022.3.52f1). Every member in our team has experience working with Unity and prefers it greatly to frameworks like Phaser, so we felt it was the best choice for our group to be able to get things done quickly and collectively.

\- Languages : Our project will primarily use **C#** for scripting in Unity, as it is the standard language supported by the engine, and likewise for Godot, we will use **GDScript**.

\- Tools : We will be using **Visual Studio** or **VS Code** alongside the Unity Editor as our primary development tools. Since we are planning to have our game be 2D, we will be relying on tools such as **Photoshop**, **Procreate**, and **Aesprite**, to create assets for our game. Our team members are already familiar with using these programs, so they seem the obvious choice for us.

\- Alternate Platform : We chose **Godot** as our alternate engine, primarily because it seems that a lot of Unity knowledge and design practices are transferable to working with Godot. Our Engine Lead also has substantial experience with this engine and should be able to help the rest of us orient ourselves in the alternate engine should that be necessary.

### Outlook

Our team is hoping to find ways to approach the assignment requirements creatively and in a way that speaks to what we personally enjoy in games. While we aim to meet the technical and mechanical goals of this assignment, we’d also like to work on something that expands them into a fulfilling experience and possesses some kind of narrative or theme. This additional element of storytelling is important to us, as it provides an opportunity to create something that feels distinctly ours among the work of our peers! Of course, the most difficult part about this is creating a balanced and satisfying player experience in the limited timeframe that we have to complete this game. Meaningfully incorporating some sort of narrative adds a layer of complexity to this project, and we already know that new requirements will be introduced during development, potentially forcing us to adapt and implement features in Unity or Godot. We also face the challenge of juggling this project alongside other game group projects from different classes, which limits how much time we can dedicate to iterating and polishing our work. Some aspects of development are likely to be tricky, but we will be learning new skills from each other and growing as developers along the way!

## Devlog Entry 2 ( F0 )

### How We Satisfied the Software requirements

\- F0.a: Player movement is handled in the PlayerMovement.cs script. The player moves across a 2D grid defined in the Map.cs script based on the scene environment. Using the type property, movement can be toggled between grid-based or free-form styles. In grid-based movement, the player's position is aligned to specific cells via MoveTarget, which updates the \_targetCell and \_targetPos properties based on input. The HandleGridInputs method processes directional input to determine the target cell for movement.

\- F0.b: Turn-based simulation is managed by incrementing a moveCount variable in PlayerMovement.cs every time the player moves across a space on the grid. Once the player has made a predefined number of moves (MOVES_PER_TURN), the script calls TurnManager.NextTurn(), advancing the game to the next turn. This ensures time only progresses after specific player action is taken.

\- F0.c: The player can sow plants by pressing the 'P' key. The player can only sow the tile that they are currently standing on, given that the cell is unoccuppied by another plant and the cell exists on a tilled tile. The player's current cell (\_targetCell) is calculated relative to their position on the grid, enforcing the requirement of proximity. The player can reap plants on the cell they are standing on by pressing the 'O' key. The proximity element is handled the same way as it is with the sowing mechanic. Plants can only be harvested when they have progressed through all growth stages and are considered fully grown. Within the game and its code, we've been referring to "reaping" and "sowing" as "harvesting" and "planting" respectively.

\- F0.d: Cell resources are primarily handled by the Map.cs script. Each cell posesses a waterLevel and sunLevel that is updated at the start of each turn. There exists a vector parameter indicating the potential amounts of water that can be accumulated per turn (currently a random float between 0 and 1) as well the potential sun levels that can be applied to a cell each turn. As is stated in the assignment guidelines, waterLevel can be accumulated but sunLevel cannot. The water and sun level of the cell that the player is standing on as well as the plant that occupies the cell is displayed through a panel at the bottom right corner of the screen.

\- F0.e: There are currently four different types of crop (Cucumber, Parsnip, Lettuce, Wheat) that are functional in the game. The types are defined by distinct scriptable objects that can be applied to any created plant object. The types have varying conditions for growth and can be identified visually by their differing sprites. Currently, a plant of a random type is planted whenever the player sows a new plant. When a plants growth requirements are met, the plant will advance one growth stage and the sprite will update to reflect this.

\- F0.f: Plants can only grow when all necessary spatial conditions are met according to the plant type. For a plant to grow, the cell it is located at must have met or surpassed the water and sun levels required by the plant type and the plants in adjacent cells must be valid according to the plant types preferred neighbors. For example, in order for a Lettuce plant to grow, the cell it is on must have a minimum water level of 5, a minimum sun level of 1.5, and must be surrounded by cells that are either empty or growing parsnips. At the start of a new turn, Map.cs will prompt a check on every plant on the grid. If a plant's cell has met the requirements, the plant will advance one growth stage. Otherwise it will remain at the current stage.

\- F0.g: The player has 31 turns ("days") to harvest as many crops as possible. After 31 turns, the player has their movement disabled and a GAME OVER panel is revealed to the player. If the player has harvested enough plants (defined by WIN_CONDITION), the player is told that they've had a good harvest and have won. Otherwise, they are told their harvest was poor and that they have lost. When the end state is reached, the player is no longer able to move. For now the play scenario is quite luck based because of all of the random elements, but the game is still winnable.

### Reflection

Our team's outlook has changed quite a bit. We all became much more aware of how difficult it is to balance this project among several others and decided to cut back on scope as much as possible. Right now we are much more focused on simply fulfilling the requirements for everything rather than centering the story that we originally intended to express in this game. Regrettably, this does mean that some of our initial efforts were wasted on things that we will likely not end up able to implement. Moving forward we will concentrate on making our game's systems and mechanics functional and as polished as we are able. It is unfortunate, but depending on how much we can get done we may be able to reintroduce these things down the line. Beyond that, we've decided to continue using the tools and frameworks we are most comfortable with. Roles have kind of devolved and everyone is just contributing where they can. So far, Damon set up the level, imported assets, and established the grid, movement, turn, and basic resource mechanics. Anais handled implementing full plant design and functionality, UI, the play scenario, and writing this Devlog.

## Devlog Entry 3 ( F1 )

### How we satisfied the software requirements

### Reflection

## Devlog Entry 4 ( F2 )

### How we satisfied the software requirements

- F0 + F1:
- Eternal DSL for Scenario Design :
- Internal DSL for Plant Growth and Conditions :
- Switch to Alternate Platform :

### Reflection
