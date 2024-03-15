using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    internal interface IRecipe
    {
        Dictionary<Type, int> Ingredients { get; set; }

        Type Result { get; set; }
    }

    public class BaseRecipe : IRecipe
    {
        public Dictionary<Type, int> Ingredients { get; set; }
        public Type Result { get; set; }
    }

    public class StickRecipe : BaseRecipe
    {
        public StickRecipe()
        {
            Ingredients = new Dictionary<Type, int>
            {
                {typeof(Wood), 2}
            };

            Result = typeof(Stick);

        }
    }

    public class PickaxeRecipe : BaseRecipe
    {
        public PickaxeRecipe(string materialType) 
        {
            switch (materialType)
            {
                case
            }
        }

        void WoodPickaxe()
        {
            Ingredients = new Dictionary<Type, int>
            {
                {typeof(Stick), 2},
                {typeof(Wood), 3}
            };

            Result = typeof(WoodPickaxe);
        }

        void StonePickaxe()
        {
            Ingredients = new Dictionary<Type, int>
            {
                {typeof(Stick), 2},
                {typeof(Ore), 3}
            };

            Result = typeof(StonePickaxe);
        }
    }
}
