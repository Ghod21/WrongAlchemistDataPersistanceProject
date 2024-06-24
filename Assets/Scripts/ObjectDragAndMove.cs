using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragAndMove : MonoBehaviour
{
    Vector3 mousePosition;
    private Counter counter;

    private AudioSource audioSource;
    public AudioClip[] audioClips;
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        audioSource = mainCamera.GetComponent<AudioSource>();
        // Получаем объект "Counter" из сцены по его имени
        GameObject counterObject = GameObject.Find("Counter");

        // Получаем компонент Counter из найденного объекта
        counter = counterObject.GetComponent<Counter>();

    }

    public string draggingTag;
    private Camera cam;
    private Vector3 dis;
    private float posX;
    private float posY;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidbody;
    private Vector3 previousPosition;

    void FixedUpdate()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (Input.touchCount != 1)
        {
            dragging = false;
            touched = false;
            if (toDragRigidbody)
            {
                SetFreeProperties(toDragRigidbody);
            }
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == draggingTag)
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = cam.WorldToScreenPoint(previousPosition);
                posX = Input.GetTouch(0).position.x - dis.x;
                posY = Input.GetTouch(0).position.y - dis.y;

                SetDraggingProperties(toDragRigidbody);

                touched = true;
            }
        }

        if (touched && touch.phase == TouchPhase.Moved)
        {
            dragging = true;

            float posXNow = Input.GetTouch(0).position.x - posX;
            float posYNow = Input.GetTouch(0).position.y - posY;
            Vector3 curPos = new Vector3(posXNow, posYNow, dis.z);

            Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
            worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

            toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);

            previousPosition = toDrag.position;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            touched = false;
            previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
            SetFreeProperties(toDragRigidbody);
        }

    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        rb.useGravity = false;
        rb.drag = 20;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        rb.useGravity = true;
        rb.drag = 5;
    }



    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectDestroy());
    }
    IEnumerator ObjectDestroy()
    {
        yield return new WaitForSeconds(3);
        if (gameObject != null)
        {
            if (counter.healthLeft > 1)
            {
                audioSource.PlayOneShot(audioClips[0], 0.5f);
            }
            counter.objDestroy = true;
            Destroy(gameObject);
            if (counter.healthLeft > 0)
            {
                counter.healthLeft--;
            }
            counter.Count = (int)(counter.Count * 0.8);
        }
    }
}
