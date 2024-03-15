using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG455_Inheritance
{
    public class ItemGiver
    {
        public virtual object Give(object[] inputs)
        {
            throw new NotImplementedException();
        }
    }

    public class InternalCrafter : ItemGiver
    {

    }
}
