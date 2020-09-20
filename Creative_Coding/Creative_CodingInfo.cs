using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Creative_Coding
{
    public class Creative_CodingInfo : GH_AssemblyInfo
    {

        public override string Name
        {
            get
            {
                return "CreativeCoding";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("9b056029-054e-4304-bb9e-5e7867b2dfbb");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
