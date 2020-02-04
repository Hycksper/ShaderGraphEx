using UnityEditor;

namespace UnityEditor.ShaderGraph
{
	static class CreateShaderGraphEx
	{
		[MenuItem("Assets/Create/Shader/SimpleLit Graph", false, 208)]
		public static void CreateSimpleLitMasterMaterialGraph()
		{
			GraphUtil.CreateNewGraph(new SimpleLitMasterNode());
		}
	}
}