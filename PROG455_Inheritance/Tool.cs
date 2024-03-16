using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public interface ITool : IItem
    {
        public void Damage(IDestructable destructable);
    }

    public abstract class Tool : ITool
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

        public Tool() 
        {
            ID = Guid.NewGuid();
        }

        public void Damage(IDestructable destructable)
        {
            destructable.Visit(this);
        }
    }

    public class Hands : Tool
    {
        public Hands() : base()
        {
            Type = ToolType.None;
            Name = "Hands";
            Desc = "Your own two hands.";
            AttackValue = 50;
        }
    }

    #region Pickaxe

    public class Pickaxe : Tool
    {
        public Pickaxe() : base()
        {
            Type = ToolType.Pickaxe;
            Name = "Pickaxe";
            Desc = "A tool for chipping rock.";
        }
    }

    #endregion

    #region Axe 

    public class Axe : Tool
    {
        public Axe() : base()
        {
            Type = ToolType.Axe;
            Name = "Axe";
            Desc = "A tool for chopping wood.";
        }
    }

    #endregion

    #region Sword

    public class Sword : Tool
    {
        public Sword() : base()
        {
            Type = ToolType.Sword;
            Name = "Sword";
            Desc = "A tool for cutting flesh.";
        }
    }

    #endregion
}
