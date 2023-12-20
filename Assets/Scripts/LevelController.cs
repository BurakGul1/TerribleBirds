using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private Monster[] _monsters;
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    void Update()
    {
        if (MonsterArealAllDead())
        {
            GoToNextLevel();
        }
    }

    private bool MonsterArealAllDead()
    {
        foreach (var item in _monsters)
        {
            if (item.gameObject.activeSelf)
            {
                return false;
            }

        }
        return true;
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
