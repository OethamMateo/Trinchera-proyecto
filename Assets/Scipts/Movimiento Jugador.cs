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
    private bool puedeMoverse = true; // Controlar si el jugador puede moverse
    private bool estaInteraccionando = false; // Controlar el estado de interacción

    void Start()
    {
        velocidadActual = velocidad; // Configurar la velocidad inicial
    }

    void Update()
    {
        // Verifica si se puede mover
        if (puedeMoverse && Input.GetKey(KeyCode.RightArrow))
        {
            // Incrementa el tiempo que la tecla ha estado presionada
            tiempoPresionado += Time.deltaTime;

            // Si se ha presionado por más de 3 segundos, aplica la velocidad acelerada
            if (tiempoPresionado >= 3f)
            {
                velocidadActual = velocidadAcelerada;
            }

            // Mueve el cuadrado hacia la derecha con la velocidad actual
            transform.Translate(Vector2.right * velocidadActual * Time.deltaTime);
        }
        else
        {
            // Reinicia el tiempo y la velocidad cuando se suelta la tecla
            tiempoPresionado = 0f;
            velocidadActual = velocidad;

            // Verifica si está cerca del NPC y no se está moviendo
            if (estaInteraccionando)
            {
                DeshabilitarMovimiento(tiempoInmovilizacion); // Deshabilita el movimiento
            }
        }
    }

    // Método público para deshabilitar el movimiento del jugador
    public void DeshabilitarMovimiento(float duracion)
    {
        StartCoroutine(DeshabilitarMovimientoCoroutine(duracion));
    }

    private System.Collections.IEnumerator DeshabilitarMovimientoCoroutine(float duracion)
    {
        puedeMoverse = false; // Deshabilita el movimiento
        yield return new WaitForSeconds(duracion); // Espera el tiempo especificado
        puedeMoverse = true; // Habilita el movimiento de nuevo
    }

    // Método para establecer el estado de interacción
    public void EstablecerInteraccion(bool estado)
    {
        estaInteraccionando = estado; // Cambia el estado de interacción
    }
}




