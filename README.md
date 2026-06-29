# ☀️ SunEngine-Executable

<p align="center">
  <img src="https://img.shields.io/badge/Language-C%2B%2B17-blue.svg" alt="C++17">
  <img src="https://img.shields.io/badge/Scripting-Sol3%20%2F%20LuaJIT-orange.svg" alt="Sol3/LuaJIT">
  <img src="https://img.shields.io/badge/Graphics-OpenGL%204.6-red.svg" alt="OpenGL 4.6">
  <img src="https://img.shields.io/badge/Status-Early%20Access-green.svg" alt="Early Access">
</p>

**Sun Engine Free Early Access** / **太阳引擎 免费抢先体验版**

---

## 🛠️ 功能特性 / Core Features

### 🎬 渲染与相机 / Rendering & Camera
* **01. 3D & 2D Scene Rendering** * 🇨🇳 支持 3D 与 2D 场景的高效混合渲染。  
  * 🇬🇧 High-performance rendering for both 3D and 2D scenes.
* **04. Custom GLSL Shaders** * 🇨🇳 支持自定义 GLSL 顶点着色器（Vertex Shader）与片段着色器（Fragment Shader）。  
  * 🇬🇧 Supports custom GLSL vertex and fragment shaders for bespoke rendering effects.
* **07. 2D Texture Mapping (JPG, JPEG, PNG, etc.)** * 🇨🇳 支持主流格式（JPG、JPEG, PNG 等）的 2D 纹理贴图渲染与自动管理。  
  * 🇬🇧 2D texture mapping and rendering supporting standard formats (JPG, JPEG, etc.).
* **08. Dynamic Camera Switching** * 🇨🇳 实时切换透视相机（Perspective）与正交相机（Orthographic）视口。  
  * 🇬🇧 Real-time viewport switching between Perspective and Orthographic cameras.
* **12. Advanced Rasterization Styles** * 🇨🇳 支持背面剔除（Face Culling）、渲染优先级调整，以及实体填充、线框（Wireframe）和点阵模式。  
  * 🇬🇧 Supports face culling, rendering priority management, and solid fill, wireframe, or point modes.

### 📜 脚本与核心系统 / Scripting & Core Systems
* **03. Lua-Scripted Transform & Movement** * 🇨🇳 支持通过自定义 Lua 脚本驱动物体的位移等核心控制逻辑。  
  * 🇬🇧 Drives entity transform and movement logic via custom Lua scripting.
* **09. Script Lifecycle Control** * 🇨🇳 支持在编辑器中实时激活（执行）或挂起（关闭）脚本生命周期。  
  * 🇬🇧 Toggle script execution (start/stop lifecycle) directly inside the editor.
* **14. Script & Shader Hot Reloading** * 🇨🇳 支持 Lua 脚本与 GLSL 着色器的运行时热更新，无需重启即可见效。  
  * 🇬🇧 Supports runtime hot-reloading for Lua scripts and GLSL shaders without restarting the application.

### 📐 资源导入与场景编辑 / Assets & Scene Editing
* **06. Mesh Importing (OBJ & PLY)** * 🇨🇳 支持导入标准 OBJ 格式及部分 PLY 格式的 3D 网格模型。  
  * 🇬🇧 Supports importing standard OBJ and partial PLY 3D mesh formats.
* **10. Transform Tools ** *  🇨🇳 平移功能，并集成直观的 Gizmo 轴操作。  
  * 🇬🇧 Comprehensive transform manipulation (Translation) with intuitive Gizmo controls.
* **11. Hierarchy Tree Management** * 🇨🇳 结构化的场景对象父子级层级树管理。  
  * 🇬🇧 Structured Scene Hierarchy Tree management for parenting and scene organization.
* **17. Scene Persistence & Auto-Save Control** * 🇨🇳 支持保存场景序列化文件，并提供手动保存功能的开启与关闭。  
  * 🇬🇧 Scene state serialization (saving) and runtime toggle for manually save functionality.
* **22. 2D Tilemap Painting (Partial Support)** * 🇨🇳 内置 2D 瓦片地图绘制功能（当前为部分支持）。  
  * 🇬🇧 Built-in 2D Tilemap painting features (Partial support).

### 💡 光照与材质 / Lighting & Materials
* **15. Directional Light with Adjustable Angles** * 🇨🇳 支持单光源定向光（Directional Light），并允许自由调整照射角度。  
  * 🇬🇧 Single directional light source support with adjustable inclination and orientation angles.
* **16. Material Reflection** * 🇨🇳 材质系统支持uniforms类型反射到编辑器编辑
  * 🇬🇧 Materials with support for uniforms reflection and editing.

### 🔊 音频系统 / Audio System
* **05. 2D Audio Playback** * 🇨🇳 内置 2D 音频播放引擎，支持脚本播放声音,支持.wav格式 
  * 🇬🇧 Integrated 2D audio engine for independent .wav playback.

### 📂 项目管理 / Project Management
* **13. Project Lifecycle Management** * 🇨🇳 具备完善的项目管理，支持创建新项目、打开历史项目以及删除已有项目。  
  * 🇬🇧 Full project management workflows: create new, open existing, or delete projects.

---

## 🖥️ 编辑器工作区 / Editor Workspace (UI)

| 窗口名称 (CN) | Window Name (EN) | 功能描述 / Description |
| :--- | :--- | :--- |
| **场景视口** | `SceneView Window` | 实时预览与操作编辑器的核心三维/二维舞台。 / The core viewport for real-time previewing and manipulating scenes. |
| **属性检查器** | `Inspector Window` | 查看与实时修改选中物体的组件参数。 / Inspects and modifies component properties of selected objects. |
| **场景树窗口** | `Hierarchy Window` | 清晰展现物体父子级关系的层级树。 / Displays scene objects hierarchically and manages parent-child node relationships. |
| **资产浏览器** | `Asset Browser Window` | 直观管理项目中的纹理、脚本、模型等资源。 / Graphically manages project resources such as textures, scripts, and meshes. |

---

## 🔬 实验性功能 / Experimental Features
* **23. Instanced Rendering & Advanced Optimizations** * 🇨🇳 正在研发与测试中的高效能底层优化，如**实例化渲染（Instanced Rendering）**等（尚未 100% 确认完全开放）。  
  * 🇬🇧 High-performance low-level optimizations under testing, including **Instanced Rendering** (not 100% finalized for full public access).
