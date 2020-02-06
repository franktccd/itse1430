using System;

namespace PizzaCreator
{
    class Program
    {
        static double price, sizePrice, meatPrice, vegetablePrice, saucePrice, cheesePrice, deliveryPrice;
        static string size;
        static string meats;
        static string vegetables;
        static string sauce;
        static string cheese;
        static string deliveryStatus;
        static bool isDelivery;
        static bool orderStarted;
        static void Main ( string[] args )
        {
            Console.WriteLine("PizzaCreator for C# Programming ITSE 1430-21722 by Frank Rygiewicz");

            var done = false;
            do
            {
                switch(DisplayMenu())
                {
                    case Command.Add: AddPizza(); break;
                    case Command.Display: DisplayPizza();  break;
                    case Command.Quit: done = ReadBool("Are you sure you wish to quit?"); break;
                    case Command.Modify: ModifyPizza(); break;
                }
            } while (!done);
        }

        enum Command
        {
            Quit = 0,
            Add = 1,
            Display = 2,
            Modify = 3,
        }
        private static Command DisplayMenu ()
        {
            do
            {
                Console.WriteLine("Current Price: " + price.ToString("C"));
                Console.WriteLine("1) New Order");
                Console.WriteLine("2) Display Order");
                Console.WriteLine("3) Modify Order");
                Console.WriteLine("0) Quit");

                var input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "1": return Command.Add;
                    case "2": return Command.Display;
                    case "3": return Command.Modify;
                    case "0": return Command.Quit;

                    default: Console.WriteLine("Invalid option."); break;
                };
            } while (true);
        }

        static void AddPizza ()
        {
            if (orderStarted == true)
            {
                bool overwrite = ReadBool("Would you like to overwrite your previous order?");
                if (overwrite == true)
                    orderStarted = false; Console.WriteLine("Your old order has been erased, please start your new order."); price = 0;
            } else
            {
                AddSize();
                AddMeats();
                AddVegetables();
                AddCheese();
                AddSauce();
                Delivery();
                orderStarted = true;
            }
        }

        static void DisplayPizza()
        {
            if (!orderStarted)
            {
                Console.WriteLine("There is no order to display");
            } else
            {
                Console.WriteLine(size);
                Console.WriteLine(deliveryStatus);
                if (!String.IsNullOrEmpty(meats))
                {
                    Console.WriteLine("Meats:");
                    Console.WriteLine(meats);
                }
                if (!String.IsNullOrEmpty(vegetables))
                {
                    Console.WriteLine("Vegetables:");
                    Console.WriteLine(vegetables);
                }
                Console.WriteLine("Sauce:"); Console.WriteLine(sauce);
                Console.WriteLine("Cheese:"); Console.WriteLine(cheese);
                Console.WriteLine("-----------------------------------------------------");
                Console.Write("Total\t\t" + price.ToString("C") + "\n");
            }
        }

