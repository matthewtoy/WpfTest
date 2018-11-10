using System.Windows.Input;


namespace WpfTest
{
   public class Commands
   {

       public static RoutedUICommand PrintDiagnosisCommand = new RoutedUICommand(){Text = "Print Diagnosis to Report"};
       public static RoutedUICommand SaveOverExistingCommand = new RoutedUICommand(){Text = "Save Over Current Diagnosis"};
       public static RoutedUICommand SaveAsVariantCommand = new RoutedUICommand(){Text="Save Text as Variant"};
       public static RoutedUICommand SaveReportAsDiagnosisCommand = new RoutedUICommand(){Text = "Save Report as New Diagnosis"};
       public static RoutedUICommand DeleteDiagnosisCommand = new RoutedUICommand(){Text = "Delete Current Diagnosis"};


   }
}