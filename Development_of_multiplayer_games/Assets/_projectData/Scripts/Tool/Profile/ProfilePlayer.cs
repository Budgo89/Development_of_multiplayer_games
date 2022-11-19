using Tool;

namespace Profile
{
    public class ProfilePlayers
    {
        public readonly SubscriptionProperty<GameState> CurrentState;

        public ProfilePlayers(GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);
        }
    }
}
