using UnityEngine;

public abstract class State : ScriptableObject
{ 
    public bool IsFinished {get ; protected set; }
    [HideInInspector]public Character Character;

    public abstract void Init();

    public abstract void Run();

    public abstract void Exit();
}
