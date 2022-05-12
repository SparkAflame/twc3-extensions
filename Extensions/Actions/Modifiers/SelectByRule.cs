/*
 * MIT License
 *
 * Copyright (c) 2022 SparkAflame (Kevin Preece)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;

using TWC;
using TWC.Actions;
using TWC.editor;

using UnityEditor;

using UnityEngine;


namespace SparkAflame.TWC3.Extensions.Actions.Modifiers
{
   [ActionCategory( Category = ActionCategoryAttribute.CategoryTypes.Modifiers )]
   [ActionName( Name = "Select By Rule" )]
   public class SelectByRule : TWCBlueprintAction, ITWCAction
   {
      [Serializable]
      public class SelectRule
      {
         public bool [ , ] Neighbours = new bool[ 3, 3 ];
      }


      // ReSharper disable MemberCanBePrivate.Global

      public float               RandomSelectionWeight;
      public List < SelectRule > NeighbourSelectionRules = new List < SelectRule >();

      // ReSharper restore MemberCanBePrivate.Global

      private TWCGUILayout _guiLayout;


      public ITWCAction Clone()
      {
         SelectByRule clone = new SelectByRule
         {
            RandomSelectionWeight = RandomSelectionWeight
         };

         clone.NeighbourSelectionRules.AddRange( NeighbourSelectionRules );

         return clone;
      }


      public bool [ , ] Execute( bool [ , ] map, TileWorldCreator twc )
      {
         int        mapWidth  = map.GetLength( 0 );
         int        mapHeight = map.GetLength( 1 );
         bool [ , ] finalMap  = new bool[ mapWidth, mapHeight ];

         if ( ( mapWidth < 5 ) || ( mapHeight < 5 ) )
         {
            return finalMap; // Can't do anything if the map's too small.
         }

         foreach ( SelectRule rule in NeighbourSelectionRules )
         {
            bool [ , ] neighbours = rule.Neighbours;

            for ( int x = 1; x < ( mapWidth - 1 ); x++ )
            {
               for ( int y = 1; y < ( mapHeight - 1 ); y++ )
               {
                  bool match =
                     map [ x, y ]
                     && ( map [ x - 1, y - 1 ] == neighbours [ 0, 2 ] )
                     && ( map [ x,     y - 1 ] == neighbours [ 1, 2 ] )
                     && ( map [ x + 1, y - 1 ] == neighbours [ 2, 2 ] )
                     && ( map [ x - 1, y     ] == neighbours [ 0, 1 ] )
                     && ( map [ x + 1, y     ] == neighbours [ 2, 1 ] )
                     && ( map [ x - 1, y + 1 ] == neighbours [ 0, 0 ] )
                     && ( map [ x,     y + 1 ] == neighbours [ 1, 0 ] )
                     && ( map [ x + 1, y + 1 ] == neighbours [ 2, 0 ] );
                  
                  finalMap [ x, y ] |= match; // Merge this rule's result with previous results
               }
            }
         }

         return finalMap;
      }


#if UNITY_EDITOR


      public override void DrawGUI( Rect rect, int layerIndex, TileWorldCreatorAsset asset, TileWorldCreator twc )
      {
         if ( NeighbourSelectionRules == null )
         {
            NeighbourSelectionRules = new List < SelectRule >();
         }

         using ( _guiLayout = new TWCGUILayout( rect ) )
         {
            _guiLayout.Add();
            _guiLayout.Add();

            if ( GUI.Button( _guiLayout.rect, "Add new rule" ) )
            {
               NeighbourSelectionRules.Add( new SelectRule() );
            }

            for ( int u = 0; u < NeighbourSelectionRules.Count; u++ )
            {
               _guiLayout.Add();

               GUI.Box( new Rect( _guiLayout.rect.x, _guiLayout.rect.y + 5, _guiLayout.rect.width, 67 ), string.Empty );

               if ( GUI.Button( new Rect( ( _guiLayout.rect.x + _guiLayout.rect.width ) - 20, _guiLayout.rect.y + 5, 20, 20 ), "x" ) )
               {
                  NeighbourSelectionRules.RemoveAt( u );
               }

               const float w = 30.0f;
               const float h = 5.0f;

               for ( int y = 0; y < 3; y++ )
               {
                  _guiLayout.Add();

                  for ( int x = 0; x < 3; x++ )
                  {
                     Rect r = new Rect( _guiLayout.rect.x + ( x * w ), ( _guiLayout.rect.y + ( y * h ) ) - 10, w, 15 );

                     if ( ( x == 1 ) && ( y == 1 ) )
                     {
                        GUI.color = Color.black;

                        EditorGUI.Toggle( r, string.Empty, false );
                     }
                     else
                     {
                        GUI.color = NeighbourSelectionRules [ u ].Neighbours [ x, y ] ? Color.green : Color.red;

                        NeighbourSelectionRules [ u ].Neighbours [ x, y ] =
                           EditorGUI.Toggle( r, string.Empty, NeighbourSelectionRules [ u ].Neighbours [ x, y ] );
                     }

                     GUI.color = Color.white;
                  }
               }
            }
         }
      }
#endif


      public float GetGUIHeight()
      {
         if ( _guiLayout != null )
         {
            return _guiLayout.height;
         }

         return 18;
      }
   }
}
