using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Message
{
    public class RecipeMessage
    {
        
        public string recipeUri { get; private set; }
        public string allergiaUri { get; private set; }

        public RecipeMessage(string recipeUri, string allergiaUri)
        {
            this.recipeUri = recipeUri;
            this.allergiaUri = allergiaUri;
        }
    }
}
