<
using System.Collections;
=======
﻿using System.Collections;
>>>>>>> parent of bb45b15 (简化SaveDeck)
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
