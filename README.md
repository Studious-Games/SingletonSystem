
# Studious Singleton System
 
 Is a small very simple library, that helps create singletons with ease. From creating persistent Singletons to single usage Singletons, this library helps make it simple for everyone to get up and running with very little effort.


## Installation

Studious Singleton System is best installed as a package, this can be done in a number of ways, the best is to use git and install via the package manage.

###### **Via Git Hub** 

We highly recommend installing it this way, if you have git installed on your machine, then you can simply go to main page of this repository and select the code button. Then copy the HTTPS URL using the button next to the URL.

You can then open up the Package Manager within the Unity Editor, and select the drop down on the plus icon along the top left of the Package Manager. You can then select add by Git and paste the URL from the GitHub repository here.

###### **Add Package from Disk** 

There are two ways to do this, one would be to unzip the download to an area on your harddrive, and another would be to unzip the download into the actual project in the packages folder.

Both are workable solutions, however we would suggest if you would like to use this for multiple projects, that you install somewhere on your harddrive that you will remember. And then use the Package Manager to add the package from Disk.

Installing or unzipping into the projects package folder, will work out of the box when you open the project up.

## Usage

Getting up and running is extremley easy with Studious Singleton System, in the first example we are going to create a Singleton that is persistent when the game first runs.

To do this we create a normal class, and add a standard Instance variable as shown.

```CS
using Studious.Singleton;

public class TestSingleton : MonoBehaviour
{
    public static TestSingleton Instance => Singleton<TestSingleton>.Instance;
}
```

The Insatnce property here is required for the Singleton be loaded at runtime, failure to have this will result in the Singleton not loading.

Once you have this in place it is now just a matter of adding an Attribute, in the next example, we will now make this persistent when the game first runs.

```CS
using Studious.Singleton;

[Singleton(Name = "Name Of Object", Persistent = True)]
public class TestSingleton : MonoBehaviour
{
    public static TestSingleton Instance => Singleton<TestSingleton>.Instance;
}
```

The following parameters can be used here.

###### **Name**

This is what we will call the object when the Singleton is loaded, with persistent Singletons they will be attached to an object and named with what you call it here.

###### **Persistent**

This will be used to define if the Singleton will be persistent, at this present time we recommend using the **Scene** parameter to force the non persitent Singleton to load up on that scene. All persistent Singletons will remain persistent through the life of the game, where as non persistent are only alive for that scene.

###### **Scene**

When using a non persistent Singleton, this will tell the system on which scene it needs to be loaded. If this is not added, and you create a non persistent Singleton, it will not get loaded.

###### **HideFlags**

The system also has the ability to use the normal Unity HideFlags for the objects, this means you can hide the created game object if you so desire. For more information you can read up more about these flags in the following link

[Unity HideFags Documentation](https://docs.unity3d.com/ScriptReference/HideFlags.html)

## Caveat

Due to the way scenes are loaded, if you need to access a singleton, you will need to do so in the Start() event and not the Awake(). In the future this can be changed once Unity allowes the Ability to subscribe to Before Scene Loading, as it stands Unity only allow for subscribing to when a scene has loaded.

If you have written you own, SceneManager that can fire Before Scene Loaded events then you should be safe to use the Singletons created in the Awake() event.