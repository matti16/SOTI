using SOTI.ViewModels;

namespace SOTI.Message
{
    public class NavigationMessage
    {
        public NavigationMessage(BaseGameScreenViewModel destinationScreen)
        {
            this.DestinationScreen = destinationScreen;
        }

        public BaseGameScreenViewModel DestinationScreen { get; private set; }
    }
}
