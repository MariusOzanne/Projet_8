using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWheel : MonoBehaviour
{
    [SerializeField] private Image greenWheel;
    [SerializeField] private Image redWheel;
    private void Start()
    {
        Player player = GetComponent<Player>();

    }
    void Update()
    {
        //StaminaWheelManager();
    }

    /* private void StaminaWheelManager()
     {
         // Player 1
         if (player.playerNumber == 1 && (Input.GetKey(KeyCode.Space)))
         {
             if (stamina > 0)
             {
                 player.speed += speedAcceleration * Time.deltaTime;
                 stamina -= 30 * Time.deltaTime;
             }
             redWheel.fillAmount = (stamina / maxStamina + 0.07f);
         }
         else
         {
             if (stamina < maxStamina)
             {
                 stamina += 30 * Time.deltaTime;
             }
             redWheel.fillAmount = (stamina / maxStamina);
         }
         greenWheel.fillAmount = (stamina / maxStamina);

         ////Player 2
         if (player.playerNumber == 2 && (Input.GetKey(KeyCode.Keypad0)))
         {
             if (stamina > 0)
             {
                 player.speed += speedAcceleration * Time.deltaTime;
                 stamina -= 30 * Time.deltaTime;
             }
             redWheel.fillAmount = (stamina / maxStamina + 0.07f);
         }
         else
         {
             if (stamina < maxStamina)
             {
                 stamina += 30 * Time.deltaTime;
             }
             redWheel.fillAmount = (stamina / maxStamina);
         }
         greenWheel.fillAmount = (stamina / maxStamina);
     }*/

    public Image GetRedWheel()
    {
        return redWheel;
    }
    public Image GetGreenWheel()
    {
        return greenWheel;
    }

    public void SetRedWheelFillAmount(float amount)
    {
        redWheel.fillAmount = amount;
    }

    public void SetGreenWheelFillAmount(float amount)
    {
        greenWheel.fillAmount = amount;
    }
}
