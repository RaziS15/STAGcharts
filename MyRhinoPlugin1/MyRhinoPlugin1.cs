﻿using Rhino;
using Rhino.PlugIns;
using Rhino.UI;
using System;
using System.Collections.Generic;
using STAGCharts.MyRhinoPlugin1;

namespace STAGCharts.MyRhinoPlugin1
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class MyRhinoPlugin1 : Rhino.PlugIns.PlugIn
    {

        public List<DailyStageData> LoadedDailyStages;

        public MyRhinoPlugin1()
        {
            Instance = this;
            LoadedDailyStages = new List<DailyStageData>();
        }

        ///<summary>Gets the only instance of the MyRhinoPlugin1 plug-in.</summary>
        public static MyRhinoPlugin1 Instance { get; private set; }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.


        protected override Rhino.PlugIns.LoadReturnCode OnLoad(ref string errorMessage)
        {

            return LoadReturnCode.Success;

        }
    }
}