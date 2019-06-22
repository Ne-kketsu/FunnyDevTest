using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FunnyDevTest
{
    class Program
    {
        public const int Width = 100;
        public const int Height = 50;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;
            //TODO: Intercation with the UI !
            //SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            Kitchen fridge = new Kitchen("Fridge") { Position = new Point(1, 8), UseKeyboard = true, UseMouse = true};
            Kitchen tools = new Kitchen("Tools Avaible") { Position = new Point((Global.CurrentScreen.Width / 2 - fridge.Width / 2), 25), UseKeyboard = true, UseMouse = true };
            Kitchen preparation_table = new Kitchen("Preparation Tables") { Position = new Point(Global.CurrentScreen.Width - fridge.Width, 8), UseKeyboard = true, UseMouse = true };
            fridge.MyFridge = fridge.NewFridge();
            tools.MyTools = tools.NewTools();
            preparation_table.MyTables = preparation_table.NewTables();

            fridge.MyFridge.PrintFridgeIngredients(fridge, Color.Red);
            tools.MyTools.PrintTools(tools);
            preparation_table.MyTables.PrintTables(preparation_table);
            SadConsole.Global.CurrentScreen = new SadConsole.ContainerConsole();
            SadConsole.Global.CurrentScreen.Children.Add(fridge);
            SadConsole.Global.CurrentScreen.Children.Add(tools);
            SadConsole.Global.CurrentScreen.Children.Add(preparation_table);
        }
    }

    class Kitchen : Console
    {
        public Fridge MyFridge = null;
        public AvaibleTools MyTools = null;
        public PreparationTables MyTables = null;

        public Kitchen(string title)
            : base(25, 7)
        {
            Fill(Color.White, Color.Black, 176);
            Print(0, 0, title.Align(HorizontalAlignment.Center, Width), Color.Black, Color.Yellow);
        }

        public Fridge NewFridge()
        {
            Fridge MyFridge = new Fridge();
            String[] IngredientNameArray = { "Cucumber", "Tomato", "Cheese", "Bread", "Oignons", "Chorizo" };

            MyFridge.Ingredients = new Fridge.Ingredient[MyFridge.nbr_ingredients];
            for (int i = 0; i < MyFridge.nbr_ingredients; i += 1, MyFridge.fridge_avaible_slots -= 1)
                MyFridge.Ingredients[i] = new Fridge.Ingredient(IngredientNameArray[i], 100);
            return (MyFridge);
        }

        public AvaibleTools NewTools()
        {
            AvaibleTools MyTools = new AvaibleTools();
            String[] ToolsNameArray = { "Knife", "CakePan", "Sifter", "Pan", "Whisk", "Electrical Whisk" };

            MyTools.Tools = new AvaibleTools.Tool[MyTools.nbr_tools];
            for (int i = 0; i < MyTools.nbr_tools; i += 1)
                MyTools.Tools[i] = new AvaibleTools.Tool(ToolsNameArray[i]);
            return (MyTools);
        }

        public PreparationTables NewTables()
        {
            PreparationTables MyTables = new PreparationTables();

            MyTables.Tables = new PreparationTables.Table[MyTables.nbr_tables];
            for (int i = 0; i < MyTables.nbr_tables; i += 1)
                MyTables.Tables[i] = new PreparationTables.Table("Table " + (i + 1));
            return (MyTables);
        }

        public class Fridge
        {
            public int fridge_avaible_slots = 10;
            public Ingredient[] Ingredients = null;
            public int nbr_ingredients = 6;
            public int fridge_slots = 10;

            public void PrintFridgeIngredients(Kitchen fridge, Color ingredient_color)
            {
                for (int i = 0; i < fridge.MyFridge.nbr_ingredients; i += 1)
                {
                    fridge.Print(0, (i + 1), fridge.MyFridge.Ingredients[i].name.Align(HorizontalAlignment.Center, fridge.Width), ingredient_color, Color.White);
                    //fridge.Print(0, (i + 1), fridge.MyFridge.Ingredients[i].quantity.ToString().Align(HorizontalAlignment.Right, fridge.Width), ingredient_color, Color.Transparent);
                }

            }

            public class Ingredient
            {
                public String name;
                public int quantity = 100;
                public bool is_selected;

                public Ingredient(String ingredient_name, int qty)
                {
                    name = ingredient_name;
                    quantity = qty;
                    is_selected = false;
                }
            }
        }

        public class AvaibleTools
        {
            public Tool[] Tools = null;
            public int nbr_tools = 6;

            public void PrintTools(Kitchen preparation_table)
            {
                for (int i = 0; i < preparation_table.MyTools.nbr_tools; i += 1)
                    preparation_table.Print(0, (i + 1), preparation_table.MyTools.Tools[i].name.Align(HorizontalAlignment.Center, preparation_table.Width), Color.Red);

            }

            public class Tool
            {
                public String name;

                public Tool(String tool_name)
                {
                    name = tool_name;
                }
            }
        }

        public class PreparationTables
        {
            public Table[] Tables = null;
            public int nbr_tables = 2;

            public void PrintTables(Kitchen preparation_table)
            {
                for (int i = 0; i < preparation_table.MyTables.nbr_tables; i += 1)
                    preparation_table.Print(0, (i + 1), preparation_table.MyTables.Tables[i].name.Align(HorizontalAlignment.Center, preparation_table.Width), Color.Red);

            }

            public class Table
            {
                public String name;

                public Table(String table_name)
                {
                    name = table_name;
                }
            }
        }
    }
}
