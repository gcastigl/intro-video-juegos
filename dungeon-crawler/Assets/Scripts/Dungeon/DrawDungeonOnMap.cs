using UnityEngine;
using System.Collections;

public class DrawDungeonOnMap : MonoBehaviour {

	public Font font;
	public Color floorColor = new Color(246, 214, 98, 117);
	public Color playerColor = Color.red;
	public Color treasureColor = Color.blue;
	public Color decorationColor = Color.green;

	private GUIStyle leyendStyle = new GUIStyle();
	private Texture2D mapTexture;

	void Start() {
		leyendStyle.font = font;
		DungeonManager dungeonManager = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
		Dungeon dungeon = dungeonManager.getDungeon();
		mapTexture = new Texture2D (dungeon.columnCount(), dungeon.rowsCount());
		Color transparent = new Color (0, 0, 0, 0);
		for (int row = 0; row < dungeon.rowsCount(); row++) {
			for (int col = 0; col < dungeon.columnCount(); col++) {
				Color color = transparent;
				if (dungeon.accesibles[row, col] && dungeon.value(row, col) == 0) {
					color = floorColor;
				}
				mapTexture.SetPixel(col, row, color);
			}
		}
		drawEntranceDoor(dungeon);
		drawTreasures(dungeon);
		drawDecorations(dungeon);
		mapTexture.Apply();
	}

	private void drawEntranceDoor(Dungeon dungeon) {
		drawDot(new Vector2(dungeon.doorRow, dungeon.doorCol), playerColor);
	}

	private void drawTreasures(Dungeon dungeon) {
		foreach(Treasure treasure in dungeon.treasures) {
			drawDot(treasure.position, treasureColor);
		}
	}

	private void drawDecorations(Dungeon dungeon) {
		foreach(Decoration decoration in dungeon.decorations) {
			drawDot(decoration.position, decorationColor);
		}
	}

	private void drawDot(Vector2 position, Color color) {
		int r = 2;
		for (int row = -r; row <= r; row++) {
			for (int col = -r; col <= r; col++) {
				float nRow = position.x + row;
				float nCol = position.y + col;
				if (Vector2.Distance(position, new Vector2(nRow, nCol)) <= 1) {
					mapTexture.SetPixel((int) nCol, (int) nRow, color);
				}
			}
		}
	}

	void OnGUI() {
		float maxLen = Mathf.Min (Screen.width, Screen.height) * 0.7f;
		float xOffset = (Screen.width - maxLen) / 2;
		float yOffset = (Screen.height - maxLen) / 2;
		GUI.DrawTexture (new Rect(xOffset - Screen.width * 0.15f, yOffset, maxLen, maxLen), mapTexture);

		float leyendXOffset = Screen.width * 0.6f;
		float leyendWidth = Screen.width * 0.2f;
		leyendStyle.normal.textColor = playerColor;
		leyendStyle.fontSize = (int) (maxLen / 10f);
		GUI.Label(new Rect(leyendXOffset, yOffset, leyendWidth, Screen.height * 0.1f), "Exit", leyendStyle);
		leyendStyle.normal.textColor = treasureColor;
		yOffset += Screen.height * 0.1f;
		GUI.Label(new Rect(leyendXOffset, yOffset, leyendWidth, Screen.height * 0.1f), "Treasures", leyendStyle);
		leyendStyle.normal.textColor = decorationColor;
		yOffset += Screen.height * 0.1f;
		GUI.Label(new Rect(leyendXOffset, yOffset, leyendWidth, Screen.height * 0.1f), "References", leyendStyle);
	}
}
