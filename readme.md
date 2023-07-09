Compatible only with Unity 2021.3 and above

* copy the UnityDLL~/AkroGame.ECS.Analyzer.dll and meta file (important too) and copy them in your project asset folder (compiled from https://github.com/akrogame/svelto-ecs-inspector). This dll may need the package https://github.com/merryyellow/code-analysis-3-11 installed too (Microsoft.CodeAnalysis.CSharp dependency)
* then install the Unity package https://github.com/James-Frowen/SimpleWebTransport in your Unity project using Svelto. Follow the readme instructions to install the unity package
* then install this repo package from the github source: git@github.com:sebas77/svelto-ecs-inspector-unity.git
* then in your main context add the line
```
SveltoInspector.Attach(_enginesRoot);
```
Run the game.

For the client:

you can either use the URL: https://akrogame.github.io/svelto-ecs-inspector/
or best running the node js application found at https://github.com/sebas77/svelto-ecs-inspector/tree/main/inspector

FAQ:

Q. why are engines shown multiple times? like

![image](https://user-images.githubusercontent.com/945379/208312024-8a996eae-cfa7-4f2e-83f8-b0f4c41750b7.png)

A. each copy is a separate query found in the engine

Q. why is the analyzer targinet .net standard 2.0?

A. to prevent this unity error: AkroGame.ECS.Analyzer references netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51. A roslyn analyzer should reference netstandard version 2.0

Q. why is there a UnityDLL~ folder in the package? 

A. folders ending with ~ are ignored by Unity. You must copy the AkroGame.ECS.Analyzer.dll and install the package https://github.com/merryyellow/code-analysis-3-11



