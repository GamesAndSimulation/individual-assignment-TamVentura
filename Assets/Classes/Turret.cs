using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{

    public PlayerController playerController;
    public CamerasController camerasController;
    public LayerMask playerMask;
    public float timeToKill = 1f;
    private float timer = 0f;

    public Color c1 = Color.red;
    public int lengthOfLineRenderer = 20;

    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = lengthOfLineRenderer;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f)}
        );
        lineRenderer.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        if (Physics.Raycast(transform.position, playerController.transform.position - transform.position, out hit, 100))
        {
            
            if (playerMask == (playerMask | (1 << hit.transform.gameObject.layer)))
            {
                if(timer >= timeToKill)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    Resources.UnloadUnusedAssets();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
                }
                else
                {
                    timer+= Time.deltaTime;

                    lineRenderer.enabled = true;
                    lineRenderer.positionCount=2;

                    Vector3[] positions = new Vector3[] { this.transform.position, playerController.transform.position };
                    lineRenderer.SetPositions(positions);

                    Gradient gradient = new Gradient();
                    gradient.SetKeys(
                        new GradientColorKey[] { new GradientColorKey(c1, 0.0f) },
                        new GradientAlphaKey[] { new GradientAlphaKey(timer/timeToKill, 0.0f) }
                    );
                    lineRenderer.colorGradient = gradient;


                }

            }
            else
            {
                timer = 0f;
                lineRenderer.enabled = false;
            }
        }
        else
        {
            timer = 0f;
            lineRenderer.enabled = false;
        }

        transform.LookAt(playerController.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
