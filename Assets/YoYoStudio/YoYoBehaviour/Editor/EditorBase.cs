using System.IO;
using UnityEditor;
using UnityEngine;

namespace YoYoStudio.Behaviour
{
    [CanEditMultipleObjects]
    public class EditorBase : Editor
    {
        Texture logo;
        string title;
        Color orgColor;
        float ratio;
        MonoScript monoScript;

        protected Color greyColor;
        protected Color greyColor2;
        bool colorInited = false;

        private void Init()
        {
            if (logo == null)
                logo = Resources.Load<Texture>("YoYoStudioLogo");

            if (logo != null)
                ratio = (logo.height / (float)logo.width);

            if (!colorInited)
            {
                greyColor = new Color(0.7f, 0.7f, 0.7f);
                greyColor2 = new Color(0.8f, 0.8f, 0.8f);
                colorInited = true;
            }
        }
        //*****************************************
        protected void SetTitle(string title)
        {
            this.title = title;
        }
        //*****************************************
        protected void SetMonoScript(MonoScript monoScript)
        {
            this.monoScript = monoScript;
        }
        //*****************************************
        protected void DrawLogoGUI()
        {
            Init();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                GUIStyle style = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    imagePosition = ImagePosition.ImageOnly,
                };

                float width = EditorGUIUtility.currentViewWidth - 42;
                float validWidth = (width > logo.width) ? logo.width : width;
                float height = validWidth * ratio;
                GUILayout.Label(logo, style, GUILayout.Width(width), GUILayout.Height(height), GUILayout.ExpandWidth(true));
                orgColor = GUI.backgroundColor;
                GUI.backgroundColor = greyColor;

                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        style.fontStyle = FontStyle.Bold;
                        style.imagePosition = ImagePosition.TextOnly;
                        style.alignment = TextAnchor.MiddleLeft;
                        style.wordWrap = true;
                        EditorGUILayout.LabelField(title, style, GUILayout.Height(20));

                        if (monoScript != null)
                        {
                            GUI.backgroundColor = orgColor;

                            GUIContent content = MakeContent("Select", "Select Script");
                            if (GUILayout.Button(content, GUILayout.MaxWidth(49), GUILayout.Height(18)))
                                EditorGUIUtility.PingObject(monoScript);

                            content = MakeContent("Edit", "Edit Script");
                            if (GUILayout.Button(content, GUILayout.MaxWidth(40), GUILayout.Height(18)))
                                AssetDatabase.OpenAsset(monoScript);
                        }
                    }
                    EditorGUILayout.EndHorizontal();


                }
                EditorGUILayout.EndVertical();
                GUI.backgroundColor = orgColor;
            }
            EditorGUILayout.EndVertical();
        }
        //*****************************************
        protected void DrawHeader(string title, Color color, bool centerText = false)
        {
            GUI.backgroundColor = color;
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.fontStyle = FontStyle.Bold;
                if (centerText)
                    style.alignment = TextAnchor.MiddleCenter;

                EditorGUILayout.LabelField(title, style);
            }
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = orgColor;
            GUILayout.Space(3);
        }
        //****************************************
        protected void DrawFoldOutHeader(ref bool foldOut, string title, Color color, bool openEnd = false)
        {
            GUI.backgroundColor = color;
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            {
                EditorGUI.indentLevel++;
                {
                    GUIStyle style = EditorStyles.foldout;
                    style.fontStyle = FontStyle.Bold;
                    style.alignment = TextAnchor.MiddleLeft;
                    style.fixedHeight = 20;
                    foldOut = EditorGUILayout.Foldout(foldOut, title, true, style);
                }
                EditorGUI.indentLevel--;
            }
            if (!openEnd)
                EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = orgColor;
        }
        //****************************************
        protected GUIContent MakeContent(string text, string tooltip)
        {
            return new GUIContent(text.Trim(), tooltip);
        }
    }
}
