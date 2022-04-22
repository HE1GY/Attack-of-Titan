using UnityEditor;


public class LockInspector : UnityEditor.Editor
{
    [MenuItem("Tools/Toggle Inspector Lock %q")] // Ctrl + L
    public static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }
}
