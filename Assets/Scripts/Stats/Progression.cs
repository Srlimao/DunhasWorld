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
        public class ProgressionCharacterClass
        {
            public CharacterClass charClass;
            public float[] healthAtLevel;
            public float[] damageAtLevel;

            public float GetHealth(int level)
            {
                int levelToIndex = level - 1;
                return healthAtLevel.Length>= levelToIndex? healthAtLevel[levelToIndex] : healthAtLevel[healthAtLevel.Length-1];
            }
        }

        public float GetHealth(CharacterClass characterClass, int currentLevel)
        {
            return characterClasses.Find(x => x.charClass == characterClass).GetHealth(currentLevel);
        }
    }
}