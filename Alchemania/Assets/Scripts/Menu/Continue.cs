using UnityEngine;
using System.Collections;

public class Continue : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Sprite Default;
    public Sprite Hover;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        _renderer.sprite = Hover;
        if (Input.GetMouseButtonDown(0))
        {
            GameState gameState = LevelManager.Instance.LoadGameState();
            PersistentData.Instance.CurrentState = gameState;
            if (gameState != null)
            {
                LevelManager.Instance.IsNewLevel = false;
                LevelManager.Instance.LoadSceneFromContinueMenu(gameState);
            }
        }
    }

    public void OnMouseExit()
    {
        _renderer.sprite = Default;
    }
}
