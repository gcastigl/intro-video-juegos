using UnityEngine;
using System.Collections;

public class SpeedometerUI : MonoBehaviour {
	
	public Texture2D dialTex;
	public float scale;
	public Texture2D needleTex;
	public Vector2 dialPos;
	public float topSpeed = 0;
	public float stopAngle = 0;
	public float topSpeedAngle = 0;
	public float speed = 0;

	void  OnGUI (){
		float dialWidth = scale * dialTex.width;
		float dialHeight = scale * dialTex.height;
		GUI.DrawTexture(new Rect(dialPos.x, dialPos.y, dialWidth, dialHeight), dialTex);
		Vector2 centre = new Vector2(dialPos.x + dialWidth / 2, dialPos.y + dialHeight / 2);
		Matrix4x4 savedMatrix = GUI.matrix;
		float speedFraction = speed / topSpeed;
		float needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
		GUIUtility.RotateAroundPivot(needleAngle, centre);
		float needleWidth = scale * needleTex.width;
		float neddleHeight = scale * needleTex.height;
		GUI.DrawTexture(new Rect(centre.x, centre.y - neddleHeight / 2, needleWidth, neddleHeight), needleTex);
		GUI.matrix = savedMatrix;
	}
}
