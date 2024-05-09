using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Test
{
   public static Action OpenInventory;
   public static Action ClickCrystal;
   public static int scene = 1;
   public static List<Crystal> GroupCrystals;

   public static void LoadScene()
   {
      SceneManager.LoadScene(Test.scene);
   }
}
