using UnityEngine;
using System.Collections;

public class PersistentData : Singleton<PersistentData> {

    private GameState _actualGameState;

    public GameState CurrentState { get { return _actualGameState; } set { _actualGameState = value; } }

    public override void Awake()
    {
        base.Awake();
        _actualGameState = _actualGameState ?? new GameState();
    }
}
