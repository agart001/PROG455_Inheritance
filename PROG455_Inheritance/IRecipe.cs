using PROG455_Inheritance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public interface IFurnaceRecipe { }
    public interface ICrafterRecipe { }
    public interface IRecipe
    {
        public Dictionary<Type, int> Ingredients { get;}

        public IItem Result { get;}
    }

    public interface IMultiRecipe<T> : IRecipe where T : Type
    {
        public Dictionary<Type, IItem> MaterialLookup { get; }
    }

    public struct StickRecipe : IRecipe, ICrafterRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Wood), 2}
                };
        public IItem Result => new Stick();
    }

    public struct CoalRecipe : IRecipe, IFurnaceRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Wood), 1},
                    {typeof(Ore), 1}
                };
        public IItem Result => new Coal();
    }

    public struct IngotRecipe : IRecipe, IFurnaceRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Coal), 1},
                    {typeof(Ore), 1}
                };
        public IItem Result => new Ingot();
    }

    public struct PickaxeRecipe : IRecipe, ICrafterRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Ingot), 3},
                    {typeof(Stick), 2}
                };

        public IItem Result => new Pickaxe();
    }

    public struct AxeRecipe : IRecipe, ICrafterRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Ingot), 1},
                    {typeof(Stick), 2}
                };

        public IItem Result => new Axe();
    }

    public struct SwordRecipe : IRecipe, ICrafterRecipe
    {
        public Dictionary<Type, int> Ingredients => new Dictionary<Type, int>
                {
                    {typeof(Ingot), 2},
                    {typeof(Stick), 1}
                };

        public IItem Result => new Sword();
    }

    /*public struct ToolRecipe<TMaterial, TTool> : IMultiRecipe<TMaterial> , ICrafterRecipe
        where TTool : Tool
        where TMaterial : Type
    {
        public Dictionary<Type, IItem> MaterialLookup => ToolLookup() ?? throw new NullReferenceException();

        Dictionary<Type, IItem>? ToolLookup()
        {

            switch(typeof(Tool).Name)
            {
                case "Pickaxe": return new Dictionary<Type, IItem>
                {
                    {typeof(Wood), new Pickaxe("Wood")},
                    {typeof(Ore), new Pickaxe("Stone")},
                    {typeof(Ingot), new Pickaxe("Metal")}
                };
                case "Axe": return new Dictionary<Type, IItem>
                {
                    {typeof(Wood), new Axe("Wood")},
                    {typeof(Ore), new Axe("Stone")},
                    {typeof(Ingot), new Axe("Metal")}
                };
                case "Sword": return new Dictionary<Type, IItem>
                {
                    {typeof(Wood), new Sword("Wood")},
                    {typeof(Ore), new Sword("Stone")},
                    {typeof(Ingot), new Sword("Metal")}
                };
                default: return null;
            }

        }

        public Dictionary<Type, int> Ingredients => IngLookup() ?? throw new NullReferenceException();

        Dictionary<Type, int>? IngLookup()
        {
            switch (typeof(Tool).Name)
            {
                case "Pickaxe": return new Dictionary<Type, int>
                {
                    {typeof(TMaterial), 3},
                    {typeof(Stick), 2}
                };
                case "Axe": return new Dictionary<Type, int>
                {
                    {typeof(TMaterial), 1},
                    {typeof(Stick), 2}
                };
                case "Sword": return new Dictionary<Type, int>
                {
                    {typeof(TMaterial), 2},
                    {typeof(Stick), 1}
                };
                default: return null;
            }
        }

        public IItem Result => MaterialLookup[typeof(TMaterial)];
    }*/
}
