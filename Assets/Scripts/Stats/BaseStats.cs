using System;
using GameDevTV.Utils;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour 
    {
        [Range(1,99)][SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject lvlUpPartivleEffect = null;
        public event Action onLevelUp;
        LazyValue<int> currentLevel;
        Experience experience;
        private void Awake() 
        {
            experience = GetComponent<Experience>();      
            currentLevel = new LazyValue<int>(CalculateLevel);     
        }
        private void Start()
        {
            currentLevel.ForceInit();
        }   
        private void OnEnable() 
        {
            if(experience != null)
            {
                experience.onXPGained += UpdateLevel;
            }
        }
        private void OnDisable() 
        {
            if(experience != null)
            {
                experience.onXPGained -= UpdateLevel;
            }
        }
        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LvlUpEffect();
                onLevelUp();
            }
        }

        private void LvlUpEffect()
        {
            Instantiate(lvlUpPartivleEffect, transform);
        }
        

        public int GetLevel()
        {
            return currentLevel.value;
        }
        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifier(stat) * (1 + GetPercentageModifier(stat)/100);
        }

        private float GetPercentageModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifier(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            //enemy has starting level
            if(experience == null) return startingLevel;
            float currentXP = experience.GetPoints();
            int MaxLevel = progression.GetLevels(Stat.XPToLevelUp, characterClass);
            for (int level = 1; level <= MaxLevel; level++)
            {
                float levelXP = progression.GetStat(Stat.XPToLevelUp, characterClass, level);
                if(levelXP > currentXP )
                {
                    return level;
                }
            }
            return MaxLevel + 1;
        }
    }
}