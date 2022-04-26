using UnityEditor;

namespace Editor
{
    public class LockInspector : UnityEditor.Editor
    {
        [MenuItem("Tools/Toggle Inspector Lock %q")] 
        public static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}
