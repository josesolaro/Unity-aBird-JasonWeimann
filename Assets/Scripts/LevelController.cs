using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    string _nextLevelName;

    Monster[] _monsters;
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }
    private void Update()
    {
        if (MonsterAreAllDead())
        {
            GoToNextLevel();
        }
    }

    private bool MonsterAreAllDead()
    {
        return !_monsters.Any(m => m.gameObject.activeSelf);
    }
    private void GoToNextLevel()
    {
        Debug.Log($"Go to level {_nextLevelName}");
        SceneManager.LoadScene(_nextLevelName);
    }
}
