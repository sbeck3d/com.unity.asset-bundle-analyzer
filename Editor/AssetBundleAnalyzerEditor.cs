using UnityEngine;

namespace UnityEditor.Support.AssetBundleAnalyzer
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetBundleAnalyzerEditorWindow : EditorWindow
    {
        private AnalyzeAssetBundleRequest m_analyzeAssetBundleRequest = new AnalyzeAssetBundleRequest();
        private string m_toolsFolderPath = "/Applications/Unity/Unity.app/Contents/Tools";
        private string m_assetBundlesPath = "/AssetBundles";

        private bool m_usePattern;
        // add a string here to handle pattern

        private bool m_groupEnabled;
        private string m_outputName = "database";
        private bool m_keepTemp;
        private bool m_storeRaw;
        
        [MenuItem("Window/Analysis/AssetBundle Analyzer")]
        private static void InitWindow()
        {
            var window = (AssetBundleAnalyzerEditorWindow) GetWindow(typeof(AssetBundleAnalyzerEditorWindow));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("AssetBundle Analyzer Settings", EditorStyles.boldLabel);
            
            EditorGUILayout.Space();
            m_assetBundlesPath = EditorGUILayout.TextField("AssetBundle Path", m_assetBundlesPath);
            if (GUILayout.Button("Select AssetBundle path"))
            {
                m_assetBundlesPath = EditorUtility.OpenFolderPanel("AssetBundle Path", "", "");
            }
            
            EditorGUILayout.Space();
            m_toolsFolderPath = EditorGUILayout.TextField("Unity tools path", EditorApplication.applicationContentsPath + "/Tools");
            if (GUILayout.Button("Select Unity tools path"))
            {
                m_toolsFolderPath = EditorUtility.OpenFolderPanel("Unity tools path", "", "");
            }
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            // Features TODO
            //m_groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", m_groupEnabled);
            //m_outputName = EditorGUILayout.TextField("Output Database Name", m_outputName);
            //m_usePattern = EditorGUILayout.Toggle("Use Pattern", m_usePattern);
            //m_keepTemp = EditorGUILayout.Toggle("Keep Temp Files", m_keepTemp);
            //m_storeRaw = EditorGUILayout.Toggle("Store Raw Files", m_storeRaw);
            //EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button("Analyze AssetBundle(s)"))
            {
                m_analyzeAssetBundleRequest.CreateDatabase(m_toolsFolderPath, m_assetBundlesPath);
                EditorUtility.DisplayDialog("AssetBundle analyzer",
                    "Database is saved in Project Folder.", "Close");
            }
        }
    }
}
