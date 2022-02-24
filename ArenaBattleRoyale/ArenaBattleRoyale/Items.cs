using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaBattleRoyale
{
    internal class Items
    {
        protected Random Random = new Random();
        protected string[] ItemsFirstName = { "Brian's ", "Blue's ", "Bob's ", "Blake's ", "Justin's ", "Richard's ", "Shawn's ", "Will's ", "Journey's " };
        protected string[] AttackItemsMiddleName = { "Sword", "Axe", "Dagger", "Club", "Scimatar", "Hammer", "Bow", "Katana", "Sling", "Knife", "Wrench", "Random Blunt Instrument" };
        protected string[] DodgeItemsMiddleName = { "Boots", "Cloak", "Scarf", "Slippers", "Shoes", "Sandals", "Crocs", "Socks", "Pants", "Trousers","Rabbits Foot" };
        protected string[] MagicItemsMiddleName = { "Wand", "Stick", "Staff", "Book", "Orb", "Scroll", "Lamp", "Parchment", "Pointy Finger" };
        protected string[] ItemsLastName = { " of the Dead", " of the Heavens", " of Smiting", " of Swiftness", " of Energy", " of the Realm", " of the Gods", " of the Dawn", " of the Night", " of the Lake", " of the Forgotten", " of Haste", " of Tatics" };
        protected string AttackItem;
        protected string DodgeItem;
        protected string MagicItem;
        protected bool ItemPurchased;
        protected bool AttackItemPurchased;
        protected bool DodgeItemPurchased;
        protected bool MagicItemPurchased;
        protected int OwnersMoney;
        public void CreateItems()
        {
            AttackItem = ItemsFirstName[Random.Next(ItemsFirstName.Length)] + AttackItemsMiddleName[Random.Next(AttackItemsMiddleName.Length)] + ItemsLastName[Random.Next(ItemsLastName.Length)];
            DodgeItem = ItemsFirstName[Random.Next(ItemsFirstName.Length)] + DodgeItemsMiddleName[Random.Next(DodgeItemsMiddleName.Length)] + ItemsLastName[Random.Next(ItemsLastName.Length)];
            MagicItem = ItemsFirstName[Random.Next(ItemsFirstName.Length)] + MagicItemsMiddleName[Random.Next(MagicItemsMiddleName.Length)] + ItemsLastName[Random.Next(ItemsLastName.Length)];
        }
        public string GetAttackItem()
        {
            return AttackItem;
        }
        public string GetDodgeItem()
        { 
            return DodgeItem;
        }
        public string GetMagicItem()
        { 
            return MagicItem;
        }
        public int GetOwnersMoney()
        { 
            return OwnersMoney;
        }
        public void SetOwnersMoney(int MoneyEarned = 1)
        {
            OwnersMoney = OwnersMoney + MoneyEarned;
        }
        public bool GetIfItemWasPurchased()
        { 
            return ItemPurchased;
        }
        public void SetIfItemWasPurchased(bool ToSet)
        {
            ItemPurchased = ToSet;
        }
        public int WhatItemWasPurchased()
        {
            if (ItemPurchased)
            {
                if (AttackItemPurchased)
                {
                    return 1;
                }
                else if (DodgeItemPurchased)
                {
                    return 2;
                }
                else if (MagicItemPurchased)
                {
                    return 3;
                }
            }

            return 0;
        }
        public void ItemsStore(int OwnersMoney = 1)
        {
            bool ValidChoice = false;

            while (!ValidChoice)
            {
                Console.Clear();
                Console.SetCursorPosition(2, 2);
                Console.WriteLine("Welcome to the store.");
                Console.SetCursorPosition(2, 4);
                Console.WriteLine("Let's see what is available today: ");
                Console.SetCursorPosition(2, 6);
                Console.WriteLine($"Attack item: {AttackItem} for 1GP.");
                Console.SetCursorPosition(2, 8);
                Console.WriteLine($"Dodge item: {DodgeItem} for 1GP.");
                Console.SetCursorPosition(2, 10);
                Console.WriteLine($"Magic item: {MagicItem} for 1GP.");
                Console.SetCursorPosition(2, 12);
                Console.WriteLine($"You currently have {OwnersMoney}GP.");

                Console.SetCursorPosition(2, 16);
                Console.WriteLine($"1. Purchase {AttackItem}.");
                Console.SetCursorPosition(2, 18);
                Console.WriteLine($"2. Purchase {DodgeItem}.");
                Console.SetCursorPosition(2, 20);
                Console.WriteLine($"3. Purchase {MagicItem}.");
                Console.SetCursorPosition(2, 22);
                Console.WriteLine("4. On second thought, I don't want to buy anything today.");
                Console.SetCursorPosition(2, 14);
                Console.Write("What would you like to do: ");

                string UsersChoice = Console.ReadLine().ToLower();

                if (OwnersMoney == 0)
                {
                    Console.SetCursorPosition(2, 24);
                    Console.WriteLine($"Sorry, you have no gold. Thank you for window shopping!");
                    Program.PressAKey();
                    break;
                }

                switch (UsersChoice)
                {
                    case "1":
                        ValidChoice = true;
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"You have bought the {AttackItem}, this will increase your fighters attack attempts by + 1.");
                        Console.SetCursorPosition(2, 26);
                        Console.WriteLine("You have spent 1GP.");
                        
                        Program.PressAKey();
                        AttackItemPurchased = true;
                        ItemPurchased = true;
                        break;
                    case "2":
                        ValidChoice = true;
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"You have bought the {DodgeItem}, this will increase your fighters dodge attempts by + 1.");
                        Console.SetCursorPosition(2, 26);
                        Console.WriteLine("You have spent 1GP.");
                        
                        Program.PressAKey();
                        DodgeItemPurchased = true;
                        ItemPurchased = true;
                        break;
                    case "3":
                        ValidChoice = true;
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"You have bought the {MagicItem}, this will increase your magic attempts attack by + 1.");
                        Console.SetCursorPosition(2, 26);
                        Console.WriteLine("You have spent 1GP.");
                        
                        Program.PressAKey();
                        MagicItemPurchased = true;
                        ItemPurchased = true;
                        break;
                    case "4":
                        ValidChoice = true;
                        break;
                }
            }
        }
        public void ItemsStoreClosed()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 2);
            Console.WriteLine("Items store is closed.");
            Console.SetCursorPosition(2, 4);
            Console.WriteLine("Please come back later.");
            Program.PressAKey();
        }
    }
}
