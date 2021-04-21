using CyberCradle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CyberCradle
{



    public enum MinigameLevel
    {
        invalid = 0 ,
        trainee = 1 ,
        agent = 2 ,
        specagent = 3 ,
        inspector = 4 ,
        leader = 5 ,
    }


    public enum HogLevel
    {
        invalid = 0 ,
        trainee = 1 ,
        agent = 2 ,
        specagent = 3 ,
        inspector = 4 ,
        leader = 5 ,
    }

    public enum SearchModeExt : int
    {
        //DONT CHANGE!!!!
        None = 0 ,
        TextDay = 1 ,
        TextNight = 2 ,
        AnagramDay = 3 ,
        AnagramNight = 4 ,//>>>>>
        SilhouetteDay = 5 ,
        SilhouetteNight = 6 ,//>>>>
        GroupDay = 7 ,
        GroupNight = 8 ,//>>>
        DifferenceDay = 9 ,

        DifferenceNight = 10 ,//
        NormalDay = 11 ,
        NormalNight = 12 ,
        //DONT CHANGE!!!!
    }

    public enum SearchMode
    {
        None = 0 ,
        Text = 1 ,
        Anagram = 2 ,
        Pair = 3 ,
        Silhouette = 4 ,
        Difference = 5 ,
        Group = 6 ,
    }

    public enum SearchDayMode
    {
        None = 0 ,
        Day = 1 ,
        Night = 2
    }

    [Serializable]
    public class PanelLayout
    {
        public int rowCount =1;
        public int columnCount = 4;
        public float xStep = 5;
        public float yStep = 5;
        public float xOffset =5;
        public float yOffset = 5;
        public float zOffset = 0;
    };

    [Serializable]
    public class SearchModeDesc
    {
        public SearchMode searchMode;
        public SearchDayMode searchDayMode;
        public PanelLayout layout = new PanelLayout( );
        public List<int> options = new List<int>( );
        public int totalCount;
        public int passiveCount;
        public int panelCount;
        public bool isTutor = false;
        public int time = 180;

        internal List<string> itemsTutor = new List<string>( );


        static internal SearchModeExt GetSearchModeExt( SearchDayMode day , SearchMode mode )
        {
            if( day == SearchDayMode.Day && mode == SearchMode.Anagram )
                return SearchModeExt.AnagramDay;

            if( day == SearchDayMode.Night && mode == SearchMode.Anagram )
                return SearchModeExt.AnagramNight;

            if( day == SearchDayMode.Day && mode == SearchMode.Difference )
                return SearchModeExt.DifferenceDay;

            if( day == SearchDayMode.Night && mode == SearchMode.Difference )
                return SearchModeExt.DifferenceNight;

            if( day == SearchDayMode.Day && mode == SearchMode.Group )
                return SearchModeExt.GroupDay;

            if( day == SearchDayMode.Night && mode == SearchMode.Group )
                return SearchModeExt.GroupNight;

            if( day == SearchDayMode.Day && mode == SearchMode.Silhouette )
                return SearchModeExt.SilhouetteDay;

            if( day == SearchDayMode.Night && mode == SearchMode.Silhouette )
                return SearchModeExt.SilhouetteNight;

            if( day == SearchDayMode.Day && mode == SearchMode.Text )
                return SearchModeExt.TextDay;

            if( day == SearchDayMode.Night && mode == SearchMode.Text )
                return SearchModeExt.TextNight;

            return SearchModeExt.None;
        }




        /*
        public static SearchModeDesc Create( HogSettings hog )
        {


            SearchModeDesc modeDesc = new SearchModeDesc( );
            modeDesc.totalCount = hog.totalCount;
            modeDesc.passiveCount = hog.passiveCount;

            modeDesc.panelCount = 4;

            modeDesc.time = hog.time;// ParseUtils.Parse<int>( "time" , data , 0 );
            int optionsFlag      = hog.options;//  ParseUtils.Parse<int>( "options" , data , 1 );


            List<int> options = new List<int>( );
            if( ( optionsFlag & 0x1 ) != 0 )
            {
                options.Add(0);
            }

            if( ( optionsFlag & 0x2 ) != 0 )
            {
                options.Add(1);
            }

            if( ( optionsFlag & 0x3 ) != 0 )
            {
                options.Add(2);
            }

            if( ( optionsFlag & 0x4 ) != 0 )
            {
                options.Add(3);
            }


            modeDesc.options.Clear( );
            modeDesc.options.AddRange(options.ToArray( ));


            modeDesc.layout = new PanelLayout( )
            {
                rowCount = 2 ,
                columnCount = 2 ,
                xStep = 160.0f ,
                yStep = 0.0f ,
                xOffset = 0.0f ,
                yOffset = -10.0f ,
                zOffset = -10.0f
            };

            return modeDesc;

        }
        */

    }
}

