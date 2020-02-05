using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] List<ProgressionCharacterClass> characterClasses;

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass charClass;
            public List<ProgressionStat> stats;
            /*
            public float[] healthAtLevel;
            public float[] damageAtLevel;
            */
            internal float GetHealth(int level)
            {
                int levelToIndex = level - 1;
                return stats.Find(x => x.stat == Stat.Health).levels[level];

                //healthAtLevel.Length>= levelToIndex? healthAtLevel[levelToIndex] : healthAtLevel[healthAtLevel.Length-1];
            }

            internal float GetXpReward(int currentLevel)
            {
                int levelToIndex = currentLevel - 1;
                return stats.Find(x => x.stat == Stat.ExperienceReward).levels[currentLevel];
            }
        }

        



        public float GetExperienceReward(CharacterClass characterClass, int currentLevel)
        {
            return characterClasses.Find(x => x.charClass == characterClass).GetXpReward(currentLevel);
        }

        public float GetHealth(CharacterClass characterClass, int currentLevel)
        {
            return characterClasses.Find(x => x.charClass == characterClass).GetHealth(currentLevel);
        }
    }
}