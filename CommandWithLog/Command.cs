using System;

using NLog;
using NLog.Config;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace CommandWithLog
{
	[TransactionAttribute(TransactionMode.Manual)]
	[RegenerationAttribute(RegenerationOption.Manual)]
	public class Command : IExternalCommand
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		UIApplication uiApp;
		Document doc;

		public Result Execute(ExternalCommandData commandDate,
			ref string messege,
			ElementSet elements)
		{
			LogManager.Configuration =
				new XmlLoggingConfiguration("C:/ProgramData/Autodesk/Revit/Addins/2020/CommandWithLog/NLog.config");

			uiApp = commandDate.Application;
			doc = uiApp.ActiveUIDocument.Document;

			try
			{
				TaskDialog.Show("Error", "Throw Exaption");

				throw new Exception("Throw Exaption");

				//return Result.Succeeded;
			}

			catch (Autodesk.Revit.Exceptions.OperationCanceledException)
			{
				return Result.Cancelled;
			}
			catch (Exception ex)
			{
				logger.Error("User {0} | Document {1} | Message {2}",
					uiApp.Application.Username, doc.PathName, ex.Message);
				return Result.Failed;
			}
		}
	}
}