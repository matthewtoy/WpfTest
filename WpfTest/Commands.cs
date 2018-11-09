using System.Windows.Input;


namespace WpfTest
{
   public class Commands
   {

       public static RoutedUICommand PrintDiagnosisCommand = new RoutedUICommand(){Text = "Print Diagnosis to Report"};
       public static RoutedUICommand SaveOverDiagnosisCommand = new RoutedUICommand(){Text = "Save to Replace Current Diagnosis"};
       public static RoutedUICommand SaveAsVariantCommand = new RoutedUICommand(){Text="Save Text as Variant"};
       public static RoutedUICommand SaveReportAsDiagnosisCommand = new RoutedUICommand(){Text = "Save Report as New Diagnosis"};


   }
}