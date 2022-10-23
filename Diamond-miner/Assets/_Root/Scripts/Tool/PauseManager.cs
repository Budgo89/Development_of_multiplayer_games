using UnityEngine;

namespace Tool
{
    public class PauseManager
    {
        private bool Pause = false;

        /// <summary>
        /// Включить паузу
        /// </summary>
        public void EnablePause()
        {
            Pause = true;
            Time.timeScale = 0;
        }
        
        /// <summary>
        /// Отключить паузу
        /// </summary>
        public void DisablePause()
        {
            Pause = false;
            Time.timeScale = 1;
        }
        
        public bool IsPause()
        {
            return Pause;
        }
    }
}
