using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class Battle
    {
        protected Random Random = new Random();
        protected bool PlayerIsEffectedByRandomEvent;
        protected bool EnemyIsEffectedByRandomEvent;
        protected bool RandomEventOccured;
        protected int RandomEventModifier;
        protected Items Items = new Items();
        public Battle()
        { 
        
        }
        public Battle(Player Human, Enemy Computer, string ItemsName, int ItemPurchased)
        {
            int RoundNumber = 1;

            int PlayerHP = Human.GetPlayerHP();
            int PlayerAttack = Human.GetPlayerAttackValue();
            int PlayerDodge = Human.GetPlayerDodgeValue();
            int PlayerMagic = Human.GetPlayerMagicValue();
            string PlayerType = Human.GetPlayerType();
            int PlayerDodgeBonus = 0;
            bool PlayerAttacking;
            bool PlayerDodging;
            bool PlayerUsingMagic;
            bool PlayerWantsToQuit = false;

            int EnemyHP = Computer.GetEnemyHP();
            int EnemyAttack = Computer.GetEnemyAttackValue();
            int EnemyDodge = Computer.GetEnemyDodgeValue();
            int EnemyMagic = Computer.GetEnemyMagicValue();
            string EnemyType = Computer.GetEnemyType();
            int EnemyDodgeBonus = 0;
            int EnemyChoice;
            int Damage;

            while (PlayerHP > 0 && EnemyHP > 0)
            {
                bool ValidChoice = false;
                PlayerIsEffectedByRandomEvent = false;
                EnemyIsEffectedByRandomEvent = false;

                while (!ValidChoice)
                {
                    PlayersTurnColors(PlayerType);

                    Console.Clear();

                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine($"Round {RoundNumber}...{PlayerType}'s turn");

                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"1. Attack (you can roll a 1d{PlayerAttack})");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"2. Dodge (you can roll a 1d{PlayerDodge})");
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"3. Use Magic (you can roll a 1d{PlayerMagic})");
                    Console.SetCursorPosition(50, 4);
                    Console.WriteLine($"Your {PlayerType} currently has {PlayerHP}HP");
                    Console.SetCursorPosition(50, 6);
                    Console.WriteLine($"{EnemyType} currently has {EnemyHP}HP");
                    Console.SetCursorPosition(2, Console.WindowHeight - 3);
                    Console.Write("4. I'm done. I want to quit. (Choose option 4 or type stop, end or quit)");

                    if (ItemPurchased > 0)
                    { 
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"Remember, your {PlayerType} is using {ItemsName}.");
                    
                        if (ItemPurchased == 1)
                        {
                            Console.SetCursorPosition(2, 18);
                            Console.WriteLine($"This gives them a + 1 on your attack attempts.");
                        }
                        if (ItemPurchased == 2)
                        {
                            Console.SetCursorPosition(2, 18);
                            Console.WriteLine($"This gives them a + 1 on your dodge attempts.");
                        }
                        if (ItemPurchased == 3)
                        {
                            Console.SetCursorPosition(2, 18);
                            Console.WriteLine($"This gives them a + 1 on your magic attempts.");
                        }
                    }

                    Console.SetCursorPosition(2, 4);
                    Console.Write("Choose your action: ");
                    string PlayersChoice = Console.ReadLine().ToLower();

                    switch (PlayersChoice)
                    {
                        case "1":
                            // player chooses to attack
                            ValidChoice = true;
                            Program.PressAKey();
                            Console.Clear();
                            PlayerDodge = Human.GetPlayerDodgeValue();
                            PlayerDodgeBonus = 0;
                            PlayerAttacking = true;
                            Damage = Attack(PlayerType, PlayerType, PlayerAttack, EnemyDodge, EnemyDodgeBonus, ItemPurchased, PlayerAttacking);
                            EnemyHP = EnemyHP - Damage;
                            RoundNumber++;
                            break;
                        case "2":
                            // player chooses to dodge
                            ValidChoice = true;
                            Program.PressAKey();
                            Console.Clear();
                            PlayerDodging = true;
                            PlayerDodgeBonus = AttemptToDodge(PlayerType, PlayerDodge, ItemPurchased, PlayerDodging);
                            RoundNumber++;
                            break;
                        case "3":
                            // player chooses to use magic
                            ValidChoice = true;
                            Program.PressAKey();
                            Console.Clear();
                            PlayerDodge = Human.GetPlayerDodgeValue();
                            PlayerDodgeBonus = 0;
                            PlayerUsingMagic = true;
                            Damage = UseMagic(PlayerType, PlayerMagic, EnemyMagic, EnemyDodgeBonus, ItemPurchased, PlayerUsingMagic);
                            EnemyHP = EnemyHP - Damage;
                            RoundNumber++;
                            break;
                        case "4":
                        case "quit":
                        case "end":
                        case "stop":
                            ValidChoice = true;
                            PlayerWantsToQuit = true;
                            break;
                    }
                }

                if (EnemyHP <= 0 || PlayerHP <= 0 || PlayerWantsToQuit)
                {
                    break;
                }

                PlayerDodging = true;
                PlayerUsingMagic = true;

                RandomEventModifier = RandomEvent(RoundNumber, PlayerType, EnemyType, PlayerMagic, PlayerDodge, PlayerDodgeBonus, EnemyDodge, EnemyMagic, EnemyDodgeBonus, PlayerDodging, PlayerUsingMagic, ItemPurchased);

                if (RandomEventOccured)
                {
                    PlayerDodgeBonus = 0;
                    EnemyDodgeBonus = 0;
                }

                if (PlayerIsEffectedByRandomEvent)
                { 
                    PlayerHP = PlayerHP - RandomEventModifier;
                }

                if (EnemyIsEffectedByRandomEvent)
                { 
                    EnemyHP = EnemyHP - RandomEventModifier;
                }

                if (EnemyHP <= 0 || PlayerHP <= 0)
                {
                    break;
                }
                
                EnemysTurnColors();

                Console.Clear();
                EnemyChoice = Random.Next(1, 4);

                Console.SetCursorPosition(2, 2);
                Console.WriteLine($"Round {RoundNumber}...{EnemyType}'s turn");

                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"1. Attack (They can roll a 1d{EnemyAttack})");
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"2. Dodge (They can roll a 1d{EnemyDodge})");
                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"3. Use Magic (They can roll a 1d{EnemyMagic})");
                Console.SetCursorPosition(50, 6);
                Console.WriteLine($"{EnemyType} currently has {EnemyHP}HP");
                Console.SetCursorPosition(50, 4);
                Console.WriteLine($"Your {PlayerType} currently has {PlayerHP}HP");
                Console.SetCursorPosition(2, 4);
                Console.Write($"{EnemyType} chooses action: {EnemyChoice}");

                if (ItemPurchased > 0)
                {
                    Console.SetCursorPosition(2, 16);
                    Console.WriteLine($"Remember, your {PlayerType} is using {ItemsName}.");

                    if (ItemPurchased == 1)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine($"This gives them a + 1 on your attack attempts.");
                    }
                    if (ItemPurchased == 2)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine($"This gives them a + 1 on your dodge attempts.");
                    }
                    if (ItemPurchased == 3)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine($"This gives them a + 1 on your magic attempts.");
                    }
                }

                switch (EnemyChoice)
                {
                    case 1:
                        // Enemy Attacks
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"{EnemyType} attempts to attack!");
                        Program.PressAKey();
                        Console.Clear();
                        EnemyDodge = Computer.GetEnemyDodgeValue();
                        EnemyDodgeBonus = 0;
                        PlayerAttacking = false;
                        PlayerDodging = true;
                        Damage = Attack(EnemyType, PlayerType, EnemyAttack, PlayerDodge, PlayerDodgeBonus, ItemPurchased, PlayerAttacking, PlayerDodging);
                        PlayerHP = PlayerHP - Damage;
                        RoundNumber++;
                        break;
                    case 2:
                        // Enemy Dodges
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"{EnemyType} attempts to dodge the next attack!");
                        Program.PressAKey();
                        Console.Clear();
                        EnemyDodgeBonus = AttemptToDodge(EnemyType, EnemyDodge);
                        RoundNumber++;
                        break;
                    case 3:
                        // Enemy Magic
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"{EnemyType} attempts to use magic!");
                        Program.PressAKey();
                        Console.Clear();
                        EnemyDodge = Computer.GetEnemyDodgeValue();
                        EnemyDodgeBonus = 0;
                        PlayerUsingMagic = false;
                        PlayerDodging = true;
                        Damage = UseMagic(EnemyType, EnemyMagic, PlayerMagic, PlayerDodgeBonus, ItemPurchased, PlayerUsingMagic, PlayerDodging);
                        PlayerHP = PlayerHP - Damage;
                        RoundNumber++;
                        break;
                }

                PlayerDodging = true;
                PlayerUsingMagic = true;

                RandomEventModifier = RandomEvent(RoundNumber, PlayerType, EnemyType, PlayerMagic, PlayerDodge, PlayerDodgeBonus, EnemyDodge, EnemyMagic, EnemyDodgeBonus, PlayerDodging, PlayerUsingMagic, ItemPurchased);

                if (RandomEventOccured)
                {
                    PlayerDodgeBonus = 0;
                    EnemyDodgeBonus = 0;
                }

                if (PlayerIsEffectedByRandomEvent)
                {
                    PlayerHP = PlayerHP - RandomEventModifier;
                }

                if (EnemyIsEffectedByRandomEvent)
                {
                    EnemyHP = EnemyHP - RandomEventModifier;
                }
            }

            if (PlayerHP <= 0 && EnemyHP <= 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.SetCursorPosition(2, 2);
                Console.WriteLine("Both fighters have lost all their HP");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine($"Your {PlayerType} and {EnemyType} lasted for {RoundNumber} rounds");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine("Everyone loses this fight!");
                Program.PressAKey();
            }
            else if (PlayerHP <= 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.SetCursorPosition(2, 2);
                Console.WriteLine("You have lost all your HP");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine($"You lasted for {RoundNumber} rounds");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine("You lose this fight!");
                Program.PressAKey();
            }
            else if (EnemyHP <= 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.SetCursorPosition(2, 2);
                Console.WriteLine("Your enemy has lost all their HP");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine($"{EnemyType} lasted for {RoundNumber} rounds");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine("You win this fight!");
                Program.PressAKey();
            }
            else if (PlayerWantsToQuit)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
            }
        }
        public int Attack(string WhoseAttacking, string PlayerType, int AttackValue, int DodgeValue, int DodgeBonus, int ItemPurchased = 0, bool PlayerAttacking = false, bool PlayerDodging = false)
        {
            string Attacker = WhoseAttacking;
            int MissedBy = 0;
            int Attack = Random.Next(1, AttackValue + 1);
            int Dodge = Random.Next(1, DodgeValue + 1) + DodgeBonus;
            int Damage;

            if (ItemPurchased == 1 && PlayerAttacking)
            {
                Attack = Attack + 1;
            }
            else if (ItemPurchased == 2 && PlayerDodging)
            {
                Dodge = Dodge + 1;
            }

            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Attack!");

            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"{Attacker} rolled a ... {Attack}");

            Console.SetCursorPosition(2, 6);
            Console.WriteLine($"Defender rolled a ... {Dodge}");


            if (DodgeBonus > 0)
            {
                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"Defender rolled a ... {Dodge}, this includes the {DodgeBonus} bonus.");
            }

            if (Attack > Dodge)
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"{Attacker}'s attack is successful!");
                Damage = Attack - Dodge;
                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"{Attacker} hits for {Damage}");
            }
            else if (Attack < Dodge)
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"{Attacker} swings and misses!");
                MissedBy = Dodge - Attack;
                Damage = 0;
            }
            else
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"WOW! That was close! {Attacker}'s attack barely missed.");
                Damage = 0;
            }

            FlavorTextForAttacks(Attacker, PlayerType, Damage, MissedBy, PlayerAttacking, PlayerDodging);

            Program.PressAKey();

            return Damage;
        }
        public int AttemptToDodge(string WhoseDodging, int DodgeValue, int ItemPurchased = 0, bool PlayerDodging = false)
        {
            string Dodger = WhoseDodging;
            int Dodge = Random.Next(1, DodgeValue + 1);

            if (ItemPurchased == 2 && PlayerDodging)
            { 
                Dodge = Dodge + 1;
            }

            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Dodge!");

            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"{Dodger} rolled a ... {Dodge}");

            Console.SetCursorPosition(2, 6);
            Console.WriteLine($"A bonus of {Dodge} will be added to {Dodger}'s next dodge attempt");

            Program.PressAKey();

            return Dodge;
        }
        public int UseMagic(string WhoseMagic, int AttackingMagicValue, int DodgingMagicValue, int DodgeBonus, int ItemPurchased = 0, bool PlayerUsingMagic = false, bool PlayerDodging = false)
        {
            string Magic = WhoseMagic;
            int AttackersMagic = Random.Next(1, AttackingMagicValue + 1);
            int DodgerMagic = Random.Next(1, DodgingMagicValue + 1) + DodgeBonus;
            int MagicDamage;

            if (ItemPurchased == 3 && PlayerDodging)
            {
                DodgerMagic = DodgerMagic + 1;
            }
            else if (ItemPurchased == 3 && PlayerUsingMagic)
            {
                AttackersMagic = AttackersMagic + 1;
            }

            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Magic!");

            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"{Magic} rolled a ... {AttackersMagic}");

            Console.SetCursorPosition(2, 6);
            Console.WriteLine($"Defender rolled a ... {DodgerMagic}");


            if (DodgeBonus > 0)
            {
                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"Defender rolled a ... {DodgerMagic}, this includes the {DodgeBonus} bonus.");
            }

            if (AttackersMagic > DodgerMagic)
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"{Magic}'s magic missle is successful!");
                MagicDamage = AttackersMagic - DodgerMagic;
                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"{Magic} hits for {MagicDamage}");
            }
            else if (AttackersMagic < DodgerMagic)
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"{Magic}'s magic missle missed!");
                MagicDamage = 0;
            }
            else
            {
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"WOW! That was close! {Magic}'s magic missle barely missed.");
                MagicDamage = 0;
            }

            Program.PressAKey();

            return MagicDamage;
        }
        public int RandomEvent(int RoundNumber, string PlayerType, string EnemyType, int PlayerMagic, int PlayerDodge, int PlayerDodgeBonus, int EnemyDodge, int EnemyMagic, int EnemyDodgeBonus, bool PlayerDodging, bool PlayerUsingMagic, int ItemPurchased = 0)
        {
            int PossibleEvent = Random.Next(1,101);
            int PlayerRandomEventDodge = Random.Next(1, PlayerDodge + 1) + PlayerDodgeBonus;
            int EnemyRandomEventDodge = Random.Next(1, EnemyDodge + 1) + EnemyDodgeBonus;
            int PlayerRandomEventMagic = Random.Next(1, PlayerMagic + 1) + PlayerDodgeBonus; 
            int EnemyRandomEventMagic = Random.Next(1, EnemyMagic + 1) + EnemyDodgeBonus;

            if (ItemPurchased == 2 && PlayerDodging)
            {
                PlayerRandomEventDodge = PlayerRandomEventDodge + 1;
            }
            else if (ItemPurchased == 3 && PlayerUsingMagic)
            {
                PlayerRandomEventMagic = PlayerRandomEventMagic + 1;
            }

            RandomEventModifier = 0;
            PlayerIsEffectedByRandomEvent = false;
            EnemyIsEffectedByRandomEvent = false;
            RandomEventOccured = false;

            RandomEventColors();

            if (RoundNumber <= 10)
            {
                if (PossibleEvent > 90 && PossibleEvent <= 95)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A bored spectator starts throwing food at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the thrown food!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the thrown food!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge < 2)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets pummeled with food. You lose 1HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 1;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the food.");
                    }
                    if (EnemyRandomEventDodge < 2)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets pummeled with food. They lose 1HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 1;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the food.");
                    }

                    Program.PressAKey();
                    Console.SetCursorPosition(2, 18);
                    Console.WriteLine("Security escorts the spectator out of the arena.");
                    Program.PressAKey();

                }
                else if (PossibleEvent >= 96 && PossibleEvent <= 100)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A mystery portal opens above the arena floor and rocks are falling out of it.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the falling rocks!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the falling rocks!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge < 2)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets pummeled by the rocks. You lose 1HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 1;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the rocks.");
                    }
                    if (EnemyRandomEventDodge < 2)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets pummeled by the rocks. They lose 1HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 1;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the rocks.");
                    }

                    if (RandomEventModifier == 1)
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("After what seems like a hundred rocks fall out, you hear an evil laught as the portal closes.");
                    }
                    else
                    { 
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("After what seems like a hundred rocks fall out, the portal closes.");
                        Program.PressAKey();
                    }
                }
            }
            else if (RoundNumber > 10 && RoundNumber <= 30)
            {
                if (PossibleEvent > 85 && PossibleEvent <= 90)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A bear breaks out of its cage and into the arena. It charges the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the bear!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the bear!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets mauled by the bear. You lose 2HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the bear.");
                    }
                    if (EnemyRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets mauled by the bear. They lose 2HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the bear.");
                    }

                    Program.PressAKey();
                    Console.SetCursorPosition(2, 18);
                    Console.WriteLine("The bear gets subdued by security and put back in its cage.");
                    Program.PressAKey();
                }
                else if (PossibleEvent > 90 && PossibleEvent <= 95)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A mage waiting to fight gets bored and starts throwing magic missles at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to use magic to disperse the missles coming at them!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to use magic to disperse the missles coming at them!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventMagic} for their magic use.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventMagic} for their magic use.");

                    if (PlayerRandomEventMagic <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets hit by one of the magic missles. You lose 2HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} defuses the magic missles headed towards them.");
                    }
                    if (EnemyRandomEventMagic <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets hit by one of the magic missles. They lose 2HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} defuses the magic missles headed towards them.");
                    }

                    Program.PressAKey();
                    Console.SetCursorPosition(2, 18);
                    Console.WriteLine("The mage is arrested by security.");
                    Program.PressAKey();
                }
                else if (PossibleEvent > 95 && PossibleEvent <= 100)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A mystery portal opens above the arena floor and bats come pouring out.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the swarm of bats!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the swarm of bats!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets swarmed by the bats. They bite and claw at you. You lose 2HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the swarm of bats.");
                    }
                    if (EnemyRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets swarmed by the bats. They bite and claw at them. They lose 2HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 2;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the swarm of bats.");
                    }

                    if (RandomEventModifier == 2)
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear an evil laugh as the portal closes and the swarm of bats fly off out of the arena.");
                        Program.PressAKey();
                    }
                    else
                    { 
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("The portal closes and the swarm of bats fly off out of the arena.");
                        Program.PressAKey();
                    }
                }

            }
            else if (RoundNumber > 30 && RoundNumber <= 50)
            {
                if (PossibleEvent > 80 && PossibleEvent <= 85)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A bored spectator starts throwing knives at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the knives thrown at them!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the knives thrown at them!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets sliced up by the knives thrown at them. You lose 3HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the knives thrown at them.");
                    }
                    if (EnemyRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets sliced up by the knives thrown at them. They lose 3HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the knives thrown at them.");
                    }

                    if (RandomEventModifier == 3)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear the spectator laugh as security hauls them away.");
                        Program.PressAKey();
                    }
                    else
                    { 
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("Security arrests the spectator.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 85 && PossibleEvent <= 90)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A leprechaun angry for being ripped off by the arena owner, shoots a rainbow of decay at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to use magic to disperse the rainbow around them.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to use magic to disperse the rainbow around them.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventMagic} for their magic use.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventMagic} for their magic use.");

                    if (PlayerRandomEventMagic <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets struck by the rainbow of decay. You lose 3HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} uses magic to disperse the rainbow around them.");
                    }
                    if (EnemyRandomEventMagic <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets struck by the rainbow of decay. They lose 3HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} uses magic to disperse the rainbow around them.");
                    }

                    if (RandomEventModifier == 3)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear the leprechaun laugh hartily. And as the rainbow disperses you see security hauling him off. ");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("As the rainbow disperses, you see security hauling off the leprechaun.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 90 && PossibleEvent <= 95)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("An angry cockatrice breaks out of its cage and runs at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the cockatrice.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the cockatrice.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets clawed and pecked by the cockatrice. You lose 3HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the cockatrice as it flurries by.");
                    }
                    if (EnemyRandomEventDodge <= 3)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets clawed and pecked by the cockatrice. They lose 3HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 3;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the cockatrice as it flurries by.");
                    }

                    Program.PressAKey();
                    Console.SetCursorPosition(2, 18);
                    Console.WriteLine("You watch as the cockatrice is subdued by security.");
                    Program.PressAKey();
                }
                else if (PossibleEvent > 95 && PossibleEvent <= 100)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A mystery portal opens above the arena floor and energy bolts come flying out.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to use magic to disperse the energy bolts flying at them!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to use magic to disperse the energy bolts flying at them!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventMagic} for their magic use.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventMagic} for their magic use.");

                    if (PlayerRandomEventMagic <= 4)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets hit by an energy bolt. You lose 4HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dispurses the energy bolts flying at them.");
                        PlayerIsEffectedByRandomEvent = false;
                    }
                    if (EnemyRandomEventMagic <= 4)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets hit by an energy bolt. They lose 4HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dispurses the energy bolts flying at them.");
                        EnemyIsEffectedByRandomEvent = false;
                    }

                    if (RandomEventModifier == 4)
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear an evil laugh as the portal closes and the last of the energy bolts hit the arena floor.");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("The portal closes and the last of the energy bolts hit the arena floor.");
                        Program.PressAKey();
                    }
                }
            }
            else
            {
                if (PossibleEvent > 70 && PossibleEvent <= 75)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("Part of the arena wall collapses and threatens to fall on the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the falling wall.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the falling wall.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 4)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} part of the wall falls on you. You lose 4HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges out of the way of the falling wall.");
                    }
                    if (EnemyRandomEventDodge <= 4)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} part of the wall falls on you. They lose 4HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges dodges out of the way of the falling wall.");
                    }

                    Program.PressAKey();
                    Console.SetCursorPosition(2, 18);
                    Console.WriteLine("The arena workers clear the rubble and add supports to prop up the faulty wall.");
                    Program.PressAKey();

                }
                else if (PossibleEvent > 75 && PossibleEvent <= 80)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A fight break out in the stands between two wizards and some bolts of lightning fly at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to use magic to disperse the lightning head at them.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to use magic to disperse the lightning head at them.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventMagic} for their magic use.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventMagic} for their magic use.");

                    if (PlayerRandomEventMagic <= 4)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets struck by one of the lightning bolts. You lose 4HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} uses magic to disperse the lightning flying at them.");
                        PlayerIsEffectedByRandomEvent = false;
                    }
                    if (EnemyRandomEventMagic <= 4)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets struck by one of the lightning bolts. They lose 4HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} uses magic to disperse the lightning flying at them.");
                        EnemyIsEffectedByRandomEvent = false;
                    }

                    if (RandomEventModifier == 4)
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("Both wizards laugh as security hauls them away.");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("Security takes away one wizard as the arena workers haul away the body of the other.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 80 && PossibleEvent <= 85)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("The arena workers rebel and flood the arena floor!");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the mob of workers.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the mob of workers.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 4)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets mobbed by the workers as they angrily swarm the arena floor. You lose 4HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the mob of workers as they swarm the arena floor.");
                    }
                    if (EnemyRandomEventDodge <= 4)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets mobbed by the workers as they angrily swarm the arena floor. They lose 4HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 4;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the mob of workers as they swarm the arena floor.");
                    }

                    if (RandomEventModifier == 4)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("Several workers shout with joy as the mob breaks through one of the arena doors and out to freedom.");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("The workers disperse after several of them are killed by the security force.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 85 && PossibleEvent <= 90)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("An enraged giant breaks out of its cell and rushes into the arena. Swinging at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the giants swing.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the giants swing.");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 5)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets struck by the giant and goes flying. You lose 5HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 5;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the giants swing.");
                    }
                    if (EnemyRandomEventDodge <= 5)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets struck by the giant and goes flying. They lose 5HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 5;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the giants swing.");
                    }

                    if (RandomEventModifier == 5)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("The giant roars and laughs with glee as it crashes through one of the arena doors.");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("Security subdues the giant and places it in a new cell.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 90 && PossibleEvent <= 95)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("The arena owner gets bored and starts shooting arrows at the fighters.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to dodge the arrows shot at them!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to dodge the arrows shot at them!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventDodge} for their dodge.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventDodge} for their dodge.");

                    if (PlayerRandomEventDodge <= 5)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets struck by an arrow. You lose 5HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 5;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dodges the arrows shot at them.");
                    }
                    if (EnemyRandomEventDodge <= 5)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets struck by an arrow. They lose 5HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 5;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dodges the arrows shot at them.");
                    }

                    if (RandomEventModifier == 5)
                    {
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear a laugh from the arena owner as the crowd cheers.");
                        Program.PressAKey();
                    }
                    else
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear the arena owner bock in dismay as the crowd laughs at him.");
                        Program.PressAKey();
                    }
                }
                else if (PossibleEvent > 95 && PossibleEvent <= 100)
                {
                    RandomEventOccured = true;
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("A Random Event Occurs!");
                    Console.SetCursorPosition(2, 4);
                    Console.WriteLine("A mystery portal opens above the arena floor and a fireball comes flying out.");
                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine($"Your {PlayerType} attempts to use magic to disperse the fireball around them!");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine($"{EnemyType} attempts to use magic to disperse the fireball around them!");
                    Program.PressAKey();
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine($"Your {PlayerType} rolls a {PlayerRandomEventMagic} for their magic use.");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine($"{EnemyType} rolls a {EnemyRandomEventMagic} for their magic use.");

                    if (PlayerRandomEventMagic <= 6)
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} gets burnt by the fireball. You lose 6HP.");
                        PlayerIsEffectedByRandomEvent = true;
                        RandomEventModifier = 6;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 12);
                        Console.WriteLine($"Your {PlayerType} dispurses the fireball around them.");
                        PlayerIsEffectedByRandomEvent = false;
                    }
                    if (EnemyRandomEventMagic <= 6)
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} gets burnt by the fireball. They lose 6HP.");
                        EnemyIsEffectedByRandomEvent = true;
                        RandomEventModifier = 6;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, 16);
                        Console.WriteLine($"{EnemyType} dispurses the fireball around them.");
                        EnemyIsEffectedByRandomEvent = false;
                    }

                    if (RandomEventModifier == 6)
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("You hear an evil laugh as the portal closes and the fireball hits the arena floor.");
                        Program.PressAKey();
                    }
                    else 
                    {
                        Program.PressAKey();
                        Console.SetCursorPosition(2, 18);
                        Console.WriteLine("The portal closes and the fireball hits the arena floor.");
                        Program.PressAKey();
                    }
                }
            }

            return RandomEventModifier;
        }
        public void FlavorTextForAttacks(string WhosTalking, string PlayerType, int DamageDone = 0, int MissedBy = 0, bool PlayerAttacking = false, bool PlayerDodging = false)
        {
            if (PlayerDodging)
            {
                WhosTalking = PlayerType; 
            }

            if (WhosTalking == "Warrior" && PlayerAttacking)
            {
                if (DamageDone >= 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your warrior laughs at their opponent and says:");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("Don't worry this will all be over soon.");
                }
                else if (DamageDone > 5 && DamageDone < 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your warrior mockingly says, ");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("You won't last much longer.");
                }
                else if (DamageDone >= 3 && DamageDone <= 5)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your warrior calls out");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("Hah! That was a good hit don't you think.");
                }
            }
            else if (WhosTalking == "Warrior" && PlayerDodging)
            {
                if (MissedBy >= 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your warrior mockingly yawns at their opponenet, ");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("I think I just felt the wind pick up, did you feel that breeze?");
                }
                else if (MissedBy > 5 && MissedBy < 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your warrior laughs at their opponenet,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Were you aiming for me?! How cute.");
                }
                else if (MissedBy >= 3 && MissedBy <= 5)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your warrior says to their opponenet,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("You're going have to do better then that.");
                }
            }

            if (WhosTalking == "Mage" && PlayerAttacking)
            {
                if (DamageDone >= 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your mage sneers at his opponent and says:");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("And to think I'm not even using my magic.");
                }
                else if (DamageDone > 5 && DamageDone < 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your mage jeers his opponent, ");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("I call that one my foot of ass-kickery");
                }
                else if (DamageDone >= 3 && DamageDone <= 5)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your mage says to his opponent, ");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("How do you like an imbued fist to the face?!");
                }
            }
            else if (WhosTalking == "Mage" && PlayerDodging)
            {
                if (MissedBy >= 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("The blow passes through your mage who fades in and out then says, ");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Wow! That tickled a little.");
                }
                else if (MissedBy > 5 && MissedBy < 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your mage mockingly laughs at their opponenet,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Are you actually trying to hit me?");
                }
                else if (MissedBy >= 3 && MissedBy <= 5)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your mage says to their opponenet,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("I've studied the best defensive books there are. That'll never work.");
                }
            }

            if (WhosTalking == "Rogue" && PlayerAttacking)
            {
                if (DamageDone >= 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your rogue whispers into his opponents ear from behind, ");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("My blade will sever your neck soon");
                }
                else if (DamageDone > 5 && DamageDone < 10)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your rogue mockingly says, ");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("If I knew you'd go down this easy, I would've put more money on this fight.");
                }
                else if (DamageDone >= 3 && DamageDone <= 5)
                {
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your rogue laughs and says");
                    Console.SetCursorPosition(2, 14);
                    Console.WriteLine("This is too easy!");
                }
            }
            else if (WhosTalking == "Rogue" && PlayerDodging)
            {
                if (MissedBy >= 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your rogue sneers from behind their opponent, ");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("Your never going to hit a shadow like me.");
                }
                else if (MissedBy > 5 && MissedBy < 10)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your rogue moves daftly out of the way and says,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("I barely have to move to dodge the likes of you.");
                }
                else if (MissedBy >= 3 && MissedBy <= 5)
                {
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("Your rogue says to their opponenet,");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("You swing like a warrior brute I know. Slow and clumsy.");
                }
            }

                // "Beast", "Warlock", "Assassin", "Gladiator", "Bard", "Monk", "Hero", "Samurai", "Ninja", "Wizard", "Paladin", "Blacksmith", "Hobo", "Knight", "Ranger", "Barbarian", "Jester", "Thief", "Tanner", "Adventurer"
        }
        public void FlavorTextForDodges(string WhosTalking, int Dodge, bool PlayerDodging = false)
        {
            WhosTalking = WhosTalking.ToLower();


            // "Beast", "Warlock", "Assassin", "Gladiator", "Bard", "Monk", "Hero", "Samurai", "Ninja", "Wizard", "Paladin", "Blacksmith", "Hobo", "Knight", "Ranger", "Barbarian", "Jester", "Thief", "Tanner", "Adventurer"
        }
        public void FlavorTextForMagic()
        {

            // "Beast", "Warlock", "Assassin", "Gladiator", "Bard", "Monk", "Hero", "Samurai", "Ninja", "Wizard", "Paladin", "Blacksmith", "Hobo", "Knight", "Ranger", "Barbarian", "Jester", "Thief", "Tanner", "Adventurer"
        }
        public void PlayersTurnColors(string PlayerType)
        {
            if (PlayerType == "Warrior")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (PlayerType == "Mage")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (PlayerType == "Rogue")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (PlayerType == "Jester")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (PlayerType == "Beast")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (PlayerType == "Warlock")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (PlayerType == "Assassin")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Gladiator")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Bard")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Monk")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (PlayerType == "Hero")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (PlayerType == "Samurai")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Ninja")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (PlayerType == "Wizard")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Paladin")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (PlayerType == "Blacksmith")
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (PlayerType == "Hobo")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (PlayerType == "Knight")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (PlayerType == "Ranger")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (PlayerType == "Barbarian")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (PlayerType == "Thief")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (PlayerType == "Tanner")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (PlayerType == "Adventurer")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
        }
        public void EnemysTurnColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public void RandomEventColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
    }
}
