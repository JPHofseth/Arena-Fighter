using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class Player
    {
        protected RandomPlayer RandomPlayer = new RandomPlayer();
        protected int PlayerAttackValue;
        protected int PlayerDodgeValue;
        protected int PlayerMagicValue;
        protected int PlayerHP;
        protected string PlayerSelected;
        public Player()
        {
        }
        public Player CreatePlayer()
        {
            Player Player = new Player();
            bool ValidPick = false;

            RandomPlayer.CreateRandomPlayer();
            int RandomPlayer1AttackValue = RandomPlayer.GetRandomPlayerAttackValue();
            int RandomPlayer1DodgeValue = RandomPlayer.GetRandomPlayerDodgeValue();
            int RandomPlayer1MagicValue = RandomPlayer.GetRandomPlayerMagicValue();
            int RandomPlayer1HP = RandomPlayer.GetRandomPlayerHP();
            string RandomPlayer1Name = RandomPlayer.GetRandomPlayerName();

            RandomPlayer.CreateRandomPlayer();
            int RandomPlayer2AttackValue = RandomPlayer.GetRandomPlayerAttackValue();
            int RandomPlayer2DodgeValue = RandomPlayer.GetRandomPlayerDodgeValue();
            int RandomPlayer2MagicValue = RandomPlayer.GetRandomPlayerMagicValue();
            int RandomPlayer2HP = RandomPlayer.GetRandomPlayerHP();
            string RandomPlayer2Name = RandomPlayer.GetRandomPlayerName();

            RandomPlayer.CreateRandomPlayer();
            int RandomPlayer3AttackValue = RandomPlayer.GetRandomPlayerAttackValue();
            int RandomPlayer3DodgeValue = RandomPlayer.GetRandomPlayerDodgeValue();
            int RandomPlayer3MagicValue = RandomPlayer.GetRandomPlayerMagicValue();
            int RandomPlayer3HP = RandomPlayer.GetRandomPlayerHP();
            string RandomPlayer3Name = RandomPlayer.GetRandomPlayerName();

            while (!ValidPick)
            {
                Console.Clear();
                Console.SetCursorPosition(2, 1);
                Console.Write("Whose in the arena today...");

                Console.SetCursorPosition(2, 3);
                Console.WriteLine("1. Warrior");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine("   Attack 1d10");
                Console.SetCursorPosition(2, 5);
                Console.WriteLine("   Dodge 1d6");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine("   Magic 1d4");
                Console.SetCursorPosition(2, 7);
                Console.WriteLine("   Hit Points = 20");
                Console.SetCursorPosition(25, 3);
                Console.WriteLine("2. Mage");
                Console.SetCursorPosition(25, 4);
                Console.WriteLine("   Attack 1d4");
                Console.SetCursorPosition(25, 5);
                Console.WriteLine("   Dodge 1d6");
                Console.SetCursorPosition(25, 6);
                Console.WriteLine("   Magic 1d10");
                Console.SetCursorPosition(25, 7);
                Console.WriteLine($"   Hit Points = 20");
                Console.SetCursorPosition(47, 3);
                Console.WriteLine("3. Rogue");
                Console.SetCursorPosition(47, 4);
                Console.WriteLine("   Attack 1d6");
                Console.SetCursorPosition(47, 5);
                Console.WriteLine("   Dodge 1d8");
                Console.SetCursorPosition(47, 6);
                Console.WriteLine("   Magic 1d6");
                Console.SetCursorPosition(47, 7);
                Console.WriteLine("   Hit Points = 20");

                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"4. {RandomPlayer1Name}");
                Console.SetCursorPosition(2, 11);
                Console.WriteLine($"   Attack 1d{RandomPlayer1AttackValue}");
                Console.SetCursorPosition(2, 12);
                Console.WriteLine($"   Dodge 1d{RandomPlayer1DodgeValue}");
                Console.SetCursorPosition(2, 13);
                Console.WriteLine($"   Magic 1d{RandomPlayer1MagicValue}");
                Console.SetCursorPosition(2, 14);
                Console.WriteLine($"   Hit Points = {RandomPlayer1HP}");

                Console.SetCursorPosition(25, 10);
                Console.WriteLine($"5. {RandomPlayer2Name}");
                Console.SetCursorPosition(25, 11);
                Console.WriteLine($"   Attack 1d{RandomPlayer2AttackValue}");
                Console.SetCursorPosition(25, 12);
                Console.WriteLine($"   Dodge 1d{RandomPlayer2DodgeValue}");
                Console.SetCursorPosition(25, 13);
                Console.WriteLine($"   Magic 1d{RandomPlayer2MagicValue}");
                Console.SetCursorPosition(25, 14);
                Console.WriteLine($"   Hit Points = {RandomPlayer2HP}");

                Console.SetCursorPosition(47, 10);
                Console.WriteLine($"6. {RandomPlayer3Name}");
                Console.SetCursorPosition(47, 11);
                Console.WriteLine($"   Attack 1d{RandomPlayer3AttackValue}");
                Console.SetCursorPosition(47, 12);
                Console.WriteLine($"   Dodge 1d{RandomPlayer3DodgeValue}");
                Console.SetCursorPosition(47, 13);
                Console.WriteLine($"   Magic 1d{RandomPlayer3MagicValue}");
                Console.SetCursorPosition(47, 14);
                Console.WriteLine($"   Hit Points = {RandomPlayer3HP}");

                Console.SetCursorPosition(2, 16);
                Console.Write("Choose your fighter: ");
                string UserPick = Console.ReadLine().ToLower();

                switch (UserPick)
                {
                    case "1":
                        Warrior();
                        ValidPick = true;
                        break;
                    case "2":
                        Mage();
                        ValidPick = true;
                        break;
                    case "3":
                        Rogue();
                        ValidPick = true;
                        break;
                    case "4":
                        PlayerAttackValue = RandomPlayer1AttackValue;
                        PlayerDodgeValue = RandomPlayer1DodgeValue;
                        PlayerMagicValue = RandomPlayer1MagicValue;
                        PlayerHP = RandomPlayer1HP;
                        PlayerSelected = RandomPlayer1Name;
                        ValidPick = true;
                        break;
                    case "5":
                        PlayerAttackValue = RandomPlayer2AttackValue;
                        PlayerDodgeValue = RandomPlayer2DodgeValue;
                        PlayerMagicValue = RandomPlayer2MagicValue;
                        PlayerHP = RandomPlayer2HP;
                        PlayerSelected = RandomPlayer2Name;
                        ValidPick = true;
                        break;
                    case "6":
                        PlayerAttackValue = RandomPlayer3AttackValue;
                        PlayerDodgeValue = RandomPlayer3DodgeValue;
                        PlayerMagicValue = RandomPlayer3MagicValue;
                        PlayerHP = RandomPlayer3HP;
                        PlayerSelected = RandomPlayer3Name;
                        ValidPick = true;
                        break;
                }
            }

            return Player;
        }
        public int GetPlayerHP()
        { 
            return PlayerHP;
        }
        public int GetPlayerAttackValue()
        {
            return PlayerAttackValue;
        }
        public int GetPlayerDodgeValue()
        {
            return PlayerDodgeValue;
        }
        public int GetPlayerMagicValue()
        {
            return PlayerMagicValue;
        }
        public string GetPlayerType()
        {
            return PlayerSelected;
        }
        public void DisplayFighterStats()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine($"You have chosen the {PlayerSelected}");
            Console.SetCursorPosition(2, 3);
            Console.WriteLine($"For attacks you will roll 1d{PlayerAttackValue}");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine($"For dodging you will roll 1d{PlayerDodgeValue}");
            Console.SetCursorPosition(2, 5);
            Console.WriteLine($"For magic you will roll 1d{PlayerMagicValue}");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine($"You can sustain {PlayerHP} points of damage");

            Program.PressAKey();
        }
        public void Warrior()
        {
            PlayerAttackValue = 10;
            PlayerDodgeValue = 6;
            PlayerMagicValue = 4;
            PlayerHP = 20;
            PlayerSelected = "Warrior";
        }
        public void Mage()
        {
            PlayerAttackValue = 4;
            PlayerDodgeValue = 6;
            PlayerMagicValue = 10;
            PlayerHP = 20;
            PlayerSelected = "Mage";
        }
        public void Rogue()
        {
            PlayerAttackValue = 6;
            PlayerDodgeValue = 8;
            PlayerMagicValue = 6;
            PlayerHP = 20;
            PlayerSelected = "Rogue";
        }
        public void DrawPlayer()
        {
            ConsoleColor OldForeColor = Console.ForegroundColor;
            ConsoleColor OldBackColor = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(3, 13);
            Console.WriteLine("      ");
            Console.SetCursorPosition(3, 14);
            Console.WriteLine("     ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7, 14);
            Console.WriteLine(" ");
            Console.SetCursorPosition(3, 15);
            Console.WriteLine("     ");
            Console.SetCursorPosition(4, 14);
            Console.WriteLine(" o o ");
            Console.SetCursorPosition(4, 15);
            Console.WriteLine("  =  ");
            Console.SetCursorPosition(4,16);
            Console.WriteLine("   ");
            Console.SetCursorPosition(3, 17);
            Console.WriteLine("      ");
            Console.SetCursorPosition(3, 18);
            Console.WriteLine(" |  | ");
            Console.SetCursorPosition(3, 19);
            Console.WriteLine(" |  | ");
            Console.SetCursorPosition(4, 20);
            Console.WriteLine(")  (");
            Console.SetCursorPosition(4, 21);
            Console.WriteLine(" ");
            Console.SetCursorPosition(4, 22);
            Console.WriteLine(" ");
            Console.SetCursorPosition(3, 23);
            Console.WriteLine("  ");
            Console.SetCursorPosition(7, 21);
            Console.WriteLine(" ");
            Console.SetCursorPosition(7, 22);
            Console.WriteLine(" ");
            Console.SetCursorPosition(7, 23);
            Console.WriteLine("  ");

            Console.ForegroundColor = OldForeColor;
            Console.BackgroundColor = OldBackColor;
        }
    }
}
