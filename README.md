# Unity Generic Singleton Design Pattern
This project contains boilerplate code that can be expanded upon whenever using the [_Singleton Design Pattern_][SingletonDP] in new Unity Projects. The singletons that exist in this package include:
- Generic Singletons
- Persistent Singletons
- Regulator Singletons

## Credits
These scripts were made with the help of Adam Myhre's amazingly informative youtube channel, [_git-amend_][Youtube]. If you haven't already seen it, definitely check out [_Adam's Generic Singletons_][Repo]!
This repository is for my convenience and I will modify these scripts to fit my use cases specifically.

## Generic Singletons
A normal singleton: only one instance exists. 

_Useful for singletons that stay in one scene, like **GameManagers** or maybe **Players**._

## Persistent Singletons
A singleton that persists between scenes, **destroying** any **new** singletons that are made. Makes use of Unity's ```DontDestroyOnLoad()``` function. 

_The **inverse** of the Regulator Singleton._

_Useful for singletons that may persist between scenes, like **AudioManagers**, **MenuManagers**._

## Regulator Singletons

A singleton that **destroys all other** older components of the same type. It will ensure that it is the one and only instance. 

_The **inverse** of the Persistent Singleton._ 

_Useful for **swapping** between one type of singleton for a completely different type of singleton. Usually between systems with similar, but different logic, like a **MenuAudioManager** and a **GameAudioManager**._

[SingletonDP]: https://refactoring.guru/design-patterns/singleton
[Youtube]: https://www.youtube.com/watch?v=LFOXge7Ak3E&list=PLnJJ5frTPwRMCCDVE_wFIt3WIj163Q81V
[Repo]: https://github.com/adammyhre/Unity-Utils
