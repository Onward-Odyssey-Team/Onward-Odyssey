using UnityEngine;
using System.Threading.Tasks;
public class SelfDestructor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
    await Task.Delay(2000);
    Destroy(transform.gameObject);
    }
}
