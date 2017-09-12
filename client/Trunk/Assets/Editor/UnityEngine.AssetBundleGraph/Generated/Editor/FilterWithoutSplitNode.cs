using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine.AssetBundles.GraphTool;
using Model=UnityEngine.AssetBundles.GraphTool.DataModel.Version2;

[CustomNode("Custom/Filter Out Type", 1000)]
public class FilterByType : IFilter
{
    [SerializeField] private HashSet<string> m_fliterKeyType;

    public string Label
    {
        get { return "Filter Out"; }
    }

    public FilterByType()
    {
        m_fliterKeyType = new HashSet<string>();
    }

    public bool FilterAsset(AssetReference a)
    {
        var assumedType = a.filterType;
        var match = assumedType != null && !m_fliterKeyType.Contains(assumedType.ToString());
        return match;
    }

    public void OnInspectorGUI(Action onValueChanged)
    {
        GUIStyle s = new GUIStyle((GUIStyle)"label");

        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Not Included:", s, GUILayout.Width(120));
            if (GUILayout.Button(m_fliterKeyType.Count == 0 ? "None" : "Mixed...", "Popup"))
            {
                NodeGUI.ShowFliterKeyTypeMenu(
                    "None", m_fliterKeyType.Count == 0, m_fliterKeyType,
                    (string selectedTypeStr) => {

                        if (selectedTypeStr == "None")
                        {
                            m_fliterKeyType.Clear();
                            onValueChanged();
                            return;
                        }

                        if (selectedTypeStr == Model.Settings.DEFAULT_FILTER_KEYTYPE)
                        {
                            m_fliterKeyType.Clear();
                            m_fliterKeyType = new HashSet<string>(TypeUtility.KeyTypes);
                            onValueChanged();
                            return;
                        }

                        if (m_fliterKeyType.Contains(selectedTypeStr))
                        {
                            m_fliterKeyType.Remove(selectedTypeStr);
                        }
                        else
                        {
                            m_fliterKeyType.Add(selectedTypeStr);
                        }
                        onValueChanged();
                    }
                );
            }
        }
    }
}
