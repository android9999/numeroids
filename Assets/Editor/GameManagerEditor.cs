using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager gameManager = (GameManager)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Reset Stats"))
        {
            gameManager.ResetStats();
        }

        GUILayout.BeginHorizontal();

        gameManager.NewScore = EditorGUILayout.IntField("New Score", gameManager.NewScore);

        if (GUILayout.Button("Set Score"))
        {
            gameManager.SetScore();
        }

        GUILayout.EndHorizontal();
    }

    /*
    [CustomEditor(typeof(mathImage))]
    public class mathImageEditor : Editor
    {
        public Object RGBfield;

        public Object HSVfield;

        public override void OnInspectorGUI()
        {
            var mathimage = target as mathImage;

            mathimage.Name = EditorGUILayout.TextField("Name", mathimage.Name);

            mathimage.width = (short)EditorGUILayout.IntField("width", mathimage.width);

            mathimage.height = (short)EditorGUILayout.IntField("height", mathimage.height);

            mathimage.HSV = GUILayout.Toggle(mathimage.HSV, "HSV");

            if (mathimage.HSV)
            {
                mathimage.hExpression = EditorGUILayout.TextField("hExpression", mathimage.hExpression);

                mathimage.sExpression = EditorGUILayout.TextField("sExpression", mathimage.sExpression);

                mathimage.vExpression = EditorGUILayout.TextField("vExpression", mathimage.vExpression);
            }

            else
            {
                mathimage.rExpression = EditorGUILayout.TextField("rExpression", mathimage.rExpression);

                mathimage.gExpression = EditorGUILayout.TextField("gExpression", mathimage.gExpression);

                mathimage.bExpression = EditorGUILayout.TextField("bExpression", mathimage.bExpression);
            }

            mathimage.Panel = (GameObject)EditorGUILayout.ObjectField("Panel", mathimage.Panel, typeof(GameObject), true);

            mathimage.infoPanel = (GameObject)EditorGUILayout.ObjectField("info Panel", mathimage.infoPanel, typeof(GameObject), true);

            mathimage.Rfield = (GameObject)EditorGUILayout.ObjectField("R field", mathimage.Rfield, typeof(GameObject), true);

            mathimage.Gfield = (GameObject)EditorGUILayout.ObjectField("G field", mathimage.Gfield, typeof(GameObject), true);

            mathimage.Bfield = (GameObject)EditorGUILayout.ObjectField("B field", mathimage.Bfield, typeof(GameObject), true);

            mathimage.Hfield = (GameObject)EditorGUILayout.ObjectField("H field", mathimage.Hfield, typeof(GameObject), true);

            mathimage.Sfield = (GameObject)EditorGUILayout.ObjectField("S field", mathimage.Sfield, typeof(GameObject), true);

            mathimage.Vfield = (GameObject)EditorGUILayout.ObjectField("V field", mathimage.Vfield, typeof(GameObject), true);
        }
    }*/
}
