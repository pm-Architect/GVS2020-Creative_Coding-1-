using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using Grasshopper.Kernel;
using Rhino.Geometry;
using ShapeDiver.Public.Grasshopper.Parameters;

namespace Creative_Coding
{
    public class WebcamComponent : GH_Component
    {
        Bitmap bitmap;
        VideoCaptureDevice videoSource;
        NewFrameEventHandler frameHandler;
        bool computing = false;
        GrasshopperBitmapGoo gh_bitmap;

        /// <summary>
        /// Initializes a new instance of the WebcamComponent class.
        /// </summary>
        public WebcamComponent()
          : base("WebcamComponent", "Nickname",
              "Description",
              "Category", "Subcategory")
        {
        }

        public override void AddedToDocument(GH_Document document)
        {
            frameHandler = new NewFrameEventHandler(video_NewFrame);
            bitmap = null;
            // enumerate video devices
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // create video source
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
            // set NewFrame event handler
            videoSource.NewFrame += frameHandler;
            // start the video source
            videoSource.Start();
            // ...
            // signal to stop when you no longer need capturing
            //videoSource.SignalToStop();
            // ...
            base.AddedToDocument(document);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new GrasshopperBitmapParam(), "Webcam Image", "WI", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            this.computing = true;
            if (bitmap != null)
            {
                Bitmap b = new Bitmap(bitmap.Width, bitmap.Height);
                // get new frame
                b.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                using (Graphics g = Graphics.FromImage(b))
                {
                    //g.Clear(Color.White);
                    g.DrawImageUnscaled(bitmap, 0, 0);
                }
                //Bitmap b = new Bitmap(bitmap);
                //b = bitmap;
                gh_bitmap = new GrasshopperBitmapGoo();
                gh_bitmap.Image = b;
                //gh_bitmap.Image = b;
                if (!gh_bitmap.IsValid)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, gh_bitmap.IsValidWhyNot);
                }
                else
                {
                    DA.SetData(0, gh_bitmap);
                    //gh_bitmap.Dispose();
                }
                //b.Dispose();
            }
            else
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Invalid Bitmap");
            }
            this.computing = false;
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!this.computing)
            {
                //if (bitmap != null) bitmap.Dispose();
                //bitmap = null;
                bitmap = new Bitmap(eventArgs.Frame.Width, eventArgs.Frame.Height);
                // get new frame
                bitmap.SetResolution(eventArgs.Frame.HorizontalResolution, eventArgs.Frame.VerticalResolution);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(eventArgs.Frame, 0, 0);
                }
            }
            //bitmap = eventArgs.Frame;
            //bitmap = new Bitmap(eventArgs.Frame);
            //bitmap = eventArgs.Frame.Clone(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            // process the frame
            //this.ExpireSolution(true);
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

        public override void RemovedFromDocument(GH_Document document)
        {
            videoSource.NewFrame -= frameHandler;
            videoSource.SignalToStop();
            base.RemovedFromDocument(document);
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6d82e3d6-eef1-4ade-8b25-e1d904db15f1"); }
        }
    }
}