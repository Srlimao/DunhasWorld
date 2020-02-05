using UnityEngine;
using System.Collections;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public void GainExperience(float xp)
        {
            experiencePoints += xp;
        }
    }
}
