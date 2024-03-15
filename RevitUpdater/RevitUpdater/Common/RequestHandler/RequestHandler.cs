using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;

namespace RevitUpdater.Common.RequestHandler
{
    public class RequestHandler
    {

    }

    public class MEPUpdaterRequestHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            // throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
