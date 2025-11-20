using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] public GameObject platformPrefab;
    
    private static int PLATFORMS_NUM = 4;

    private GameObject[] platforms;

    private Vector3[] positions;
    private float[] angles;

    private float radius = 4.2137f;
    float angleStep = 360f / PLATFORMS_NUM;
    private float rotationSpeed =5;
    // Start is called before the first frame update
    void Start()
    {
      

        for (int i = 0; i < PLATFORMS_NUM; i++)
        {  
            float angle = i * angleStep;
            float x = transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 platformPosition = new Vector2(x, y);
            platforms[i] = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            positions[i] = platformPosition; 
            angles[i] = angle; 
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        float rotation = rotationSpeed * Time.deltaTime;

        for (int i = 0; i < PLATFORMS_NUM; i++)
        {
            angles[i] += rotation; 

            float x = transform.position.x + radius * Mathf.Cos(angles[i] * Mathf.Deg2Rad);
            float y = transform.position.y + radius * Mathf.Sin(angles[i] * Mathf.Deg2Rad);

            Vector2 platformPosition = new Vector2(x, y);
            platforms[i].transform.position = Vector3.MoveTowards(platforms[i].transform.position, platformPosition, rotation);

            if (Mathf.Approximately(Vector2.Distance(platforms[i].transform.position, platformPosition), 0f))
            {
                angles[i] += angleStep;
            }
        }
    }

    void Awake()
    {
        platforms = new GameObject[PLATFORMS_NUM];
        positions = new Vector3[PLATFORMS_NUM];
        angles = new float[PLATFORMS_NUM];
    }
}
