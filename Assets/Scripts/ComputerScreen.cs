using UnityEngine;
using UnityEngine.UI;

public class ComputerScreen : MonoBehaviour {

    public Texture bobsComputerScreen;
    public Texture bobsPic;
    public Texture tomsComputerScreen;
    public Texture joesComputerScreen;
    public Texture homeScreen;
    public Texture broken;

    public Button backButton;
    public Button photos;
    public Button notes;
    public Button printButton;

    private RawImage screen;

    public Texture lockScreen;
    public InputField password;
    public Button enter;

    public GameObject picOfBob;

    private void Start() {
        screen = GetComponent<RawImage>();
    }

    public enum Computers {
        Bob,
        Joe,
        Tom,
        Broken
    }

    private Computers currentComputer;

    public void OnNotesClicked() {
        AudioManager.instance.GetSound("computerBeep").Play();

        backButton.gameObject.SetActive(true);
        photos.gameObject.SetActive(false);
        notes.gameObject.SetActive(false);
        printButton.gameObject.SetActive(false);

        switch (currentComputer) {
            case Computers.Bob:
                screen.texture = bobsComputerScreen;
                break;

            case Computers.Joe:
                screen.texture = joesComputerScreen;
                break;

            case Computers.Tom:
                screen.texture = tomsComputerScreen;
                break;
        }
    }

    public void OnPhotosClicked() {
        AudioManager.instance.GetSound("computerBeep").Play();

        screen.texture = bobsPic;
        backButton.gameObject.SetActive(true);
        printButton.gameObject.SetActive(true);
        photos.gameObject.SetActive(false);
        notes.gameObject.SetActive(false);
    }

    public void OnBackClicked() {
        AudioManager.instance.GetSound("computerBeep").Play();

        screen.texture = homeScreen;

        backButton.gameObject.SetActive(false);
        printButton.gameObject.SetActive(false);
        if (currentComputer == Computers.Bob) {
            photos.gameObject.SetActive(true);
        }
        notes.gameObject.SetActive(true);
        password.gameObject.SetActive(false);
        enter.gameObject.SetActive(false);
    }

    public void OnPrintButton() {
        AudioManager.instance.GetSound("printer").Play();

        FindObjectOfType<PlayerData>().SetHavePrintedPicture(true);
        picOfBob.SetActive(true);
    }

    public void OnBootUp(Computers computerType) {

        currentComputer = computerType;

        if (currentComputer == Computers.Broken) {
            screen.texture = broken;
        } else {
            AudioManager.instance.GetSound("computerBeep").Play();
            
            if (screen.enabled) {
                screen.texture = lockScreen;
                password.gameObject.SetActive(true);
                enter.gameObject.SetActive(true);
            } else {
                backButton.gameObject.SetActive(false);
                printButton.gameObject.SetActive(false);
                photos.gameObject.SetActive(false);
                notes.gameObject.SetActive(false);
                password.gameObject.SetActive(false);
                enter.gameObject.SetActive(false);
            }
        }
    }

    public void OnEnterClicked() {
        switch (currentComputer) {
            case Computers.Bob:
                if (password.text == "I<3Bombs") {
                    OnBackClicked();
                    AudioManager.instance.GetSound("computerBeep").Play();
                } else {
                    AudioManager.instance.GetSound("wrong").Play();
                }
                break;

            case Computers.Joe:
                if (password.text == "CoolKid123") {
                    OnBackClicked();
                    AudioManager.instance.GetSound("computerBeep").Play();
                } else {
                    AudioManager.instance.GetSound("wrong").Play();
                }
                break;

            case Computers.Tom:
                if (password.text == "password") {
                    OnBackClicked();
                    AudioManager.instance.GetSound("computerBeep").Play();
                } else {
                    AudioManager.instance.GetSound("wrong").Play();
                }
                break;
        }

        password.text = "";
    }
}
