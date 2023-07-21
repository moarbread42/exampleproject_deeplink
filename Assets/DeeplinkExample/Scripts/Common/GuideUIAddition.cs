using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

namespace deeplinkGuide
{
    [CreateAssetMenu(fileName = "Guidedocument", menuName = "Scriptable Object/Guidedocument", order = int.MaxValue)]  //프로젝트 시작할 때 포커스
    public class GuideUIAddition : ScriptableObject
    {
        [SerializeField]
        private string DeeplinkGuide;

        public Texture2D Icon;

        public string Version;
        public bool loadedLayout;
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

        public override void OnInspectorGUI()
        {
            var tgt = (TARGET)target;

            UpdateStyles();
            EditorGUILayout.LabelField("엘포박스 런처 연결을 위한 딥링크씬 세팅 가이드", headerStyle);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Step 1", headerStyle);
            EditorGUILayout.LabelField("\nstep 1 내용\n본문본문본문\n", bodyStyle);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Step 2", headerStyle);
            EditorGUILayout.LabelField("\nstep 2 내용\n본문본문본문\n", bodyStyle);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Step 3", headerStyle);
            EditorGUILayout.LabelField("\nstep 3 내용\n본문본문본문\n", bodyStyle);

            EditorGUILayout.Separator();

            
            EditorGUILayout.LabelField("\n유니티 공식 홈페이지에서 더 자세한 가이드를 보실 수 있습니다.", bodyStyle);
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
    }
}
#endif

