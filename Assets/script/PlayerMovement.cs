using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 3f;
    public float velocidadeRotacao = 120f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Rodar esquerda/direita (teclas A e D ou setas)
        float horizontal = Input.GetAxis("Horizontal") * velocidadeRotacao * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        // Andar para a frente/trás (W e S)
        float vertical = Input.GetAxis("Vertical") * velocidade;

        // Movimento
        Vector3 movimento = transform.forward * vertical;
        controller.Move(movimento * Time.deltaTime);
    }
}
