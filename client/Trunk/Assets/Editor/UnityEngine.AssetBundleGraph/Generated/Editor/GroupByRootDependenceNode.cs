using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Monster.BaseSystem;
using UnityEngine.AssetBundles.GraphTool;
using Model=UnityEngine.AssetBundles.GraphTool.DataModel.Version2;

[CustomNode("Custom/Group By Root DependenceNode", 1000)]
public class GroupByRootDependenceNode : Node
{

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
        var newNode = new GroupByRootDependenceNode();
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

			// Draw tab contents
			using (disabledScope) {
				var val = m_myValue[editor.CurrentEditingGroup];

				var newValue = EditorGUILayout.TextField("My Value:", val);
				if (newValue != val) {
					using(new RecordUndoScope("My Value Changed", node, true)){
						m_myValue[editor.CurrentEditingGroup] = newValue;
						onValueChanged();
					}
				}
			}
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
					Output(destination, Group(ag.assetGroups));
				}
			} else {
				// Overwrite output with empty Dictionary when no there is incoming asset
				Output(destination, new Dictionary<string, List<AssetReference>>());
			}
		}
	}

    private Dictionary<string , AssetReference>  mRootMap =  new Dictionary<string, AssetReference>();

    private new Dictionary<string, List<AssetReference>> Group(Dictionary<string, List<AssetReference>> assetGroups)
    {
        var output = new Dictionary<string, List<AssetReference>>();

        //准备
        var allMap = new Dictionary<string, AssetReference>();
        mRootMap.Clear();
        var rootList = assetGroups["root"];
        foreach (var reference in rootList)
        {
            var path = reference.path;
            mRootMap.Add(path, reference);
            allMap.Add(path, reference);
        }

        List<AssetReference> depList = null;
        if (assetGroups.ContainsKey("dep"))
        {
            depList = assetGroups["dep"];
        }

        for (int i = 0, count = depList.Count; i < count; i++)
        {
            var reference = depList[i];
            var path = reference.path;
            allMap.Add(path, reference);

        }

        //计算依赖
        var nodeMap = new Dictionary<string, AssetRefrenceNode>();
        foreach (var reference in allMap.Values)
        {
            var path = reference.path;
            nodeMap[path] = new AssetRefrenceNode() {path = reference.path, assetName = reference.fileName} ;
        }
        foreach (var reference in allMap.Values)
        {
            var path = reference.path;
            var curNode = nodeMap[path];

            foreach (var depPath in AssetDatabase.GetDependencies(path))
            {
                if (allMap.ContainsKey(depPath) && path != depPath)
                {
                    curNode.depence.Add(depPath);
                    nodeMap[depPath].depenceOnMe.Add(path);
                }
            }
        }

        //合并依赖 清除同层之间的依赖 把同层之间被依赖的结点下移 两层结构 a->b,a->c,b->c 下移c 为:a->b->c 三层结构
        /*
         *          a                      a
         *         /  \                   /
         *        b -> c       ==>       b
         *                              /
         *                             c
         */
        var rootQueue = new Queue<string>();
        List<string> toRemove = new List<string>();
        HashSet<string> depMap = new HashSet<string>();
        foreach (var reference in rootList)
        {
            rootQueue.Enqueue(reference.path);
        }
        while (rootQueue.Count > 0)
        {
            var nodePath = rootQueue.Dequeue();
            var node = nodeMap[nodePath];
            toRemove.Clear();
            depMap.Clear();
            foreach (var depPath in node.depence)
            {
                depMap.Add(depPath);
            }
            foreach (var depPath in node.depence)
            {
                var depNode = nodeMap[depPath];
                foreach (var depenceOnMepath in depNode.depenceOnMe)
                {
                    if (depMap.Contains(depenceOnMepath))
                        toRemove.Add(depPath);
                }
            }

            foreach (var depPath in toRemove)
            {
                node.depence.Remove(depPath);
                var depNode = nodeMap[depPath];
                depNode.depenceOnMe.Remove(nodePath);
            }
            foreach (var depPath in node.depence)
            {
                if (!rootQueue.Contains(depPath))
                    rootQueue.Enqueue(depPath);
            }
        }

        //打组 向上合并, a->b->c
        /*                                          
         *          a        e                  (a,b) (e,f ) --> d          
         *           \      /                   / |  \        _-^   
         *  root:     b    f       ==>         c  h   L_____-`        ==>     group:   (a,b,c,h) -> (d) <- (e,f)
         *          / | \ /                          
         *         c  h  d                   
         */
        HashSet<string> hasSearchSet = new HashSet<string>();
        foreach (var reference in rootList)
        {
            rootQueue.Enqueue(reference.path);
        }
        while (rootQueue.Count >0)
        {
            var nodePath = rootQueue.Dequeue();
            var node = nodeMap[nodePath];
            hasSearchSet.Add(nodePath);
            var depQueue = new Queue<string>(node.depence);
            while (depQueue.Count >0)
            {
                var depNodePath = depQueue.Dequeue();
                var depNode = nodeMap[depNodePath];
                if (depNode.depenceOnMe.Count == 1)
                {
                    node.depence.Remove(depNodePath);
                    nodeMap.Remove(depNodePath);
                    node.incluedDepReference.Add(depNodePath);
                    foreach (var dep2Path in depNode.depence)
                    {
                        node.depence.Add(dep2Path);
                        depQueue.Enqueue(dep2Path);
                        var dep2Node = nodeMap[dep2Path];
                        dep2Node.depenceOnMe.Remove(depNodePath);
                        dep2Node.depenceOnMe.Add(nodePath);
                    }
                }
                else if (depNode.depenceOnMe.Count > 1)
                {
                    if (!rootQueue.Contains(depNodePath) && !hasSearchSet.Contains(depNodePath))
                        rootQueue.Enqueue(depNodePath);
                }
            }
        }
        foreach (var node in nodeMap.Values)
        {
            var reference = allMap[node.path];
            var groupName = node.assetName + ".assetbundle";
            var list = new List<AssetReference>() { reference };
            foreach (var includeDepPath in node.incluedDepReference)
            {
                list.Add(allMap[includeDepPath]);
            }
            output[groupName] = list;
        }
        var content = JsonMapper.ToJson(nodeMap);
        if (File.Exists(GameConfig.BundleConfigPath))
        {
            File.Delete(GameConfig.BundleConfigPath);
        }
        using (var writer = File.CreateText(GameConfig.BundleConfigPath))
        {
            writer.Write(content);
        }
        return output;
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
