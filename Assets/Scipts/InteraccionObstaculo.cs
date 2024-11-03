using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionObstaculo : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaInteraccion = 1.5f; // Distancia para activar la interacción con el obstáculo
    public float duracionPenalizacion = 2f; // Duración base de la penalización
    public float duracionPenalizacionExtra = 4f; // Duración extra de la penalización si no cumple con el mínimo de NPCs
    public int minNPCs = 3; // Número mínimo de NPCs que el jugador debe alcanzar
    private static int contadorNPCs = 0; // Contador de NPCs alcanzados (compartido entre todos los obstáculos)
    private bool enInteraccion = false; // Controla si la interacción está activa
    private float tiempoInteraccion = 0f; // Temporizador para controlar la duración de la interacción

    private MovimientoJugador movimientoJugador; // Referencia al script de movimiento del jugador

    private void Start()
    {
        // Obtiene el componente MovimientoJugador del jugador
        movimientoJugador = jugador.GetComponent<MovimientoJugador>();
    }

    private void Update()
    {
        // Verificar la distancia entre el jugador y el obstáculo
        float distancia = Vector2.Distance(transform.position, jugador.position);

        // Iniciar la interacción si el jugador está cerca del obstáculo y no está en interacción
        if (distancia <= distanciaInteraccion && !enInteraccion)
        {
            IniciarInteraccion();
        }

        // Si la interacción está activa, controlar el temporizador de penalización
        if (enInteraccion)
        {
            ControlarInteraccion();
        }
    }

    private void IniciarInteraccion()
    {
        enInteraccion = true; // Activa la interacción
        tiempoInteraccion = 0f; // Reinicia el temporizador de interacción

        // Determina la duración penalizada basada en el contador de NPCs
        float penalizacionActual = (contadorNPCs < minNPCs) ? duracionPenalizacionExtra : duracionPenalizacion;
        movimientoJugador.DeshabilitarMovimiento(penalizacionActual); // Deshabilita el movimiento del jugador por la penalización actual
    }

    private void ControlarInteraccion()
    {
        tiempoInteraccion += Time.deltaTime; // Incrementa el tiempo de interacción

        // Verifica si el tiempo de penalización ha terminado
        float penalizacionActual = (contadorNPCs < minNPCs) ? duracionPenalizacionExtra : duracionPenalizacion;
        if (tiempoInteraccion >= penalizacionActual)
        {
            FinalizarInteraccion();
        }
    }

    private void FinalizarInteraccion()
    {
        enInteraccion = false; // Finaliza la interacción
        movimientoJugador.EstablecerInteraccion(false); // Permite que el jugador se mueva de nuevo
    }

    // Método para actualizar el contador de NPCs (llamar este método cada vez que el jugador interactúe con un NPC)
    public static void IncrementarNPCContador()
    {
        contadorNPCs++;
        Debug.Log("Contador de NPCs: " + contadorNPCs); // Para verificar el contador en la consola
    }
}















