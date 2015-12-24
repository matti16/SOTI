using SOTI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class RecipeMessage
    {
        
        public Ricetta recipe{ get; private set; }

        public RecipeMessage(Ricetta recipe)
        {
            this.recipe = recipe;

        }
    }
}
