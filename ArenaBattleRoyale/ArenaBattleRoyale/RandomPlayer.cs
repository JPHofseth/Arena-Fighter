using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class RandomPlayer
    {
        protected Random Random = new Random();
        protected int[] RandomPlayerValues = {4, 6, 8, 10};
        protected string[] RandomPlayerType = {"Beast", "Warlock", "Assassin", "Gladiator", "Bard", "Monk", "Hero", "Samurai", "Ninja", "Wizard", "Paladin", "Blacksmith", "Hobo", "Knight", "Ranger", "Barbarian", "Jester", "Thief", "Tanner", "Adventurer"};
        protected int RandomPlayerAttackValue;
        protected int RandomPlayerDodgeValue;
        protected int RandomPlayerMagicValue;
        protected int RandomPlayerHP;
        protected string RandomPlayerName;
        public void CreateRandomPlayer()
        {
            RandomPlayerAttackValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];
            RandomPlayerDodgeValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];
            RandomPlayerMagicValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];

            while (RandomPlayerAttackValue == 10 && RandomPlayerDodgeValue == 10 && RandomPlayerMagicValue == 10 || RandomPlayerAttackValue == 10 && RandomPlayerDodgeValue == 10 || RandomPlayerDodgeValue == 10 && RandomPlayerMagicValue == 10 || RandomPlayerAttackValue == 10 && RandomPlayerMagicValue == 10 || RandomPlayerAttackValue == 10 && RandomPlayerDodgeValue == 8 && RandomPlayerMagicValue == 8 || RandomPlayerAttackValue == 8 && RandomPlayerDodgeValue == 10 && RandomPlayerMagicValue == 8 || RandomPlayerAttackValue == 8 && RandomPlayerDodgeValue == 8 && RandomPlayerMagicValue == 10 || RandomPlayerAttackValue == 8 && RandomPlayerDodgeValue == 8 && RandomPlayerMagicValue == 8 || RandomPlayerAttackValue == 4 && RandomPlayerDodgeValue == 4 && RandomPlayerMagicValue == 4 || RandomPlayerAttackValue == 4 && RandomPlayerDodgeValue == 4 || RandomPlayerDodgeValue == 4 && RandomPlayerMagicValue == 4 || RandomPlayerAttackValue == 4 && RandomPlayerMagicValue == 4)
            {
                RandomPlayerAttackValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];
                RandomPlayerDodgeValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];
                RandomPlayerMagicValue = RandomPlayerValues[Random.Next(RandomPlayerValues.Length)];
            }

            RandomPlayerHP = 20;
            RandomPlayerName = RandomPlayerType[Random.Next(RandomPlayerType.Length)];
        }
        public int GetRandomPlayerAttackValue()
        { 
            return RandomPlayerAttackValue;
        }
        public int GetRandomPlayerDodgeValue()
        {
            return RandomPlayerDodgeValue;
        }
        public int GetRandomPlayerMagicValue()
        {
            return RandomPlayerMagicValue;
        }
        public int GetRandomPlayerHP()
        {
            return RandomPlayerHP;
        }
        public string GetRandomPlayerName()
        {
            return RandomPlayerName;
        }
    }
}
