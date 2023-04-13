using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private TMPro.TMP_Text uiSpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;


    private void Start()
    {
        uiSpinButton.onClick.AddListener(() => {

            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning";

            pickerWheel.OnSpinEnd(wheelPiece => {
                

                Game.CoinAmount = PlayerPrefs.GetFloat("Coin");
                Game.CoinAmount = Game.CoinAmount + wheelPiece.Amount;
                PlayerPrefs.SetFloat("Coin", Game.CoinAmount);

                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
            });

            pickerWheel.Spin();

        });

    }

}