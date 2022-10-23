namespace Profile
{
    public class GameLevel
    {
        /// <summary>
        /// Максимально доступный левел
        /// </summary>
        public int AvailableLevel = 0;
        /// <summary>
        /// Текущий уровень
        /// </summary>
        public int CurrentLevel = 0;

        public GameLevel()
        {
        }

        public GameLevel(int availableLevel, int currentLevel)
        {
            AvailableLevel = availableLevel;
            CurrentLevel = currentLevel;
        }
    }
}
