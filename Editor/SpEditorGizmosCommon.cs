using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Spettro.Editors
{
    public static class SpEditorGizmosCommon
    {
        /// <summary>
        /// Displays a block-by-block preview of tiles.
        /// </summary>
        /// <param name="tiles"> List of positions.</param>
        /// <param name="tilemap">The transform. Used to add offsets.</param>
        public static void DrawTiles(List<Vector3Int> tiles, Transform tilemap)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < tiles.Count; i++)
            {
                Gizmos.DrawWireCube(tiles[i]
                    + new Vector3(Mathf.Floor(tilemap.transform.position.x) + 0.5f, Mathf.Floor(tilemap.transform.position.y) + 0.5f), new Vector2(1, 1));
            }
        }
        /// <summary>
        /// Previews a teleported object
        /// </summary>
        /// <param name="teleportObject">Object that will be teleported, to get the size.</param>
        /// <param name="position">Position.</param>
        /// <param name="name">Name of the label in the sceneview.</param>
        public static void TeleportPreview(Transform teleportObject, Vector3 position, string name = "Teleported Object Preview")
        {
            Vector3 sizeCube = Vector3.zero;
            if (teleportObject == null)
                sizeCube = new Vector3(1, 1, 1);
            else
                sizeCube = teleportObject.lossyScale;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(position, sizeCube);
            Handles.Label(position, name);
        }

        public static void ShowText(Vector3 position, string text)
        {
            Handles.Label(position, text);
        }
        public static void OutlineWithText(Vector2 center, Vector2 size, string text, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, size);
            ShowText(center, text);
        }

    }
}