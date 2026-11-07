using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                Debug.Log("Firebase conectado correctamente");

                GuardarDatos();

                CargarDatos();
            }
            else
            {
                Debug.LogError(
                    "No se pudieron resolver las dependencias de Firebase: " +
                    dependencyStatus
                );
            }
        });
    }

    public void GuardarDatos()
    {
        databaseReference.Child("jugador").Child("monedas").SetValueAsync(10);
        databaseReference.Child("jugador").Child("nivel1").SetValueAsync(true);
        databaseReference.Child("jugador").Child("nivel2").SetValueAsync(false);

        Debug.Log("Datos enviados a Firebase");
    }

    public void CargarDatos()
    {
        databaseReference.Child("jugador").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int monedas = int.Parse(snapshot.Child("monedas").Value.ToString());
                bool nivel1 = bool.Parse(snapshot.Child("nivel1").Value.ToString());
                bool nivel2 = bool.Parse(snapshot.Child("nivel2").Value.ToString());

                Debug.Log("Monedas: " + monedas);
                Debug.Log("Nivel 1: " + nivel1);
                Debug.Log("Nivel 2: " + nivel2);
            }
        });
    }

}