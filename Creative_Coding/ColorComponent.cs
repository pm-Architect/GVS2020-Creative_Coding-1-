using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.Display;
using Rhino.Geometry;

namespace Creative_Coding
{
    public class ColorComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ColorComponent class.
        /// </summary>
        public ColorComponent()
          : base("ColorComponent", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //pManager.AddColourParameter("Color", "C", "C", GH_ParamAccess.item, Color.White);
            //pManager.AddTimeParameter("Time IN", "", "", GH_ParamAccess.item);
            //pManager.AddTimeParameter("Time OUT", "", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            //pManager.AddColourParameter("Color Theme", "T", "T", GH_ParamAccess.list);
            //pManager.AddTimeParameter("Difference", "", "", GH_ParamAccess.item);
            pManager.AddPlaneParameter("Planes", "P", "", GH_ParamAccess.list);
            pManager.AddTransformParameter("Tranformations", "T", "", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Plane> outPoints = new List<Plane>();
            List<Transform> outT = new List<Transform>();
            for (double x = 0; x < 15.0; x = x + 1.2)
            {
                for (double y = 0; y < 20.0; y = y + 2.1)
                {
                    Point3d p = new Point3d(x, y, 0.0);
                    Transform t = Transform.Translation((Vector3d)p);
                    Plane p1 = new Plane(p, Vector3d.ZAxis);
                    outPoints.Add(p1);
                    outT.Add(t);
                }
            }

            DA.SetDataList(0, outPoints);
            DA.SetDataList(1, outT);


            //Mesh mesh = new Mesh();
            //foreach (MeshFace f in mesh.Faces)
            //{
            //    List<Point3d> faceVerts = new List<Point3d>();
            //    faceVerts.Add(mesh.Vertices[f.A]);
            //    faceVerts.Add(mesh.Vertices[f.B]);
            //    faceVerts.Add(mesh.Vertices[f.C]);
            //    //if f.isquad then add D
            //    Point3d c = (mesh.Vertices[f.A] + mesh.Vertices[f.B] + mesh.Vertices[f.C]);
            //    // get the xyz vals, add them and / 3 to get the avg
            //    Plane p = new Plane(mesh.Vertices[f.A], mesh.Vertices[f.B], mesh.Vertices[f.C]);
            //    p.Origin = c;
            //}

            //Mesh newMesh = new Mesh();
            //newMesh.Vertices.AddVertices(new List<Point3d>());
            //newMesh.Faces.AddFaces(mesh.Faces);
            //newMesh.RebuildNormals();
            //newMesh.UnifyNormals();
            //newMesh.Weld(RhinoDoc.ActiveDoc.ModelAbsoluteTolerance);

            //ColorHSL hSL = new ColorHSL();

            //TimeSpan ts = new TimeSpan();

            //ts.Subtract(ts2)

            //GH_Time time = new GH_Time(DateTime.Now);



            //Color inColor = Color.White;
            //if (!DA.GetData(0, ref inColor)) return;

            //List<Color> outColors = new List<Color>();

            //for (var i = 1; i < 6; i++)
            //{
            //    int a = inColor.A;
            //    int r = 255 - (inColor.R / i);
            //    int g = 255 - (inColor.G / i);
            //    int b = 255 - (inColor.B / i);
            //    Color newColor = Color.FromArgb(a, r, g, b);
            //    outColors.Add(newColor);
            //}

            //DA.SetDataList(0, outColors);
            //System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            //colorDialog.ShowDialog();
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("fff24275-55a8-44a2-a393-63ff21d228d4"); }
        }
    }
}