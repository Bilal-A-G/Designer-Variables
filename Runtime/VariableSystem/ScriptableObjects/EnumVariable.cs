using UnityEngine;

[CreateAssetMenu(fileName = "New Enum", menuName = "Enum Variables")]
public class EnumVariable : GenericVariable<EnumVariable>
{
    new public EnumVariable GetValue => this;

}
