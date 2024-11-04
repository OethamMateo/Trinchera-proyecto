using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad normal del jugador
    public float velocidadAcelerada = 10f; // Velocidad cuando se mantiene presionada la tecla
    public float tiempoInmovilizacion = 3f; // Tiempo de inmovilización al interactuar con el NPC

    private float tiempoPresionado = 0f; // Tiempo que se ha mantenido presionada la tecla
    private float velocidadActual; // Velocidad actual del jugador
    private bool puedeMoverse = true; // Controla si el jugador puede moverse
    private bool estaInteraccionando = false; // Controla el estado de interacción

    private Animator animator; // Referencia al Animator

    void Start()
    {
        velocidadActual = velocidad; // Configura la velocidad inicial
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
    }

    void Update()
    {
        // Verifica si el jugador puede moverse
        if (puedeMoverse && !estaInteraccionando && Input.GetKey(KeyCode.RightArrow))
        {
            tiempoPresionado += Time.deltaTime;

            // Si se ha presionado por más de 3 segundos, aplica la velocidad acelerada
            if (tiempoPresionado >= 3f)
            {
                velocidadActual = velocidadAcelerada;
            }

            // Mueve al jugador hacia la derecha con la velocidad actual
            transform.Translate(Vector2.right * velocidadActual * Time.deltaTime);

            // Configura el parámetro "Velocidad" para activar la animación de correr
            animator.SetFloat("Velocidad", velocidadActual);
        }
        else
        {
            // Reinicia el tiempo y la velocidad cuando se suelta la tecla
            tiempoPresionado = 0f;
            velocidadActual = velocidad;

            // Configura el parámetro "Velocidad" en 0 para activar la animación de idle
            animator.SetFloat("Velocidad", 0);
        }
    }

    // Método público para deshabilitar el movimiento del jugador temporalmente
    public void DeshabilitarMovimiento(float duracion)
    {
        StartCoroutine(DeshabilitarMovimientoCoroutine(duracion));
    }

    private System.Collections.IEnumerator DeshabilitarMovimientoCoroutine(float duracion)
    {
        puedeMoverse = false; // Deshabilita el movimiento
        animator.SetFloat("Velocidad", 0); // Cambia a animación Idle
        yield return new WaitForSeconds(duracion); // Espera el tiempo especificado
        puedeMoverse = true; // Habilita el movimiento de nuevo
    }

    // Método para establecer el estado de interacción
    public void EstablecerInteraccion(bool estado)
    {
        estaInteraccionando = estado; // Cambia el estado de interacción
        if (!estado) puedeMoverse = true; // Permite el movimiento si no está interactuando
    }
}






