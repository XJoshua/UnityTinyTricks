# Unity Tricks

Some Unity tricks, helpful in my work. Most is collected from anywhere.  
一些Unity的小技巧，从各个角落收集而来。

### 01 Unity Debugger
* **简介**  
对Unity `Debug.Log` 的封装，同时显示输出时间，输出语句所在的函数，并可以改变输出的颜色。
* **直接使用**  
    1. 把 DLL 文件放到Unity工程中，在 `Player Setting - Other Settings - Scripting Define Symbols` 中，添加 `ENABLE_LOG` 。
    2. 在需要输出Log的地方，通过 `this.Log()` 调用函数，如果要显示指定颜色的输出，使用 `this.Log("log", Color.cyan)` 语句。
* **更新**  
2018/02/09：第一版  
2018/04/18：修改了 `this.Warn()` 和 `this.Error()` 输出显示错误的问题  
* **参考文章**  
http://www.sunjiahaoz.com/archives/1266

### Working Stuff
#### 1. Unity Align  
可以把 `Hierarchy` 中的选中多个物体或者某个物体的子物体按照 `rect` 组件的高度，上下对齐排列  
Ps：功能尚未完善
