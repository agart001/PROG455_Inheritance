using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public interface IGiver
    {
        public object Give(object[] parameters);
    }
    public abstract class ItemGiver : IGiver
    {
        public virtual object Give(object[] parameters)
        {
            throw new NotImplementedException();
        }
    }

    public class Crafter : ItemGiver
    {
        Type[] Recipes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(t => t.GetInterfaces().Contains(typeof(ICrafterRecipe))).ToArray();

        public Type[] CurrentlyCraftable(Dictionary<Type, int> inv)
        {
            List<Type> res = new List<Type>();
            foreach (var recipe in Recipes)
            {
                IRecipe rep = (IRecipe)Activator.CreateInstance(recipe);
                bool[] ing = new bool[rep.Ingredients.Count()];
                for(int i = 0; i < rep.Ingredients.Count(); i++)
                {
                    var kvp = rep.Ingredients.ElementAt(i);

                    int in_count;
                    if (inv.TryGetValue(kvp.Key, out in_count)) { in_count = inv[kvp.Key]; }
                    else { in_count = 0; }

                    if(in_count >= kvp.Value) { ing[i] = true; }
                    else { ing[i] = false; }
                }

                if(ing.All(b => b == true)){ res.Add(recipe); }

            }

            return res.ToArray();
        }


        public override object Give(object[] parameters)
        {
            var user = parameters[0] as IUser; 
            var recipe = parameters[1] as IRecipe;

            foreach(var kvp in recipe.Ingredients)
            {
                user.ChangeInv(kvp.Key, -kvp.Value);
            }

            return recipe.Result;   
        }
    }


    public class Furnace : ItemGiver
    {
        Type[] Recipes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes())
                        .Where(t => t.GetInterfaces().Contains(typeof(IFurnaceRecipe))).ToArray();

        public Type[] CurrentlyCraftable(Dictionary<Type, int> inv)
        {
            List<Type> res = new List<Type>();
            foreach (var recipe in Recipes)
            {
                IRecipe rep = (IRecipe)Activator.CreateInstance(recipe);
                bool[] ing = new bool[rep.Ingredients.Count()];
                for (int i = 0; i < rep.Ingredients.Count(); i++)
                {
                    var kvp = rep.Ingredients.ElementAt(i);

                    int in_count;
                    if (inv.TryGetValue(kvp.Key, out in_count)) { in_count = inv[kvp.Key]; }
                    else { in_count = 0; }

                    if (in_count >= kvp.Value) { ing[i] = true; }
                    else { ing[i] = false; }
                }

                if (ing.All(b => b == true)) { res.Add(recipe); }

            }

            return res.ToArray();
        }


        public override object Give(object[] parameters)
        {
            var user = parameters[0] as IUser;
            var recipe = parameters[1] as IRecipe;

            foreach (var kvp in recipe.Ingredients)
            {
                user.ChangeInv(kvp.Key, -kvp.Value);
            }

            return recipe.Result;
        }
    }
}
