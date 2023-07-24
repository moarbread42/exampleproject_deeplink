using UnityEngine;
using System;

namespace deeplinkGuide
{
    [CreateAssetMenu(fileName = "Guidedocument", menuName = "Scriptable Object/Guidedocument", order = int.MaxValue)]  //프로젝트 시작할 때 포커스
    public class GuideUIAddition : ScriptableObject
    {
        [SerializeField]
        private string DeeplinkGuide;

        public Texture2D Icon;
        public Section[] sections;
        public Texture2D Icon2;
        public string Version;
        public bool loadedLayout;
        [Serializable]
        public class Section
        {
            public string heading, text, linkText, url;public Texture2D pic;
        }
    }
}
#if UNITY_EDITOR
namespace deeplinkGuide
{
    using UnityEditor;
    using TARGET = GuideUIAddition;
    [CustomEditor(typeof(TARGET))]
    public class GuideUI_Inspector:Editor
    {
        static string kShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";

        static float kSpace = 16f;
        static GuideUI_Inspector()
        {
            EditorApplication.delayCall += SelectReadmeAutomatically;
        }

        private static GUIStyle titleStyle;

        private static GUIStyle headerStyle;

        private static GUIStyle bodyStyle;

        private static GUIStyle rateStyle;
        static void SelectReadmeAutomatically()
        {
            if (!SessionState.GetBool(kShowedReadmeSessionStateName, false))
            {
                var readme = SelectReadme();
                SessionState.SetBool(kShowedReadmeSessionStateName, true);
                //if (readme && !readme.loadedLayout)
                //{
                //    LoadLayout();
                //    readme.loadedLayout = true;
                //}
            }
        }
        //static void LoadLayout()
        //{
        //    var assembly = typeof(EditorApplication).Assembly;
        //    var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
        //    var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
        //    method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "DeeplinkExample/Layout.wlt"), false });
        //}
        [MenuItem("DeeplinkExample/Show Tutorial Instructions")]
        static TARGET SelectReadme()
        {
            var ids = AssetDatabase.FindAssets("GuideDocument t: GuideUIAddition");
            if (ids.Length == 1)
            {
                var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

                Selection.objects = new UnityEngine.Object[] { readmeObject };

                return (TARGET)readmeObject;
            }
            else
            {
                Debug.Log("Couldn't find a GuideDocument");
                return null;
            }
        }
        public static void UpdateStyles()
        {
            if (bodyStyle == null)
            {
                bodyStyle = new GUIStyle(EditorStyles.label);
                bodyStyle.wordWrap = true;
                bodyStyle.fontSize = 14;

                titleStyle = new GUIStyle(bodyStyle);
                titleStyle.fontSize = 26;
                titleStyle.alignment = TextAnchor.MiddleCenter;

                headerStyle = new GUIStyle(bodyStyle);
                headerStyle.fontSize = 18;

                rateStyle = new GUIStyle(EditorStyles.toolbarButton);

                rateStyle.fontSize = 20;
            }
        }

        //테스트 종료 후 
        //안드로이드 매니페스트 수정


        public override void OnInspectorGUI()
        {
            var tgt = (TARGET)target;
            var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);
            var screenWidth = 750f;
            UpdateStyles();
            EditorGUILayout.LabelField("안드로이드 빌드 환경 기준입니다.", bodyStyle);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("엘포박스 런처 연결을 위한 딥링크씬 세팅 가이드", headerStyle);
            EditorGUILayout.Separator();
            if (tgt.Icon2 != null)  //스크린샷 여기에 추가 &스크린샷을 클릭하면 큰 이미지로 불러오기 
            {
                GUILayout.Label(tgt.Icon2, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(screenWidth), GUILayout.Height(screenWidth));
            }
            EditorGUILayout.LabelField("Step 1", headerStyle);
            EditorGUILayout.LabelField("기존 씬 앞에 새로운 씬을 추가합니다. 이 씬에 DeepLinktoScene.cs 가 추가된 오브젝트를 첨부합니다.", bodyStyle);

            EditorGUILayout.Separator();
            if (tgt.Icon2 != null)
            {
                GUILayout.Label(tgt.Icon2, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(screenWidth), GUILayout.Height(screenWidth));
            }
            EditorGUILayout.LabelField("Step 2", headerStyle);
            EditorGUILayout.LabelField("\nstep 2 내용\n본문본문본문\n", bodyStyle);

            EditorGUILayout.Separator();
            if (tgt.Icon2 != null)
            {
                GUILayout.Label(tgt.Icon2, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(screenWidth), GUILayout.Height(screenWidth));
            }
            EditorGUILayout.LabelField("Step 3", headerStyle);
            EditorGUILayout.LabelField("\nstep 3 내용\n본문본문본문\n", bodyStyle);

            EditorGUILayout.Separator();

            if (tgt.Icon2 != null)
            {
                GUILayout.Label(tgt.Icon2, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(screenWidth), GUILayout.Height(screenWidth));
            }
            EditorGUILayout.LabelField("\n유니티 공식 홈페이지에서 더 자세한 가이드와 예제 코드를 둘러볼 수 있습니다.", bodyStyle);
            EditorGUILayout.Separator();
            if (GUILayout.Button(new GUIContent("딥링크 도큐먼트 보기", "웹브라우저로 연결")) == true)
            {
                Application.OpenURL("https://docs.unity3d.com/kr/2021.1/Manual/enabling-deep-linking.html");
            }

            if (GUILayout.Button(new GUIContent("외부연결", "웹브라우저로 연결")) == true)
            {
                Application.OpenURL("https://docs.unity3d.com/kr/2021.1/Manual/enabling-deep-linking.html");
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            
    
           
            var readme = (TARGET)target;
       
            foreach (var section in readme.sections)
            {
                if (!string.IsNullOrEmpty(section.heading))
                {
                    GUILayout.Label(section.heading, headerStyle);
                }
                if (!string.IsNullOrEmpty(section.text))
                {
                    GUILayout.Label(section.text, bodyStyle);
                }
                if (!string.IsNullOrEmpty(section.linkText))
                {
                    if (LinkLabel(new GUIContent(section.linkText)))
                    {
                        Application.OpenURL(section.url);
                    }
                }
                GUILayout.Space(kSpace);
            }
        }

        protected override void OnHeaderGUI()
        {
            var tgt = (TARGET)target;

            UpdateStyles();

            GUILayout.BeginHorizontal("In BigTitle");
            {
                var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);
                var content = new GUIContent("딥링크 씬 세팅 v"+tgt.Version);
                var height = Mathf.Max(titleStyle.CalcHeight(content, EditorGUIUtility.currentViewWidth - iconWidth), iconWidth);

                if (tgt.Icon != null)
                {
                    GUILayout.Label(tgt.Icon, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
                }

                GUILayout.Label(content, titleStyle, GUILayout.Height(height));
            }
            GUILayout.EndHorizontal();
        }
        bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
        {
            //m_LinkStyle = new GUIStyle(m_BodyStyle);
            //m_LinkStyle.wordWrap = false;
            //// Match selection color which works nicely for both light and dark skins
            //m_LinkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
            //m_LinkStyle.stretchWidth = false;
            var position = GUILayoutUtility.GetRect(label,bodyStyle, options);

            Handles.BeginGUI();
            Handles.color = Color.blue;
            Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
            Handles.color = Color.white;
            Handles.EndGUI();

            EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

            return GUI.Button(position, label, bodyStyle);
        }
    }
}
#endif

