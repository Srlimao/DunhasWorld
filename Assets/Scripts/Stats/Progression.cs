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

        private Dictionary<CharacterClass, Dictionary<Stat, float[]>> innerClasses;


        private bool OnValidate()
        {
            innerClasses = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass c in characterClasses)
            {
                Dictionary<Stat, float[]> innerStats = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat p in c.stats)
                {
                    innerStats.Add(p.stat, p.levels);
                }
                innerClasses.Add(c.charClass, innerStats);
            }
            return true;
        }

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
        }

        public float GetExperienceReward(CharacterClass characterClass, int currentLevel)
        {
            return innerClasses[characterClass][Stat.ExperienceReward][currentLevel - 1];
        }

        public float GetHealth(CharacterClass characterClass, int currentLevel)
        {
            return innerClasses[characterClass][Stat.Health][currentLevel - 1];
        }
    }
}