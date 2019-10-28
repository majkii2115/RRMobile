using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        public event Action onXPGained;
        [SerializeField] float xpPoints = 0;
        public void GainXP(float xp)
        {
            xpPoints += xp;
            onXPGained();
        }
        public object CaptureState()
        {
           return xpPoints;
        }
        public float GetMaxXP()
        {
            return GetComponent<BaseStats>().GetStat(Stat.XPToLevelUp);
        }

        public void RestoreState(object state)
        {
            xpPoints = (float)state;
        }
        public float GetPoints()
        {
            return xpPoints;
        }
    }
}