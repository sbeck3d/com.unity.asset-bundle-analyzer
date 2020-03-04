using UnityEngine;

namespace UnityEditor.AssetBundleAnalyzer
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
        private string m_pattern = "*";
        private bool m_keepTemp;
        private bool m_storeRaw;
        private bool m_debug;
        private bool m_verbose;

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
            
            m_groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", m_groupEnabled);
            m_outputName = EditorGUILayout.TextField("Output database name", m_outputName);
            m_keepTemp = EditorGUILayout.Toggle("Keep temp files", m_keepTemp);
            m_debug = EditorGUILayout.Toggle("Run in debug mode", m_debug);
            m_verbose = EditorGUILayout.Toggle("Run in verbose mode", m_verbose);

            m_storeRaw = EditorGUILayout.Toggle("Store Raw Files", m_storeRaw);
            if ( m_usePattern = EditorGUILayout.BeginToggleGroup("Use Pattern", m_usePattern));
            {
                m_pattern = EditorGUILayout.TextField("Selected pattern", m_pattern);
            }

            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button("Analyze AssetBundle(s)"))
            {
                m_analyzeAssetBundleRequest.CreateDatabase(m_toolsFolderPath, m_assetBundlesPath, m_outputName, m_usePattern, m_pattern, m_keepTemp, m_storeRaw, m_debug, m_verbose);
                EditorUtility.DisplayDialog("AssetBundle analyzer",
                    "Database is saved in Project Folder.", "Close");
            }
        }
    }
}
