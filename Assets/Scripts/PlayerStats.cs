using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class PlayerStats : BaseStats
{
    public int Score;

    public PlayerStats(int lifePoint, int score) : base(lifePoint)
    {
        Score = score;
    }

    public void SetLifePoint(int value) => LifePoint += value;
    public void SetScore(int value) => Score += value;
}
