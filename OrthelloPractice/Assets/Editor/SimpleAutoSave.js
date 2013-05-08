// Simple editor window that autosaves the working scene
    // Make sure to have this window opened to be able to execute the auto save.
    
    //import UnityEditor;
    
    class SimpleAutoSave extends EditorWindow {
        
        var saveTime : float = 300;
        var nextSave : float = 0;
    
        @MenuItem("Example/Simple autoSave")
        static function Init() {
            var window : SimpleAutoSave = 
                EditorWindow.GetWindowWithRect(
                    SimpleAutoSave, 
                    Rect(0,0,165,40));
            window.Show();
        }
        function OnGUI() {
             EditorGUILayout.LabelField("Save Each:", saveTime + " Secs");
             var timeToSave : int = nextSave - EditorApplication.timeSinceStartup;
             EditorGUILayout.LabelField("Next Save:", timeToSave.ToString() + " Sec");
             this.Repaint();
             
            if(EditorApplication.timeSinceStartup > nextSave) {
                var path : String [] = EditorApplication.currentScene.Split(char.Parse("/"));
                path[path.Length -1] = "AutoSave_" + path[path.Length-1];    
                EditorApplication.SaveScene(String.Join("/",path), true);
                Debug.Log("Saved Scene");
                nextSave = EditorApplication.timeSinceStartup + saveTime;
            }
        }
    }