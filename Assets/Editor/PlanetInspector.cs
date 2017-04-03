using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetBase))]
public class PlanetInspector : Editor {
    PlanetBase planet;
    public const int MIN= 0;
    public void OnEnable()
    {
        planet = (PlanetBase)target;
    }
   

    public override void OnInspectorGUI()
    {
        DrawConditions();
        DrawInfo();
        serializedObject.Update();
        GUIStyle myStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
        myStyle.alignment = TextAnchor.MiddleCenter;
        SerializedProperty myElem = serializedObject.FindProperty("mainElements");

        EditorGUILayout.PropertyField(myElem, true);
        serializedObject.ApplyModifiedProperties();
  
    }

    private void DrawInfo()
    {
        EditorGUILayout.LabelField("Planet Info", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        planet.mainColor = EditorGUILayout.ColorField("Planet Color", planet.mainColor);
        planet.orbitTime= EditorGUILayout.FloatField(new GUIContent("Orbit Time (Years)","Time it takes for planet to orbit around its mother star"),Mathf.Max( planet.orbitTime,0));
        planet.revolutionTime= EditorGUILayout.FloatField(new GUIContent("Revolution Time (Hours)","The time it takes to complete one revolution"), Mathf.Max(planet.revolutionTime,0));
        planet.moonAmount= EditorGUILayout.IntField("Moon Amount", Mathf.Max(planet.moonAmount, 0));
        planet.highTemp= EditorGUILayout.FloatField("High Temp",planet.highTemp);
        planet.lowTemp= EditorGUILayout.FloatField("Low Temp",planet.lowTemp);
        EditorGUILayout.MinMaxSlider(new GUIContent("Temp Range (In Celcius)","Temperature Value between -100 degrees C and 200 C."), ref planet.lowTemp, ref planet.highTemp, -100f, 100f);
        EditorGUILayout.FloatField(new GUIContent("High Elavation","In Miles"), planet.highElevation);
        EditorGUILayout.FloatField(new GUIContent("Low Eleavation","In Miles"), planet.lowElevation);
        EditorGUILayout.MinMaxSlider(new GUIContent("Elevation", "Elevation: (in miles)"), ref planet.lowElevation, ref planet.highElevation, -50f, 1000f);

        planet.radiationAmount=EditorGUILayout.FloatField(new GUIContent("Rads Level","Level of Radiation, measured in units Rads. Earth is 10 rads(Safe for life)"),Mathf.Max( planet.radiationAmount,0));
        planet.planetSize=EditorGUILayout.IntField(new GUIContent("Planet size in Miles", "Earth is 3,959 miles"), Mathf.Max( planet.planetSize,0));
        EditorGUILayout.EndVertical();
    }

    private void DrawConditions()
    {
        
        EditorGUILayout.LabelField("Planet Qualifications", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        planet.hasWater = EditorGUILayout.Toggle("Has Water", planet.hasWater);
        GUI.enabled = planet.hasWater;
        planet.isHabitable = EditorGUILayout.Toggle("Supports Life", planet.isHabitable);
        GUI.enabled = true;
        GUI.enabled = planet.isHabitable&&planet.hasWater;
        planet.intelligentCreatures = EditorGUILayout.Toggle("Intillegnet Creatures", planet.intelligentCreatures);
        GUI.enabled = planet.isHabitable &&planet.intelligentCreatures&& planet.hasWater;
        planet.icPopulation = EditorGUILayout.IntField("# of Creatures", Mathf.Max(planet.icPopulation, 0));
        GUI.enabled = true;
        GUI.enabled = planet.isHabitable && planet.hasWater;
        planet.flora = EditorGUILayout.Toggle(new GUIContent("Has Flora","Plant Life"), planet.flora);
        GUI.enabled = true;
        GUI.enabled = planet.flora && planet.isHabitable && planet.hasWater;
        planet.fauna = EditorGUILayout.Toggle(new GUIContent("Has Fauna","Animal Life"), planet.fauna);
        GUI.enabled = true;
        EditorGUILayout.EndVertical();
    }


}
