using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class IngredientResultMessage
    {
        public bool right { get; private set; }

        public IngredientResultMessage(bool right)
        {
            this.right = right;
        }
    }
}
