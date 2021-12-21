using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class input_tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void _0_ButtonDecoder_decodes_decimal_to_bool_array()
    {




        bool[] buttons = ButtonDecoder.ButtonsDecoded(14);

        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log(buttons[i]);
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator input_testsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
