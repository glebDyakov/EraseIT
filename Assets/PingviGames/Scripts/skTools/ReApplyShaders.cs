using UnityEngine;
using UnityEngine.UI;

public class ReApplyShaders : MonoBehaviour
{
    private Renderer[] renderers;
    private Material[] materials;
    private string[] shaders;

    void Awake()
    {
        //renderers = GetComponentsInChildren<Renderer>();
    }

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (var rend in renderers)
        {
            /*
            materials = rend.sharedMaterials;
            shaders = new string[materials.Length];

            for (int i = 0; i < materials.Length; i++)
            {
                shaders[i] = materials[i].shader.name;
            }

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].shader = Shader.Find(shaders[i]);
            }
            */
            foreach (Material m in rend.sharedMaterials)
            {

                if (m != null && m.shader != null)
                    m.shader = Shader.Find(m.shader.name);

            }
        }

        //for ui
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            Material m = img.material;

            if (m != null && m.shader != null)
                m.shader = Shader.Find(m.shader.name);
        }
    }
}