using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml;
using System.IO;

namespace CyberCradle
{
    public class SearchItemPsdCreator /*: IPsdCreator*/
    {
        /*
        private string CorrectItemName( string name )
        {
            name = StringUtils.ReplaceWithPattern("_item??" , "" , name);
            name = StringUtils.UppercaseFirst(name);

            return name;
        }
        */
        
        /*
        public string GetDisplayName( )
        {
            return "Search Item";
        }
        */

        //sincity распарсим псд и создадим дату(координаты и тд) по проедметам
        public SearchItemDesc[] ParseAndCreateData(PsdDocument psdDocument, bool includeChildren = false)
        {
            List<SearchItemDesc> searchItems = new List<SearchItemDesc>();



            //PsdLayer gameLayer = psdDocument.FindLayerByExactName("Game");
            //PsdLayer animationLayer = psdDocument.FindLayerByExactName("Animation");


            


            return searchItems.ToArray();

            /*
            List<PsdLayer> ungroupedItems = psdDocument.FindLayersByName("_item??", includeChildren);

            var itemGroups =
                from ungroupedItem in ungroupedItems
                group ungroupedItem by CorrectItemName(ungroupedItem.Name) into groupedItems
                select new { Name = groupedItems.Key, GroupedItems = groupedItems };

            

            foreach (var itemGroup in itemGroups)
            {

                PsdLayer originalImageLayer = null;
                //PsdLayer silhouetteImageLayer = null;
                
                

                foreach (PsdLayer item in itemGroup.GroupedItems)
                {
                    PsdLayer hiddenImageLayer = psdDocument.FindLayerByName(item, "case");

                    originalImageLayer.Add(hiddenImageLayer);

                    
                    if (originalImageLayer == null)
                    {
                        originalImageLayer = psdDocument.FindLayerByName(item, "original");
                    }
                    
                }

                SearchItemDesc searchItem = new SearchItemDesc();

                searchItem.name = itemGroup.Name;
                searchItem.searchText = itemGroup.Name;               


                if (originalImageLayer != null)
                    searchItem.original = PsdParserUtils.SpriteFromLayerFullname(psdDocument, originalImageLayer.Children[0].Fullname);
                if (silhouetteImageLayer != null)
                    searchItem.silhouette = PsdParserUtils.SpriteFromLayerFullname(psdDocument, silhouetteImageLayer.Children[0].Fullname);
                if (popupImageLayer != null)
                    searchItem.popup = PsdParserUtils.SpriteFromLayerFullname(psdDocument, popupImageLayer.Children[0].Fullname);

                searchItem.hiddenItemOptions = new HiddenItemDesc[hiddenImageLayers.Count];

                for (int j = 0; j < searchItem.hiddenItemOptions.Length; ++j)
                {
                    HiddenItemDesc hiddenItem = new HiddenItemDesc();
                    PsdLayer patchLayer = psdDocument.FindLayerByName(hiddenImageLayers[j], "patch");
                    PsdLayer mainLayer = psdDocument.FindLayerByName(hiddenImageLayers[j], "image");
                    PsdLayer shadowLayer = psdDocument.FindLayerByName(hiddenImageLayers[j], "shadow");

                    hiddenItem.name = itemGroup.Name;
                    if (mainLayer == null)
                        Debug.LogError("-----------------------------------mainLayer == null " + itemGroup.Name);

                    if (patchLayer != null)
                    {
                        hiddenItem.patchRect = PsdParserUtils.CorrectY(psdDocument, patchLayer.Rect);
                        hiddenItem.patchImage = PsdParserUtils.SpriteFromLayerFullname(psdDocument, patchLayer.Fullname);
                        hiddenItem.patchLayer = PsdParserUtils.LayerToZ(patchLayer.Layer);
                    }

                    hiddenItem.mainRect = PsdParserUtils.CorrectY(psdDocument, mainLayer.Rect);
                    hiddenItem.mainImage = PsdParserUtils.SpriteFromLayerFullname(psdDocument, mainLayer.Fullname);
                    hiddenItem.mainLayer = PsdParserUtils.LayerToZ(mainLayer.Layer);

                    if (shadowLayer != null)
                    {
                        hiddenItem.shadowRect = PsdParserUtils.CorrectY(psdDocument, shadowLayer.Rect);
                        hiddenItem.shadowImage = PsdParserUtils.SpriteFromLayerFullname(psdDocument, shadowLayer.Fullname);
                        hiddenItem.shadowLayer = PsdParserUtils.LayerToZ(shadowLayer.Layer);
                    }

                    //GenerateCollider(hiddenItem);

                    searchItem.hiddenItemOptions[j] = hiddenItem;
                }

                searchItems.Add(searchItem);
            }

            int groupIndex = -1;
            for (int i = 0; i < searchItems.Count; ++i)
            {
                if (groupIndex != -1)
                {
                    int hiddenOptionCount = searchItems[i].hiddenItemOptions.Length;
                    int groupHiddenOptionCount = searchItems[groupIndex].hiddenItemOptions.Length;

                    if (hiddenOptionCount > groupHiddenOptionCount)
                    {
                        groupIndex = i;
                    }
                }
                else
                {
                    groupIndex = i;
                }
            }

            SearchItemDesc groupItem = searchItems[groupIndex];

            searchItems[groupIndex] = searchItems[searchItems.Count - 1];
            searchItems[searchItems.Count - 1] = groupItem;


            return searchItems.ToArray();
            */
        }

        
    
        bool IsSaveLocales = false;


        
    }
    
}
