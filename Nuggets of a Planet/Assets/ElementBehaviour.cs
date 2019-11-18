using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour
{
    // private IslandBehaviour IslandScript;
    // private GameObject island;
    // private bool islandIsUp;
    // private MeshRenderer mesh;

    // private GameObject[] elements;

    // Start is called before the first frame update
    // void Start()
    // {
    // // Look for all active elements in the game and put in array
    // elements = GameObject.FindGameObjectsWithTag("Element");

    // mesh = this.GetComponent<MeshRenderer>();
    // mesh.enabled = false;
    // island = GameObject.Find("Fire Island");
    // IslandScript = island.GetComponent<IslandBehaviour>();

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //   islandIsUp = IslandScript.islandAppeared();
    //   Debug.Log();

    //   if (islandIsUp) StartCoroutine(ElementReveal());
    // }

        //private GameObject halo;
        // private ParticleSystem dust;
        // private ParticleSystem streaks;

        private ParticleSystem.EmissionModule dustEm;
        private ParticleSystem.EmissionModule streaksEm;
        private ParticleSystem.MainModule dustParticles;
        private ParticleSystem.MainModule streaksParticles;
        private GameObject halo;

        private MeshRenderer mesh;
        private IslandBehaviour islandScript;

        int num = 0;
        int num2 = 0;
        float alphaIndex = 0;

        private bool newIsland;
        private GameObject element;

        private GameObject[] elements;

        void Start() {
            

            // elements = GameObject.FindGameObjectsWithTag("Element");

            // foreach (GameObject el in elements)
            // {
               
            //     Material m = el.GetComponent<Renderer>().material;

            //     m.SetFloat("_Mode", 2);

            //     m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

            //     m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            //     m.SetInt("_ZWrite", 0);

            //     m.DisableKeyword("_ALPHATEST_ON");

            //     m.EnableKeyword("_ALPHABLEND_ON");

            //     m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            //     m.renderQueue = 3000;
            // }
            

            islandScript = GameObject.Find("Water Island").GetComponent<IslandBehaviour>();

            newIsland = false;
            halo = GameObject.Find("Aura");
            GameObject dustObj = halo.transform.GetChild(0).gameObject;
            GameObject streaksObj = halo.transform.GetChild(1).gameObject;

            ParticleSystem dust = dustObj.GetComponent<ParticleSystem>();
            ParticleSystem streaks = streaksObj.GetComponent<ParticleSystem>();

            dustParticles = dust.main;
            streaksParticles = streaks.main;    

            dustEm = dust.emission;
            streaksEm = streaks.emission;

            dustEm.enabled = false;
            streaksEm.enabled = false;

        }

        void Update() {

            //Debug.Log("num : "+num);
            
            if (newIsland) {

            var firstSpaceIndex = islandScript.recentIsland().IndexOf(" ");

            string elementName = islandScript.recentIsland().Substring(0, firstSpaceIndex);
            element = GameObject.Find(elementName);

            streaksEm.enabled = true;
            dustEm.enabled =  true;




            SetMaterialOpaque();

            iTween.FadeTo(element, 0, 1);
    

                if(num < 120) {
                    num++;
                    streaksParticles.maxParticles = num/4;
                    //streaksEm.rate = num;
                } else {
                    num = 121;
                    streaksParticles.maxParticles = 30;
                }
                //else streaksEm.rate = 50.0f;
                
                if(num2 < 60) {
                    num2++;
                    dustParticles.maxParticles = num2/4;
                    //dustEm.rate = num2;
                } else {
                    num2 = 61;
                    dustParticles.maxParticles = 15;
                }

                checkIfVisitedNew();
            }
        }

        //     private IEnumerator FadeIn(GameObject element) {
        //         Renderer rend = element.GetComponent<Renderer>();
        //         //Material m_material = rend.material;

        //         for (float f = 0.05f; f <= 1; f += 0.05f) {
        //             Color c = rend.material.color;
        //             c.a = f;
        //             rend.material.color = c;
        //             yield return new WaitForSeconds(0.05f);
        //         }
        //     }

        // private void startFading(GameObject element) {
        //     StartCoroutine(FadeIn(element));
        // }

        private void SetMaterialTransparent() {
            //Material m = element.GetComponent<Renderer>().material;
            elements = GameObject.FindGameObjectsWithTag("Element");

            foreach (GameObject el in elements)
            {
               
                Material m = el.GetComponent<Renderer>().material;

                m.SetFloat("_Mode", 2);

                m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                m.SetInt("_ZWrite", 0);

                m.DisableKeyword("_ALPHATEST_ON");

                m.EnableKeyword("_ALPHABLEND_ON");

                m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                m.renderQueue = 3000;
            }
        }

            private void SetMaterialOpaque()

    {

        Material m = element.GetComponent<Renderer>().material;  

            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            m.SetInt("_ZWrite", 1);

            m.DisableKeyword("_ALPHATEST_ON");

            m.DisableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = -1;


    }

        private void checkIfVisitedNew() {

            //element.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

            GameObject player = GameObject.Find("Player");
            
            float dist = Vector3.Distance(player.transform.position, element.transform.position);
            float minDist = 0.8f;

            if (dist < minDist) {
            streaksEm.enabled = false;
            dustEm.enabled =  false;
            newIsland = false;

            }
        }

        // public void DeactivateHalo(string islandName, bool inRange, GameObject nearestElement) {
        // var firstSpaceIndex = islandName.IndexOf(" ");

        // string elementName = islandName.Substring(0, firstSpaceIndex);
        // GameObject element = GameObject.Find(elementName);

        // if (inRange) if(nearestElement == element) Debug.Log("hello");
        // }

        // private IEnumerator IncreaseEm() {
        //     int num = 0;
            

        //     if(num < 25) {
        //         num += 2;
        //         streaksEm.rate = num;
        //     } else streaksEm.rate = 50.0f;
            
        //     if(num < 50) {
        //         num += 2;
        //         dustEm.rate = num;
        //     } else dustEm.rate = 50.0f;
        //     Debug.Log(num);

        // }

    //     private IEnumerator ElementAppearance(GameObject element)
    // {

    //     float alpha = 0;
    //     halo.transform.position = new Vector3(element.transform.position.x, halo.transform.position.y, element.transform.position.z);

    //     MeshRenderer mesh = element.GetComponent<MeshRenderer>();
    //     //StartCoroutine("IncreaseEm");
    //     mesh.enabled = true;

    //     if (alpha < 1) {
    //         alpha++;
    //         mesh.material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    //     } else mesh.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    //     yield return new WaitForSeconds(0.5f);
    //     ready = true;
    //     // mesh.enabled = true;
    //     //IncreaseEm();
    // }

    private IEnumerator ControlBool() {
        yield return new WaitForSeconds(0.3f);
        newIsland = true;
    }

    public void ElementReveal(string islandName) {
        // Get the first word only of the island name, which is our element name
        var firstSpaceIndex = islandName.IndexOf(" ");

        string elementName = islandName.Substring(0, firstSpaceIndex);
        GameObject element = GameObject.Find(elementName);
        halo.transform.position = new Vector3(element.transform.position.x, halo.transform.position.y, element.transform.position.z);
        mesh = element.GetComponent<MeshRenderer>();
        //mesh.enabled = true;
        //startFading(element);

        StartCoroutine("ControlBool");
    }

}
