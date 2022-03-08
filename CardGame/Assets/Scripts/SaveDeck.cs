<<<<<<< HEAD
using System.Collections;
=======
ï»¿using System.Collections;
>>>>>>> parent of bb45b15 (ç®€åŒ–SaveDeck)
using System.Collections.Generic;
using UnityEngine;

/// <summary>
<<<<<<< HEAD
/// <para>µã»÷SaveDeck°´Å¥¾Í°Ñ¿¨×é±£´æµ½PlayerData.csv</para>
/// <para>¹ÒÔÚDataManagerÉÏ</para>
/// <para>È»ºóÒªÔÚSaveDeckÕâ¸ö°´Å¥µÄInspectorÒ³ÃæÉÏ°ÑOn Click()°óµ½¸Ã½Å±¾µÄOnClickSaveDeck()·½·¨</para>
=======
/// <para>ç‚¹å‡»SaveDeckæŒ‰é’®å°±æŠŠå¡ç»„ä¿å­˜åˆ°PlayerData.csv</para>
/// <para>æŒ‚åœ¨DataManagerä¸Š</para>
/// <para>ç„¶åè¦åœ¨SaveDeckè¿™ä¸ªæŒ‰é’®çš„Inspectoré¡µé¢ä¸ŠæŠŠOn Click()ç»‘åˆ°è¯¥è„šæœ¬çš„OnClickSaveDeck()æ–¹æ³•</para>
>>>>>>> parent of bb45b15 (ç®€åŒ–SaveDeck)
/// </summary>
public class SaveDeck : MonoBehaviour
{
    /// <summary>
<<<<<<< HEAD
    /// <para>µã»÷SaveDeck°´Å¥¾Í°Ñ¿¨×é±£´æµ½PlayerData.csv</para>
=======
    /// <para>ç‚¹å‡»SaveDeckæŒ‰é’®å°±æŠŠå¡ç»„ä¿å­˜åˆ°PlayerData.csv</para>
>>>>>>> parent of bb45b15 (ç®€åŒ–SaveDeck)
    /// </summary>
    public void OnClickSaveDeck()
    {
        GetComponent<PlayerData>().SavePlayerData();
    }
}
