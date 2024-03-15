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
        string Name { get; protected set; }

        /// <summary>
        /// The item's description.
        /// </summary>
        string Desc { get; protected set; }

        /// <summary>
        /// The item's unique guid Id.
        /// </summary>
        Guid ID { get; protected set; }
    }


    public class Wood : IItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid ID { get; set; }

        public Wood()
        {
            Name = "Wood";
            Desc = "A small log.";
            ID = Guid.NewGuid();
        }
    }

    public class Ore : IItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid ID { get; set; }
        public Ore() 
        {
            Name = "Ore";
            Desc = "A small lump of ore.";
            ID = Guid.NewGuid();
        }
    }

    public class Meat : IItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid ID { get; set; }

        public Meat()
        {
            Name = "Meat";
            Desc = "A small chunk of flesh.";
            ID = Guid.NewGuid();
        }
    }

    public class Stick : IItem
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid ID { get; set; }

        public Stick()
        {
            Name = "Stick";
            Desc = "A small stick.";
            ID = Guid.NewGuid();
        }
    }
}
