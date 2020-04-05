# Metis2020

The **Metis2020** is an interactive single player, multi-selection Trivia Game; developed with `Unity`, `C#` and `C++` with in-line `Assembly`.

## Prerequisites

- Visual Studio 2019 (can also use 2017). Make sure `Game development with Unity` and `Game development with C++` are selected when running Visual Studio Installer.
- Unity Hub (Download here: https://unity3d.com/get-unity/download . Our application runs on version `2019.2.19f1`).
- GitHub for version control. (could use `GitHub Desktop` https://desktop.github.com/ or `GitBash` command line)

## Getting Started

1. Clone GitHub Repository.
2. Open Unity Hub.
3. Click Add button and select `Metis2020` folder (make sure it contains the `Assets` folder).
4. Click on Metis2020 Project in Unity Hub.
5. Go to `Project/Assets/Scenes` and open `Main` scene.
6. Ensure `Edit/Preferences/External Tools/External Script Editor` has Visual Studio 2019 (Community) selected.
7. Go to `Assets/Open C# Project` this will either open your file explorer from where you need to open the poject's solution or it could open Visual Studio directly.
8. Visual Studio should have 3 projects under the solution: `Assembly-CSharp`, `Assembly-CSharp-Editor` and `UnmanagedCode` (each one described in the next section). If `UnmanagedCode` project is not there, it needs to be added to the solution by just `Add/New Project`.
9. Make sure `UnmanagedCode` is in `x64` in Configuration Manager.
10. Build `UnmanagedCode` project, it will generate an `UnmanagedCode.dll` file in `Metis2020/x64/Debug` (file directory).
11. Copy and paste `UnmanagedCode.dll` over to `Metis2020/Assets`.

## Application and File Structure

### Unity Metis2020 (Unity Hub)
1. Assets folder:
- `UnmanagedCode`: Used to import C++ library (dll).
- `Scripts`: C# resources (classes).
- `Scenes`: IU container.
- `Resources`: Application resources (images, icons, etc).
- `MaterialUI and Animations`: resources specific to Unity.

2. Main component:
- `GameManager`: C# container class, conduit between Unity and C#.
- `Canvas`: UI controllers.

### Solution Metis2020 (Visual Studio)
1. Assembly-CSharp:
- MaterialUI: Specific to Unity components. 
- Scripts: C# assests. 
`Anwer.cs` and `Question.cs`: Objects.
`GameManager.cs`: Main class, connects to C++ through `DllImport`.

2. Assembly-CSharp-Editor:
- Specific to Unity components.

3. UnmangedCode: 
- `.h` files: Autogenereted haders.
- `UnmanagedCode.cpp`: C++ class where we will call the assembly functions. 
- Assembly folder: Contains all of our `.asm` file(s).

## Unity Editor Build Related Comment
1. After placing `UnmanagedCode.dll` in the `.\Asset\` directory, the game can be built in Unity Editor through `File -> Build And Run` or Ctrl+B. You will be prompt a directory to store the executable file.

2. (Experimental) The game can  also be built directly in Visual Studio. First, export the project as a Visual Studio Solution along with all the assets and source code in Unity Editor through `File -> Build Settings...` or Ctrl+ShiftB. The Build Settings dialog displays. Check the `Create Visual Studio Solution` check box, and select `Build`. You will be prompt a directory to store the solution file and associated directories. At the current moment, only Windows x86_64 is a valid target to export. 

3. You may encounter an error message by opening the exported solution file. The exception appears to be thrown by microsoft.servicehub.controller.exe. The error may be similar to "Exception Processing Message 0x00005 Parameters 0x000007fefcee819c 0x000007fefcee819c  0x000007fefcee819c  0x000007fefcee819c." The error does not prevent you to build the solution and generate the working executable. Further investigation is required.  

## System flow description
As previously stated **Metis2020** has been built with `Unity Framework`. `Unity` handles all the UI related components (buttons, labels and effects) and the logic/functionality behind for those components was done with `C#`. 

**For example:** When a user clicks on a button to respond a question it also triggers a series of events like: verifies the correctness of the answer, activates triggers for the button box sliding effect, sets the next question, etc. 

**The entry point of the game is the `void Awake()` (`constructor`) method in `class GameManager` which is `C#`**

### Existing languages integration: 
As off 4/5/20 the main methods are written in `C#`. From `C#` a **DLL** generated with `C++` gets called and last `C++` calls `Assembly.

**For example:** The `GetTimeInSeconds()` method which specifies the time interval between questions to be displayed.

1 - `C#` gets triggered on initial load and calls `C++` using the imported `DLL`:

```C#
Void Awake() {
TimeBetweenQuestions = GetTimeInSeconds();
}
```

2 - `C++` calls `Assembly`:

```C++
int GetTimeInSeconds() {
    return GetTimeInSecondsAsm();
}
```
3 - `Assembly` is where that actual function get executed returning the value needed:

```Assembly
.code
GetTimeInSecondsAsm proc
    mov rax, 3
    ret
GetTimeInSecondsAsm endp
end
```
4 - `Assembly` returns the value **3** (number of seconds between questions) to `C++`, which then sends it to `C#` which communicates with `Unity` to assign the giving value to the next question event.

<br />
<br />
<br />

(_Descriptive comments have been added to the code that explains intent_)

