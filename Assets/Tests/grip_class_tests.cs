using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class grip_class_tests
{
    string MethodNameToTestMethodName(string str)
    {
        var startIndex = str.IndexOf(' ') + 1;
        if (startIndex == 0 || startIndex == str.Length)
            return null;

        var endIndex = str.IndexOf("(", startIndex, StringComparison.CurrentCulture);
        if (endIndex == -1)
            endIndex = str.Length - 1;
        return str.Substring(startIndex, endIndex - startIndex);// + 1);
    }

    bool IsTestMethodName(string str)
    {
        var startIndex = str.IndexOf(' ') + 1;
        if (startIndex == 0 || startIndex == str.Length)
        {
            return false;
        }
            
        else
        {
            return str.Substring(startIndex, str.Length-1 - startIndex).StartsWith("_");
        }

    }


    // A Test behaves as an ordinary method
    [Test]
    public void _0_has_tests_for_all_methods()
    {



        var thisMethodInfos = this.GetType().GetMethods();

        string methodsString = "";

        for (int i = 0; i < thisMethodInfos.Length; i++)
        {
            if (IsTestMethodName(thisMethodInfos[i].ToString()))
            {
                
                methodsString += thisMethodInfos[i];
            }
            else
            {
                Debug.Log($"Non-test methods: { thisMethodInfos[i]}");
            }
        }

        var methodInfos = typeof(Grip).GetMethods();

        for (int i = 0; i < methodInfos.Length; i++)
        {
            string methodDefinitionString = MethodNameToTestMethodName(methodInfos[i].ToString());
            if (!methodsString.Contains(methodDefinitionString))
            {
                Debug.Log($"Did not find: { methodDefinitionString } in: {methodsString}");
                //Assert.AreEqual("MethodString", $"Did not find: { methodDefinitionString } in: {methodsString}");
            }
            else
            {
                Debug.Log($"Found: { methodDefinitionString } in: {methodsString}");
            }
            //Assert.Contains(methodsString, methodDefinitionString);.AreEqual("",methodDefinitionString);
        }







    }


}
