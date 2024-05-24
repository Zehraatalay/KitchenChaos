using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    //string kullanýmýnýn önüne geçmek için enum oluþtururuz
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;

    //editörden bu degerin degistirilmesini istemiyoruz so bu metodu yazdým
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        //2 tane LoadScene metodu arka arkaya kullanýldýýgýnda ilk baþtakinin ardýndan 
        //hemen diðeri geldiði için ilki gözükmez
        //belirli bir frame kadar bekletmemiz lazým (LoaderCallback scripti bu yüzden)
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        //asaðýdaki kod parçasýnda int veya string parametre olarak kullanýlabildiðinden
        //tostring metodunu çaðýrýrýz 
        SceneManager.LoadScene(targetScene.ToString());
    }
}
