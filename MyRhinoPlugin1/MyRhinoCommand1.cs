using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Newtonsoft.Json;
using System.IO;

namespace MyRhinoPlugin1.MyRhinoPlugin1
{
    public class MyRhinoCommand1 : Command
    {


        public override string EnglishName => "MyRhinoCommand1";

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
