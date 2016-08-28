// TransformCopier.cs v 1.1
// homepage: http://wiki.unity3d.com/index.php/CopyTransform

using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class TransformCopier : ScriptableObject {
	
	private static Vector3 position;
	private static Quaternion rotation;
	private static Vector3 scale;
	private static string myName;
	
	[MenuItem ("Window/Transform Copier/Copy",false,0)]
	static void DoRecord () {
		position = Selection.activeTransform.localPosition;
		rotation = Selection.activeTransform.localRotation;
		scale = Selection.activeTransform.localScale;
		myName = Selection.activeTransform.name;
		EditorUtility.DisplayDialog("Transform Copy", "Local position, rotation, & scale of "+myName +" copied relative to parent.", "OK", "");
    }
	
	// PASTE POSITION:
	[MenuItem ("Window/Transform Copier/Paste Position",false,50)]
    static void DoApplyPositionXYZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = position;
    }
		
	[MenuItem ("Window/Transform Copier/Paste Position X",false,51)]
    static void DoApplyPositionX () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(position.x, selection.localPosition.y, selection.localPosition.z);
    }
	
	[MenuItem ("Window/Transform Copier/Paste Position Y",false,52)]
    static void DoApplyPositionY () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(selection.localPosition.x, position.y, selection.localPosition.z);
	}
	
	[MenuItem ("Window/Transform Copier/Paste Position Z",false,53)]
    static void DoApplyPositionZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localPosition = new Vector3(selection.localPosition.x, selection.localPosition.y, position.z);
    }
	
	// PASTE ROTATION:
	[MenuItem ("Window/Transform Copier/Paste Rotation",false,100)]
    static void DoApplyRotationXYZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = rotation;
    }
	
	[MenuItem ("Window/Transform Copier/Paste Rotation X",false,101)]
	static void DoApplyRotationX () {
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(rotation.eulerAngles.x, selection.localRotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
	}
	
	[MenuItem ("Window/Transform Copier/Paste Rotation Y",false,102)]
    static void DoApplyRotationY () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, rotation.eulerAngles.y, selection.localRotation.eulerAngles.z);
	}
	
	[MenuItem ("Window/Transform Copier/Paste Rotation Z",false,103)]
    static void DoApplyRotationZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = Quaternion.Euler(selection.localRotation.eulerAngles.x, selection.localRotation.eulerAngles.y, rotation.eulerAngles.z);
	}
	
	// PASTE SCALE:
	[MenuItem ("Window/Transform Copier/Paste Scale",false,150)]
    static void DoApplyScaleXYZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = scale;
    }
	
	[MenuItem ("Window/Transform Copier/Paste Scale X",false,151)]
    static void DoApplyScaleX () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(scale.x, selection.localScale.y, selection.localScale.z);
    }
	
	[MenuItem ("Window/Transform Copier/Paste Scale Y",false,152)]
    static void DoApplyScaleY () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(selection.localScale.x, scale.y, selection.localScale.z);
    }
	
	[MenuItem ("Window/Transform Copier/Paste Scale Z",false,153)]
    static void DoApplyScaleZ () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3(selection.localScale.x, selection.localScale.y, scale.z);
    }
	
	// CHANGE LOCAL ROTATION :
	[MenuItem ("Window/Transform Copier/localRotation.x + 90",false,200)]
    static void localRotateX90 () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(90f,0f,0f);
    }
	
	[MenuItem ("Window/Transform Copier/localRotation.y + 90",false,201)]
    static void localRotateY90 () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(0f,90f,0f);
    }
	
	[MenuItem ("Window/Transform Copier/localRotation.z + 90",false,202)]
    static void localRotateZ90 () {
        Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localRotation = selection.localRotation*Quaternion.Euler(0f,0f,90f);
	}
	
	// SWAP:	
	[MenuItem ("Window/Transform Copier/Swap Y and Z Scale", false, 251)]
	static void SwapYZScale () {
		Transform[] selections  = Selection.transforms;
		foreach (Transform selection  in selections) selection.localScale = new Vector3 (selection.localScale.x,selection.localScale.z,selection.localScale.y);
	}
}