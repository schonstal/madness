
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

// TODO: Button to recalc lengths
// TEST: Build a simple scene in max then have a road, barrier, fence etc

[CustomEditor(typeof(MegaShape))]
public class MegaShapeEditor : Editor
{
	int		selected = -1;
	Vector3 pm = new Vector3();
	Vector3 delta = new Vector3();

	bool showsplines = false;
	bool showknots = false;

	float ImportScale = 1.0f;

	public virtual bool Params()	{ return false; }

	public bool showcommon = true;

	public override void OnInspectorGUI()
	{
		MegaShape shape = (MegaShape)target;

		EditorGUILayout.BeginHorizontal();

		if ( GUILayout.Button("Add Knot") )
		{
			if ( shape.splines == null || shape.splines.Count == 0 )
			{
				MegaSpline spline = new MegaSpline();	// Have methods for these
				shape.splines.Add(spline);
			}

			//Undo.RegisterUndo(target, "Add Knot");

			MegaKnot knot = new MegaKnot();

			int sp = 0;

			if ( selected == -1 || shape.splines[0].knots.Count == 1 )
			{
				shape.splines[0].knots.Add(knot);
				selected = shape.splines[0].knots.Count - 1;
			}
			else
			{
				if ( selected < shape.splines[0].knots.Count - 1 )
				{
					sp = selected + 1;
					knot.p = shape.splines[0].knots[selected].Interpolate(0.5f, shape.splines[0].knots[selected + 1]);
					Vector3 t = shape.splines[0].knots[selected].Interpolate(0.51f, shape.splines[0].knots[selected + 1]);
					knot.outvec = (t - knot.p);	//.Normalize();
					knot.outvec.Normalize();
					knot.outvec *= shape.splines[0].knots[selected].seglength * 0.25f;
					knot.invec = -knot.outvec;
					knot.invec += knot.p;
					knot.outvec += knot.p;
				}
				else
				{
					if ( shape.splines[0].closed )
					{
						sp = selected + 1;
						knot.p = shape.splines[0].knots[selected].Interpolate(0.5f, shape.splines[0].knots[0]);
						Vector3 t = shape.splines[0].knots[selected].Interpolate(0.51f, shape.splines[0].knots[0]);
						knot.outvec = (t - knot.p);	//.Normalize();
						knot.outvec.Normalize();
						knot.outvec *= shape.splines[0].knots[selected].seglength * 0.25f;
						knot.invec = -knot.outvec;
						knot.invec += knot.p;
						knot.outvec += knot.p;
					}
					else
					{
						sp = selected - 1;

						//Debug.Log("selected " + selected + " count " + shape.splines[0].knots.Count + " sp " + sp);
						knot.p = shape.splines[0].knots[sp].Interpolate(0.5f, shape.splines[0].knots[sp + 1]);
						Vector3 t = shape.splines[0].knots[sp].Interpolate(0.51f, shape.splines[0].knots[sp + 1]);
						knot.outvec = (t - knot.p);	//.Normalize();
						knot.outvec.Normalize();
						knot.outvec *= shape.splines[0].knots[sp].seglength * 0.25f;
						knot.invec = -knot.outvec;
						knot.invec += knot.p;
						knot.outvec += knot.p;
						sp++;
					}
				}

				shape.splines[0].knots.Insert(sp, knot);
				selected = sp;	//++;
			}
			shape.CalcLength(10);
			EditorUtility.SetDirty(target);
		}

		if ( GUILayout.Button("Delete Knot") )
		{
			if ( selected != -1 )
			{
				//Undo.RegisterUndo(target, "Delete Knot");
				shape.splines[0].knots.RemoveAt(selected);
				selected--;
				shape.CalcLength(10);
			}
			EditorUtility.SetDirty(target);
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();

		if ( GUILayout.Button("Match Handles") )
		{
			if ( selected != -1 )
			{
				//Undo.RegisterUndo(target, "Match Handles");

				Vector3 p = shape.splines[0].knots[selected].p;
				Vector3 d = shape.splines[0].knots[selected].outvec - p;
				shape.splines[0].knots[selected].invec = p - d;
				shape.CalcLength(10);
			}
			EditorUtility.SetDirty(target);
		}

		if ( GUILayout.Button("Load") )
		{
			// Load a spl file from max, so delete everything and replace
			LoadShape(ImportScale);
		}

		EditorGUILayout.EndHorizontal();

		showcommon = EditorGUILayout.Foldout(showcommon, "Common Params");

		bool rebuild = false;	//Params();

		if ( showcommon )
		{
			ImportScale = EditorGUILayout.FloatField("Import Scale", ImportScale);

			MegaAxis av = (MegaAxis)EditorGUILayout.EnumPopup("Axis", shape.axis);
			if ( av != shape.axis )
			{
				shape.axis = av;
				rebuild = true;
			}

			shape.col1 = EditorGUILayout.ColorField("Col 1", shape.col1);
			shape.col2 = EditorGUILayout.ColorField("Col 2", shape.col2);

			shape.KnotCol = EditorGUILayout.ColorField("Knot Col", shape.KnotCol);
			shape.HandleCol = EditorGUILayout.ColorField("Handle Col", shape.HandleCol);
			shape.VecCol = EditorGUILayout.ColorField("Vec Col", shape.VecCol);

			shape.KnotSize = EditorGUILayout.FloatField("Knot Size", shape.KnotSize);
			shape.stepdist = EditorGUILayout.FloatField("Step Dist", shape.stepdist);

			if ( shape.stepdist < 0.01f )
				shape.stepdist = 0.01f;

			shape.normalizedInterp = EditorGUILayout.Toggle("Normalized Interp", shape.normalizedInterp);
			shape.drawHandles = EditorGUILayout.Toggle("Draw Handles", shape.drawHandles);
			shape.drawKnots = EditorGUILayout.Toggle("Draw Knots", shape.drawKnots);
			shape.drawspline = EditorGUILayout.Toggle("Draw Spline", shape.drawspline);
			shape.lockhandles = EditorGUILayout.Toggle("Lock Handles", shape.lockhandles);

			shape.animate = EditorGUILayout.Toggle("Animate", shape.animate);
			if ( shape.animate )
			{
				shape.time = EditorGUILayout.FloatField("Time", shape.time);
				shape.MaxTime = EditorGUILayout.FloatField("Loop Time", shape.MaxTime);
				shape.speed = EditorGUILayout.FloatField("Speed", shape.speed);
				shape.LoopMode = (MegaRepeatMode)EditorGUILayout.EnumPopup("Loop Mode", shape.LoopMode);
			}

			showsplines = EditorGUILayout.Foldout(showsplines, "Splines");

			if ( showsplines )
			{
				for ( int i = 0; i < shape.splines.Count; i++ )
				{
					DisplaySpline(shape, shape.splines[i]);
				}
			}
		}

		if ( Params() )
		{
			rebuild = true;
		}

		if ( GUI.changed )
		{
			EditorUtility.SetDirty(target);
			//shape.CalcLength(10);
		}

		if ( rebuild )
		{
			shape.MakeShape();
			EditorUtility.SetDirty(target);
		}
	}

	void DisplayKnot(MegaShape shape, MegaSpline spline, MegaKnot knot)
	{
		bool recalc = false;

		Vector3 p = EditorGUILayout.Vector3Field("Pos", knot.p);
#if false
		Vector3 invec = EditorGUILayout.Vector3Field("In", knot.invec);
		//Vector3 outvec = EditorGUILayout.Vector3Field("Out", knot.outvec);

		if ( invec != knot.invec )
		{
			if ( shape.lockhandles )
			{
				Vector3 d = invec - knot.invec;
				knot.outvec -= d;
			}

			knot.invec = invec;
			recalc = true;
		}

		Vector3 outvec = EditorGUILayout.Vector3Field("Out", knot.outvec);

		if ( outvec != knot.outvec )
		{
			if ( shape.lockhandles )
			{
				Vector3 d = outvec - knot.outvec;
				knot.invec -= d;
			}

			knot.outvec = outvec;
			recalc = true;
		}

		//Vector3 p = EditorGUILayout.Vector3Field("Pos", knot.p);
#endif
		delta = p - knot.p;

		knot.invec += delta;
		knot.outvec += delta;

		if ( knot.p != p )
		{
			recalc = true;
			knot.p = p;
		}

		if ( recalc )
		{
			shape.CalcLength(10);
		}
	}

	void DisplaySpline(MegaShape shape, MegaSpline spline)
	{
		bool closed = EditorGUILayout.Toggle("Closed", spline.closed);

		if ( closed != spline.closed )
		{
			spline.closed = closed;
			shape.CalcLength(10);
			EditorUtility.SetDirty(target);
		}

		EditorGUILayout.LabelField("Length ", spline.length.ToString("0.000"));

		showknots = EditorGUILayout.Foldout(showknots, "Knots");

		if ( showknots )
		{
			for ( int i = 0; i < spline.knots.Count; i++ )
			{
				DisplayKnot(shape, spline, spline.knots[i]);
				//EditorGUILayout.Separator();
			}
		}
	}

	public void OnSceneGUI()
	{
		Undo.RegisterUndo(target, "Move Shape Points");

		MegaShape shape = (MegaShape)target;

		Handles.matrix = shape.transform.localToWorldMatrix;

		bool recalc = false;

		for ( int s = 0; s < shape.splines.Count; s++ )
		{
			for ( int p = 0; p < shape.splines[s].knots.Count; p++ )
			{
				if ( shape.drawKnots )	//&& recalc == false )
				{
					pm = shape.splines[s].knots[p].p;

					if ( p == selected )
					{
						Handles.color = Color.white;
						Handles.Label(pm, " Selected\n" + pm.ToString("0.000"));
					}
					else
					{
						Handles.color = shape.KnotCol;
						Handles.Label(pm, " " + p);
					}

					//if ( p == selected )
					//shape.splines[s].knots[p].p = Handles.PositionHandle(pm, Quaternion.identity);
					//else
					//{
					shape.splines[s].knots[p].p = Handles.FreeMoveHandle(pm, Quaternion.identity, shape.KnotSize, Vector3.zero, Handles.CubeCap);

					//if ( shape.splines[s].knots[p].p != pm )
					//selected = p;
					//}

					delta = shape.splines[s].knots[p].p - pm;

					shape.splines[s].knots[p].invec += delta;
					shape.splines[s].knots[p].outvec += delta;

					if ( shape.splines[s].knots[p].p != pm )
					{
						selected = p;
						recalc = true;
					}

					pm = shape.transform.TransformPoint(shape.splines[s].knots[p].p);

					//Handles.CubeCap(0, pm, Quaternion.identity, shape.KnotSize);
				}

				if ( shape.drawHandles )	//&& recalc == false )
				{
					Handles.color = shape.VecCol;
					pm = shape.transform.TransformPoint(shape.splines[s].knots[p].p);

					Handles.DrawLine(pm, shape.transform.TransformPoint(shape.splines[s].knots[p].invec));
					Handles.DrawLine(pm, shape.transform.TransformPoint(shape.splines[s].knots[p].outvec));

					Handles.color = shape.HandleCol;

					//shape.splines[s].knots[p].invec = Handles.PositionHandle(shape.splines[s].knots[p].invec, Quaternion.identity);	//shape.hsize);
					//shape.splines[s].knots[p].outvec = Handles.PositionHandle(shape.splines[s].knots[p].outvec, Quaternion.identity);	//shape.hsize);

					Vector3 invec;
					//if ( p == selected )
						//invec = Handles.PositionHandle(shape.splines[s].knots[p].invec, Quaternion.identity);	//shape.hsize);
					//else
						invec = Handles.FreeMoveHandle(shape.splines[s].knots[p].invec, Quaternion.identity, shape.KnotSize, Vector3.zero, Handles.CubeCap);

					//Debug.Log("sel " + selected + " new " + invec.ToString("0.0000") + " old " + shape.splines[s].knots[p].invec.ToString("0.0000"));
					if ( invec != shape.splines[s].knots[p].invec )
					{
						if ( shape.lockhandles )
						{
							Vector3 d = invec - shape.splines[s].knots[p].invec;
							shape.splines[s].knots[p].outvec -= d;
						}

						shape.splines[s].knots[p].invec = invec;

						recalc = true;
					}
					Vector3 outvec;	// = Handles.PositionHandle(shape.splines[s].knots[p].outvec, Quaternion.identity);	//shape.hsize);

					//if ( p == selected )
						//outvec = Handles.PositionHandle(shape.splines[s].knots[p].outvec, Quaternion.identity);	//shape.hsize);
					//else
						outvec = Handles.FreeMoveHandle(shape.splines[s].knots[p].outvec, Quaternion.identity, shape.KnotSize, Vector3.zero, Handles.CubeCap);

					if ( outvec != shape.splines[s].knots[p].outvec )
					{
						if ( shape.lockhandles )
						{
							Vector3 d = outvec - shape.splines[s].knots[p].outvec;
							shape.splines[s].knots[p].invec -= d;
						}

						shape.splines[s].knots[p].outvec = outvec;
						recalc = true;
					}
					Vector3 hp = shape.transform.TransformPoint(shape.splines[s].knots[p].invec);
					//Handles.CubeCap(0, hp, Quaternion.identity, shape.KnotSize);
					if ( selected == p )
						Handles.Label(hp, " " + p);

					hp = shape.transform.TransformPoint(shape.splines[s].knots[p].outvec);
					//Handles.CubeCap(0, hp, Quaternion.identity, shape.KnotSize);
					
					if ( selected == p )
						Handles.Label(hp, " " + p);
				}
#if false
				if ( shape.drawKnots )
				{
					pm = shape.splines[s].knots[p].p;

					if ( p == selected )
					{
						Handles.color = Color.white;
						Handles.Label(pm, " Selected\n" + pm.ToString("0.000"));
					}
					else
					{
						Handles.color = shape.KnotCol;
						Handles.Label(pm, " " + p);
					}

					//if ( p == selected )
						shape.splines[s].knots[p].p = Handles.PositionHandle(pm, Quaternion.identity);
					//else
					//{
						//shape.splines[s].knots[p].p = Handles.FreeMoveHandle(pm, Quaternion.identity, shape.KnotSize, Vector3.zero, Handles.CubeCap);

						//if ( shape.splines[s].knots[p].p != pm )
						//selected = p;
					//}

					delta = shape.splines[s].knots[p].p - pm;

					shape.splines[s].knots[p].invec += delta;
					shape.splines[s].knots[p].outvec += delta;

					if ( shape.splines[s].knots[p].p != pm )
					{
						selected = p;
						recalc = true;
					}

					pm = shape.transform.TransformPoint(shape.splines[s].knots[p].p);

					Handles.CubeCap(0, pm, Quaternion.identity, shape.KnotSize);
				}
#endif
			}
		}

		if ( recalc )
			shape.CalcLength(10);

		Handles.matrix = Matrix4x4.identity;
	}

	[DrawGizmo(GizmoType.NotSelected | GizmoType.Pickable)]
	static void RenderGizmo(MegaShape shape, GizmoType gizmoType)
	{
		if ( (gizmoType & GizmoType.NotSelected) != 0 )
		{
			if ( (gizmoType & GizmoType.Active) != 0 )
				DrawGizmos(shape, new Color(1.0f, 1.0f, 1.0f, 1.0f));
			else
				DrawGizmos(shape, new Color(1.0f, 1.0f, 1.0f, 0.25f));
		}
		Gizmos.DrawIcon(shape.transform.position, "MegaSpherify icon.png");
		Handles.Label(shape.transform.position, " " + shape.name);
	}

	// Dont want this in here, want in editor
	// If we go over a knot then should draw to the knot
	static void DrawGizmos(MegaShape shape, Color modcol)
	{
		if ( ((1 << shape.gameObject.layer) & Camera.current.cullingMask) == 0 )
			return;

		if ( !shape.drawspline )
			return;

		for ( int s = 0; s < shape.splines.Count; s++ )
		{
			float ldist = shape.stepdist;
			if ( ldist < 0.01f )
				ldist = 0.01f;

			float ds = shape.splines[s].length / (shape.splines[s].length / ldist);

			if ( ds > shape.splines[s].length )
			{
				ds = shape.splines[s].length;
			}

			int c	= 0;
			int k	= -1;
			int lk	= -1;

			Vector3 first = shape.splines[s].Interpolate(0.0f, shape.normalizedInterp, ref lk);

			for ( float dist = ds; dist < shape.splines[s].length; dist += ds )
			{
				float alpha = dist / shape.splines[s].length;
				Vector3 pos = shape.splines[s].Interpolate(alpha, shape.normalizedInterp, ref k);

				if ( (c & 1) == 1 )
					Gizmos.color = shape.col1 * modcol;
				else
					Gizmos.color = shape.col2 * modcol;

				if ( k != lk )
				{
					for ( lk = lk + 1; lk <= k; lk++ )
					{
						Gizmos.DrawLine(shape.transform.TransformPoint(first), shape.transform.TransformPoint(shape.splines[s].knots[lk].p));
						first = shape.splines[s].knots[lk].p;
					}
				}

				lk = k;

				Gizmos.DrawLine(shape.transform.TransformPoint(first), shape.transform.TransformPoint(pos));

				c++;

				first = pos;
			}

			if ( (c & 1) == 1 )
				Gizmos.color = shape.col1 * modcol;
			else
				Gizmos.color = shape.col2 * modcol;

			Vector3 lastpos;
			if ( shape.splines[s].closed )
				lastpos = shape.splines[s].Interpolate(0.0f, shape.normalizedInterp, ref k);
			else
				lastpos = shape.splines[s].Interpolate(1.0f, shape.normalizedInterp, ref k);

			Gizmos.DrawLine(shape.transform.TransformPoint(first), shape.transform.TransformPoint(lastpos));
		}
	}

	// Load stuff
	string lastpath = "";

	public delegate bool ParseBinCallbackType(BinaryReader br, string id);
	public delegate void ParseClassCallbackType(string classname, BinaryReader br);

	void LoadShape(float scale)
	{
		MegaShape ms = (MegaShape)target;
		//Modifiers mod = mr.GetComponent<Modifiers>();	// Do this at start and store

		string filename = EditorUtility.OpenFilePanel("Shape File", lastpath, "spl");

		if ( filename == null || filename.Length < 1 )
			return;

		lastpath = filename;

		// Clear what we have
		ms.splines.Clear();

		ParseFile(filename, ShapeCallback);

		ms.Scale(scale);

		ms.MaxTime = 0.0f;

		for ( int s = 0; s < ms.splines.Count; s++ )
		{
			if ( ms.splines[s].animations != null )
			{
				for ( int a = 0; a < ms.splines[s].animations.Count; a++ )
				{
					MegaControl con = ms.splines[s].animations[a].con;
					if ( con != null )
					{
						float t = con.Times[con.Times.Length - 1];
						if ( t > ms.MaxTime )
							ms.MaxTime = t;
					}
				}
			}
		}
	}

	public void ShapeCallback(string classname, BinaryReader br)
	{
		switch ( classname )
		{
			case "Shape": LoadShape(br); break;
		}
	}

	public void LoadShape(BinaryReader br)
	{
		//MegaMorphEditor.Parse(br, ParseShape);
		MegaParse.Parse(br, ParseShape);
	}

	public void ParseFile(string assetpath, ParseClassCallbackType cb)
	{
		FileStream fs = new FileStream(assetpath, FileMode.Open, FileAccess.Read, System.IO.FileShare.Read);

		BinaryReader br = new BinaryReader(fs);

		bool processing = true;

		while ( processing )
		{
			string classname = MegaParse.ReadString(br);

			if ( classname == "Done" )
				break;

			int	chunkoff = br.ReadInt32();
			long fpos = fs.Position;

			cb(classname, br);

			fs.Position = fpos + chunkoff;
		}

		br.Close();
	}

	static public Vector3 ReadP3(BinaryReader br)
	{
		Vector3 p = Vector3.zero;

		p.x = br.ReadSingle();
		p.y = br.ReadSingle();
		p.z = br.ReadSingle();

		return p;
	}

	bool SplineParse(BinaryReader br, string cid)
	{
		MegaShape ms = (MegaShape)target;
		MegaSpline ps = ms.splines[ms.splines.Count - 1];

		switch ( cid )
		{
			case "Transform":
				Vector3 pos = ReadP3(br);
				Vector3 rot = ReadP3(br);
				Vector3 scl = ReadP3(br);
				rot.y = -rot.y;
				ms.transform.position = pos;
				ms.transform.rotation = Quaternion.Euler(rot * Mathf.Rad2Deg);
				ms.transform.localScale = scl;
				break;

			case "Flags":
				int count = br.ReadInt32();
				ps.closed = (br.ReadInt32() == 1);
				count = br.ReadInt32();
				ps.knots = new List<MegaKnot>(count);
				ps.length = 0.0f;
				break;

			case "Knots":
				for ( int i = 0; i < ps.knots.Capacity; i++ )
				{
					MegaKnot pk = new MegaKnot();

					pk.p = ReadP3(br);
					pk.invec = ReadP3(br);
					pk.outvec = ReadP3(br);
					pk.seglength = br.ReadSingle();

					ps.length += pk.seglength;
					pk.length = ps.length;
					ps.knots.Add(pk);
				}
				break;
		}
		return true;
	}

	MegaKnotAnim ma;

	bool AnimParse(BinaryReader br, string cid)
	{
		MegaShape ms = (MegaShape)target;

		switch ( cid )
		{
			case "V":
				int v = br.ReadInt32();
				ma = new MegaKnotAnim();
				int s = ms.GetSpline(v, ref ma);	//.s, ref ma.p, ref ma.t);

				if ( ms.splines[s].animations == null )
					ms.splines[s].animations = new List<MegaKnotAnim>();

				ms.splines[s].animations.Add(ma);
				break;

			case "Anim":
				//ma.con = MegaBezVector3KeyControl.LoadBezVector3KeyControl(br);
				ma.con = MegaParseBezVector3Control.LoadBezVector3KeyControl(br);
				break;
		}
		return true;
	}

	bool ParseShape(BinaryReader br, string cid)
	{
		MegaShape ms = (MegaShape)target;

		switch ( cid )
		{
			case "Num":
				int count = br.ReadInt32();
				ms.splines = new List<MegaSpline>(count);
				//id = 0;
				break;

			case "Spline":
				MegaSpline spl = new MegaSpline();
				ms.splines.Add(spl);
				//MegaMorphEditor.Parse(br, SplineParse);
				MegaParse.Parse(br, SplineParse);
				break;

			case "Anim":
				//Debug.Log("Anim info");
				//MegaMorphEditor.Parse(br, AnimParse);
				MegaParse.Parse(br, AnimParse);
				break;
		}

		return true;
	}
}
