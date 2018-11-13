using System.Reflection;
using System.Windows.Input;


namespace WpfTest
{
   public class Commands
   {

       public static RoutedUICommand PrintDiagnosisCommand = new RoutedUICommand
       (
           "Print Diagnosis to Report",
           "PrintDiagnosisCommand",
           typeof(Commands),
           new InputGestureCollection()
           {
               new KeyGesture(Key.Enter, ModifierKeys.Control)
           }
       );
//todo - this command saves from preview to Fred (i.e. first in collection or maybe search box contents rather than namebox.
       public static RoutedUICommand SaveOverExistingCommand = new RoutedUICommand
       (
           "Save Over Current Diagnosis",
           "SaveOverExistingCommand",
           typeof(Commands),
           new InputGestureCollection()
           {
               new KeyGesture(Key.S, ModifierKeys.Control|ModifierKeys.Alt)
           }
       );
       public static RoutedUICommand SaveAsVariantCommand = new RoutedUICommand
       (
       "Save Text as Variant",
       "SaveAsVariantCommand",
       typeof(Commands),
       new InputGestureCollection()
       {
           new KeyGesture(Key.S, ModifierKeys.Control)
       }
       );


       public static RoutedUICommand SaveAsNewDiagnosisCommand = new RoutedUICommand
       (
       "Save Report as New Diagnosis",
       "SaveAsNewDiagnosisCommand",
       typeof(Commands),
       new InputGestureCollection()
       {
           new KeyGesture(Key.S, ModifierKeys.Control|ModifierKeys.Shift)
       }
       );

       public static RoutedUICommand DeleteDiagnosisCommand = new RoutedUICommand
       (
       "Delete Current Diagnosis",
       "DeleteDiagnosisCommand",
       typeof(Commands),
       new InputGestureCollection()
       {
           new KeyGesture(Key.Delete, ModifierKeys.Control)
       }
       );

   }
}