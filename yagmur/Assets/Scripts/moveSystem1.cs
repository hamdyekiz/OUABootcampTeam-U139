using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveSystem1 : MonoBehaviour
{
   
  
    /* public float speed = 10f;

     // Update is called once per frame
     void Update()
     {
         float xHorizontal = Input.GetAxis("Horizontal");
         float zVertical = Input.GetAxis("Vertical");
         Vector3 moveSystem = new Vector3(xHorizontal, 0.0f, zVertical);
         transform.position += moveSystem * speed * Time.deltaTime;

     }*/
    public float moveSpeed = 5f;
    public float healthDecreaseRate = 1f; // Can azalma hızı
    private int currentHexagonIndex = 1;
    private float currentHealth = 100f;

    public Image healthImage;
    private void Start()
    {
        healthImage = GetComponent<Image>();
        UpdateHealthBar();
    }

    private void Update()
    {
        // Hareket kontrolleri
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Hareket vektörü oluşturma
        Vector3 moveSystem = new Vector3(moveX, 0.0f, moveZ);
        transform.position += moveSystem *moveSpeed* Time.deltaTime;



        // Can azalması
        if (currentHexagonIndex > 0 && !IsOnHexagon())
        {
            currentHealth -= healthDecreaseRate * Time.deltaTime;
            if (currentHealth <= 0f)
            {
                // Ölme işlemleri
                Die();
            }
            UpdateHealthBar();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 1; i <= 28; i++)
        {
            string hexagonTag = "Hex (" + i.ToString() + ")";

            if (other.CompareTag(hexagonTag))
            {
                int index = GetHexagonIndex(other.gameObject.name);
                if (index > currentHexagonIndex)
                {
                    currentHexagonIndex = index;
                }
                break;
            }
        }
    }

    private bool IsOnHexagon()
    {
        for (int i = 1; i <= 28; i++)
        {
            string hexagonTag = "Hex (" + i.ToString() + ")";
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag(hexagonTag))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private int GetHexagonIndex(string hexagonName)
    {
        int startIndex = hexagonName.IndexOf('(');
        int endIndex = hexagonName.IndexOf(')');
        string indexString = hexagonName.Substring(startIndex + 1, endIndex - startIndex - 1);
        int index = int.Parse(indexString);
        return index;
    }
    private void Die()
    {
        // Karakterin ölme işlemleri
        // Örneğin, oyunu yeniden başlatma veya karakteri başlangıç noktasına geri yerleştirme
    }
    private void UpdateHealthBar()
    {
        healthImage.fillAmount = currentHealth / 100f;
    }
}
