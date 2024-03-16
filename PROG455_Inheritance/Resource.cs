using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public interface IDestructable
    {
        int Health { get; set; }
        public Type Material { get; protected set; }

        public void Visit(Tool tool);
    }

    public abstract class Resource : IDestructable
    {
        public enum MaterialType
        {
            Wood,
            Stone,
            Flesh
        }

        public MaterialType Type { get; protected set; }

        public Type Material { get; set; }

        public int Health { get; set; }

        public Resource()
        {
        }

        public Resource(MaterialType type, int health)
        {
            Type = type;
            Health = health;
        }

        public void Visit(Tool tool)
        {
            switch (tool.Type)
            {
                case Tool.ToolType.Pickaxe:

                    if(Type == MaterialType.Stone) { Health -= tool.AttackValue; }
                    else { Health -= tool.AttackValue / 4; }

                    break;

                case Tool.ToolType.Axe: 

                    if (Type == MaterialType.Wood) { Health -= tool.AttackValue; }
                    else { Health -= tool.AttackValue / 4; }

                    break;

                case Tool.ToolType.Sword:

                    if (Type == MaterialType.Flesh) { Health -= tool.AttackValue; }
                    else { Health -= tool.AttackValue / 4; }

                    break;

                default: Health -= tool.AttackValue / 2; break;
            }
        }

        internal void ResourceMaterial()
        {
            switch(Type)
            {
                case MaterialType.Wood: Material = typeof(Wood); break;
                case MaterialType.Stone: Material = typeof(Ore); break;
                case MaterialType.Flesh: Material = typeof(Meat); break;
            }
        }
    }

    public class Tree : Resource
    {
        public Tree() 
        {
            Type = MaterialType.Wood;
            Health = 20;
            ResourceMaterial();
        }
    }

    public class Rock : Resource
    {
        public Rock()
        {
            Type = MaterialType.Stone;
            Health = 20;
            ResourceMaterial();
        }
    }

    public class Animal : Resource
    {
        public Animal()
        {
            Type = MaterialType.Flesh;
            Health = 20;
            ResourceMaterial();
        }
    }
}
