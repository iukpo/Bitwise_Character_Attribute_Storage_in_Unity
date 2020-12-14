/**
@file       AttributeManager.cs
@brief      Manages collision attributes between player character and door object. Depending on the door touched, the attributes of the player character are altered.
            Character attributes are stored in a byte to save memory (more efficient than storing each possible value in its own byte, int, etc.); Bit twiddling is
            employed to set/unset values for each character attribute. Mind, a byte is used here because we are not using more than five attributes. If eight or more are
            needed, the attributes data type will have to be scaled up to a different data type (Int, etc.).
@author     Ihimu Ukpo
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttributeManager : MonoBehaviour
{
    static public byte MAGIC = 16;
    static public byte MAGIC_BIT_POS = 4;
    static public byte INTELLIGENCE = 8;
    static public byte INTELLIGENCE_BIT_POS = 3;
    static public byte CHARISMA = 4;
    static public byte CHARISMA_BIT_POS = 2;
    static public byte FLY = 2;
    static public byte FLY_BIT_POS = 1;
    static public byte INVISIBLE = 1;
    static public byte INVISIBLE_BIT_POS = 0;
    static public byte ALL = (byte) (MAGIC | INTELLIGENCE | CHARISMA | FLY | INVISIBLE);
    static public byte NOTHING = 0;

    public Text attributeDisplay;
    public Text attributeDisplayComment;
    public byte attributes = 0;

    //Function for checking if bit is set. Adapted from https://stackoverflow.com/questions/2431732/checking-if-a-bit-is-set-or-not
    bool IsBitSet(byte b, byte pos)
    {
        return (b & (1 << pos)) != 0;
    }

    //On collision, examine game object tag. If it is one of the twelve doors, alter user attributes.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MAGIC")
        {
            attributes |= MAGIC;
        }
        else if (other.gameObject.tag == "INTELLIGENCE")
        {
            attributes |= INTELLIGENCE;
        }
        else if (other.gameObject.tag == "CHARISMA")
        {
            attributes |= CHARISMA;
        }
        else if (other.gameObject.tag == "FLY")
        {
            attributes |= FLY;
        }
        else if (other.gameObject.tag == "INVISIBLE")
        {
            attributes |= INVISIBLE;
        }
        else if (other.gameObject.tag == "ANTIMAGIC")
        {
            attributes &= (byte) ~MAGIC;
        }
        else if (other.gameObject.tag == "ANTIINTELLIGENCE")
        {
            attributes &= (byte)~INTELLIGENCE;
        }
        else if (other.gameObject.tag == "ANTICHARISMA")
        {
            attributes &= (byte) ~CHARISMA;
        }
        else if (other.gameObject.tag == "ANTIFLY")
        {
            attributes &= (byte) ~FLY;
        }
        else if (other.gameObject.tag == "ANTIINVISIBLE")
        {
            attributes &= (byte) ~INVISIBLE;
        }
        else if (other.gameObject.tag == "RESET")  //Clears all bits
        {
            attributes = 0;
        }
        else if (other.gameObject.tag == "SET")  //Sets all bits
        {
            attributes = (byte) (MAGIC | INTELLIGENCE | CHARISMA | FLY | INVISIBLE);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame. Here is where the player character text is updated according to values of the attributes.
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-50,0);
        attributeDisplay.text = Convert.ToString(attributes, 2).PadLeft(8, '0');

        //Set the position of the attribute comment to be some distance just below the bit text.
        attributeDisplayComment.transform.position = screenPoint + new Vector3(0, -80, 0);

        if (attributes == AttributeManager.ALL)
        {
            attributeDisplayComment.text = "You became a SUPERPERSON!";
        }
        else if (attributes == AttributeManager.NOTHING)
        {
            attributeDisplayComment.text = "You were ordinary, until one day...";
        }

        //Check the individual bits to see if they have been set. Create a sentence according to the set bit values.
        else
        {
            attributeDisplayComment.text = "";
            if (IsBitSet(attributes, AttributeManager.INTELLIGENCE_BIT_POS))
            {
                attributeDisplayComment.text += "Your intelligence increased! ";
            }
            else if (IsBitSet(attributes, AttributeManager.MAGIC_BIT_POS))
            {
                attributeDisplayComment.text += "You got magic powers! ";
            }
            if (IsBitSet(attributes, AttributeManager.INVISIBLE_BIT_POS))
            {
                attributeDisplayComment.text += "You could become invisible! ";
            }
            if (IsBitSet(attributes, AttributeManager.CHARISMA_BIT_POS))
            {
                attributeDisplayComment.text += "You became a real charmer! ";
            }
            if (IsBitSet(attributes, AttributeManager.FLY_BIT_POS))
            {
                attributeDisplayComment.text += "You could fly! ";
            }
        }
    } 
}
