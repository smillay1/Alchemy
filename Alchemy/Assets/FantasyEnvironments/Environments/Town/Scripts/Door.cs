using UnityEngine;

public class Door : MonoBehaviour {
    private Animator anim;
    private bool isOpen = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    // 🚪 Call this from VillagerManager when all tasks are complete
    public void OpenDoor() {
        if (!isOpen) {
            anim.SetBool("DoorOpen", true);
            anim.SetBool("DoorClose", false);
            isOpen = true;
        }
    }
}
