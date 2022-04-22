
[System.Serializable]
public class GenericReference<T>
{
    public bool useOverride;

    public GenericVariable<T> variableValue;
    public T overrideValue;

    public T GetValue() => useOverride ? overrideValue : variableValue.GetValue;
}
