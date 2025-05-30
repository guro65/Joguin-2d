using UnityEngine;

public class Reload : MonoBehaviour
{
    public string cena;
    public string nome;
    public bool entrada;

    public string NomePortal()
    {
        return nome;
    }
    public bool EntradaOuSaida()
    {
        return entrada;
    }

    public string NomeCena()
    {
        return cena;
    }
}
