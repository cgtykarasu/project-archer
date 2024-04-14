using Interfaces;
using UnityEngine;

public class Target : MonoBehaviour, IScorable
{
    [SerializeField] int scoreValue = 10;

    public int ScoreValue => scoreValue;
}