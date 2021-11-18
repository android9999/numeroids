using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConsumableObject : MonoBehaviour//, IPointerDownHandler
{
    [SerializeField]
    private List<Consumable> consumables = new List<Consumable>();

    [SerializeField]
    public Consumable consumable;

    private GameManager gameManager;

    public float size;

    // Start is called before the first frame update
    void Awake()
    {
        //AddPhysics2DRaycaster();

        int random = Random.Range(0, consumables.Count);

        consumable = consumables[random];

        GetComponent<SpriteRenderer>().sprite = consumable.sprite;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        transform.localScale = Vector3.one * 0.25f * size;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LayerMask layerMask = LayerMask.GetMask("Consumables");

            Debug.Log($"layermask is equal to {layerMask.value}");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 30);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, layerMask, -100000,100000);

            if (hit.collider != null)
            {
                Debug.Log("CLICKED " + hit.collider.name);

                gameManager.ConsumableTaken(this);

                Destroy(gameObject);
            }
        }
    }

    /*public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.ConsumableTaken(this);

        Destroy(gameObject);

        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }*/

}
