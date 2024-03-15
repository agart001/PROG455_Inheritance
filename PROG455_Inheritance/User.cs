using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PROG455_Inheritance
{
    public interface IUser
    {
        public string Name { get; protected set; }
        public Dictionary<Type, int> Inventory { get; set; }
    }

    public class ItemUser
    {
        public virtual void Use(object[] args)
        {

        }
    }

    public class Player : ItemUser, IUser
    {
        public string Name { get; set; }
        public Dictionary<Type, int> Inventory { get; set; }

        public Tool CurrentTool { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new Dictionary<Type, int>
            {
                {typeof(Hands), 1}
            };

            Type tool = Inventory.Where(kvp => kvp.Key == typeof(Tool)).FirstOrDefault().Key;

            CurrentTool = (Tool?)Activator.CreateInstance(tool)
                ?? new Hands();
        }
    }
}
