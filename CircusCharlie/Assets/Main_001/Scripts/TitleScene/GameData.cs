using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Create GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int life = 3;
    public int score_Stage1;
    public int score_Stage2;
    public int bestScore_Stage1;
    public int bestScore_Stage2;
}
