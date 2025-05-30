
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float px;
    [SerializeField] private float py;
    private Vector3 pontoInicial;
    // Start is called before the first frame update
    void Start()
    {
        pontoInicial = transform.position;
        player = GameObject.FindWithTag("Player");//procura o objeto com a tag player
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //pega a posição do player
            px = player.transform.position.x;
            py = player.transform.position.y;
        }
        //distancia onde a camera percorre  
        if (px > -3 && py > -3)
        {
            transform.position = new Vector3(px, py, -1);
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            transform.position = pontoInicial;
        }
    }
}