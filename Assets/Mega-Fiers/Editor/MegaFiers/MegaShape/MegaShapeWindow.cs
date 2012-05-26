
using UnityEditor;
using UnityEngine;

// TODO: Select axis for shapes
// TODO: Add new spline to shape
// TODO: Button to recalc lengths
// TEST: Build a simple scene in max then have a road, barrier, fence etc
// Import of simple text file for path
public class MegaShapeWindow : EditorWindow
{
	static bool		showcommon;
	//string name = "Shape";
	static MegaAxis	axis = MegaAxis.Y;
	static bool		drawknots = true;
	static bool		drawhandles = true;
	static float	stepdist = 0.1f;
	static float	knotsize = 0.05f;
	static Color	col1 = Color.white;
	static Color	col2 = Color.black;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/MegaShape")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		//MegaShapeWindow window = (MegaShapeWindow)EditorWindow.GetWindow(typeof(MegaShapeWindow));
		EditorWindow.GetWindow(typeof(MegaShapeWindow));
	}

	[MenuItem("GameObject/Create Other/MegaShape/Star Shape")]			static void CreateStarShape()	{ CreateShape("Star"); }
	[MenuItem("GameObject/Create Other/MegaShape/Circle Shape")]		static void CreateCircleShape() { CreateShape("Circle"); }
	[MenuItem("GameObject/Create Other/MegaShape/NGon Shape")]			static void CreateNGonShape() { CreateShape("NGon"); }
	[MenuItem("GameObject/Create Other/MegaShape/Arc Shape")]			static void CreateArcShape() { CreateShape("Arc"); }
	[MenuItem("GameObject/Create Other/MegaShape/Ellipse Shape")]		static void CreateEllipseShape() { CreateShape("Ellipse"); }
	[MenuItem("GameObject/Create Other/MegaShape/Rectangle Shape")]		static void CreateRectangleShape() { CreateShape("Rectangle"); }
	[MenuItem("GameObject/Create Other/MegaShape/Helix Shape")]			static void CreateHelixShape() { CreateShape("Helix"); }

	// Put common params in, and each shape has its sections
	void OnGUI()
	{
		//name = EditorGUILayout.TextField("Name", name);
		showcommon = EditorGUILayout.Foldout(showcommon, "Common");

		if ( showcommon )
		{
			axis = (MegaAxis)EditorGUILayout.EnumPopup("Axis", axis);
			stepdist = EditorGUILayout.FloatField("Step Dist", stepdist);
			knotsize = EditorGUILayout.FloatField("Knot Size", knotsize);
			drawknots = EditorGUILayout.Toggle("Draw Knots", drawknots);
			drawhandles = EditorGUILayout.Toggle("Draw Handles", drawhandles);

			col1 = EditorGUILayout.ColorField("Color 1", col1);
			col2 = EditorGUILayout.ColorField("Color 2", col2);
		}

		if ( GUILayout.Button("Circle") )	CreateShape("Circle");
		if ( GUILayout.Button("Star") ) CreateShape("Star");
		if ( GUILayout.Button("NGon") ) CreateShape("NGon");
		if ( GUILayout.Button("Arc") ) CreateShape("Arc");
		if ( GUILayout.Button("Ellipse") ) CreateShape("Ellipse");
		if ( GUILayout.Button("Rectangle") ) CreateShape("Rectangle");
		if ( GUILayout.Button("Helix") ) CreateShape("Helix");
	}

	static void CreateShape(string type)
	{
		Vector3 pos = UnityEditor.SceneView.lastActiveSceneView.pivot;

		MegaShape ms = null;
		GameObject go = new GameObject(type + " Shape");
		
		switch ( type )
		{
			case "Circle":		ms = go.AddComponent<MegaShapeCircle>(); break;
			case "Star":		ms = go.AddComponent<MegaShapeStar>(); break;
			case "NGon":		ms = go.AddComponent<MegaShapeNGon>(); break;
			case "Arc":			ms = go.AddComponent<MegaShapeArc>(); break;
			case "Ellipse":		ms = go.AddComponent<MegaShapeEllipse>(); break;
			case "Rectangle":	ms = go.AddComponent<MegaShapeRectangle>(); break;
			case "Helix":		ms = go.AddComponent<MegaShapeHelix>(); break;
		}

		go.transform.position = pos;
		Selection.activeObject = go;

		if ( ms != null )
		{
			ms.axis			= axis;
			ms.drawHandles	= drawhandles;
			ms.drawKnots	= drawknots;
			ms.col1			= col1;
			ms.col2			= col2;
			ms.KnotSize		= knotsize;
			ms.stepdist		= stepdist;
		}
	}
}