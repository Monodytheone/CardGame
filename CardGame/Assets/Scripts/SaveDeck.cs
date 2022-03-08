<
using System.Collections;
=======
锘縰sing System.Collections;
>>>>>>> parent of bb45b15 (绠€鍖朣aveDeck)
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDeck : MonoBehaviour
{
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
