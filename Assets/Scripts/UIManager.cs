using UnityEngine;


public class UIManager : MonoBehaviour
{
    public GameObject bookUI;
    public GameObject mapUI;
    private bool bookIsOpen;
    private bool mapIsOpen;
    private InputSystem_Actions controls;
    AudioSource AudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookIsOpen = false;
        bookUI.SetActive(false);

        mapIsOpen = false;
        mapUI.SetActive(false);
    }
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    //void OnOpenBook(InputValue value)
    //{
    //    bookIsOpen = !bookIsOpen;
    //    bookUI.SetActive(bookIsOpen);
    //}
    // Update is called once per frame
    void Update()
    {
        OpenBook();
        OpenMap();
    }
    private void OpenBook()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioSource.Play();
            if(mapIsOpen)
            {
                mapIsOpen = false;
                mapUI.SetActive(mapIsOpen);
            }
            bookIsOpen = !bookIsOpen;
            bookUI.SetActive(bookIsOpen);
        }
    }
    private void OpenMap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioSource.Play();
            if (bookIsOpen)
            {
                bookIsOpen = false;
                bookUI.SetActive(bookIsOpen);
            }
            mapIsOpen = !mapIsOpen;
            mapUI.SetActive(mapIsOpen);
        }
    }
}
