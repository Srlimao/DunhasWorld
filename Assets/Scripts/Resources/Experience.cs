using UnityEngine;
using System.Collections;
using RPG.Saving;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        

        public void GainExperience(float xp)
        {
            experiencePoints += xp;
        }

        public float GetCurrentExp()
        {
            return experiencePoints;
        }

        #region Save/Load

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        #endregion 
    }
}
