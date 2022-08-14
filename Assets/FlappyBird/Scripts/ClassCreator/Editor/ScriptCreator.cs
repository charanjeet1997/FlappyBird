using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptCreator : EditorWindow
{
   private string className;
   private string nameSpaceName;
   private Vector2 scrollPos;
   [MenuItem("Window/ScriptCreator")]
   static void OpenScriptCreatereWindow()
   {
      ScriptCreator scriptCreator = (ScriptCreator) EditorWindow.GetWindow(typeof(ScriptCreator));
      scriptCreator.Show();
   }

   private void OnGUI()
   {
      GUILayout.BeginVertical();
      scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true);
      GUILayout.BeginHorizontal();
      EditorGUILayout.LabelField("NameSpace");
      nameSpaceName = EditorGUILayout.TextField(nameSpaceName);
      GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal();
      EditorGUILayout.LabelField("Class");
      className = EditorGUILayout.TextField(className);
      GUILayout.EndHorizontal();
      GUILayout.Space(10);
      if (GUILayout.Button("Create Interface"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"I{className}.cs",GetInterfaceContent(),null);
      }
      GUILayout.Space(10);
      if (GUILayout.Button("Create Simple Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}.cs",GetClassContent(),null);
      }   
      
      GUILayout.Space(10);
      if (GUILayout.Button("Create Abstract Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}.cs",GetAbstractClassContent(),null);
      }   
      GUILayout.Space(10);
      if (GUILayout.Button("Create MonoBehaviour Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}.cs",GetMonoBehaviourClassContent(),null);
      }  
      
      GUILayout.Space(10);
      if (GUILayout.Button("Create ScriptableObject Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}.cs",GetScriptableObjectClassContent(),null);
      }  
      
      GUILayout.Space(10);
      if (GUILayout.Button("Create Screen Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}Screen.cs",GetScreenContent(),null);
      }
      
      GUILayout.Space(10);
      if (GUILayout.Button("Create Popup Class"))
      {
         ProjectWindowUtil.CreateAssetWithContent($"{className}Popup.cs",GetPopupContent(),null);
      }
      EditorGUILayout.EndScrollView();
      GUILayout.EndVertical();
   }

   private string GetInterfaceContent()
   {
      string interfaceContent = "using System;\nusing UnityEngine;\n\n" 
                                + NameSpaceStart()
                                 +$"\tpublic interface I{className}\n" 
                                 +"\t{\n\n\t}"+NameSpaceEnd();
      return interfaceContent;
   }
      
   private string GetClassContent()
   {
      string classContent = "using System;\nusing UnityEngine;\n\n" 
                            + NameSpaceStart()
                                +$"\tpublic class {className}\n" 
                                +"\t{\n\n\t}"+NameSpaceEnd();
      return classContent;
   }
   
   private string GetAbstractClassContent()
   {
      string abstractClassContent = "using System;\nusing UnityEngine;\n\n" 
                                    + NameSpaceStart()
                                +$"\tpublic abstract class {className}\n" 
                                +"\t{\n\n\t}"+NameSpaceEnd();
      return abstractClassContent;
   }
   
   private string GetMonoBehaviourClassContent()
   {
      string monoBehaviourClassContent = "using System;\nusing UnityEngine;\n\n" 
                                         + NameSpaceStart()
                                 +$"\tpublic class {className} : MonoBehaviour\n" 
                                 +"\t{\n"
                                +"\n\t\t#region PUBLIC_VARS"
                                +"\n\n\t\t#endregion\n"
   
                                +"\n\t\t#region PRIVATE_VARS"
                                +"\n\n\t\t#endregion\n"

                                +"\n\t\t#region UNITY_CALLBACKS"
                                +"\n\n\t\t#endregion\n"

                                +"\n\t\t#region PUBLIC_METHODS"
                                 +"\n\n\t\t#endregion\n"

                                 +"\n\t\t#region PRIVATE_METHODS"
                                 +"\n\n\t\t#endregion\n"
                                 +"\n\t}"+NameSpaceEnd();
      return monoBehaviourClassContent;
   }
   
   private string GetScriptableObjectClassContent()
   {
      string monoBehaviourClassContent = "using System;\nusing UnityEngine;\n\n"
                                         + NameSpaceStart()
                                 +$"\t[CreateAssetMenu(fileName = \"{className}\" )]"
                                 +$"\n\tpublic class {className} : ScriptableObject\n" 
                                 +"\t{\n"
                                +"\n\t\t#region PUBLIC_VARS"
                                +"\n\n\t\t#endregion\n"
   
                                +"\n\t\t#region PRIVATE_VARS"
                                +"\n\n\t\t#endregion\n"

                                +"\n\t\t#region UNITY_CALLBACKS"
                                +"\n\n\t\t#endregion\n"

                                +"\n\t\t#region PUBLIC_METHODS"
                                 +"\n\n\t\t#endregion\n"

                                 +"\n\t\t#region PRIVATE_METHODS"
                                 +"\n\n\t\t#endregion\n"
                                 +"\n\t}"+NameSpaceEnd();
      return monoBehaviourClassContent;
   }
   
   private string GetScreenContent()
   {
      string screenContent = "using System;\nusing UnityEngine;\nusing UISystem;\n" 
                                         + NameSpaceStart()
                                         +$"\tpublic class {className}Screen : UISystem.Screen\n" 
                                         +"\t{\n"
                                         +"\n\t\t#region PUBLIC_VARS"
                                         +"\n\n\t\t#endregion\n"
   
                                         +"\n\t\t#region PRIVATE_VARS"
                                         +"\n\n\t\t#endregion\n"

                                         +"\n\t\t#region UNITY_CALLBACKS"
                                         +"\n\n\t\t#endregion\n"

                                         +"\n\t\t#region PUBLIC_METHODS"
                                         +"\n\n\t\t#endregion\n"

                                         +"\n\t\t#region PRIVATE_METHODS"
                                         +"\n\n\t\t#endregion\n"
                                         
                                         +"\n\t\t#region UISystem_Callbacks"
                                         +"\n\n\t\tpublic override void Enable()\n\t\t{\n\n\t\t}"
                                         +"\n\n\t\tpublic override void Disable()\n\t\t{\n\n\t\t}"
                                         +"\n\n\t\tpublic override void Show()\n\t\t{\n\n\t\t}"
                                         +"\n\n\t\tpublic override void Hide()\n\t\t{\n\n\t\t}"
                                         +"\n\n\t\t#endregion\n"
                                         +"\n\t}"+NameSpaceEnd();
      return screenContent;
   }
   
   private string GetPopupContent()
   {
      string popupContent = "using System;\nusing UnityEngine;\nusing UISystem;\n"
                            + NameSpaceStart()
                            + $"\tpublic class {className}Popup : UISystem.Popup\n"
                            + "\t{\n"
                            + "\n\t\t#region PUBLIC_VARS"
                            + "\n\n\t\t#endregion\n"

                            + "\n\t\t#region PRIVATE_VARS"
                            + "\n\n\t\t#endregion\n"

                            + "\n\t\t#region UNITY_CALLBACKS"
                            + "\n\n\t\t#endregion\n"

                            + "\n\t\t#region PUBLIC_METHODS"
                            + "\n\n\t\t#endregion\n"

                            + "\n\t\t#region PRIVATE_METHODS"
                            + "\n\n\t\t#endregion\n"

                            + "\n\t\t#region UISystem_Callbacks"
                            + "\n\n\t\tpublic override void Enable()\n\t\t{\n\n\t\t}"
                            + "\n\n\t\tpublic override void Disable()\n\t\t{\n\n\t\t}"
                            + "\n\n\t\tpublic override void Show()\n\t\t{\n\n\t\t}"
                            + "\n\n\t\tpublic override void Hide()\n\t\t{\n\n\t\t}"
                            + "\n\n\t\t#endregion\n"
                            + "\n\t}" + NameSpaceEnd();
      return popupContent;
   }
   
   private string GetModuleContent()
   {
      string popupContent = "using System;\nusing UnityEngine;\nusing UISystem;\n" 
                            + NameSpaceStart()
                            +$"\tpublic class {className}Module : UISystem.Module\n" 
                            +"\t{\n"
                            +"\n\t\t#region PUBLIC_VARS"
                            +"\n\n\t\t#endregion\n"
   
                            +"\n\t\t#region PRIVATE_VARS"
                            +"\n\n\t\t#endregion\n"

                            +"\n\t\t#region UNITY_CALLBACKS"
                            +"\n\n\t\t#endregion\n"

                            +"\n\t\t#region PUBLIC_METHODS"
                            +"\n\n\t\t#endregion\n"

                            +"\n\t\t#region PRIVATE_METHODS"
                            +"\n\n\t\t#endregion\n"
                                         
                            +"\n\t\t#region UISystem_Callbacks"
                            +"\n\n\t\tpublic override void Enable()\n\t\t{\n\n\t\t}"
                            +"\n\n\t\tpublic override void Disable()\n\t\t{\n\n\t\t}"
                            +"\n\n\t\tpublic override void Show()\n\t\t{\n\n\t\t}"
                            +"\n\n\t\tpublic override void Hide()\n\t\t{\n\n\t\t}"
                            +"\n\n\t\t#endregion\n"
                            +"\n\t}"+NameSpaceEnd();
      return popupContent;
   }

   string NameSpaceStart()
   {
      if (!string.IsNullOrEmpty(nameSpaceName))
      {
         return $"namespace {nameSpaceName}\n" + "{\n";
      }

      return $"namespace nameSpaceName\n" + "{\n";
   }

   string NameSpaceEnd()
   {
      if (!string.IsNullOrEmpty(nameSpaceName))
      {
         return "\n}";
      }
      return string.Empty;
   }
}