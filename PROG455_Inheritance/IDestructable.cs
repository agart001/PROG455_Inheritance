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

        public void Visit(Tool tool);
    }
}
