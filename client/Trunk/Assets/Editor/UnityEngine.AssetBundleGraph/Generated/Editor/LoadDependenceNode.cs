using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine.AssetBundles.GraphTool;
using Model=UnityEngine.AssetBundles.GraphTool.DataModel.Version2;

[CustomNode("Custom/LoadDependence", 1000)]
public class MyNode : Node {

	[SerializeField] private SerializableMultiTargetString m_myValue;

	public override string ActiveStyle {
		get {
			return "node 8 on";
		}
	}

	public override string InactiveStyle {
		get {
			return "node 8";
		}
	}

	public override string Category {
		get {
			return "Custom";
		}
	}

	public override void Initialize(Model.NodeData data) {
		m_myValue = new SerializableMultiTargetString();
		data.AddDefaultInputPoint();
		data.AddDefaultOutputPoint();
	}

	public override Node Clone(Model.NodeData newData) {
		var newNode = new MyNode();
		newNode.m_myValue = new SerializableMultiTargetString(m_myValue);
		newData.AddDefaultInputPoint();
		newData.AddDefaultOutputPoint();
		return newNode;
	}

	public override void OnInspectorGUI(NodeGUI node, AssetReferenceStreamManager streamManager, NodeGUIEditor editor, Action onValueChanged) {

		EditorGUILayout.HelpBox("My Custom Node: Implement your own Inspector.", MessageType.Info);
		editor.UpdateNodeName(node);

		GUILayout.Space(10f);

		//Show target configuration tab
		editor.DrawPlatformSelector(node);
		using (new EditorGUILayout.VerticalScope(GUI.skin.box)) {
			// Draw Platform selector tab. 
			var disabledScope = editor.DrawOverrideTargetToggle(node, m_myValue.ContainsValueOf(editor.CurrentEditingGroup), (bool b) => {
				using(new RecordUndoScope("Remove Target Platform Settings", node, true)) {
					if(b) {
						m_myValue[editor.CurrentEditingGroup] = m_myValue.DefaultValue;
					} else {
						m_myValue.Remove(editor.CurrentEditingGroup);
					}
					onValueChanged();
				}
			});
		}
	}

	/**
	 * Prepare is called whenever graph needs update. 
	 */ 
	public override void Prepare (BuildTarget target, 
		Model.NodeData node, 
		IEnumerable<PerformGraph.AssetGroups> incoming, 
		IEnumerable<Model.ConnectionData> connectionsToOutput, 
		PerformGraph.Output Output) 
	{
		// Pass incoming assets straight to Output
		if(Output != null) {
			var destination = (connectionsToOutput == null || !connectionsToOutput.Any())? 
				null : connectionsToOutput.First();

			if(incoming != null) {
				foreach(var ag in incoming) {
                    LoadDependence(ag.assetGroups);
                    Output(destination, ag.assetGroups);
				}
			} else {
				// Overwrite output with empty Dictionary when no there is incoming asset
				Output(destination, new Dictionary<string, List<AssetReference>>());
			}
		}
	}

    private void LoadDependence(Dictionary<string, List<AssetReference>> assetGroups)
    {
        Dictionary<string, AssetReference> map = new Dictionary<string, AssetReference>();
        List<AssetReference> list = new List<AssetReference>();
        HashSet<string> rootSet = new HashSet<string>();
        foreach (var refs in assetGroups.Values)
        {
            foreach (var refence in refs)
            {
                if(!rootSet.Contains(refence.path))
                {
                    rootSet.Add(refence.path);
                }
            }
        }
        foreach (var refs in assetGroups.Values)
        {
            foreach (var refence in refs)
            {
                foreach (var path in AssetDatabase.GetDependencies(refence.path))
                {
                    var type = AssetDatabase.GetMainAssetTypeAtPath(path);
                    if (!map.ContainsKey(path) && !rootSet.Contains(path))
                    {
                        map[path] = AssetReference.CreateReference(path, type);
                    }
                }
            }
        }
        assetGroups["dep"] = map.Values.ToList();
    }

    /**
	 * Build is called when Unity builds assets with AssetBundle Graph. 
	 */
    public override void Build (BuildTarget target, 
		Model.NodeData nodeData, 
		IEnumerable<PerformGraph.AssetGroups> incoming, 
		IEnumerable<Model.ConnectionData> connectionsToOutput, 
		PerformGraph.Output outputFunc,
		Action<Model.NodeData, string, float> progressFunc)
	{
		// Do nothing
	}
}
