using System;
using System.Collections.Generic;
using Creative_Coding.Properties;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Creative_Coding
{
    public class MyComponent1 : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public MyComponent1()
          : base("Days in Month", "DaysInMnth",
              "Days in Month",
              "Math", "Time")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Year", "Y", "", GH_ParamAccess.item, 2020);
            pManager.AddIntegerParameter("Month", "M", "", GH_ParamAccess.item, 9);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Number of Days", "D", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //1. Define variables
            int year = 2020;
            int month = 2;
            //2. Import data from input params into variables
            if (!DA.GetData(0, ref year)) return;
            if (!DA.GetData(1, ref month)) return;
            //3. Check input validity
            if ((year < DateTime.MinValue.Year) || (year > DateTime.MaxValue.Year))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid Year");
                return;
            }
            if ((month < 1) || (month > 12))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Invalid Month");
                return;
            }

            //4. Start solving / computing to arrive at the result
            int days = System.DateTime.DaysInMonth(year, month);

            //5. Assign the results to variables if not already done so
            //6. Export results through output params
            DA.SetData(0, days);
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
                return Resources.icon;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b824dbf8-42ef-4016-a2b8-fd4f7050cbd8"); }
        }
    }
}