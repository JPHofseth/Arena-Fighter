using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class Enemy
    {
        protected Random Random = new Random();
        protected int EnemyAttackValue;
        protected int EnemyDodgeValue;
        protected int EnemyMagicValue;
        protected int EnemyHP;
        protected string EnemyName;
        protected int[] EnemyValues = {4, 6, 8, 10};
        protected string[] EnemyType = {"Warrior", "Rogue", "Mage", "Brute", "Butcher", "Bard", "Violator", "Miscreant", "Demon", "Felon", "Inmate", "Evil One", "Killer", "Defiler", "Rebel", "Grade Destroyer"};
        protected string[] EnemyCommonName = {"Andrew", "Brian", "Blue", "Bob", "Blake", "Justin", "Richard", "Shawn", "Will", "Journey"};
        public Enemy()
        {
            int AttackValue = EnemyValues[Random.Next(EnemyValues.Length)];
            EnemyAttackValue = AttackValue;
            int DodgeValue = EnemyValues[Random.Next(EnemyValues.Length)];
            EnemyDodgeValue = DodgeValue;
            int MagicValue = EnemyValues[Random.Next(EnemyValues.Length)];
            EnemyMagicValue = MagicValue;
            string Type = EnemyType[Random.Next(EnemyType.Length)];
            string CommonName = EnemyCommonName[Random.Next(EnemyCommonName.Length)];
            EnemyName = CommonName + " the " + Type;

            while (AttackValue == 10 && DodgeValue == 10 && MagicValue == 10 || AttackValue == 10 && DodgeValue == 10 || DodgeValue == 10 && MagicValue == 10 || AttackValue == 10 && MagicValue == 10 || AttackValue == 10 && DodgeValue == 8 && MagicValue == 8 || AttackValue == 8 && DodgeValue == 10 && MagicValue == 8 || AttackValue == 8 && DodgeValue == 8 && MagicValue == 10 || AttackValue == 8 && DodgeValue == 8 && MagicValue == 8 || AttackValue == 4 && DodgeValue == 4 && MagicValue == 4 || AttackValue == 4 && DodgeValue == 4 || DodgeValue == 4 && MagicValue == 4 || AttackValue == 4 && MagicValue == 4)
            {
                AttackValue = EnemyValues[Random.Next(EnemyValues.Length)];
                EnemyAttackValue = AttackValue;
                DodgeValue = EnemyValues[Random.Next(EnemyValues.Length)];
                EnemyDodgeValue = DodgeValue;
                MagicValue = EnemyValues[Random.Next(EnemyValues.Length)];
                EnemyMagicValue = MagicValue;
            }

            EnemyHP = 20;
        }
        public int GetEnemyHP()
        {
            return EnemyHP;
        }
        public int GetEnemyAttackValue()
        {
            return EnemyAttackValue;
        }
        public int GetEnemyDodgeValue()
        {
            return EnemyDodgeValue;
        }
        public int GetEnemyMagicValue()
        {
            return EnemyMagicValue;
        }
        public string GetEnemyType()
        {
            return EnemyName;
        }
        public void DisplayEnemyStats()
        {
            
            Console.SetCursorPosition(2, 10);
            Console.WriteLine($"You will fight {EnemyName}");
            Console.SetCursorPosition(2, 11);
            Console.WriteLine($"For attacks they will roll 1d{EnemyAttackValue}");
            Console.SetCursorPosition(2, 12);
            Console.WriteLine($"For dodging they will roll 1d{EnemyDodgeValue}");
            Console.SetCursorPosition(2, 13);
            Console.WriteLine($"For magic they will roll 1d{EnemyMagicValue}");
            Console.SetCursorPosition(2, 14);
            Console.WriteLine($"They can sustain {EnemyHP} points of damage");

            Program.PressAKey();
        }
        public void DrawEnemy()
        {
            ConsoleColor OldForeColor = Console.ForegroundColor;
            ConsoleColor OldBackColor = Console.BackgroundColor;

            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.SetCursorPosition(32, 13);
            //Console.WriteLine(" ");
            //Console.SetCursorPosition(29, 14);
            //Console.WriteLine("     ");
            //Console.SetCursorPosition(29, 15);
            //Console.WriteLine("     ");
            //Console.SetCursorPosition(30, 14);
            //Console.WriteLine("o o");
            //Console.SetCursorPosition(30, 15);
            //Console.WriteLine(" = ");

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(32, 13);
            Console.WriteLine("      ");
            Console.SetCursorPosition(32, 14);
            Console.WriteLine("      ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(32, 15);
            Console.WriteLine("     ");
            Console.SetCursorPosition(32, 14);
            Console.WriteLine(" o o ");
            Console.SetCursorPosition(32, 15);
            Console.WriteLine("  =   ");
            Console.SetCursorPosition(33, 16);
            Console.WriteLine("   ");
            Console.SetCursorPosition(32, 17);
            Console.WriteLine("      ");
            Console.SetCursorPosition(32, 18);
            Console.WriteLine(" |  | ");
            Console.SetCursorPosition(32, 19);
            Console.WriteLine(" |  | ");
            Console.SetCursorPosition(33, 20);
            Console.WriteLine(")  (");
            Console.SetCursorPosition(33, 21);
            Console.WriteLine(" ");
            Console.SetCursorPosition(33, 22);
            Console.WriteLine(" ");
            Console.SetCursorPosition(32, 23);
            Console.WriteLine("  ");
            Console.SetCursorPosition(36, 21);
            Console.WriteLine(" ");
            Console.SetCursorPosition(36, 22);
            Console.WriteLine(" ");
            Console.SetCursorPosition(36, 23);
            Console.WriteLine("  ");

            Console.ForegroundColor = OldForeColor;
            Console.BackgroundColor = OldBackColor;
        }
    }
}
