using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform Player;
    public Vector2 Smoothing;
    public Vector2 Margin;
    public BoxCollider2D Bounds;

    private Vector3 _min;
    private Vector3 _max;
    private Camera Camera;

    public bool IsFollowing { get; set; }

    void Start()
    {
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
        IsFollowing = true;
        Camera = GetComponent<Camera>();
        transform.position = new Vector3(Player.position.x, transform.position.y, -1);
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (IsFollowing)
        {
            if (Mathf.Abs(x - Player.position.x) > Margin.x)
            {
                x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
            }
            if (Mathf.Abs(y - Player.position.y) > Margin.y)
            {
                y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
            }
        }

        float cameraHalfWidth = Camera.orthographicSize * ((float)Screen.width / Screen.height);
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, _min.y + Camera.orthographicSize, _max.y - Camera.orthographicSize);
        transform.position = new Vector3(x, y, transform.position.z);
    }

}
