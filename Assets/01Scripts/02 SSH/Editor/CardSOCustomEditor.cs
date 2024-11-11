using System;
using UnityEditor;
using UnityEngine;

public class CardSOCustomEditor : EditorWindow
{
    private CardDataSO _dataSO;

    public CardType cardType;
    public CardInfo cardInfo;

    public GameObject _prefab;

    [MenuItem("Tools/Utility")]
    public static void ShowWindow()
    {
        GetWindow<CardSOCustomEditor>("test");
    }

    private void OnEnable()
    {
        _dataSO = CreateInstance<CardDataSO>(); // _dataSO 초기화
    }

    private void OnGUI()
    {
        // CardInfo의 필드 입력
        cardInfo.cardName = EditorGUILayout.TextField("Name", cardInfo.cardName);
        cardInfo.cardDescription = EditorGUILayout.TextField("Description", cardInfo.cardDescription);
        // cardInfo.cardEffectDescription = EditorGUILayout.TextField("EffectDescription", cardInfo.cardEffectDescription);
        cardInfo.cost = EditorGUILayout.IntField("Cost", cardInfo.cost);
        cardInfo.cardSprite = (Sprite)EditorGUILayout.ObjectField("sprite", cardInfo.cardSprite, typeof(Sprite), false);
        cardInfo.cardRarity = (CardRarity)EditorGUILayout.EnumPopup("Rarity", cardInfo.cardRarity);
        
        
        // CardType 입력
        cardType = (CardType)EditorGUILayout.EnumPopup("Type", cardType);

        if (GUILayout.Button("Create Scriptable Object"))
        {
            CreateScriptableObject();
        }
        if (GUILayout.Button("Create Prefab and SO"))
        {
            CreatePrefab();
        }
    }

    private void CreatePrefab()
    {
        if (_prefab == null)
        {
            Debug.LogError("Prefab reference is null. Please assign a prefab.");
            return;
        }

        var asset = CreateInstance<CardDataSO>();
        asset.cardInfo = cardInfo;
        asset.cardType = cardType;

//todo : use const variable to read directory => ex. const str identifier = "literal";
        string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/00SODatas/02 SSH/Created/{cardInfo.cardName}.asset");

        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

//todo : use const variable to read directory => ex. const str identifier = "literal";
        path = AssetDatabase.GenerateUniqueAssetPath($"Assets/03Prefabs/02 SSH/Created/{cardInfo.cardName}prefab.prefab");

        // Instantiate a new prefab instance
        GameObject g = PrefabUtility.InstantiatePrefab(_prefab) as GameObject;
        if (g != null)
        {

//conflict. changed CardData to protected.
//g.GetComponent<SkillCard>().CardData = asset;
            PrefabUtility.SaveAsPrefabAssetAndConnect(
                g,
                path,
                InteractionMode.AutomatedAction
            );
            DestroyImmediate(g); // Instantiate된 프리팹은 에디터에서 제거
        }
        else
        {
            Debug.LogError("Failed to instantiate prefab.");
        }

        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();

        Debug.Log($"Created ScriptableObject at: {path}");
    }


    private void CreateScriptableObject()
    {
        // 새로운 ScriptableObject 인스턴스 생성
        var asset = CreateInstance<CardDataSO>();
        asset.cardInfo = cardInfo;
        asset.cardType = cardType;

        // 파일 경로 생성
//todo : use const variable to read directory => ex. const str identifier = "literal";
        string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/00SODatas/02 SSH/Created/{cardInfo.cardName}.asset");

        // ScriptableObject 저장
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // 새로 생성된 ScriptableObject를 선택하고 포커스
        Selection.activeObject = asset;
        EditorUtility.FocusProjectWindow();

        Debug.Log($"Created ScriptableObject at: {path}");
    }
}