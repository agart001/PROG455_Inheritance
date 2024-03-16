using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PROG455_Inheritance
{
    public static class UI
    {
        #region Print

        /// <summary>
        /// Prints a single line.
        /// </summary>
        public static void Print(string line) => Console.WriteLine(line);

        /// <summary>
        /// Prints an array of lines.
        /// </summary>
        public static void Print(string[] lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Prints an array of objects with a specified header.
        /// </summary>
        public static void Print(object[] entities, string header)
        {
            foreach (object entity in entities)
            {
                Console.WriteLine($"{header} {entity}");
            }
        }

        public static void Print<TKey, TValue>(Dictionary<TKey, TValue> dict, string keyHeader, string valHeader)
        {
            foreach (var kvp in dict)
            {
                var name = Type.GetType(kvp.Key.ToString()).Name;
                Console.WriteLine($"{keyHeader} : {name} | {valHeader} : {kvp.Value}");
            }
        }

        #endregion

        // Question methods
        #region Question

        /// <summary>
        /// Asks a yes/no question.
        /// </summary>
        public static bool BoolQuestion(string question, string confirm, string cancel)
        {
            bool result = false;
            Print($"{question}? ({confirm}/{cancel}): ");
            string input = Console.ReadLine() ?? string.Empty;

            if (input.ToLower() == confirm)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Asks a multiple-choice question.
        /// </summary>
        public static int MultiQuestion(string question, string[] answers)
        {
            int answer;
            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine($"#{i + 1}: {answers[i]}");
            }
            Print("------------------------------------");
            Print($"{question}? (Enter an integer #): ");
            var input = Console.ReadLine() ?? string.Empty;
            answer = int.Parse(input);

            return answer - 1;
        }

        #endregion
    }

    public class Game
    {
        public Player Player;
        public Crafter Crafter;
        public Furnace Furnace;
        public List<IDestructable> Resources;


        public Game(string p_name)
        {
            Player = new Player(p_name);
            Crafter = new Crafter();
            Furnace = new Furnace();

            Resources = GenerateRandomResources();
        }

        List<IDestructable> GenerateRandomResources()
        {
            var resources = new List<IDestructable>();
            Random rand = new Random();
            var res_count = rand.Next(10, 20);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(t =>
                        {
                            if (t.GetInterfaces().Contains(typeof(IDestructable)) && t.IsAbstract == false) { return true; }
                            else { return false; }
                        }).ToArray();

            for (int i = 0;i < res_count;i++)
            {
                var res_type = rand.Next(0, types.Length - 1);
                var des = Activator.CreateInstance(types[res_type]) as IDestructable;
                resources.Add(des);
            }

            return resources;
        }

        public void Loop()
        {
            Console.Clear();
            UI.Print(new string[]
            {
                "------------------------------------",
                "   Welcome to Main Menu",
                "------------------------------------"
            });
            var loop = UI.MultiQuestion("What would you like to do?", new string[]
            {
                "View Inventory",
                "Craft Items",
                "Harvest Resources"
            });

            switch (loop)
            {
                case 0: InvLoop(); break;
                case 1: CraftLoop(); break;
                case 2: ResourceLoop(); break;
                default: throw new Exception();
            }
        }

        void InvLoop()
        {
            Console.Clear();
            UI.Print(new string[]
            {
                "------------------------------------",
                $"   {Player.Name}'s Inventory",
                "------------------------------------"
            });
            UI.Print(Player.Inventory, "Item", "Count");

            UI.Print("------------------------------------");

            var choice = UI.MultiQuestion("What would you like to do?", new string[]
            {
                "Select a tool",
                "Main Menu"
            });

            switch(choice)
            {
                case 0: ToolSelect();  break;
                case 1: Loop(); break;
            }
        }

        void ToolSelect()
        {
            Console.Clear();
            var tools = Player.Inventory.Keys.Where(k => k.IsSubclassOf(typeof(Tool)));
            var choices = new List<string>();

            for (int i = 0; i < tools.Count(); i++)
            {
                choices.Add($"{tools.ElementAt(i).Name}");
            }

            var tool_choice = UI.MultiQuestion("Which tool to select?", choices.ToArray());

            Player.CurrentTool = (ITool)Activator.CreateInstance(tools.ElementAt(tool_choice)) ?? throw new InvalidCastException();

            Console.Clear();
            UI.Print($"{tools.ElementAt(tool_choice).Name} selected!");
            UI.Print("------------------------------------");

            var cont = UI.MultiQuestion("What would you like to do next?", new string[]
            {
                "Back to Inventory",
                "Back to Tool Select",
                "Main Menu"
            });

            switch (cont)
            {
                case 0: InvLoop(); break;
                case 1: ToolSelect(); break;
                case 2: Loop(); break;
            }
        }

        #region Craft

        void CraftLoop()
        {
            Console.Clear();
            UI.Print(new string[]
            {
                "------------------------------------",
                "   Welcome to the Craft Center",
                "------------------------------------"
            });

            var choice = UI.MultiQuestion("What would you like to do?", new string[]
            {
                "1) Use Crafter",
                "2) Use Furnace",
                "3) Main Menu"
            });

            switch(choice)
            {
                case 0: CraftSelect();  break;
                case 1: FurnaceSelect(); break;
                case 2: Loop(); break;
            }
        }

        void CraftSelect()
        {
            Console.Clear();
            var recipes = Crafter.CurrentlyCraftable(Player.Inventory);

            var choices = new List<string>();

            for (int i = 0; i < recipes.Count(); i++)
            {
                choices.Add($"{recipes.ElementAt(i).Name}");
            }

            UI.Print(new string[]
            {
                "------------------------------------",
                "              Crafter",
                "------------------------------------"
            });

            var craft_choice = UI.MultiQuestion("What would you like to craft?", choices.ToArray());
            var recipe = (IRecipe)Activator.CreateInstance(recipes.ElementAt(craft_choice)) ?? throw new NullReferenceException();
            Player.Use(new object[]
            {
                Crafter,
                new object[]
                {
                    Player,
                    recipe
                }
            });

            Console.Clear();
            UI.Print("------------------------------------");
            UI.Print($"Item Added: {recipe.Result} | Count: 1");
            UI.Print("------------------------------------");

            var cont = UI.MultiQuestion("What would you like to do next?", new string[]
            {
                "Back to Craft Center",
                "Back to Crafter",
                "Main Menu"
            });

            switch (cont)
            {
                case 0: CraftLoop(); break;
                case 1: CraftSelect(); break;
                case 2: Loop(); break;
            }
        }

        void FurnaceSelect()
        {
            Console.Clear();
            var recipes = Furnace.CurrentlyCraftable(Player.Inventory);

            var choices = new List<string>();

            for (int i = 0; i < recipes.Count(); i++)
            {
                choices.Add($"{recipes.ElementAt(i).Name}");
            }

            UI.Print(new string[]
            {
                "------------------------------------",
                "              Furnace",
                "------------------------------------"
            });

            var craft_choice = UI.MultiQuestion("What would you like to craft?", choices.ToArray());
            var recipe = (IRecipe)Activator.CreateInstance(recipes.ElementAt(craft_choice)) ?? throw new NullReferenceException();
            Player.Use(new object[]
            {
                Furnace,
                new object[]
                {
                    Player,
                    recipe
                }
            });

            Console.Clear();
            UI.Print("------------------------------------");
            UI.Print($"Item Added: {recipe.Result} | Count: 1");
            UI.Print("------------------------------------");

            var cont = UI.MultiQuestion("What would you like to do next?", new string[]
            {
                "Back to Craft Center",
                "Back to Furnace",
                "Main Menu"
            });

            switch (cont)
            {
                case 0: CraftLoop(); break;
                case 1: FurnaceSelect(); break;
                case 2: Loop(); break;
            }
        }

        #endregion

        #region Resources

        void ResourceLoop()
        {
            Console.Clear();
            UI.Print(new string[]
            {
                "------------------------------------",
                "   Welcome to the Resource Grounds",
                "------------------------------------"
            });

            Dictionary<Type, int> res_counts = new Dictionary<Type, int>();

            var distinct = Resources.Select(r => r.GetType()).Distinct();
            var choices = new List<string>();
            int index = 0;

            foreach(var dis in distinct)
            {
                int count = Resources.Where(r => r.GetType() == dis).ToArray().Length;
                res_counts.Add(dis, count);
                choices.Add($"{dis.Name}"); 
            }

            UI.Print(res_counts, "Resource", "Count");
            UI.Print("------------------------------------");
            var choice = UI.MultiQuestion("What would you like to harvest?", choices.ToArray());

            var type = distinct.ElementAt(choice);

            HarvestGround(type);
        }

        void HarvestGround(Type type)
        {
            Console.Clear();
            UI.Print(new string[]
            {
                "------------------------------------",
                $"    Harvet Grounds: {type.Name}",
                "------------------------------------"
            });
            var toHarvest = Resources.Where(r => r.GetType() == type).ToArray();
            var choices = new List<string>();

            for (int i = 0; i < toHarvest.Length; i++)
            {
                choices.Add($"Health: {toHarvest.ElementAt(i).Health}");
            }

            var choice = UI.MultiQuestion($"What {type.Name} would you like to harvest?", choices.ToArray());
            var resource = toHarvest.ElementAt(choice);
            Player.Use(new object[]
            {
                resource
            });

            Console.Clear();
            if (resource.Health <= 0) 
            { 
                Resources.Remove(resource);
                UI.Print("------------------------------------");
                UI.Print($"#{choice + 1} : {type.Name} Harvested | {resource.Material.Name} Added");
                UI.Print("------------------------------------");
            }
            else
            {
                UI.Print("------------------------------------");
                UI.Print($"#{choice + 1} : {type.Name} Health:{toHarvest.ElementAt(choice).Health}");
                UI.Print("------------------------------------");
            }



            var cont = UI.MultiQuestion("What would you like to do next?", new string[]
            {
                "Back to Resource",
                $"Back to Harvest : {type.Name}",
                "Main Menu"
            });

            switch(cont)
            {
                case 0: ResourceLoop(); break;
                case 1: HarvestGround(type); break;
                case 2: Loop(); break;
            }
        }
        #endregion
    }
}
