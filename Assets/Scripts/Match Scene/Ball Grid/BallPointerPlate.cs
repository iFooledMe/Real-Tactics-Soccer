using UnityEngine;

public class BallPointerPlate : MonoBehaviour
{
    private MatchManager MatchManager;

    [SerializeField]
    private float yAxisPosition = 0.25f;

    void Awake()
    {
        this.MatchManager = MatchManager.Instance;
    }
    
    void Update()
    {
        followMouse();
    }

    private void followMouse()
    {
        if (MatchManager.MatchPlayerManager.CurrentActivePlayer.PlayerMode == PlayerMode.Pass)
        {
            Plane plane=new Plane(Vector3.up,new Vector3(0, yAxisPosition, 0));
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            
            if(plane.Raycast(ray, out distance)) 
            {
                transform.position=ray.GetPoint(distance);
            }
        }
    }

    private void OnTriggerEnter(Collider collidingPoint)
    {
        BallGridPoint ballPoint = collidingPoint.GetComponent<BallGridPoint>();
        MatchManager.BallGrid.SetCurrentPoint(ballPoint);
        MatchManager.BallPointer.SetPosition();
    }
}


