using UnityEngine;

public class GenericVariable<T> : ScriptableObject
{
    [SerializeField]
    T value;

    public T GetValue => value;
}
