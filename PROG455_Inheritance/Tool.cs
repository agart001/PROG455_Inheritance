using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public abstract class Tool : IItem
    {
        public enum ToolType
        {
            None,
            Pickaxe,
            Axe,
            Sword
        }

        public ToolType Type { get; set; }
        public int AttackValue;

        public string Name { get; set; }
        public string Desc { get; set ; }
        public Guid ID { get; set; }

        public void Damage(IDestructable destructable)
        {
            destructable.Visit(this);
        }
    }

    public class Hands : Tool
    {
        public Hands()
        {
            Type = ToolType.None;
            Name = "Hands";
            Desc = "Your own two hands.";
            AttackValue = 10;
        }
    }

    #region Pickaxe

    public class Pickaxe : Tool
    {
        public Pickaxe(string material, int atkValue)
        {
            Type = ToolType.Pickaxe;
            Name = $"{material} Pickaxe";
            Desc = $"A {material} Pickaxe.";
            AttackValue = atkValue;
        }
    }

    public class WoodPickaxe : Pickaxe
    {
        public WoodPickaxe(string material = "Wood", int atkValue = 15) 
            : base(material, atkValue) { }
    }

    public class StonePickaxe : Pickaxe
    {
        public StonePickaxe(string material = "Stone", int atkValue = 20) 
            : base(material, atkValue) { }
    }

    #endregion

    #region Axe 

    public class Axe : Tool
    {
        public Axe(string material, int atkValue)
        {
            Type = ToolType.Axe;
            Name = $"{material} Axe";
            Desc = $"A {material} Axe.";
            AttackValue = atkValue;
        }
    }

    public class WoodAxe : Axe
    {
        public WoodAxe(string material = "Wood", int atkValue = 15)
            : base(material, atkValue) { }
    }

    public class StoneAxe : Axe
    {
        public StoneAxe(string material = "Stone", int atkValue = 20)
            : base(material, atkValue) { }
    }

    #endregion

    #region Sword

    public class Sword : Tool
    {
        public Sword(string material, int atkValue)
        {
            Type = ToolType.Pickaxe;
            Name = $"{material} Sword";
            Desc = $"A {material} Sword.";
            AttackValue = atkValue;
        }
    }

    public class WoodSword: Sword
    {
        public WoodSword(string material = "Wood", int atkValue = 15)
            : base(material, atkValue) { }
    }

    public class StoneSword: Sword
    {
        public StoneSword(string material = "Stone", int atkValue = 20)
            : base(material, atkValue) { }
    }

    #endregion
}
