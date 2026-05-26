using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
