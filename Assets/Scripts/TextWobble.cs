using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWobble : MonoBehaviour
{
    public float Wob1 = 3.3f;
    public float Wob2 = 2.5f;
    Mesh mesh;
    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    [SerializeField] TextMeshProUGUI TextWoble;

    private void Start()
    {
        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = TextWoble.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    void Update()
    {
        TextWoble.ForceMeshUpdate();
        mesh = TextWoble.mesh;
        vertices = mesh.vertices;

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = TextWoble.textInfo.characterInfo[wordIndex + i];

                int index = c.vertexIndex;

                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
                vertices[index + 4] += offset;
            }
        }
        mesh.vertices = vertices;
        TextWoble.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * Wob1), Mathf.Cos(time * Wob2));
    }
}
