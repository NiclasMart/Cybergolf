
using UnityEngine;

[CreateAssetMenu]
public class IntegerVariable : ScriptableObject {

    public int value;

    public void SetValue(int _value)
    {
        value = _value;
    }

    public void SetValue(IntegerVariable _value)
    {
        value = _value.value;
    }

    public void ApplyChange(int amount)
    {
        value += amount;
    }

    public void ApplyChange(IntegerVariable amount)
    {
        value += amount.value;
    }

}
