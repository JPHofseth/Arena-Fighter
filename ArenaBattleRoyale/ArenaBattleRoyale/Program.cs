using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool GameOn = true;
            Items Items = new Items();
            Player Player = new Player();
            Enemy Enemy = new Enemy();
            string ItemName = "";
            int WhichItem = 0;
            int OwnersMoney = 1;

            TitleScreen();

            while (GameOn)
            {
                bool ValidPick = false;
                //bool PurchaseItems;
                OwnersMoney = 1; //Items.GetOwnersMoney();

                while (!ValidPick)
                {
                    Console.Clear();
                    Console.SetCursorPosition(2, 2);
                    Console.WriteLine("It's a great day to be at the arena!");

                    Console.SetCursorPosition(2, 6);
                    Console.WriteLine("1. Enter the arena, and choose a fighter.");
                    Console.SetCursorPosition(2, 8);
                    Console.WriteLine("2. Enter the store, maybe an item can help your fighter.");
                    Console.SetCursorPosition(2, 10);
                    Console.WriteLine("3. See the rules of the arena.");
                    Console.SetCursorPosition(2, 12);
                    Console.WriteLine("4. Leave the arena, maybe fight another day.");
                    Console.SetCursorPosition(2, 4);
                    Console.Write("What would you like to do: ");
                    string UserPick = Console.ReadLine().ToLower();

                    switch (UserPick)
                    {
                        case "1":
                            ValidPick = true;
                            Player.CreatePlayer();
                            Player.DisplayFighterStats();
                            Enemy.DisplayEnemyStats();

                            Battle Battle = new Battle(Player, Enemy, ItemName, WhichItem);
                            Items.SetIfItemWasPurchased(false);
                            Items.SetOwnersMoney(1);
                            break;
                        case "2":
                            ValidPick = true;
                            //PurchaseItems = Items.GetIfItemWasPurchased();
                        
                            //if (!PurchaseItems)
                            //{
                                Items.CreateItems();
                                Items.ItemsStore(OwnersMoney);
                                WhichItem = Items.WhatItemWasPurchased();

                                if (WhichItem == 1)
                                {
                                    ItemName = Items.GetAttackItem();
                                }
                                else if (WhichItem == 2)
                                {
                                    ItemName = Items.GetDodgeItem();
                                }
                                else if (WhichItem == 3)
                                {
                                    ItemName = Items.GetMagicItem();
                                }
                            //}
                            //else if (PurchaseItems)
                            //{ 
                            //    Items.ItemsStoreClosed();
                            //}
                            break;
                        case "3":
                            Rules();
                            break;
                        case "4":
                            ValidPick = true;
                            GameOn = false;
                            EndingScreen();
                            break;
                    }
                }
            }
        }

        public static void PressAKey()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Press a key to continue");
            Console.ReadKey();
        }

        public static void TitleScreen()
        {
            Console.Title = "Arena Fighter";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("Welcome to Arena Fighter");
            Console.SetCursorPosition(50, 11);
            Console.WriteLine("by JP Hofseth");
            PressAKey();
            Console.Clear();
        }

        public static void EndingScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(42, 10);
            Console.WriteLine("Thank you for playing Arena Fighter");
            PressAKey();
        }

        public static void Rules()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 1);
            Console.WriteLine("Actions:");
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("In Arena Fighter you will use 3 basic actions:");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("1. Attack - this will pit your attack die vs your oppent's ");
            Console.SetCursorPosition(2, 5);
            Console.WriteLine("dodge die and any bonus they may have from dodging a"); 
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("previous round.");
            Console.SetCursorPosition(2, 7);
            Console.WriteLine("2. Dodge - this will allow you to roll your dodge die, and");
            Console.SetCursorPosition(2, 8);
            Console.WriteLine("add the result against your oppent's next attack or magic. ");
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("Once you attack or use magic this bonus goes away. If you ");
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("dodge again, you will replace your last dodge bonus with the");
            Console.SetCursorPosition(2, 11);
            Console.WriteLine("new one.");
            Console.SetCursorPosition(2, 12);
            Console.WriteLine("3. Magic - this will pit your magic die vs your oppent's");
            Console.SetCursorPosition(2, 13);
            Console.WriteLine("magic die and any bonus they may have from dodging a");
            Console.SetCursorPosition(2, 14);
            Console.WriteLine("previous round.");
            Console.SetCursorPosition(2, 16);
            Console.WriteLine("Fighters:");
            Console.SetCursorPosition(2, 18);
            Console.WriteLine("Fighters 1 - 3 are the default and will be the same each game.");
            Console.SetCursorPosition(2, 19);
            Console.WriteLine("Fighters 4 - 6 are randomized every game.");
            Console.SetCursorPosition(2, 20);
            Console.WriteLine("Your opponent is randomized every game as well.");
            Console.SetCursorPosition(2, 22);
            Console.WriteLine("Goal: ");
            Console.SetCursorPosition(2, 24);
            Console.WriteLine("Each player starts with 20 HP. The rounds will continue until");
            Console.SetCursorPosition(2, 25);
            Console.WriteLine("one player is reduced to 0 HP.");
            PressAKey();
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Random Events:");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("Between each round there is a possibility of a random event");
            Console.SetCursorPosition(2, 5);
            Console.WriteLine("occuring. During these events you and your opponent will use");
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("either your dodge or your magic to avoid them. Any dodge");
            Console.SetCursorPosition(2, 7);
            Console.WriteLine("modifier will be added in to help. However, this will mean");
            Console.SetCursorPosition(2, 8);
            Console.WriteLine("that your modifier will be used for this event and set back");
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("to 0 after the event. These events can cost either player a");
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("significant amount of HP possibly turning the tide in the");
            Console.SetCursorPosition(2, 11);
            Console.WriteLine("fight or even ending it!");
            Console.SetCursorPosition(2, 13);
            Console.WriteLine("Note:");
            Console.SetCursorPosition(2, 15);
            Console.WriteLine("a) If at anytime you wish to end a fight before its over, ");
            Console.SetCursorPosition(2, 16);
            Console.WriteLine("type quit, stop, or end as your selction for your turn and");
            Console.SetCursorPosition(2, 17);
            Console.WriteLine("the fight will end returning you to the main menu.");
            Console.SetCursorPosition(2, 18);
            Console.WriteLine("b) Each round is distinguished by color to help identify ");
            Console.SetCursorPosition(2, 19);
            Console.WriteLine("who's turn it is. Each fighter has a unique color of their ");
            Console.SetCursorPosition(2, 20);
            Console.WriteLine("own. All the enemies will all have the same color. The random");
            Console.SetCursorPosition(2, 21);
            Console.WriteLine("events all have the same color as each other as well. These");
            Console.SetCursorPosition(2, 22);
            Console.WriteLine("are different from the enemies and the fighters' colors to");
            Console.SetCursorPosition(2, 23);
            Console.WriteLine("help set them apart. ");
            Console.SetCursorPosition(2, 25);
            Console.WriteLine("Good Luck!");
            PressAKey();
            Console.Clear();
        }
    }
}
