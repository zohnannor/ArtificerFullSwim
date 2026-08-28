# Artificer Full Swim

[SPOILERS FOR ARTIFICER ALT ENDING!]

Makes you swim the full distance in the Void Sea as Artificer.

In the regular ascension endings, the Void Worm helps you out by grabbing you and dragging you deep down, closer to the White Light. In the case of Artificer, the Worm ignores you and swims down, shortly followed by the ending being triggered. This mod delays that until you have swum the full distance other slugcats cover, without any assistance from the Worm.

Beware: this will take a VERY LONG time (around an hour of dashing straight down).

Build:

```sh
dotnet build
```

<details>
<summary>

(requires these files in the `lib/` directory:)

</summary>

```
Assembly-CSharp-firstpass.dll -> RainWorld_Data/Managed/Assembly-CSharp-firstpass.dll
BepInEx.dll -> BepInEx/core/BepInEx.dll
HOOKS-Assembly-CSharp.dll -> BepInEx/plugins/HOOKS-Assembly-CSharp.dll
Mono.Cecil.dll -> RainWorld_Data/Managed/Mono.Cecil.dll
Mono.Cecil.Rocks.dll -> RainWorld_Data/Managed/Mono.Cecil.Rocks.dll
MonoMod.dll -> RainWorld_Data/Managed/MonoMod.Common.dll
MonoMod.RuntimeDetour.dll -> RainWorld_Data/Managed/MonoMod.RuntimeDetour.dll
MonoMod.Utils.dll -> RainWorld_Data/Managed/MonoMod.Utils.dll
PUBLIC-Assembly-CSharp.dll -> BepInEx/utils/PUBLIC-Assembly-CSharp.dll
UnityEngine.CoreModule.dll -> RainWorld_Data/Managed/UnityEngine.CoreModule.dll
UnityEngine.dll -> RainWorld_Data/Managed/UnityEngine.dll
```

</details>
