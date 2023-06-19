using System.Collections;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Animator noMoney;

    public int coin;
    public GameObject[] hands;

    private void Start()
    {
        noMoney.gameObject.SetActive(false);
        coin = PlayerPrefs.GetInt("myCoin", coin);
        handController();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown((KeyCode.N)))
        {
            coin += 1000;
            PlayerPrefs.SetInt("myCoin", coin);
        }
    }

    public void Buy(GameObject obj)
    {
        if (obj.transform.GetChild(2).GetComponent<Container>().purcahesd)
        {
            PlayerPrefs.SetFloat(obj.transform.GetChild(2).GetComponent<Container>().id, 1);
            obj.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
            PlayerPrefs.SetString("selected", obj.transform.GetChild(2).GetComponent<Container>().id);
        }
        else
        {
            if (coin >= obj.transform.GetChild(2).GetComponent<Container>().money)
            {
                PlayerPrefs.SetFloat(obj.transform.GetChild(2).GetComponent<Container>().id, 1);
                PlayerPrefs.SetString("selected", obj.transform.GetChild(2).GetComponent<Container>().id);
                obj.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
                coin -= (int)obj.transform.GetChild(2).GetComponent<Container>().money;
                obj.transform.GetChild(2).GetComponent<Container>().purcahesd = true;
                PlayerPrefs.SetInt("myCoin", coin);
            }
            else
            {
                noMoney.gameObject.SetActive(true);
                noMoney.Play(0);
                StartCoroutine(NoMoneyAnimClose());
                Debug.Log("No Coin");
            }
        }

        handController();
    }

    IEnumerator NoMoneyAnimClose()
    {
        yield return new WaitForSeconds(2.2f);
        noMoney.gameObject.SetActive(false);
    }

    private void handController()
    {
        foreach (var item in hands)
        {
            item.SetActive(false);
        }
    }

    // private void ShopSpawner()
    // {
    //     for (int i = 0; i < items.Count; i++)
    //     {
    //         GameObject g = Instantiate(slot, container);
    //         g.transform.GetChild(0).GetComponent<Image>().sprite = items[i].Image;
    //         g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = items[i].price.ToString();
    //         int price = i;
    //         g.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => Purchase(price, g));
    //     }
    // }
    //
    // void Purchase(int id, GameObject obj)
    // {
    //     if (coin >= items[id].price && !items[id].isPurchased)
    //     {
    //         obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
    //         coin -= items[id].price;
    //         PlayerPrefs.SetInt("myCoin", coin);
    //         items[id].isPurchased = true;
    //         Debug.Log(items[id].name);
    //     }
    //     else
    //     {
    //         Debug.Log("No Coin");
    //         //Todo 
    //     }
    //
    //     if (items[id].isPurchased)
    //     {
    //         obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Selected";
    //     }
    //     else if (items[id].isPurchased)
    //     {
    //         obj.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
    //     }
    // }


    // [System.Serializable]
    // public class ShopItem
    // {
    //     public Sprite Image;
    //     public int Price;
    //     public bool IsPurchased;
    // }
    //
    // public List<ShopItem> shopItemsList;
    // [SerializeField] private Transform shopScrollView;
    // [SerializeField] private GameObject itemTemplate;
    //
    // private Image _purchaseImage;
    // private Button _purchaseButton;
    // private TextMeshProUGUI _purchaseText;

    //private void Start()
    //{
    // foreach (var t in shopItemsList)
    // {
    //     var value = Instantiate(itemTemplate, shopScrollView);
    //
    //     value.transform.GetChild(0).GetComponent<Image>().sprite = t.Image;
    //     value.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"BUY ${t.Price}";
    //     _purchaseButton = value.transform.GetChild(1).GetComponent<Button>();
    //     _purchaseButton.interactable = !t.IsPurchased;
    // }
    //}
}

// public class Items
// {
//     public string name;
//     public Sprite Image;
//     public int price;
//     public bool isPurchased;
//
//     public Items(string _name, Sprite _image, int _price, bool _isPurchased)
//     {
//         name = _name;
//         Image = _image;
//         price = _price;
//         isPurchased = _isPurchased;
//     }
// }