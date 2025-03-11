using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using YamlDotNet.Core.Tokens;

namespace OVisionPro.Services._01._Core.ShapesPics
{

    public class Shapes
    {
        private Dictionary<string, Dictionary<string, Shape>> _shapes = new Dictionary<string, Dictionary<string, Shape>>();
        private readonly static Shapes instance = new Shapes();
        public static Shapes Instance { get { return instance; } }
        // Dictionary cấp 2: GroupKey -> (ShapeID -> Shape)
        
        public Dictionary<string, Dictionary<string, Shape>> shapes { 
            get { 
                return _shapes; } 
        }
        public void AddShape(string groupID, string shapeID, Shape shape1)
        {
            if (!_shapes.ContainsKey(groupID)) { _shapes.Add(groupID, new Dictionary<string, Shape>()); };
            if (!_shapes[groupID].ContainsKey(shapeID)) { _shapes[groupID].Add(shapeID, shape1); };
        }
        public void RemoveShape(string groupID, string shapeID, Shape shape1)
        {
            if (!_shapes.ContainsKey(groupID)) { return; };
            if (!_shapes[groupID].ContainsKey(shapeID)) { return; };
            _shapes[groupID].Remove(shapeID);
        }
        public void RemoveShape(string groupID)
        {
            if (!_shapes.ContainsKey(groupID)) { return; };
            _shapes.Remove(groupID);
        }

    }

    public class Shape
    {
        public string shapeId { get; set; }
        public string shapeName { get; set; }
        public string shapeParent { get; set; }
        public PointF TLPoint { 
            get; 
            set; }
        public PointF BRPoint { get; set; }
        public PointF TRPoint { get; set; }
        public PointF BLPoint { get; set; }
        public bool IsSelected { get; set; }
    }
}
