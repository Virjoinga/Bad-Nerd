using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StaticMeshPostProcessor : EditorWindow
{
    static List<MeshFilter> filters = new List<MeshFilter>();
    static List<ParticleRenderer> particleRenderers = new List<ParticleRenderer>();

    static bool wasSet;

    [MenuItem("BadTools/Static Mesh Post Processor")]
    static void GetMe()
    {
        Search();
        GetWindow<StaticMeshPostProcessor>("SMPP");
    }

    void OnGUI()
    {
        if (!wasSet)
        {
            ClearAll();
            EditorSceneManager.sceneLoaded += (scene, mode) => { Search(); };
            wasSet = true;
        }

        if (GUILayout.Button("Search")) Search();

        GUILayout.Label("Static Mesh Filters Found: " + filters.Count);
        GUILayout.Label("Static Particle Renderers Found: " + particleRenderers.Count);

        if (GUILayout.Button("Unmark"))
        {
            foreach (MeshFilter filter in filters) 
                if (filter != null) filter.gameObject.isStatic = false;

            foreach (ParticleRenderer particleRenderer in particleRenderers)
                if (particleRenderer != null) particleRenderer.gameObject.isStatic = false;

            EditorSceneManager.MarkAllScenesDirty();
            Search();
        }
    }

    static void Search()
    {
        ClearAll();

        foreach (Renderer meshRenderer in FindObjectsOfType<Renderer>())
        {
            if (!meshRenderer.gameObject.isStatic) continue;

            MeshFilter m = meshRenderer.GetComponent<MeshFilter>();

            if (m != null && !m.sharedMesh.name.StartsWith("Combined Mesh")) filters.Add(m);
        }

        foreach (ParticleRenderer particleRenderer in FindObjectsOfType<ParticleRenderer>())
        {
            if (particleRenderer.gameObject.isStatic) particleRenderers.Add(particleRenderer);
        }
    }

    static void ClearAll()
    {
        filters.Clear();
        particleRenderers.Clear();
    }
}
