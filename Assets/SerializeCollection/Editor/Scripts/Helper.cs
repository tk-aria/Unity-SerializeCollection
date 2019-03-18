using UnityEditor;
using UnityEngine;

namespace SerializeCollection
{

    [InitializeOnLoad]
    public class Helper
    {

        static Helper()
        {

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

        }

    }


}