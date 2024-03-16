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

        public void ChangeInv(Type type, int count);
    }

    public class ItemUser
    {
        public virtual void Use(object[] parameters)
        {

        }
    }

    public class Player : ItemUser, IUser
    {
        public string Name { get; set; }
        public Dictionary<Type, int> Inventory { get; set; }

        public ITool CurrentTool { get; set; }

        public Player(string name)
        {
            Name = name;
            Inventory = new Dictionary<Type, int>
            {
                {typeof(Hands), 1},
                {typeof(Axe), 1}
            };
            Type tool = Inventory.FirstOrDefault(kvp => kvp.Key.GetInterfaces().Contains(typeof(ITool))).Key;

            CurrentTool = (ITool?)Activator.CreateInstance(tool)
                ?? new Hands();
        }

        public void ChangeInv(Type type, int count)
        {
            if (Inventory.ContainsKey(type) == false)
            {
                if(count > 0)
                {
                    Inventory.Add(type, count);
                }
                else { return; }

                return;
            }

            Inventory[type] += count;

            if (Inventory[type] <= 0) { Inventory.Remove(type); }
        }

        public override void Use(object[] parameters)
        {
            var objInUse = parameters[0];
            var interfaces = objInUse.GetType().GetInterfaces();

            if (interfaces.Contains(typeof(IGiver)))
            {
                var giver = objInUse as IGiver;
                object[] param = parameters[1] as object[] ?? throw new Exception();
                var given = giver.Give(param);
                ChangeInv(given.GetType(), 1);
            }
            else if (interfaces.Contains(typeof(IDestructable)))
            {
                var dest = objInUse as IDestructable;
                if(dest.Health <= 0)
                {
                    ChangeInv(dest.Material, 1);
                }
                else
                {
                    CurrentTool.Damage(dest);

                    if (dest.Health <= 0)
                    {
                        ChangeInv(dest.Material, 1);
                    }
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
