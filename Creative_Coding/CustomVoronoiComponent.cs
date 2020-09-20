using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace Creative_Coding
{
    public class CustomVoronoiComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CustomVoronoiComponent class.
        /// </summary>
        public CustomVoronoiComponent()
          : base("Vogels Sunflower", "Sunflower",
              "Vogels Sunflower Component",
              "Vector", "Custom")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Iterations", "I", "Number of Iterations", GH_ParamAccess.item);
            pManager.AddNumberParameter("Degree", "D", "Degree", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "Output Points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            int Iterations = 1000;
            double Degree = 5;

            List<Point3d> outPoints = new List<Point3d>();

            if (!(DA.GetData(0, ref Iterations)))
            {
                return;
            }
            if (Iterations < 1)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Must be at least 1 iteration");
                return;
            }    

            if (!(DA.GetData(1, ref Degree)))
            {
                return;
            }
            if (Math.Sqrt(Degree) - Math.Floor(Math.Sqrt(Degree)) == 0.0)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Degree must not be a perfect square!");
            }

            double c = (Math.Sqrt(Degree) + 1.0) / 2.0;
            double R = 0.0;
            double Angle = 0.0;

            for (int i = 0; i <= Iterations; i++)
            {
                // Run Algoritm
                R = Math.Pow(i, c) / ((double)Iterations / 2.0);
                Angle = (Math.PI * 2.0) * c * ((double)i);
                // Convert to Cartesian Coordinates
                double x = Plane.WorldXY.Origin.X + (R * Math.Sin(Angle) + (double)Iterations / 10.0);
                double y = Plane.WorldXY.Origin.Y + (R * Math.Cos(Angle) + (double)Iterations / 10.0);
                Point3d p = new Point3d(x, y, 0.0);
                // Add to list
                outPoints.Add(p);
            }

            DA.SetDataList(0, outPoints);
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
            get { return new Guid("6c8f1d62-a3e2-4230-9ba8-0dc7fab12a38"); }
        }
    }
}