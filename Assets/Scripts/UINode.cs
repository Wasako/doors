using UnityEngine;
using System.Collections;

public class UINode : MonoBehaviour {
	public static UINode inst=null;

	void Awake() {
		if( inst!=this && inst!=null ) {
			GameObject.Destroy(this.gameObject);
			return;
		}
		inst=this;
		GameObject.DontDestroyOnLoad(inst.gameObject);
	}

	public static GameObject GetCanvasGO(){
		//Debug.LogError("GetCanvasGO2");
		var canvas=GameObject.Find("Canvas");

		if(canvas==null){
			canvas = new GameObject("Canvas");

			var rt = canvas.AddComponent<RectTransform>();
			var cv = canvas.AddComponent<Canvas>();
			cv.renderMode= RenderMode.ScreenSpaceOverlay;
			cv.pixelPerfect = false;
			cv.sortingOrder = 0;
			var cs = canvas.AddComponent<UnityEngine.UI.CanvasScaler>();
			cs.scaleFactor = 1;
			cs.referencePixelsPerUnit = 100f;
			cs.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
			var gr = canvas.AddComponent<UnityEngine.UI.GraphicRaycaster>();
			gr.ignoreReversedGraphics=true;
			gr.blockingObjects= UnityEngine.UI.GraphicRaycaster.BlockingObjects.None;


			var raw = new GameObject("RawImage");
			var ri = raw.AddComponent<UnityEngine.UI.RawImage>();
			ri.transform.SetParent( canvas.transform );
			ri.texture = null;
			ri.transform.localPosition = Vector3.zero;
			ri.rectTransform.anchorMin = Vector3.zero;
			ri.rectTransform.anchorMax = Vector3.one;
			ri.rectTransform.offsetMin = ri.rectTransform.offsetMax = Vector3.zero;

		}

		GameObject.DontDestroyOnLoad(canvas);

		return canvas;
	}
}