        static void ModifyPizza ()
        {
            bool display = true;
            if (orderStarted)
            {
                do
                {
                    DisplayPizza();
                    Console.WriteLine("What would you like to modify?\n1) Size\n2) Delivery\n3) Meats\n4) Vegetables\n5) Sauce\n6) Cheese\n0) Quit Modification.");
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1": ModifySize(); break;
                        case "2": ModifyDelivery(); break;
                        case "3": ModifyMeats(); break;
                        case "4": ModifyVegetables(); break;
                        case "5": ModifySauce(); break;
                        case "6": ModifyCheese(); break;
                        case "0": bool choice = ReadBool("Are you finished modifying your order?"); if (choice == true) { display = false; } break;
                        default: Console.WriteLine("Please enter a number from 0 to 6"); break;
                    }
                } while (display);
            } else Console.WriteLine("No order available to modify, please start a new order.");
        }

        static void AddSize ()
        {
            bool display = true;
            do
            {
                int input = ReadInt32("Size (One is required): \n\t1) Small ($5)\n\t2) Medium ($6.25)\n\t3) Large ($8.75)\n", 1, 3);
                switch(input)
                {
                    case 1: size = "Small Pizza\t$5.00"; sizePrice = 5; display = false; break;
                    case 2: size = "Medium Pizza\t$6.25"; sizePrice = 6.25; display = false; break;
                    case 3: size = "Large Pizza\t$8.75"; sizePrice = 8.75; display = false; break;
                    default: Console.WriteLine("Please choose 1, 2, or 3"); display = true; break;
                }

            } while (display);
            price += sizePrice;
        }

        static void AddMeats()
        {
            bool display = true;
            meatPrice = 0;
            meats = null;
            do
            {
                Console.WriteLine("Meats (Zero or more): *Each option is $0.75 extra*\n\t1) Bacon \n\t2) Ham \n\t3) Pepperoni\n\t4) Sausage\n\t0) Next");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1": meats += "Bacon\t\t$0.75\n"; meatPrice += 0.75; display = true; break;
                    case "2": meats += "Ham\t\t$0.75\n"; meatPrice += 0.75; display = true; break;
                    case "3": meats += "Pepperoni\t$0.75\n"; meatPrice +=0.75; display = true; break;
                    case "4": meats += "Sausage\t\t$0.75\n"; meatPrice += 0.75; display = true; break;
                    case "0": display = false; break;
                    default: Console.WriteLine("Please enter a number from 0 to 4.\nMeats (Zero or more): *Each option is $0.75 extra*\n\t1) Bacon \n\t2) Ham \n\t3) Pepperoni\n\t4) Sausage\n\t0) Next"); display = true; break; 
                }
            } while (display);
            price += meatPrice;
        }

        static void AddVegetables()
        {
            vegetables = null;
            vegetablePrice = 0;
            bool display = true;
            do
            {
                Console.WriteLine("Vegetables (Zero or more): Each option is $0.50 extra\n\t1) Black Olives\n\t2) Mushrooms\n\t3) Onions\n\t4) Peppers\n\t0) Next");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": vegetables += "Black Olives\t$0.50\n"; vegetablePrice += 0.5; display = true; break;
                    case "2": vegetables += "Mushrooms\t$0.50\n"; vegetablePrice += 0.5; display = true; break;
                    case "3": vegetables += "Onions\t\t$0.50\n"; vegetablePrice +=0.5; display = true; break;
                    case "4": vegetables += "Peppers\t\t$0.50\n"; vegetablePrice += 0.5; display = true; break;
                    case "0": display = false; break;
                    default: Console.WriteLine("Please enter a number from 0 to 4"); display = true; break;
                }
            } while (display);
            price += vegetablePrice;
        }

        static void AddSauce()
        {
            bool display = true;
            do
            {
                Console.WriteLine("Sauce (One is required): \n\t1) Traditional ($0)\n\t2) Garlic ($1)\n\t3) Oregano($1)");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": sauce = "Traditional Sauce"; saucePrice = 0; display = false; break;
                    case "2": sauce = "Garlic Sauce\t$1.00"; saucePrice = 1; display = false; break;
                    case "3": sauce = "Oregano Sauce\t$1.00"; saucePrice = 1; display = false; break;
                    default: Console.WriteLine("Please enter a number from 1-3"); display = true; break;
                }
            } while (display);
            price += saucePrice;
        }

        static void AddCheese()
        {
            bool display = true;
            do
            {
                Console.WriteLine("Cheese (One is required): \n\t1) Regular ($0)\n\t2) Extra ($1.25)");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": cheese = "Regular Cheese"; cheesePrice = 0; display = false; break;
                    case "2": cheese = "Extra Cheese\t$1.25"; cheesePrice = 1.25; display = false; break;
                    default: Console.WriteLine("Please Choose either 1 or 2"); display = true; break;
                }
            } while (display);
            price += cheesePrice;
        }

        static void Delivery()
        {
            isDelivery = ReadBool("Delivery (One is required): \n\tTake Out(N) ($0)\n\tDelivery(Y) ($2.50)");
            if (isDelivery)
            {
                deliveryStatus = "Delivery\t$2.50";
                deliveryPrice = 2.5;
            } else
            {
                deliveryStatus = "Takeout";
                deliveryPrice = 0;
            }
            price += deliveryPrice;
        }

        static void ModifySize()
        {
            Console.WriteLine("Current size is: " + size);
            bool choice = ReadBool("Are you sure you would like to change the size?");
            if (choice == true)
            {
                price -= sizePrice;
                AddSize();
            }
        }

        static void ModifyDelivery()
        {
            Console.WriteLine("Current delivery is: " + deliveryStatus);
            bool choice = ReadBool("Are you sure you would like to change the delivery status?");
            if (choice == true)
            {
                price -= deliveryPrice;
                Delivery();
            }
        }

        static void ModifyMeats ()
        {
            Console.WriteLine("Current meats: " + meats);
            bool choice = ReadBool("Changing meats will reset current meats to 0, are you sure you want to continue?");
            if (choice == true)
            {
                price -= meatPrice;
                AddMeats();
            }
        }

        static void ModifyVegetables ()
        {
            Console.WriteLine("Current vegetables: " + vegetables);
            bool choice = ReadBool("Changing vegetables will reset current vegetables to 0, are you sure you want to continue?");
            if (choice == true)
            {
                price -= vegetablePrice;
                AddVegetables();
            }
        }

        static void ModifySauce()
        {
            Console.WriteLine("Current sauce: " + sauce);
            bool choice = ReadBool("Are you sure you would like to change the sauce?");
            if (choice == true)
            {
                price -= saucePrice;
                AddSauce();
            }
        }

        static void ModifyCheese ()
        {
            Console.WriteLine("Current cheese: " + cheese);
            bool choice = ReadBool("Are you sure you would like to change the cheese?");
            if (choice == true)
            {
                price -= cheesePrice;
                AddCheese();
            }
        }

        private static int ReadInt32 ( string message, int minValue, int maxValue )
        {
            Console.Write(message);

            do
            {
                string temp = Console.ReadLine();
              
                if (Int32.TryParse(temp, out var value))
                {
                    if (value >= minValue && value <= maxValue)
                        return value;
                    
                };
                string error = String.Format("Value must be betwen {0} and {1}.", minValue, maxValue);
                Console.WriteLine(error);

            } while (true);
        }

        private static bool ReadBool ( string message )
        {
            Console.Write(message + " (Y/N)");

            do
            {
                string value = Console.ReadLine();

                if (!String.IsNullOrEmpty(value))
                {

                    if (String.Compare(value, "Y", true) == 0)
                        return true;
                    else if (String.Compare(value, "N", true) == 0)
                        return false;
                    else Console.WriteLine("Please enter Y or N.");
                };

                Console.WriteLine("Enter Y/N");
            } while (true);
        }
    }
}