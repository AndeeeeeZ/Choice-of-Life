using UnityEngine;

public class CreditManager : MonoBehaviour
{
    [SerializeField] Flag flag;
    [SerializeField] GameObject Win, Lose, Credits, ContinueButton;
    [SerializeField] GameObject[] CreditPages; 

    private int currentPage; 

    private void Start()
    {
        Win.SetActive(false);
        Lose.SetActive(false);
        Credits.SetActive(false);
        ContinueButton.SetActive(true);

        foreach (GameObject page in CreditPages)
        {
            page.SetActive(false);
        }

        if (flag.mark)
            Win.SetActive(true);
        else
            Lose.SetActive(true);
    }

    public void showCredit()
    {
        Credits.SetActive(true);
        Win.SetActive(false);
        Lose.SetActive(false);
        ContinueButton.SetActive(false);

        currentPage = 0;
        CreditPages[currentPage].SetActive(true);
    }
    private void changePageNumberBy(int page)
    {
        CreditPages[currentPage].SetActive(false);
        currentPage += page;
        currentPage = Mathf.Clamp(currentPage, 0, CreditPages.Length - 1);
        CreditPages[currentPage].SetActive(true);
    }
}
