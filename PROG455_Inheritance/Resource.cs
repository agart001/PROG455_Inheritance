using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public class Resource : IDestructable
    {
        public enum MaterialType
        {
            Wood,
            Stone,
            Flesh
        }

        public MaterialType Type { get; protected set; }

        public IItem Material { get; protected set; }

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

                default: break;
            }
        }
    }

    public class Tree : Resource
    {
        public Tree() 
        {
            Type = MaterialType.Wood;
            Health = 20;
        }
    }

    public class Rock : Resource
    {
        public Rock()
        {
            Type = MaterialType.Stone;
            Health = 20;
        }
    }

    public class Animal : Resource
    {
        public Animal()
        {
            Type = MaterialType.Flesh;
            Health = 20;
        }
    }
}
