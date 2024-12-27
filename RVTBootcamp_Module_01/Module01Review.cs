using Autodesk.Revit.DB;
using System.Windows.Controls;

namespace RVTBootcamp_Module_01
{
    [Transaction(TransactionMode.Manual)]
    public class Module01Review : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Document doc = uiapp.ActiveUIDocument.Document;

            // Your code goes here

            /*
             Declare a number variable and set it to 250

            Declare a starting elevation variable and set it to 0

            Declare a floor height variable and set it to 15 

            Loop through the number 1 to the number variable

            Create a level for each number

            After creating the level, increment the current elevation by the floor height value.

            If the number is divisible by 3, create a floor plan and name it "FIZZ_#"

            If the number is divisible by 5, create a ceiling plan and name it "BUZZ_#"

            If the number is divisible by both 3 and 5, create a sheet and name it "FIZZBUZZ_#"

                A few hints:

            Use a For loop to loop through the numbers

            Use the modulus operator ( % ) to check if the number is divisible by 3 or 5

            You can check two conditions in a conditional statement by using && (ie. If(a == 5 && b == 10)

            Don't forget your transaction!

            Reference the logic diagram below to see a flow chart of the add-in.
            */


            //create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("Doing something in Revit");

            //1. set variables
            int numFloors = 250;
            double currentElev = 0;
            int floorHeight = 15;


            //9. get title block type

            FilteredElementCollector tbCollector = new FilteredElementCollector(doc);
            tbCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);
            tbCollector.WhereElementIsElementType();
            ElementId tblockId = tbCollector.FirstElementId();


            //2. loop through floors and check FIZZBUZZ

            for (int i = 1; i <= numFloors; i++)
            {

                //3. create level
                Level newLevel = Level.Create(doc, currentElev);
                newLevel.Name = "LEVEL" + i.ToString();


                //4. increment elevation
                currentElev += floorHeight;

                //5. check for FIZZBUZZ

                if (i % 3 == 0 && i % 5 == 0)
                {
                    //6. FIZZBUZ - create sheet
                    ViewSheet newSheet = ViewSheet.Create(doc, tblockId);
                    newSheet.SheetNumber = i.ToString();
                    newSheet.Name = "FIZZBUZZ" + i.ToString();

                    
                }
                else if (i%3 == 0)
                {
                    //7. Fizz - create floor plan
                }
                else if (i % 5 == 0)
                {
                    //8. BUZZ - Create ceiling plan
                }




            }

            // 6. Aler user







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
