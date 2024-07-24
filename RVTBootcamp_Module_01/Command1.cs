namespace RVTBootcamp_Module_01
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here

            //create a comment using a double forward slash
            //comments do not compile
            //so you can leave yourself notes in your code

            //Let's create some variables
            //DataType VariableName = Value; <- always end the line with a semicolon!

            string text1 = "this is my text";
            string text2 = "this is my next text";

            //combine strings
            string text3 = text1 + text2;
            string text4 = text1 + " " + text2 + "abcd";

            //create number variables

            int number1 = 10;
            double number2 = 20.5;

            //do some math
            double number3 = number1 + number2;
            double number4 = number1 - number2;
            double number5 = number4 / 100;
            double number6 = number5 * number4;
            double number7 = (number6 + number5) / 100;

            //convert meters to feet
            double meters = 4;
            double meterstofeet = meters * 3.28084;

            //Convert mm to feet
            double mm = 3500;
            double mmTofeet = mm / 304.8;
            double mmTofeet2 = (mm / 1000) * 3.28084;

            //finding the remainder when dividing (ie. the modulo or mod)
            double remainder1 = 100 % 10; // equals 0 (100 divided by 10 )
            double remainder2 = 100 % 9;   //equals 1 (100 divided by 9 = 11 witha remainder of 1)

            //increment a number by 1
            number6++;
            number6--;
            number6 += 10;

            //use conditional logic to compare things
            // compare using boolean operators
            // == equals
            // != not equal
            // > greater than
            // < less than
            // >= and <=

            //check a value and perform a single action if true
            if(number6 > 10)
            {

                //do something if true


            }

            //check a value nad perform an action if true and another if false

            if(number5 == 100)
            {
                //do something if true
            }
            else
            {
                //do something if false
            }

            //check multiple calues and perform actions if true and false
            if (number6 > 10)
            {

            }
            else if (number6 == 8)
            {

            }
            else
            {

            }

            //coumpound conditional statements
            //look for two things (or more) using &&

            if(number6 > 10 && number5 == 100)
            {
            //Do something if both are true
            }

            //look for either thing using ||
            if(number6 > 10 || number5 == 100)
            {
                //do something if either is true
            }

            //create list

            List<string> list1 = new List<string>();

            // add items to list
            list1.Add(text1);
            list1.Add(text2);
            list1.Add("this is some text");

            //create list and add items to it - method 2
            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };

            //loop through a list using foreach loop
            int letterCounter = 0;
            foreach(string currentstring in list1)
            {
                //do something
                //letterCounter = letterCounter + currentstring.Length;
                letterCounter +=currentstring.Length;
            }

            //loop through a range of numbers
            int numbercount = 0;
            int counter = 100;
            for(int i = 0; i <= counter; i++)
            {
                //do something
                numbercount += i;
            }

            TaskDialog.Show("Number counter", "The number count is " + numbercount.ToString());

            //create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("Doing something in Revit");


            //create a floor level - show in Revit API (www.revitapidocs.com)
            //elevation value is in decimal feet regardless of model units
            double elevation = 100;
            Level newlevel = Level.Create(doc, elevation);
            newlevel.Name = "My New Level";

            //create a floor plan view - show in Revit API (www.revitapidoc.com)
            //but first need to get a floor plan View Family Type
            //by creating a Filtered Element Collector

            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorplanVFT = null;
            foreach(ViewFamilyType curVFT in collector1)
            {
                if(curVFT.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorplanVFT = curVFT;
                }
            }

            //Create a view by specifying the document, view family type, and level

            ViewPlan newPlan = ViewPlan.Create(doc, floorplanVFT.Id, newlevel.Id);
            newPlan.Name = "My New floor Plan ";

            //get ceiling plan view family type
            ViewFamilyType ceilingPlanVFT = null;
            foreach(ViewFamilyType curVFT in collector1)
            {
                if(curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT= curVFT;
                }
            }

            //create a ceiling plan useing the cailing plan view family type
            ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newlevel.Id);
            newCeilingPlan.Name = "My new ceiling plan";

            //create a sheet
            //but first need to get title block
            //by creating a Filtered Element Collector
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);

            //create a sheet
            ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newSheet.Name = "My New Sheet";
            newSheet.SheetNumber = "A101";

            //add a view to a sheet using a Viewport - show in API
            //First create a point
            XYZ insPoint = new XYZ(1, 0.5, 0);

            Viewport newViewport = Viewport.Create(doc,newSheet.Id, newPlan.Id, insPoint);


            //make a change in the revit model



            t.Commit();
            t.Dispose();




            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";
            string? methodBase = MethodBase.GetCurrentMethod().DeclaringType?.FullName;

            if (methodBase == null)
            {
                throw new InvalidOperationException("MethodBase.GetCurrentMethod().DeclaringType?.FullName is null");
            }
            else
            {
                Common.ButtonDataClass myButtonData1 = new Common.ButtonDataClass(
                    buttonInternalName,
                    buttonTitle,
                    methodBase,
                    Properties.Resources.Blue_32,
                    Properties.Resources.Blue_16,
                    "This is a tooltip for Button 1");

                return myButtonData1.Data;
            }
        }
    }

}
