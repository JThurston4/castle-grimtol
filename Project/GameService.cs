using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;
using System;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {

    public IRoom CurrentRoom { get; set; }
    public IRoom PreviousRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool playing { get; private set; }

    public bool GoodSails { get; set; } = false;
    public bool Spectacles { get; set; } = false;
    public bool Map { get; set; } = false;
    public bool Weapons { get; set; } = false;
    public bool dead { get; set; } = false;
    private bool superSpeed { get; set; } = false;

    public int Crew { get; set; }
    public int Upgrades { get; set; }
    private bool Winnable { get; set; }
    public void art()
    {
      if (dead == true)
      {
        System.Console.WriteLine(@"
         _,.-------.,_
     ,;~'             '~;, 
   ,;                     ;,
  ;                         ;
 ,'                         ',
,;                           ;,
; ;      .           .      ; ;
| ;   ______       ______   ; | 
|  `/~'     ~' . '~     '~\'  |
|  ~  ,-~~~^~, | ,~^~~~-,  ~  |
 |   |        }:{        |   | 
 |   l       / | \       !   |
 .~  (__,.--' .^. '--.,__)  ~. 
 |     ---;' / | \ `;---     |  
  \__.       \/^\/       .__/  
   V| \                 / |V  
    | |T~\___!___!___/~T| |  
    | |`IIII_I_I_I_IIII'| |  
    |  \,III I I I III,/  |  
     \   `~~~~~~~~~~'    /
       \   .       .   /
         \.    ^    ./   
           ^~~~^~~~^ ");
      }
    }
    public void GetUserInput()
    {
      System.Console.WriteLine("What is your command Captain:");
      string input = Console.ReadLine();
      string[] inputArr = input.Split(" ");
      string command = inputArr[0];
      string value = "";
      if (inputArr.Length > 1)
      {
        value = inputArr[1];
      }
      switch (command)
      {
        case "look":
          Look();
          break;
        case "go":
          if (value != "north" && value != "east" && value != "west" && value != "south")
          {
            System.Console.WriteLine("I think you might have had too much rum");
            break;
          }
          Go(value);
          break;
        case "use":
          UseItem(value);
          break;
        case "quit":
          Quit();
          break;
        case "take":
          TakeItem(value);
          break;
        case "search":
          searchThing(value);
          break;
        case "crew":
          crew();
          break;
        case "inventory":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "reset":
          Reset();
          break;
        case "attack":
          Attack();
          break;
        case "upgrades":
          upgrades();
          break;
        case "talk":
          Talk();
          break;

        default:
          System.Console.WriteLine("I think you might have had too much rum");
          break;
      }

    }

    private void crew()
    {
      System.Console.WriteLine($"Captain! Your crew numbers in the {Crew}'s");
    }
    private void upgrades()
    {
      System.Console.WriteLine($"Captain! The Drowning Whale has {Upgrades} upgrades");
    }

    private void addCrew()
    {
      Crew += 10;
    }
    private void addUpgrades()
    {
      Upgrades++;
    }

    public void Go(string direction)
    {
      if (dead == false)
      {
        Console.Clear();
        if (Spectacles == false && GoodSails == false)
        {
          CurrentRoom = CurrentRoom.ChangeRoom(direction);
          if (CurrentRoom.DoomedRoom == true)
          {
            System.Console.WriteLine("Captain! The waters be to rough for these patchy sails, we'll never make it!");
            System.Console.WriteLine("You and your crew have been swept away and will forever rot at the oceans floor.");
            dead = true;
            art();
            return;
          }
          else if (CurrentRoom.EdgeRoom == true || CurrentRoom.FogEdge == true || CurrentRoom.Name == "A2" || CurrentRoom.Name == "A3" || CurrentRoom.Name == "B2" || CurrentRoom.Name == "B3")
          {
            dead = true;
            art();
          }
        }
        else if (Spectacles == false && GoodSails == true)
        {
          if (CurrentRoom.Name == "H6" && superSpeed == false)
          {
            System.Console.WriteLine("You foolishly attempt to flee and are instantly gunned down. The sound of Blackbeard laughing echoes in your mind as you sink further and further.");
            dead = true;
            art();
            return;
          }
          else if (CurrentRoom.Name == "H6" && superSpeed == true)
          {
            System.Console.WriteLine("Thanks to the high tech boat engine you swiftly escape.");
            CurrentRoom = CurrentRoom.ChangeRoom(direction);
          }
          else
          {
            PreviousRoom = CurrentRoom;
            CurrentRoom = CurrentRoom.ChangeRoom(direction);
            if (CurrentRoom.LockedRoom == true)
            {
              CurrentRoom = PreviousRoom;
              System.Console.WriteLine("You attempt to traverse the fog making sure the compass stays true to its direction but a few minutes pass and you find yourself right back where you were.");
            }
            else if (CurrentRoom.EdgeRoom == true || CurrentRoom.FogEdge == true || CurrentRoom.Name == "A2" || CurrentRoom.Name == "A3" || CurrentRoom.Name == "B2" || CurrentRoom.Name == "B3")
            {
              dead = true;
              art();
            }
          }
        }
        else if (Spectacles == true)
        {
          if (CurrentRoom.Name == "H6" && superSpeed == false)
          {
            System.Console.WriteLine("You foolishly attempt to flee and are instantly gunned down. The sound of Blackbeard laughing echoes in your mind as you sink further and further.");
            dead = true;
            art();
            return;
          }
          else if (CurrentRoom.Name == "H6" && superSpeed == true)
          {
            System.Console.WriteLine("Thanks to the high tech boat engine you swiftly escape.");
            CurrentRoom = CurrentRoom.ChangeRoom(direction);
          }
          else
          {
            CurrentRoom = CurrentRoom.ChangeRoom(direction);
            if (CurrentRoom.EdgeRoom == true || CurrentRoom.FogEdge == true || CurrentRoom.Name == "A2" || CurrentRoom.Name == "A3" || CurrentRoom.Name == "B2" || CurrentRoom.Name == "B3")
            {
              dead = true;
              art();
            }
          }
        }
        if (Map == true)
        {
          System.Console.WriteLine($"Current location: {CurrentRoom.Name}");
        }
        Look();
      }
      else
      {
        System.Console.WriteLine("dead men cant move.");
        System.Console.WriteLine("Type reset to play again or quit to exit.");
      }
    }


    public void Help()
    {
      System.Console.WriteLine(@"List of commands:
        go + (direction): advances your ship to the next area.
        inventory: displays the items in your ship's inventory.
        look: gives you a description of your surroundings.
        take + (item): takes an item if one is available at your location.
        use + (item): done when turning in a quest.
        attack: attacks another ship if you in the same part of the sea.
        talk: talks to certain characters.
        crew: view the size of your crew.
        upgrades: view how many upgrades your ship has.
        reset: starts the game over.
        quit: stops the game entirely.");
    }

    public void Inventory()
    {
      Console.WriteLine("Your Inventory contains: ");
      foreach (Item item in CurrentPlayer.Inventory)
      {
        System.Console.WriteLine(item.Name);
      }
      System.Console.WriteLine($"Your crew numbers in the {Crew}'s");
      System.Console.WriteLine($"The Drowning Whale has {Upgrades} upgrades");
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);

      // print items in room
    }
    public void Talk()
    {
      if (CurrentRoom.Name == "F2")
      {
        System.Console.WriteLine("The sound of the sirens song sooths the soul. You the coy captain are captivated by the creatures cool charm. Mesmerized by the marvelous melody your mind miraculously mellows. You and your crew have become their prey.");
        dead = true;
        art();
      }
      else if (CurrentRoom.Name == "C6")
      {
        System.Console.WriteLine("The disgusting creature opens its mouth and beings speaking. 'Hello brave sailor, I am Marty the mermaid. I'm happy to finally have the chance to speak with someone so level-headed, most folks around here seem to hate mermaids. The either avoid us or start attacking us outright. Anyway there is something we need a little help with. There are some other mermaid looking people far north east from here who have stolen something precious from us. We ask you go attack them and bring back what's ours, if its not to much trouble that is.");
      }
      else
      {
        System.Console.WriteLine("There is nobody here...");
      }
    }

    public void Quit()
    {
      playing = false;
    }

    public void Reset()
    {
      Console.Clear();
      GameService gameService = new GameService();
      gameService.Setup();
      gameService.StartGame();
    }

    public void Setup()
    {
      Winnable = false;
      Crew = 30;
      Upgrades = 0;
      // if (CurrentRoom)
      #region //Create rooms
      string empty = "The water seems calm, not much here.";
      string rough = "seems extremely rough, caution is required";
      string rough2 = "the waters seem extremely rough but would pose no threat to you and your skilled crew.";
      string openSea = "nothing but open seas to the";
      string OpenSea = "Nothing but open seas to the";
      string island = "you see a small island";
      #region //ascii art
      string sword = @"
      /| ________________
O|===|* >________________>
      \|
      ";
      string glasses = @"
        _,--,            _
   __,-'____| ___      /' |
 /'   `\,--,/'   `\  /'   |
(       )  (       )'
 \_____/'  `\_____/   
      ";
      string aMap = @"
          _______________
    ()==(              (@==()
         '______________'|
           |             |
           |             |
         __)_____________|
    ()==(               (@==()
         '--------------'
      ";
      string fish = @"
                 |
                 |
                ,|.
               ,\|/.
             ,' .V. `.
            / .     . \
           /_`       '_\
          ,' .:     ;, `.
          |@)|  . .  |(@|
     ,-._ `._';  .  :`_,' _,-.
    '--  `-\ /,-===-.\ /-'  --`
   (----  _|  ||___||  |_  ----)
    `._,-'  \  `-.-'  /  `-._,'
             `-.___,-' 

      ";
      string siren = @"
                                                                          
                                                 _______                  
                                          _,,_,-'       `-.               
                                        ,'    .' ' _,.,-'  \              
                                       /    ,----''   \_  \ )             
            -.-                        |   || -.  ,-.  ))   `\            
       -.-                             ||    >a )  a   '/ |  /            
           -.-                          \\  (  <_      / /  |             
                                         `.  \  ___   /     |             
                                ,---.      \ \`. -' ,'|   \  \            
                               /     `,''`-.\  \`--'| | |  `.\`.          
                              / / , ,'  /`--' ) | __| | | |     \         
                          `--' ' / /   /  -- _,-''    |   |   \  |        
                        -.__,,-  ,'   /--_,-'         /      | | |        
                       -_    _, /    /,-'     ___   ,' /|      | |        
                      `-._..   /   ,'     _,-' ,---'  ,' /       |        
                       -__    /       _,-'_,-,'    -'   /       /         
                      -_ `  -|    _,-'- ,'  /       -',   ,  | |          
                        `-.  `---'  _,-'   /  ,'        ,/  ,\ |          
                           ``-----''  __,-'  / ,' ,    /|  / /`.\_,       
                                   ,-'     -' '  /    / | /  |  `\        
                        `--.__,,--'     ,--'    ','/ /  \(   /\   \       
     _,---._               -..__,,-' ,      -'   _, /    `' /  \   \      
    /       \              -. `----'' __,,----.__,-'-   -  -|   >   )     
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`''----''~~~~~~~~~~|-'       '-/~~/   /~~~~~~
    ~ ~~ ~~ ~                -     -  _  -    __|' '-  `- -/_ /  ,' -  _  
  -  ~   ~ ~         -    _     -   _      ,-'`- `-' '-'-'-| /  /    -    
       ~ ~      -             _      _,,--'`- `-`- -' -''-'|/ ,'  _    -  
 -    ~ ~ ~           -          ,-'' `-` `- `- `-`-`-'-' '| (_ -    -    
   _   ~   ~              `-  ,-' --`- `-`-`-`- `-`- `- `-'/. ,--,,, _   _
 -    ~  ~   `-   -'  -'-   ,' `-'`- `-`- `- `-`- -`-`- `,'  `'     ''--. 
       ~    `-  '-        ,' `-`- -' `- `-- `-_`_,`,,`--'                }
  -  ~  -`-   -'-  -'--  / `-' `-`-'`-,-`--'''                  ___,,-  | 
 ,-'-  ~  -`-  -'-  _,--/`- -'`-'`-,-'        _,,__                     | 
   -''---'  --'--,-'   /`-' `- -','            \   `-. --'             /  
 -'-._.-' `--.,-'     /  `-`-'  /               | | / \               /_  
            ,'       / `- `-`-,'       __,,--   | | /  |---._           | 
                    / `-' `- /     __,'         /`-  / ,---__\          | 
                         -','    ,'          ,-'`- `- ,---___ \           
                                          ,-' `- `-_`,--''   ``  
      ";
      string kraken = @"
                        ___
                     .-'   `'.
                    /         \
                    |         ;
                    |         |           ___.--,
           _.._     |0) ~ (0) |    _.---'`__.-( (_.
    __.--'`_.. '.__.\    '--. \_.-' ,.--'`     `''`
   ( ,.--'`   ',__ /./;   ;, '.__.'`    __
   _`) )  .---.__.' / |   |\   \__..--''  '''--., _
  `---' .'.'' -._.- '`_./  /\ '.  \ _.- ~~~````~~~-._`-.__.'
           | |  .' _.-' |  |  \  \  '.               `~---`
         \ \/ .'     \  \   '. '-._)
          \/ /        \  \    `=.__`~-.
         / /\         `) )    / / `''.`\
    , _.- '.'\ \        / / ((     / /
     `--~`   ) )    .-'.'      '.'.  | (
            (/`    ((`          ) )  '-;
             `      '-;         (-'


      ";
      string boss = @"
                            .xm*f''??T?@hc.
                          z@'` '~((!!!!!!!?*m.
                        z$$$K   ~~(/!!!!!!!!!Mh
                      .f` '#$k'`~~\!!!!!!!!!!!MMc
                     :'     f*! ~:~(!!!!!!!!!!XHMk
                     f      ' %n:~(!!!!!!!!!!!HMMM.
                    d          X~!~(!!!!!!!X!X!SMMR
                    M :   x::  :~~!>!!!!!!MNWXMMM@R
 n                  E ' *  ueeeeiu(!!XUWWWWWXMRHMMM>                :.
 E%                 E  8 .$$$$$$$$K!!$$$$$$$$&M$RMM>               :'5
z  %                3  $ 4$$$$$$$$!~!*$$$$$$$$!$MM$               :' `
K   ':              ?> # '#$$$$$#~!!!!TR$$$$$R?@MME              z   R
?     %.             5     ^'''~~~:XW!!!!T?T!XSMMM~            :^    J
 '.    ^s             ?.       ~~d$X$NX!!!!!!M!MM             f     :~
  '+.    #L            *c:.    .~'?!??!!!!!XX@M@~           z'    .*
    '+     %L           #c`'!+~~~!/!!!!!!@*TM8M           z'    .~
      ':    '%.         'C*X  .!~!~!!!!!X!!!@RF         .#     +
        ':    ^%.        9-MX!X!!X~H!!M!N!X$MM        .#`    +'
          #:    'n       'L'!~M~)H!M!XX!$!XMXF      .+`   .z'
            #:    ':      R *H$@@$H$*@$@$@$%M~     z`    +'
              %:   `*L    'k' M!~M~X!!$!@H!tF    z'    z'
                *:   ^*L   'k ~~~!~!!!!!M!X*   z*   .+'
                  's   ^*L  '%:.~~~:!!!!XH'  z#   .*'
                    #s   ^%L  ^'#4@UU@##'  z#   .*'
                      #s   ^%L           z#   .r'
                        #s   ^%.       u#   .r'
                          #i   '%.   u#   .@'
                            #s   ^%u#   .@'
                              #s x#   .*'
                               x#`  .@%.
                             x#`  .d'  '%.
                           xf~  .r' #s   '%.
                     u   x*`  .r'     #s   '%.  x.
                     %Mu*`  x*'         #m.  '%zX'
                     :R(h x*              'h..*dN.
                   u@NM5e#>                 7?dMRMh.
                 z$@M@$#'#'                 *''*@MM$hL
               u@@MM8*                          '*$M@Mh.
             z$RRM8F'                             'N8@M$bL
            5`RM$#                                  'R88f)R
            'h.$'                                     #$x*

      ";
      string flag = @"
      	
                         __________
                      .~#########%%;~.
                     /############%%;`\
                    /######/~\/~\%%;,;,\
                   |#######\    /;;;;.,.|
                   |#########\/%;;;;;.,.|
          XX       |##/~~\####%;;;/~~\;,|       XX
        XX..X      |#|  o  \##%;/  o  |.|      X..XX
      XX.....X     |##\____/##%;\____/.,|     X.....XX
 XXXXX.....XX      \#########/\;;;;;;,, /      XX.....XXXXX
X |......XX%,.@      \######/%;\;;;;, /      @#%,XX......| X
X |.....X  @#%,.@     |######%%;;;;,.|     @#%,.@  X.....| X
X  \...X     @#%,.@   |# # # % ; ; ;,|   @#%,.@     X.../  X
 X# \.X        @#%,.@                  @#%,.@        X./  #
  ##  X          @#%,.@              @#%,.@          X   #
, '# #X            @#%,.@          @#%,.@            X ##
   `###X             @#%,.@      @#%,.@             ####'
  . ' ###              @#%.,@  @#%,.@              ###`'
    . ';'                @#%.@#%,.@                ;'` ' .
      '                    @#%,.@                   ,.
      ` ,                @#%,.@  @@                `
                          @@@  @@@  
      ";
      string onIsland = @"
      	
                                                    ____
                                         v        _(    )
        _ ^ _                          v         (___(__)
       '_\V/ `
       ' oX`
          X                            v
          X             -HELP!
          X                                                 .
          X        \O/                                      |\
          X.a##a.   M                                       |_\
       .aa########a.>>                                    __|__
    .a################aa.                                 \   /
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
You sail up to a small island, there appears to be people on the island who wave as you draw near. Can always use more hands to swab the deck (take crew)";
      #endregion
      string edge = "the water drops straight down with nothing but open air beyond.  This appears to be the edge of the world.";
      string fog = "an ominous fog blocks your view.";
      string inFog = "You have sailed into the thick fog and lost all visibility. In the distance the sound of lightning cracks through the air. The waters are shifting rapidly and your crew looks nervous. You hear a few of them whisper among themselves 'surely there is nothing worth the risk of these waters'.";
      //create instances of rooms and items and everything youll encounter during gameplay
      Room F7 = new Room("F7", "The water seems calm, not much here. To the North you see a barrell floating in the water, open sea surrounds you to the East, South, and West.");
      Room G7 = new Room("G7", $"{empty} To the North you see a small island, the water to the east {rough}, {openSea} south and west.");
      Room E7 = new Room("E7", $"{empty} You see a ship to the West with a ragged looking green skull on it's sails.  To the South you see a small island, {openSea} North and East.");
      Room F8 = new Room("F8", $"{empty} To the South {edge} To the West {island}, {openSea} to the North and East.");
      Room F6 = new Room("F6", $"{sword} You see a barrell floating in the ocean, inside are weapons for your crew! Would you like to take the weapons? To the East {island}. {OpenSea} North, West, and South.");
      ShipRoom D7 = new ShipRoom("D7", $"{flag} You come face to face with the infamous Flying Dutchmen!  This ghostly crew doesnt look to happy to see you but they dont look so tough. If you think you're ready you can try attacking. To the West the water {rough}. {OpenSea} North, East and South.", 50, 0);
      Room D8 = new Room("D8", $"{empty} You see a ship to the North with a ragged looking green skull on it's sails.  To the East {island}.  The water to the West {rough}. When you look South {edge}");
      Room E8 = new Room("E8", $"{onIsland} {OpenSea} North, East, and West.  When you look South {edge}");
      Room G8 = new Room("G8", $"{empty} To the East the water {rough}. {OpenSea} North and West. When you look South {edge}");
      Room D6 = new Room("D6", $"{empty} You see a ship to the South with a ragged looking green skull on it's sails. To the West the water {rough}. Looking North {island} and {openSea} East.");
      Room E6 = new Room("E6", $"{empty} To the East you see a barrell floating in the sea.  {OpenSea} North, South, and West.");
      Room G6 = new Room("G6", $"{onIsland} To the West you see a barrell floating in the sea. The water over East {rough} Looking further East you see a massive warship, its sails are black with a laughing Jolly Roger. {OpenSea} North and South.");
      Room D5 = new Room("D5", $"{onIsland} The water North and West {rough} {OpenSea} East and South.");
      Room E5 = new Room("E5", $"{empty} The water North {rough}, to the West {island}, and you see {openSea} to the East and South.");
      Room F5 = new Room("F5", $"{empty} To the North the water {rough}. Looking South {island}. {OpenSea} East and West.");
      Room G5 = new Room("G5", $"{empty} The water to the North and East {rough}. Further North there appears to be a ship. Down South {island} and {openSea} to the West.");
      Room A8 = new Room("A8", $"{empty} A faint glimmer catches your eye toward the East. {OpenSea} North. Both West and South {edge}");
      Room B8 = new Room("B8", $"{aMap} You stumble upon a bottle floating in the middle of the sea. Inside you is a map. To the North {island} and {openSea} East and West. Looking South {edge}");
      Room C8 = new Room("C8", $"{empty} Looking East {rough2} A faint glimmer catches your eye toward the West. {OpenSea} North however over South {edge}", false, true);
      Room H8 = new Room("H8", $"{empty} To the North {island} and to the West {rough2}. Towards East and South {edge}", false, true);
      Room A7 = new Room("A7", $"{empty} You can faintly see a pirate ship over North. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. To the East {island} and {openSea} South. Looking West {edge}");
      Room B7 = new Room("B7", $"{onIsland} A faint glimmer catches your eye toward the South. {OpenSea} North, East, and West.");
      Room C7 = new Room("C7", $"{empty} To the East {rough2} Toward the West {island}. Looking North you see bizarre creatures bobbing at the water. {OpenSea} South", false, true);
      Room H7 = new Room("H7", $"{onIsland} A massive warship looms North, its sails are black with a laughing Jolly Roger. Etched on its side you read the words 'Queen Anne's Revenge'. The sheer sight of it fills you with dread. To the West {rough2} {OpenSea} South and looking East {edge}", false, true);
      ShipRoom A6 = new ShipRoom("A6", $"{flag} You have made contact with pirate ship Happy Delivery. Captain Lowther appears to want nothing more than to sink your vessel and plunder anything that remains. To the North {island} and {openSea} East and South. Over West {edge}", 60, 0);
      Room B6 = new Room("B6", $"{empty} You can faintly see a pirate ship over West. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. Down South {island}. Looking East you see bizarre creatures bobbing at the water and {openSea} North.");
      ShipRoom C6 = new ShipRoom("C6", $"{fish} You encounter some of the most grotesque creatures you've ever seen. From the waist down their body appears to be human and above the waist is fish. The inefficient design of their bodies is making it difficult for them to swim but as you see the mouth of one open you notice multipe rows of razor sharp teeth. To the East {rough2} {OpenSea} North, South, and West.", 0, 0, false, true);
      ShipRoom H6 = new ShipRoom("H6", $"{boss} You are now face to face with The Queen Anne's Revenge. Captained by the fearsome pirate Edward Teach this monstrous vessel knows no defeat. Outfitted with 40 cannons all pointing towards The Drowning Whale, you know escape is to late. You must fight if you want any chance of survival no matter how slim. To the East {edge} Perhaps sailing toward it would be a more merciful death...", 100, 3, false, true);
      Room A5 = new Room("A5", $"{onIsland} To the North {fog} You can faintly see a pirate ship down South. Upon closer inspection it the sails appear to be yellow with a smiling Jolly Roger. {OpenSea} East. Looking West {edge}");
      Room B5 = new Room("B5", $"{empty} To the North {fog} Looking West {island} and {openSea} East and South.");
      Room C5 = new Room("C5", $"{empty} To the North {fog} Looking South you see bizarre creatures bobbing at the water. Towards the East {rough2} {OpenSea} West.", false, true);
      Room H5 = new Room("H5", $"{empty} A massive warship looms South, its sails are black with a laughing Jolly Roger. Etched on its side you read the words 'Queen Anne's Revenge'. The sheer sight of it fills you with dread. To the North {island}. Towards the West {rough2} Looking East {edge}", false, true);
      Room A4 = new Room("A4", $"{inFog}", true);
      Room B4 = new Room("B4", $"{inFog}", true); ;
      Room C4 = new Room("C4", $"{inFog}", true);
      Room D4 = new Room("D4", $"{empty} To the West {fog} Down South {rough2} {OpenSea} North and East.", false, true);
      Room E4 = new Room("E4", $"{empty} In the distance North you see a pirate ship with yellow sails. The skull on the sail is wearing a monocle and instead of crossbones it has crossing silverware. To the South {rough2} {OpenSea} East and West.", false, true);
      Room F4 = new Room("F4", $"To the East you see a formidable pirate ship with orange sails. The skull on its sails is wearing a crown with gold piled high in the background. To the South {rough2} {OpenSea} North and West.", false, true);
      ShipRoom G4 = new ShipRoom("G4", $"{flag} The ship you have sailed upon is none other than Captain Bartholomew Roberts' Royal Fortune. Roberts is known for his ruthless plundering and is looking at your ship with hungry eyes. To the East {island} and to the South {rough2} {OpenSea} North and West.", 70, 1, false, true);
      Room H4 = new Room("H4", $"{onIsland} To the West you see a formidable pirate ship with orange sails. The skull on its sails is wearing a crown with gold piled high in the background. {OpenSea} North and South. Over East {edge}");
      Room A3 = new Room("A3", $"{kraken} The sky darkens and thunder roars around you. Some members of your crew are seen mumbling to themselves while others are screaming at eachother. A rogue wave hits your ship and a few people go flying overboard. Through the surrounding chaos a shadow blocks the sun. As you look up you see a giant tentacle towering accross the sky and following it back leads you to a enormous body bigger than some islands. As you glance back up the tentacle comes crashing down right into your vessel. Everything is black. The Kraken has claimed your life.");
      Room B3 = new Room("B3", $"{kraken} The sky darkens and thunder roars around you. Some members of your crew are seen mumbling to themselves while others are screaming at eachother. A rogue wave hits your ship and a few people go flying overboard. Through the surrounding chaos a shadow blocks the sun. As you look up you see a giant tentacle towering accross the sky and following it back leads you to a enormous body bigger than some islands. As you glance back up the tentacle comes crashing down right into your vessel. Everything is black. The Kraken has claimed your life.");
      Room C3 = new Room("C3", $"{onIsland} {inFog}", true);
      Room D3 = new Room("D3", $"{empty} In the distance East you see a pirate ship with yellow sails. The skull on the sail is wearing a monocle and instead of crossbones it has crossing silverware. To the West {fog} {OpenSea} North and South");
      ShipRoom E3 = new ShipRoom("E3", $"{flag} You've sailed upon the pirate ship Fancy! Captain Avery loves the finer things in life and while The Drowning Whale is nothing to brag about you and your crew would fetch a nice price as slaves. You are surrounded by open sea.", 60, 0);
      Room F3 = new Room("F3", $"{empty} To the North you spot some beautiful creatures jumping through the water. They take notice and appear to be waving in your direction. In the distance West you see a pirate ship with yellow sails. The skull on the sail is wearing a monocle and instead of crossbones it has crossing silverware. {OpenSea} East and South.");
      Room G3 = new Room("G3", $"{empty} To the South you see a formidable pirate ship with orange sails. The skull on its sails is wearing a crown with gold piled high in the background. {OpenSea} North, East, and West.");
      Room H3 = new Room("H3", $"{empty} A sinister looking pirate ship suddenly appears to the North. Its red sails and scowling Roger send shivers down your spine. To the South {island} and {openSea} West. Looking East {edge}");
      Room A2 = new Room("A2", $"{kraken} The sky darkens and thunder roars around you. Some members of your crew are seen mumbling to themselves while others are screaming at eachother. A rogue wave hits your ship and a few people go flying overboard. Through the surrounding chaos a shadow blocks the sun. As you look up you see a giant tentacle towering accross the sky and following it back leads you to a enormous body bigger than some islands. As you glance back up the tentacle comes crashing down right into your vessel. Everything is black. The Kraken has claimed your life.");
      Room B2 = new Room("B2", $"{kraken} The sky darkens and thunder roars around you. Some members of your crew are seen mumbling to themselves while others are screaming at eachother. A rogue wave hits your ship and a few people go flying overboard. Through the surrounding chaos a shadow blocks the sun. As you look up you see a giant tentacle towering accross the sky and following it back leads you to a enormous body bigger than some islands. As you glance back up the tentacle comes crashing down right into your vessel. Everything is black. The Kraken has claimed your life.");
      Room C2 = new Room("C2", $"{inFog}", true);
      Room D2 = new Room("D2", $"{empty} To the North {island} and to the West {fog} {OpenSea} East and South.");
      Room E2 = new Room("E2", $"{empty} To the East you spot some beautiful creatures jumping through the water. They take notice and appear to be waving in your direction. In the distance South you see a pirate ship with yellow sails. The skull on the sail is wearing a monocle and instead of crossbones it has crossing silverware. {OpenSea} North and West.");
      ShipRoom F2 = new ShipRoom("F2", $"{siren} You have sailed upon some enchanting creatures and as you draw near they begin swimming towards your ship, they look like they want to talk. You gaze wonderously at these elegant beings, their upper body a beautiful woman and lower a fish-like tail. Nothing but openseas surrounds you.", 0, 0, false, false);
      Room G2 = new Room("G2", $"{empty} A sinister looking pirate ship suddenly appears to the East. Its red sails and scowling Roger send shivers down your spine. To the West you spot some beautiful creatures jumping through the water. They take notice and appear to be waving in your direction. To the North you see a lonesome barrell floating in the water. {OpenSea} South.");
      ShipRoom H2 = new ShipRoom("H2", $"{flag} You've decided to sail straight towards the Adventure Galley and as Captain Kidd stares you down you begin to think this might be a bad idea. Very few people have the chance to meet Captain Kidd and even fewer make it out alive. {OpenSea} North, South, and West. Looking East {edge}", 80, 2);
      Room A1 = new Room("A1", $"While sailing through the thick fog you almost run directly into an island that shot up out of no where. There looks to be some crates on the island. You crack open a crate and find none other than Hollow Point Cannonballs! (take cannonballs)");
      Room B1 = new Room("B1", $"{inFog}");
      Room C1 = new Room("C1", $"{inFog}", true);
      Room D1 = new Room("D1", $"{onIsland} To the West {fog} {OpenSea} East and South. Up North {edge}");
      Room E1 = new Room("E1", $"{empty} To the West {island}, {openSea} East and South, and up North {edge}");
      Room F1 = new Room("F1", $"{empty} To the South you spot some beautiful creatures jumping through the water. They take notice and appear to be waving in your direction. To the East you see a lonesome barrell floating in the water. {OpenSea} West and up North {edge}");
      Room G1 = new Room("G1", $"{glasses} You've come upon a barrell floating at sea, inside are some strange spectacles. {OpenSea} East, West, and South. Up north {edge}");
      Room H1 = new Room("H1", $"{empty} A sinister looking pirate ship suddenly appears to the South. Its red sails and scowling Roger send shivers down your spine. To the West you see a lonesome barrell floating in the water. To the North and East {edge}");
      Room Edge1 = new Room("Edge1", "You decide to test your curiousity and sail full speed where the water drops. As you clear the steep angle your ship loses all control and gets hurdled downward. You quickly grab a nearby rope temporarily securing you to The Drowning Whale. Looking around you see members of your crew flying through the air, some screaming and others silent. Glancing up you find an island floating in the sky. Your mind can't seem to comprehend what is happening but after what seems like an enternity the sounds surrounding you become quiet and your mind achieves an inner calm. You resign to your fate as you forever continue your descent into the abyss.", false, false, true);
      Room Edge2 = new Room("Edge2", "You continue to sail through the blinding fog hoping to find some long lost treasure when suddenly you see a few members of your crew floating through the air, what an odd sight! Instantly the fog clears and the realization dawns that through your foolhardiness you've sailed past the edge of the world. Glancing up you find an island floating in the sky. Your mind can't seem to comprehend what is happening but after what seems like an enternity the sounds surrounding you become quiet and your mind achieves an inner calm. You resign to your fate as you forever continue your descent into the abyss.", false, false, false, true);
      #endregion
      //creating exits
      //Row 8   
      #region //Room Relationships
      A8.Exits.Add("north", A7);
      A8.Exits.Add("east", B8);
      A8.Exits.Add("west", Edge1);
      A8.Exits.Add("south", Edge1);

      B8.Exits.Add("north", B7);
      B8.Exits.Add("east", C8);
      B8.Exits.Add("west", A8);
      B8.Exits.Add("south", Edge1);

      C8.Exits.Add("north", C7);
      C8.Exits.Add("east", D8);
      C8.Exits.Add("west", B8);
      C8.Exits.Add("south", Edge1);

      D8.Exits.Add("north", D7);
      D8.Exits.Add("east", E8);
      D8.Exits.Add("west", C8);
      D8.Exits.Add("south", Edge1);

      E8.Exits.Add("north", E7);
      E8.Exits.Add("east", F8);
      E8.Exits.Add("west", D8);
      E8.Exits.Add("south", Edge1);

      F8.Exits.Add("north", F7);
      F8.Exits.Add("east", G8);
      F8.Exits.Add("west", E8);
      F8.Exits.Add("south", Edge1);

      G8.Exits.Add("north", G7);
      G8.Exits.Add("east", H8);
      G8.Exits.Add("west", F8);
      G8.Exits.Add("south", Edge1);

      H8.Exits.Add("north", H7);
      H8.Exits.Add("east", Edge1);
      H8.Exits.Add("west", G8);
      H8.Exits.Add("south", Edge1);
      //Row 7
      A7.Exits.Add("north", A6);
      A7.Exits.Add("east", B7);
      A7.Exits.Add("west", Edge1);
      A7.Exits.Add("south", A8);

      B7.Exits.Add("north", B6);
      B7.Exits.Add("east", C7);
      B7.Exits.Add("west", A7);
      B7.Exits.Add("south", B8);

      C7.Exits.Add("north", C6);
      C7.Exits.Add("east", D7);
      C7.Exits.Add("west", B7);
      C7.Exits.Add("south", C8);

      D7.Exits.Add("north", D6);
      D7.Exits.Add("east", E7);
      D7.Exits.Add("west", C7);
      D7.Exits.Add("south", D8);

      E7.Exits.Add("north", E6);
      E7.Exits.Add("east", F7);
      E7.Exits.Add("west", D7);
      E7.Exits.Add("south", E8);

      F7.Exits.Add("north", F6);
      F7.Exits.Add("east", G7);
      F7.Exits.Add("west", E7);
      F7.Exits.Add("south", F8);

      G7.Exits.Add("north", G6);
      G7.Exits.Add("east", H7);
      G7.Exits.Add("west", F7);
      G7.Exits.Add("south", G8);

      H7.Exits.Add("north", H6);
      H7.Exits.Add("east", Edge1);
      H7.Exits.Add("west", G7);
      H7.Exits.Add("south", H8);
      //Row 6
      A6.Exits.Add("north", A5);
      A6.Exits.Add("east", B6);
      A6.Exits.Add("west", Edge1);
      A6.Exits.Add("south", A7);

      B6.Exits.Add("north", B5);
      B6.Exits.Add("east", C6);
      B6.Exits.Add("west", A6);
      B6.Exits.Add("south", B7);

      C6.Exits.Add("north", C5);
      C6.Exits.Add("east", D6);
      C6.Exits.Add("west", B6);
      C6.Exits.Add("south", C7);

      D6.Exits.Add("north", D5);
      D6.Exits.Add("east", E6);
      D6.Exits.Add("west", C6);
      D6.Exits.Add("south", D7);

      E6.Exits.Add("north", E5);
      E6.Exits.Add("east", F6);
      E6.Exits.Add("west", D6);
      E6.Exits.Add("south", E7);

      F6.Exits.Add("north", F5);
      F6.Exits.Add("east", G6);
      F6.Exits.Add("west", E6);
      F6.Exits.Add("south", F7);

      G6.Exits.Add("north", G5);
      G6.Exits.Add("east", H6);
      G6.Exits.Add("west", F6);
      G6.Exits.Add("south", G7);

      H6.Exits.Add("north", H5);
      H6.Exits.Add("east", Edge1);
      H6.Exits.Add("west", G6);
      H6.Exits.Add("south", H7);
      //Row 5
      A5.Exits.Add("north", A4);
      A5.Exits.Add("east", B5);
      A5.Exits.Add("west", Edge1);
      A5.Exits.Add("south", A6);

      B5.Exits.Add("north", B4);
      B5.Exits.Add("east", C5);
      B5.Exits.Add("west", A5);
      B5.Exits.Add("south", B6);

      C5.Exits.Add("north", C4);
      C5.Exits.Add("east", D5);
      C5.Exits.Add("west", B5);
      C5.Exits.Add("south", C6);

      D5.Exits.Add("north", D4);
      D5.Exits.Add("east", E5);
      D5.Exits.Add("west", C5);
      D5.Exits.Add("south", D6);

      E5.Exits.Add("north", E4);
      E5.Exits.Add("east", F5);
      E5.Exits.Add("west", D5);
      E5.Exits.Add("south", E6);

      F5.Exits.Add("north", F4);
      F5.Exits.Add("east", G5);
      F5.Exits.Add("west", E5);
      F5.Exits.Add("south", F6);

      G5.Exits.Add("north", G4);
      G5.Exits.Add("east", H5);
      G5.Exits.Add("west", F5);
      G5.Exits.Add("south", G6);

      H5.Exits.Add("north", H4);
      H5.Exits.Add("east", Edge1);
      H5.Exits.Add("west", G5);
      H5.Exits.Add("south", H6);
      //Row 4
      A4.Exits.Add("north", A3);
      A4.Exits.Add("east", B4);
      A4.Exits.Add("west", Edge2);
      A4.Exits.Add("south", A5);

      B4.Exits.Add("north", B3);
      B4.Exits.Add("east", C4);
      B4.Exits.Add("west", A4);
      B4.Exits.Add("south", B5);

      C4.Exits.Add("north", C3);
      C4.Exits.Add("east", D4);
      C4.Exits.Add("west", B4);
      C4.Exits.Add("south", C5);

      D4.Exits.Add("north", D3);
      D4.Exits.Add("east", E4);
      D4.Exits.Add("west", C4);
      D4.Exits.Add("south", D5);

      E4.Exits.Add("north", E3);
      E4.Exits.Add("east", F4);
      E4.Exits.Add("west", D4);
      E4.Exits.Add("south", E5);

      F4.Exits.Add("north", F3);
      F4.Exits.Add("east", G4);
      F4.Exits.Add("west", E4);
      F4.Exits.Add("south", F5);

      G4.Exits.Add("north", G3);
      G4.Exits.Add("east", H4);
      G4.Exits.Add("west", F4);
      G4.Exits.Add("south", G5);

      H4.Exits.Add("north", H3);
      H4.Exits.Add("east", Edge1);
      H4.Exits.Add("west", G4);
      H4.Exits.Add("south", H5);
      //Row 3
      C3.Exits.Add("north", C2);
      C3.Exits.Add("east", D3);
      C3.Exits.Add("west", B3);
      C3.Exits.Add("south", C4);

      D3.Exits.Add("north", D2);
      D3.Exits.Add("east", E3);
      D3.Exits.Add("west", C3);
      D3.Exits.Add("south", D4);

      E3.Exits.Add("north", E2);
      E3.Exits.Add("east", F3);
      E3.Exits.Add("west", D3);
      E3.Exits.Add("south", E4);

      F3.Exits.Add("north", F2);
      F3.Exits.Add("east", G3);
      F3.Exits.Add("west", E3);
      F3.Exits.Add("south", F4);

      G3.Exits.Add("north", G2);
      G3.Exits.Add("east", H3);
      G3.Exits.Add("west", F3);
      G3.Exits.Add("south", G4);

      H3.Exits.Add("north", H2);
      H3.Exits.Add("east", Edge1);
      H3.Exits.Add("west", G3);
      H3.Exits.Add("south", H4);
      //Row 2
      C2.Exits.Add("north", C1);
      C2.Exits.Add("east", D2);
      C2.Exits.Add("west", B2);
      C2.Exits.Add("south", C3);

      D2.Exits.Add("north", D1);
      D2.Exits.Add("east", E2);
      D2.Exits.Add("west", C2);
      D2.Exits.Add("south", D3);

      E2.Exits.Add("north", E1);
      E2.Exits.Add("east", F2);
      E2.Exits.Add("west", D2);
      E2.Exits.Add("south", E3);

      F2.Exits.Add("north", F1);
      F2.Exits.Add("east", G2);
      F2.Exits.Add("west", E2);
      F2.Exits.Add("south", F3);

      G2.Exits.Add("north", G1);
      G2.Exits.Add("east", H2);
      G2.Exits.Add("west", F2);
      G2.Exits.Add("south", G3);

      H2.Exits.Add("north", H1);
      H2.Exits.Add("east", Edge1);
      H2.Exits.Add("west", G2);
      H2.Exits.Add("south", H3);
      //Row 1
      A1.Exits.Add("north", Edge2);
      A1.Exits.Add("east", B1);
      A1.Exits.Add("west", Edge2);
      A1.Exits.Add("south", A2);

      B1.Exits.Add("north", Edge2);
      B1.Exits.Add("east", C1);
      B1.Exits.Add("west", A1);
      B1.Exits.Add("south", B2);

      C1.Exits.Add("north", Edge2);
      C1.Exits.Add("east", D1);
      C1.Exits.Add("west", B1);
      C1.Exits.Add("south", C2);

      D1.Exits.Add("north", Edge1);
      D1.Exits.Add("east", E1);
      D1.Exits.Add("west", C1);
      D1.Exits.Add("south", D2);

      E1.Exits.Add("north", Edge1);
      E1.Exits.Add("east", F1);
      E1.Exits.Add("west", D1);
      E1.Exits.Add("south", E2);

      F1.Exits.Add("north", Edge1);
      F1.Exits.Add("east", G1);
      F1.Exits.Add("west", E1);
      F1.Exits.Add("south", F2);

      G1.Exits.Add("north", Edge1);
      G1.Exits.Add("east", H1);
      G1.Exits.Add("west", F1);
      G1.Exits.Add("south", G2);

      H1.Exits.Add("north", Edge1);
      H1.Exits.Add("east", Edge1);
      H1.Exits.Add("west", G1);
      H1.Exits.Add("south", H2);
      #endregion

      //add items to rooms
      #region //items
      Item GoodSails = new Item("Sails", "Adding these to our ship cap'n");
      Item Spectacles = new Item("Spectacles", "some sort of glass thing, when you hold them up things look clearer. Could sail through any waters with these no matter how drunk!");
      Item Crew1 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew2 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew3 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew4 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew5 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew6 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew7 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew8 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Crew9 = new Item("Crew", "More filthy mouths to feed and more power you wield.");
      Item Upgrades1 = new Item("Upgrades", "Improve your ship!");
      Item Upgrades2 = new Item("Upgrades", "Improve your ship!");
      Item Upgrades3 = new Item("Upgrades", "Improve your ship!");
      Item Upgrades4 = new Item("Upgrades", "Improve your ship!");
      Item HollowPoints = new Item("Cannonballs", "How do these even exist");
      Item Map = new Item("Map", "A map to help guide you.");
      Item Weapons = new Item("Weapons", "Swords for me mateys");
      Item ShipEngine = new Item("Engine", "zoom zoom");
      Item Flippers = new Item("flippers", "You could swim a little faster with these.");

      G6.Items.Add(Crew1);
      D5.Items.Add(Crew1);
      B7.Items.Add(Crew1);
      H7.Items.Add(Crew1);
      A5.Items.Add(Crew1);
      H4.Items.Add(Crew1);
      C3.Items.Add(Crew1);
      E8.Items.Add(Crew1);
      D1.Items.Add(Crew1);
      D7.Items.Add(GoodSails);
      G1.Items.Add(Spectacles);
      A6.Items.Add(Upgrades1);
      G4.Items.Add(Upgrades2);
      E3.Items.Add(Upgrades3);
      H2.Items.Add(Upgrades4);
      F6.Items.Add(Weapons);
      B8.Items.Add(Map);
      C6.Items.Add(ShipEngine);
      F2.Items.Add(Flippers);
      A1.Items.Add(HollowPoints);
      #endregion

      //add rooms to room's exits done
      CurrentRoom = F7;
      //default player
      CurrentPlayer = new Player();


      //game intro
      System.Console.WriteLine("Ahoy Captain! Looks like you passed out after drinking to much rum again! You're not the only one though, none of the crew know where we wound up after the feast last night. These waters seem dangerous but I bet they're full of treasure! Lets look around and see where we should go.");
      Look();
      System.Console.WriteLine(" ");
      Help();
    }

    public void StartGame()
    {
      playing = true;
      while (playing)
      {
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {
      //does that item exist in the CurrentRoom
      Item foundItem = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (CurrentRoom is ShipRoom)
      {
        ShipRoom room = (ShipRoom)CurrentRoom;
        if (foundItem != null)
        {
          if (foundItem.Name.ToLower() == "sails")
          {
            CurrentRoom.Items.Remove(foundItem);
            CurrentPlayer.Inventory.Add(foundItem);
            GoodSails = true;
            System.Console.WriteLine("I reckon we can sail much further with these cap'n.");
          }
          else if (foundItem.Name == "Upgrades" && room.defeated == true)
          {
            Upgrades++;
            CurrentRoom.Items.Remove(foundItem);
            System.Console.WriteLine("You've stripped their ship and made improvements to The Drowning Whale! ");
          }
          else if (foundItem.Name == "flippers" && room.defeated == true)
          {
            CurrentPlayer.Inventory.Add(foundItem);
            CurrentRoom.Items.Remove(foundItem);
            System.Console.WriteLine("You could swim a little faster with these.");
            superSpeed = true;
          }
          else if (foundItem.Name == "engine" && room.defeated == true)
          {
            CurrentRoom.Items.Remove(foundItem);
            CurrentPlayer.Inventory.Add(foundItem);
            superSpeed = true;
            System.Console.WriteLine("With this engine you'll be able to out run any ship in the seven seas!");
          }
        }

      }
      if (foundItem != null)
      {
        System.Console.WriteLine("It be ours now Cap'n");
        if (foundItem.Name == "Crew")
        {
          addCrew();
          CurrentRoom.Items.Remove(foundItem);
          System.Console.WriteLine(@"Added 10 crew to The Drowning Whale.
          With great power comes great responsibility to find greater treasure!");

        }

        else if (foundItem.Name == "Spectacles")
        {
          CurrentRoom.Items.Remove(foundItem);
          CurrentPlayer.Inventory.Add(foundItem);
          Spectacles = true;
          System.Console.WriteLine("some sort of glass thing, when you hold them up things look clearer. Could sail through any waters with these no matter how drunk!");
        }

        else if (foundItem.Name == "Cannonballs")
        {
          Winnable = true;
          CurrentRoom.Items.Remove(foundItem);
          CurrentPlayer.Inventory.Add(foundItem);
          System.Console.WriteLine("Hollowpoint Cannonballs?! Who even knew such a thing existed! With these surely there is no one who can stand in our way.");
        }
        else if (foundItem.Name == "Map")
        {
          Map = true;
          CurrentRoom.Items.Remove(foundItem);
          CurrentPlayer.Inventory.Add(foundItem);
          System.Console.WriteLine("A map to help guide your way.");
        }
        else if (foundItem.Name == "Weapons")
        {
          Weapons = true;
          CurrentRoom.Items.Remove(foundItem);
          CurrentPlayer.Inventory.Add(foundItem);
          System.Console.WriteLine("Swords for me mateys.");
        }
      }
      else
      {
        System.Console.WriteLine("Too much Rum I can't find that");
      }
    }

    public void searchThing(string searchName)
    {

    }


    public void Attack()
    {
      if (CurrentRoom is ShipRoom)
      {
        ShipRoom room = (ShipRoom)CurrentRoom;
        if (Weapons == true)
        {

          if (Crew >= room.CrewToWin && Upgrades >= room.UpragesToWin && room.Name != "H6")
          {
            if (room.Name == "D7")
            {
              System.Console.WriteLine("Cannonballs flying and swords clashing! After a long fought battle you and your sea dogs have claimed victory! Let's celebrate victory by stripping their ship apart and upgrading ours. (take sails)");
            }
            else if (room.Name == "F2")
            {
              System.Console.WriteLine("Seeing through their trick you order your order crew to immediately sends cannonballs flying. The surprise attack catching most of the sirens off guard leading to an easy victory. As your crew celebrates you see a small barrell float to the surface, inside it contains rubber flippers for swimming. (take flippers)");
              room.defeated = true;
            }
            else if (room.Name == "C6")
            {
              System.Console.WriteLine("On your order cannonballs begin to fly. The hideous mermaids miraculously dodge them, perhaps they're used to this? Before you have time to think any more they start chomping on your ship ripping sizeable chunks out with each bite. The Drowning Whale begins to drown leaving you and your crew to be nothing more than fish food.");
              dead = true;
              art();
            }
            else
            {
              System.Console.WriteLine("Cannonballs flying and swords clashing! After a long fought battle you and your sea dogs have claimed victory! Let's celebrate victory by stripping their ship apart and upgrading ours. (take upgrades)");
            }
            room.defeated = true;
          }
          else if (Crew >= room.CrewToWin && Upgrades >= room.UpragesToWin && Winnable == true && room.Name == "H6")
          {
            //winner
            System.Console.WriteLine(@"
                                     o
                                   $''$o
                                  $'  $$
                                   $$$$
                                   o '$o
                                  o'  '$
             oo'$$$'  oo$'$ooo   o$    '$    ooo'$oo  $$$'o
o o o o    oo'  o'      'o    $$o$'     o o$''  o$      '$  'oo   o o o o
'$o   ''$$$'   $$         $      '   o   ''    o'         $   'o$$'    o$$
  ''o       o  $          $'       $$$$$       o          $  ooo     o''
     'o   $$$$o $o       o$        $$$$$'       $o        ' $$$$   o'
      ''o $$$$o  oo o  o$'         $$$$$'        'o o o o'  '$$$  $
        '' '$'     '''''            ''$'            '''      ''' '
         'oooooooooooooooooooooooooooooooooooooooooooooooooooooo$
          '$$$$'$$$$' $$$$$$$'$$$$$$ ' '$$$$$'$$$$$$'  $$$''$$$$
           $$$oo$$$$   $$$$$$o$$$$$$o' $$$$$$$$$$$$$$ o$$$$o$$$'
           $'''''''''''''''''''''''''''''''''''''''''''''''''''$
           $'                                                 '$
           $'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$'$

            You've conquered all the seas and are now King of the Pirates!
            You win!");
          }
          else
          {
            System.Console.WriteLine("The long battle has proven to much for you and your make shift crew, the enemy overwhelms you. Time to walk the plank.");
            dead = true;
            art();
          }
        }
        else
        {
          System.Console.WriteLine("The long battle has proven to much for you and your make shift crew, the enemy overwhelms you. Time to walk the plank.");
          dead = true;
          art();
        }
      }
      else
      {
        System.Console.WriteLine("I tink ya been drinking again cap'n, aint no ships");
      }
    }

    public void UseItem(string itemName)
    {
      ShipRoom room = (ShipRoom)CurrentRoom;
      Item foundItem = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (foundItem != null)
      {
        if (itemName.ToLower() == "flippers" && CurrentRoom.Name == "C6")
        {
          System.Console.WriteLine("Our flippers! With these we'll be able to hunt again! Thank you dearest friend, as a reward we've been carrying around some ancient technology but have no real use for it, why dont you take it? (take engine)");
          room.defeated = true;
        }

      }
      else
      {
        System.Console.WriteLine("Nothing to use here cap'n.");
      }

    }
  }
}