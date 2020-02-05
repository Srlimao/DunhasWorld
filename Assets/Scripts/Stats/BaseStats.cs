using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{

    public class BaseStats : MonoBehaviour
    {
        [Range(1,100)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        private int currentLevel = 1;
        public float GetHealth()
        {
            return progression.GetStat(characterClass,Stat.Health, currentLevel);
        }
        public float GetExperienceReward()
        {
            return progression.GetStat(characterClass,Stat.ExperienceReward ,currentLevel);
        }
    }

}