using UnityEditor;
using UnityEngine;

public class GenericReferenceDrawerWrapper<T> : PropertyDrawer
{
    public bool isFolded;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty useOverride = property.FindPropertyRelative("useOverride");
        SerializedProperty overrideValue = property.FindPropertyRelative("overrideValue");
        SerializedProperty variableValue = property.FindPropertyRelative("variableValue");

        if (EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), isFolded, label, true))
        {
            isFolded = true;
            EditorGUI.indentLevel = 1;

            var typeToInstantiate = typeof(GenericReference<T>);
            string fullTypeName = typeToInstantiate.GenericTypeArguments[0].ToString();

            string[] seperators = new string[] { "." };
            string[] typeNameSegments = fullTypeName.Split(seperators, System.StringSplitOptions.None);


            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), useOverride);

            if (useOverride.boolValue)
            {
                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), overrideValue, new GUIContent("Override" + "<" + GetAlias(typeNameSegments) + ">"));
            }
            else
            {

                EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight), variableValue, new GUIContent("Variable" + "<" + GetAlias(typeNameSegments) + ">"));
            }
        }
        else
        {
            isFolded = false;
        }

        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (isFolded)
        {
            return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight * 2;
        }

        return base.GetPropertyHeight(property, label);

    }

    public virtual string GetAlias(string[] fullName) { return fullName[fullName.Length - 1]; }
}

[CustomPropertyDrawer(typeof(GenericReference<int>))]
public class GenericRefrenceIntDrawer : GenericReferenceDrawerWrapper<int>
{
    public override string GetAlias(string[] fullName) => "Int";
}

[CustomPropertyDrawer(typeof(GenericReference<float>))]
public class GenericRefrenceFloatDrawer : GenericReferenceDrawerWrapper<float>
{
    public override string GetAlias(string[] fullName) => "Float";
}

[CustomPropertyDrawer(typeof(GenericReference<string>))]
public class GenericRefrenceStringDrawer : GenericReferenceDrawerWrapper<string>
{

}

[CustomPropertyDrawer(typeof(GenericReference<Object>))]
public class GenericRefrenceObjDrawer : GenericReferenceDrawerWrapper<Object>
{

}

[CustomPropertyDrawer(typeof(GenericReference<EnumVariable>))]
public class GenericRefrenceEnumDrawer : GenericReferenceDrawerWrapper<EnumVariable>
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty overrideValue = property.FindPropertyRelative("overrideValue");

        if (EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), isFolded, label, true))
        {
            isFolded = true;
            EditorGUI.indentLevel = 1;

            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), overrideValue, new GUIContent("Override" + "<EnumVariable>"));
        }
        else
        {
            isFolded = false;
        }

        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (isFolded)
        {
            return base.GetPropertyHeight(property, label) - EditorGUIUtility.singleLineHeight;
        }

        return base.GetPropertyHeight(property, label);

    }
}
