using UnityEditor;
using UnityEngine;

public class NewScriptableObject : EditorWindow
{
    private static NewScriptableObject _window;
    private AnimalButtonSO _scriptableObject;

    public static void OpenWindow()
    {
        _window = (NewScriptableObject)GetWindow(typeof(NewScriptableObject));
        _window.minSize = new Vector2(500, 300);
        _window.maxSize = new Vector2(500, 300);
        _window._scriptableObject = CreateInstance<AnimalButtonSO>();
        _window.Show();
    }

    private void OnGUI()
    {
        Draw(_scriptableObject);
    }

    private void Draw(AnimalButtonSO scriptableObject)
    {
        GUILayout.BeginVertical();
        {
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name: ", GUILayout.Width(100));
            scriptableObject.animalName = EditorGUILayout.TextField(scriptableObject.animalName);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Sprite: ", GUILayout.Width(100));
            scriptableObject.animalSprite =
                (Sprite)EditorGUILayout.ObjectField(scriptableObject.animalSprite, typeof(Sprite), false);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Description: ", GUILayout.Width(100));
            scriptableObject.animalDescription = EditorGUILayout.TextField(scriptableObject.animalDescription);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Flier: ", GUILayout.Width(100));
            scriptableObject.isFlier = EditorGUILayout.Toggle(scriptableObject.isFlier);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Insect: ", GUILayout.Width(100));
            scriptableObject.isInsect = EditorGUILayout.Toggle(scriptableObject.isInsect);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Omnivore: ", GUILayout.Width(100));
            scriptableObject.isOmnivore = EditorGUILayout.Toggle(scriptableObject.isOmnivore);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Solo Liver: ", GUILayout.Width(100));
            scriptableObject.isSoloLiver = EditorGUILayout.Toggle(scriptableObject.isSoloLiver);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is Mammal: ", GUILayout.Width(100));
            scriptableObject.isMammal = EditorGUILayout.Toggle(scriptableObject.isMammal);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (scriptableObject.animalSprite == null)
            {
                EditorGUILayout.HelpBox("You must select a [SPRITE]!", MessageType.Warning);
            }
            else if (string.IsNullOrEmpty(scriptableObject.animalName))
            {
                EditorGUILayout.HelpBox("You must enter a [NAME]!", MessageType.Warning);
            }
            else if (string.IsNullOrEmpty(scriptableObject.animalDescription))
            {
                EditorGUILayout.HelpBox("You must enter a [DESCRIPTION]!", MessageType.Warning);
            }
            else if (GUILayout.Button("Create", GUILayout.Height(30), GUILayout.Width(100)))
            {
                SaveAndClose();
                _window.Close();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void SaveAndClose()
    {
        string path = "Assets/Resources/AnimalButtonSO/";

        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "AnimalButtonSO");
        }

        path = "Assets/Resources/AnimalButtonSO/" + _scriptableObject.animalName + ".asset";

        AssetDatabase.CreateAsset(_scriptableObject, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}