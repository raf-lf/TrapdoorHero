using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue;

    public void EnemyDeath()
    {
        GameManager.scriptHud.ChangeScore(scoreValue);
        GameManager.scriptGameplay.EnemyDown();
    }

}
