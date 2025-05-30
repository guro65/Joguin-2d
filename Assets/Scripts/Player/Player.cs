
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Player basic
    [Header("Player")]
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float moveH;
    private float moveV;
    [SerializeField] private float velocidade;
    private Animator anim;
    [SerializeField] private float ForcaPulo;
    private bool isPulo = false;
    private direcao direcao;

    //Dash
    [Header("Dash")]
    [SerializeField] private float duracao;
    public bool usando;
    private float comtagemDeTempo;
    [SerializeField] private float IntevaloUso;
    private float ContagemTempoIntervalo;
    private bool PodeUsar;
    [SerializeField] private int VelocidadeDash;

    //Escalada
    [Header("escalada & deslizar")]
    private bool escalada = false;
    private bool escaladaPodeUsar = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        this.direcao = direcao.direita;//ele so esta definido para a direita porque o personagem começa olhando para a direita.
        //Dash
        this.usando = false;
        this.comtagemDeTempo = 0;
        this.ContagemTempoIntervalo = 0;
        this.PodeUsar = true;
        escalada = false;
        this.isPulo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.usando)// Verifica se o dash esta em uso e trava os movimentos tem que arrumar depois.
        {
            moveH = Input.GetAxis("Horizontal");
            transform.position += new Vector3(moveH * velocidade * Time.deltaTime, 0, 0);


            if (escalada == true)
            {
                moveV = Input.GetAxis("Vertical");
                // transform.position += new Vector3((moveV * velocidade * Time.deltaTime,0,0))
                transform.position += new Vector3(moveH * velocidade * Time.deltaTime, moveV * velocidade * Time.deltaTime, 0);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    escalada = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                sprite.flipX = false;
                anim.SetLayerWeight(1, 1);
                this.direcao = direcao.direita;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                sprite.flipX = true;
                anim.SetLayerWeight(1, 1);
                this.direcao = direcao.esquerda;
            }

            if (moveH == 0)
            {
                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(1, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isPulo)
            {
                rb.AddForce(transform.up * ForcaPulo, ForceMode2D.Impulse);
                isPulo = true;
            }
            dashAtivo();
        }
        if (this.usando)
        {
            //verifica o tempo de uso do dash
            this.comtagemDeTempo += Time.deltaTime;
            if (comtagemDeTempo >= this.duracao)
            {
                this.comtagemDeTempo = 0;
                this.usando = false;
            }
            else
            {
                if (this.direcao == direcao.direita)//calcula a direção do dash.
                {
                    this.rb.linearVelocity = new Vector2(this.VelocidadeDash, 0);

                }
                else
                {
                    this.rb.linearVelocity = new Vector2(-this.VelocidadeDash, 0);

                }
            }

        }
        else
        {
            if (!this.PodeUsar)
            {
                this.ContagemTempoIntervalo += Time.deltaTime;//quando o intervalo acabar vai poder usar o dash.
                if (this.ContagemTempoIntervalo >= this.IntevaloUso)
                {
                    this.ContagemTempoIntervalo = 0;
                    this.PodeUsar = true;
                }
            }
        }


        if (escalada)
        {
            this.gameObject.CompareTag("Player");
            gameObject.CompareTag("Parede");
            escalada = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D other) //metodo para detectar coloizão.
    {
        if (other.gameObject.CompareTag("chão"))
        {
            isPulo = false;
        }
        if(other.gameObject.CompareTag("Morte"))
        {
            SceneManager.LoadScene(other.gameObject.GetComponent<Reload>().NomeCena());
        }

    }

    private void dashAtivo()
    {

        if (Input.GetKeyDown(KeyCode.Q))

        {
            this.aplicar(this.direcao);//pega a variavel (aplicar).
        }
    }

    private bool Usando()
    {
        return this.usando;
    }

    public void aplicar(direcao direcaoDash)
    {
        if (this.PodeUsar)
        {
            this.direcao = direcaoDash;
            this.usando = true;
            this.PodeUsar = false;

        }
    }
}
// esta funcionando
