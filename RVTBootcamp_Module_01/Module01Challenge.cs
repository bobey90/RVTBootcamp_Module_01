using Autodesk.Revit.DB;

namespace RVTBootcamp_Module_01
{
    [Transaction(TransactionMode.Manual)]
    public class Module01Challenge : IExternalCommand
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

            int level_number = 250;

            double starting_elevation = 0;
            double floor_height = 15;
            double remainder_3;
            double remainder_5;

            
            //string buzz = "Buzz ";

            Level NewLevel;
            
            FilteredElementCollector collector_titleblock;
            FilteredElementCollector collector_plan;
            
            ViewPlan new_Mod_Plan;
            ViewSheet newSheet;
            ViewFamilyType floorplanVFT;

            for (int i = 1; i <= level_number; ++i)
            {

                NewLevel = Level.Create(doc, starting_elevation);
                NewLevel.Name = "Level " + i;
                starting_elevation += floor_height;

                remainder_3 = i % 3;
                remainder_5 = i % 5;



                if (remainder_3 == 0 && remainder_5 == 0)
                {
                    collector_titleblock = new FilteredElementCollector(doc);
                    collector_titleblock.OfCategory(BuiltInCategory.OST_TitleBlocks);

                    newSheet = ViewSheet.Create(doc, collector_titleblock.FirstElementId());
                    newSheet.Name = "FizzBuzz" + i;
                    newSheet.SheetNumber = "A10" + i;

                }

                if (remainder_3 == 0)
                {



                    collector_plan = new FilteredElementCollector(doc);
                    collector_plan.OfClass(typeof(ViewFamilyType));

                    floorplanVFT = null;
                    foreach (ViewFamilyType curVFT in collector_plan)
                    {
                        if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorplanVFT = curVFT;

                        }
                        new_Mod_Plan = ViewPlan.Create(doc, floorplanVFT.Id, NewLevel.Id);
                        new_Mod_Plan.Name = "Fizz " + i;

                    }
                }



            }





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
