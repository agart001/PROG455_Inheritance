using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public interface IItem
    {
        /// <summary>
        /// The item's name.
        /// </summary>
        string Name { get;}

        /// <summary>
        /// The item's description.
        /// </summary>
        string Desc { get;}

        /// <summary>
        /// The item's unique guid Id.
        /// </summary>
        Guid ID { get; protected set; }
    }

    public abstract class Item : IItem
    {
        public virtual string Name { get; }

        public virtual string Desc { get; }

        public Guid ID { get; set; } = Guid.NewGuid();
    }


    public class Wood : Item
    {
        public override string Name => "Wood";
        public override string Desc => "A small log.";
    }

    public class Coal : Item
    {
        public override string Name => "Coal";

        public override string Desc => "A lump of coal.";
    }

    public class Ore : Item
    {
        public override string Name => "Ore";
        public override string Desc => "A small lump of ore.";
    }

    public class Ingot : Item
    {
        public override string Name => "Ingot";
        public override string Desc => "An ingot of metal.";
    }

    public class Meat : Item
    {
        public override string Name => "Meat";
        public override string Desc => "A small chunk of flesh.";
    }

    public class Stick : Item
    {
        public override string Name => "Stick";
        public override string Desc => "A small stick.";
    }
}
