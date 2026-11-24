# Corrección, Trabajo Práctico N°07 (Side Scroller 2D)

- **Alumno/a:** Bruno
- **Entrega:** `TP07_C1_Bruno_Scroller`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP07:** 9

## 1. Checklist de requisitos

Bloque único: Side Scroller 2D

| #   | Requisito | Estado | Evidencia |
|-----|-----------|--------|-----------|
| R1  | Repositorio nuevo `TP07_C1_Apellido_Titulo` | ✓ | `.git/config` apunta a `github.com/ignosuke/TP07_C1_Bruno_Scroller` |
| R2  | Proyecto de Unity con el mismo nombre | ✓ | `ProjectSettings/ProjectSettings.asset`, `productName: TP07_C1_Bruno_Scroller` |
| R3  | El personaje va a derecha e izquierda | ✓ | `Assets/Scripts/Player/PlayerMover.cs:30`, acción `Move` en `Assets/PlayerActions.inputactions` |
| R4  | El personaje puede disparar | ✓ | `Assets/Scripts/Player/PlayerShooter.cs:55-65`, se habilita con `WeaponPickup` (`PlayerShooter.cs:67`) |
| R5  | El personaje puede saltar | ✓ | `Assets/Scripts/Player/PlayerJumper.cs:102-111` |
| R6  | Enemigos u obstáculos que quitan vida | ✓ | `Assets/Scripts/Enemies/EnemyDamage.cs:7`, `Assets/Scripts/World/Hazard.cs`, `PlayerHealth` hereda de `Health` |
| R7  | Se eliminan con más de 2 disparos, tienen vida y barra de vida arriba (Canvas World) | Parcial | La barra está en `HealthBarCanvas.prefab` (World Space) con `EnemyHealthBar.cs` ✓. Pero solo `Fly` necesita 3 disparos: `BeeDataSo.asset` tiene `maxHealth: 1` y `WormDataSo.asset` tiene `maxHealth: 2`, con bala de `damage: 1` |
| R8  | ParticleSystem al destruir al enemigo | ✓ | `Assets/Scripts/Enemies/EnemyHealth.cs:13-14`, prefabs `Assets/Art/Particles/*DeathParticles.prefab` asignados en `Bee`, `Fly` y `Worm` |
| R9  | Pickables | ✓ | `Assets/Scripts/Pickups/` (5 tipos), repartidos por `Assets/Scripts/Game/PickupDropAssigner.cs` |
| R10 | Sonido en todas las acciones | ✓ | Música de gameplay: `MusicPlayer` en `GameScene.unity` con `echoes of lumen - track.mp3`. Saltar, disparar y perder: `Assets/Scripts/Audio/PlayerAudio.cs`, con los clips asignados en `Player.prefab`. Coleccionable: `PickupAudio.prefab` dentro de los 5 pickups. Eliminar enemigo: `EnemyAudio.prefab` (`diedClip`) dentro de los 3 enemigos. Además: daño, golpe al enemigo y click de UI |
| R11 | Volumen configurable desde Settings directo a un AudioMixer | ✓ | `Assets/Scripts/Audio/VolumeStepper.cs:52` llama a `AudioVolume.Apply` sobre `MainMixer.mixer`, y `UiSettingsConfig.prefab` configura `MasterVolume`, `MusicVolume` y `SfxVolume` |
| R12 | ScriptableObject para datos o configuraciones | ✓ | 12 clases en `Assets/Scripts/Data/` y 17 `.asset` en `Assets/Data/` (por ejemplo `PlayerSo`, que leés en `PlayerJumper.Awake`) |
| R13 | Power ups o modificación de jugabilidad | ✓ | `FireRatePickup` (cadencia temporal, `PlayerShooter.cs:83`) y el escudo de `BlueHeartPickup` (`PlayerHealth.TryAddShield`) |
| R14 | Personaje animado | ✓ | `Assets/Art/Animations/Player/PlayerAnimator.controller` con 10 `.anim`, `Assets/Scripts/Player/PlayerAnimator.cs:116` |
| R15 | Escena de menú principal y otra de Gameplay | ✓ | `ProjectSettings/EditorBuildSettings.asset`: `MainMenuScene` y `GameScene` |

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repo público en GitHub | ✓ | El repositorio es público (al 2026-11-16) |
| P3 | Nombre del repo | ✓ | Carpeta `TP07_C1_Bruno_Scroller` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, enums (`PlayerAnimationState`) y escenas en inglés. Los comentarios están en español, y eso está permitido |
| P5 | Un repo por TP, sin branches | ✓ | Solo existe `main` |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Art/Animations/` | ✓ | |
| `Art/Audio/` | ✓ | Con subcarpetas `SFX/` y `Tracks/` |
| `Art/Fonts/` | ✓ | |
| `Art/Sprites/` | ✓ | |
| `Art/Materials/` | No aplica | No tenés materiales propios |
| `Data/` | ✓ | Subcarpetas por dominio |
| `Prefabs/` | ✓ | |
| `Scenes/` | ✓ | |
| `Scripts/` | ✓ | Subcarpetas por dominio (`Player`, `Enemies`, `UI`, `Audio`, etc.) en lugar de `Game/` y `Ui/` |

Carpetas extra: `Assets/Art/Backgrounds/`, `Assets/Art/Particles/`, `Assets/Art/Tiles/`, `Assets/Art/UI/`, `Assets/PhysicsMaterials/`. Te las marco, pero el problema son solo los archivos mal ubicados de abajo.

Archivos mal ubicados:

- `Assets/PlayerActions.inputactions` está suelto en la raíz de `Assets/`. Va en `Assets/Data/` o en una carpeta de settings.
- `Assets/Art/Particles/BeeDeathParticles.prefab`, `FlyDeathParticles.prefab`, `PickupParticles.prefab` y `WormDeathParticles.prefab` son prefabs fuera de `Assets/Prefabs/`.

Ruido de repo (no lo corrijo ni lo tengo en cuenta): `Assets/InputSystem_Actions.inputactions` es el archivo de la plantilla de Unity y no lo usa ninguna escena, prefab ni `.asset`. `Assets/TextMesh Pro/` es un paquete importado.

## 4. Qué le falta

- Subir `maxHealth` de `BeeDataSo.asset` y `WormDataSo.asset` a 3 o más, para que todos los enemigos necesiten más de 2 disparos (R7).
- Que el nombre de clase coincida con el del archivo en `PlayerHazardRespawn.cs`, `PlayerSafepointTracker.cs` y `UiHeartsDisplay.cs`.
- Evitar que una misma bala dañe dos veces por `OnTriggerStay2D` (`Health.cs:43`).
- Arreglar o borrar el objeto `Music` de `MainMenuScene.unity`: su `AudioSource` apunta a un grupo de mixer y a un clip que no existen en el proyecto.
- Mover `PlayerActions.inputactions` y los prefabs de partículas a sus carpetas.

## 5. Hallazgos de código

### Error (3)
- `Assets/Scripts/Player/PlayerHazardRespawn.cs:5` la clase `HazardRespawn` está en un archivo que se llama distinto. Unity lo compila pero tira un warning: renombrá el archivo o la clase para que coincidan.
- `Assets/Scripts/Player/PlayerSafepointTracker.cs:4` la clase `SafePointTracker` está en un archivo que se llama distinto. Unity lo compila pero tira un warning: renombrá el archivo o la clase para que coincidan.
- `Assets/Scripts/UI/UiHeartsDisplay.cs:4` la clase `UIHeartsDisplay` está en un archivo que se llama distinto (difiere en mayúsculas). Unity lo compila pero tira un warning: renombrá el archivo o la clase para que coincidan.

### Bug (1)
- `Assets/Scripts/Shared/Health.cs:43` `OnTriggerStay2D` vuelve a llamar a `TryTakeDamageFrom`. Los enemigos tienen `InvulnerabilityTime = 0` y el `Destroy` de la bala es diferido, así que si en un frame corren dos `FixedUpdate` la misma bala hace daño dos veces.

### Warning (3)
- `Assets/Scripts/Player/PlayerAnimator.cs:58` `OnDisable` no quita la suscripción a `shooter.OnArmedChanged` que agregás en la línea 53.
- `Assets/Scripts/UI/UIMainMenuManager.cs:69` `SceneManager.LoadScene("GameScene")` con string mágico, mientras que `UIGameManager` usa una const.
- `Assets/Scenes/MainMenuScene.unity:311-313` (no tiene comentario inline porque es una escena) el `AudioSource` del objeto `Music` apunta a un `OutputAudioMixerGroup` y a un clip que no existen en el proyecto.

### Suggestion (10)
- `Assets/Scripts/Player/PlayerShooter.cs:4` `using Unity.VisualScripting;` sin usar.
- `Assets/Scripts/Player/PlayerShooter.cs:97` recalculás el cooldown sin el `Mathf.Max` de la línea 35. Conviene un único método.
- `Assets/Scripts/World/LevelBounds.cs:2` `using Unity.VisualScripting;` sin usar.
- `Assets/Scripts/Player/PlayerGroundChecker.cs:10` valores iniciales (`0.3f`, `0.2f`) que se pisan en `Awake` con los del `PlayerSo`.
- `Assets/Scripts/Player/PlayerGroundChecker.cs:16` campos privados `IsGrounded` e `IsFullyGrounded` en PascalCase.
- `Assets/Scripts/Player/PlayerGroundChecker.cs:67` el `if (!hit) return false; return true;` es lo mismo que `return hit;`.
- `Assets/Scripts/Player/PlayerMover.cs:12` valor inicial `10.0f` que se pisa en `Awake`.
- `Assets/Scripts/Parallax/ParallaxLayer.cs:41` usás `renderers[0]` sin validar que haya al menos un `SpriteRenderer`.
- `Assets/Scripts/Audio/AudioVolume.cs:7` constantes en camelCase, distinto del PascalCase de `VolumeStepper` y `PlayerAnimator`.
- `Assets/Scripts/Camera/CameraFollow.cs:40` tag `"Player"` como string mágico.

## 6. Recomendaciones

1. Subí `maxHealth` de `BeeDataSo.asset` y `WormDataSo.asset` a 3 para que todos los enemigos necesiten más de 2 disparos (R7).
2. Renombrá `PlayerHazardRespawn.cs`, `PlayerSafepointTracker.cs` y `UiHeartsDisplay.cs` (o sus clases) para que coincidan.
3. En `Bullet`, desactivá el collider o marcá la bala como usada en el primer impacto, y revisá si `Health` necesita `OnTriggerStay2D` para las balas.
4. Arreglá o borrá el objeto `Music` de `MainMenuScene.unity`.
5. Agregá `shooter.OnArmedChanged -= SetArmed;` en `PlayerAnimator.OnDisable`.
6. Mové `PlayerActions.inputactions` y los prefabs de `Art/Particles/` a `Data/` y `Prefabs/`, y borrá los `using` sin usar.
