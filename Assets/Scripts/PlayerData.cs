using UnityEngine;

public class PlayerData : MonoBehaviour {

    private bool haveKey, haveScrewdriver, havePrintedPicture, havePicture;

    public bool HaveKey => haveKey;

    public bool HaveScrewdriver => haveScrewdriver;

    public bool HavePrintedPicture => havePrintedPicture;

    public bool HavePicture => havePicture;

    public void SetHaveKey(bool state) => haveKey = state;

    public void SetHaveScrewdriver(bool state) => haveScrewdriver = state;

    public void SetHavePrintedPicture(bool state) => havePrintedPicture = state;

    public void SetHavePicture(bool state) => havePicture = state;

}
