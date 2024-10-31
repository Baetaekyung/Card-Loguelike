using System;
using UnityEditor;
using UnityEngine;

public class EditorTest : EditorWindow
{
    private string _name, _description;
    private int _cost;
    private TempCardEnum _cardEffect;
    
    private SerializedObject serializedObject;
    private SerializedProperty nameProp;
    private SerializedProperty costProp;
    private SerializedProperty descriptionProp;
    private SerializedProperty enumEffectProp;
    
    [MenuItem("Tools/Utility")]
    public static void ShowWindow()
    {
        GetWindow<EditorTest>("test");
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(CreateInstance<TempCardSO>());
        enumEffectProp = serializedObject.FindProperty("typeEnum");
        nameProp = serializedObject.FindProperty("name");
        costProp = serializedObject.FindProperty("cost");
        descriptionProp = serializedObject.FindProperty("description");
    }

    private void OnGUI()
    {
        EditorGUILayout.PropertyField (nameProp);
        EditorGUILayout.PropertyField(costProp);
        EditorGUILayout.PropertyField (descriptionProp);
        EditorGUILayout.PropertyField(enumEffectProp);

        if (GUILayout.Button("Create Scriptable Object"))
        {
            CreateScriptableObject();
        }
    }

    private void CreateScriptableObject()
    {
        var asset = ScriptableObject.CreateInstance<TempCardSO>();
        
        string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/00SODatas/02 SSH/Created/{nameProp.stringValue}.asset");
        
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();
        
        Debug.Log($"Created ScriptableObject at: {path}");
    }
    
}
