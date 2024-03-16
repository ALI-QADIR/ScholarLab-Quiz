using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreatorWindow : EditorWindow
{
    private Rect _headerRect;
    private Texture2D _headerTexture;
    private Rect _bodyRect;
    private Texture2D _bodyTexture2D;
    private string _csvPath;

    [MenuItem("Window/Scriptable Object Creator")]
    private static void OpenWindow()
    {
        ScriptableObjectCreatorWindow window = (ScriptableObjectCreatorWindow)GetWindow(typeof(ScriptableObjectCreatorWindow));
        window.minSize = new Vector2(300, 200);
        window.maxSize = new Vector2(300, 200);
        window.Show();
    }

    private void OnEnable()
    {
        _headerRect = new Rect();
        _headerTexture = new Texture2D(1, 1);
        _headerTexture.SetPixel(0, 0, new Color(0.13f, 0.13f, 0.13f));
        _headerTexture.Apply();

        _bodyRect = new Rect();
        _bodyTexture2D = new Texture2D(1, 1);
        _bodyTexture2D.SetPixel(0, 0, new Color(0.3f, 0.3f, 0.3f));
        _bodyTexture2D.Apply();
    }

    private void OnGUI()
    {
        DrawLayout();
        DrawHeader();
        DrawBody();
    }

    private void DrawLayout()
    {
        _headerRect.x = 0;
        _headerRect.y = 0;
        _headerRect.width = 300;
        _headerRect.height = 50;

        GUI.DrawTexture(_headerRect, _headerTexture);

        _bodyRect.x = 0;
        _bodyRect.y = 50;
        _bodyRect.width = 300;
        _bodyRect.height = 200 - 50;

        GUI.DrawTexture(_bodyRect, _bodyTexture2D);
    }

    private void DrawHeader()
    {
        GUILayout.BeginArea(_headerRect);
        GUILayout.BeginVertical();
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Scriptable Object Creator", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void DrawBody()
    {
        GUILayout.BeginArea(_bodyRect);

        GUILayout.Space(10);

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Create a new Scriptable Object");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create", GUILayout.Height(30), GUILayout.Width(294)))
        {
            NewScriptableObject.OpenWindow();
        }

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Create Scriptable Object From CSV");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create", GUILayout.Height(30), GUILayout.Width(294)))
        {
            // get the path of the csv file
            string directory = Application.dataPath;
            _csvPath = EditorUtility.OpenFilePanel("Select CSV", directory, "csv");
            CsvToScriptableObject();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void CsvToScriptableObject()
    {
        if (string.IsNullOrEmpty(_csvPath))
        {
            Debug.LogError("No CSV file selected!");
            return;
        }

        string[] lines = File.ReadAllLines(_csvPath);

        var path = "Assets/Resources/AnimalButtonSO";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "AnimalButtonSO");
        }

        Dictionary<string, string> data = new Dictionary<string, string>();
        string[] headers = lines[0].Split(',');
        for (int i = 1; i < lines.Length; i++)
        {
            data.Clear();
            string[] values = lines[i].Split(',');
            for (int j = 0; j < headers.Length; j++)
            {
                data.Add(headers[j], values[j]);
            }
            AnimalButtonSO animalButtonSO = CreateInstance<AnimalButtonSO>();
            animalButtonSO.animalName = data["animalName"];
            animalButtonSO.animalDescription = data["animalDescription"];
            string spritePath = "Assets/Art/AnimalButtonSprites/" + data["animalName"] + ".png";
            animalButtonSO.animalSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath, typeof(Sprite));
            if (animalButtonSO.animalSprite == null)
            {
                spritePath = "Assets/Art/AnimalButtonSprites/Ant.png";
                animalButtonSO.animalSprite = (Sprite)AssetDatabase.LoadAssetAtPath(spritePath, typeof(Sprite));
                Debug.Log(data["animalName"] + " sprite not found. Assigning default sprite.");
            }
            animalButtonSO.isFlier = bool.Parse(data["isFlier"].ToLower());
            animalButtonSO.isInsect = bool.Parse(data["isInsect"].ToLower());
            animalButtonSO.isOmnivore = bool.Parse(data["isOmnivore"].ToLower());
            animalButtonSO.isSoloLiver = bool.Parse(data["isSoloLiver"].ToLower());
            animalButtonSO.isMammal = bool.Parse(data["isMammal"].ToLower());

            path = "Assets/Resources/AnimalButtonSO/" + data["animalName"] + ".asset";
            AssetDatabase.CreateAsset(animalButtonSO, path);
        }

        AssetDatabase.Refresh();
        Debug.Log("Scriptable Objects created successfully!");
    }
}