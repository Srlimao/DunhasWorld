using UnityEngine;
using System.Collections;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float ExperiencePoints = 0;

        public void GainExperience(float xp)
        {
            ExperiencePoints += xp;
        }
    }
}
