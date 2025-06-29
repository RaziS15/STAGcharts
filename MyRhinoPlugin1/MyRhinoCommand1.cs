﻿using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Newtonsoft.Json;
using System.IO;

namespace STAGCharts.MyRhinoPlugin1
{
    public class STAGCharts : Command
    {


        public override string EnglishName => "StagCharts";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            string j = ReadJson();
            ProcessJson(j);

            RhinoApp.WriteLine("Opening the chart window...");

            var win = new ChartWindow();
            win.Topmost = true;
            win.Show();

            return Result.Success;
        }

        public string ReadJson()
        {
            return File.ReadAllText("cleaned.json");
        }

        public void ProcessJson(string json)
        {
            MyRhinoPlugin1.Instance.LoadedDailyStages = JsonConvert.DeserializeObject<List<DailyStageData>>(json);
        }
    }
}
