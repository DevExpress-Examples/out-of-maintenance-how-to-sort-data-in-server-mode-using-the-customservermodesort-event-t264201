using System.Windows;
using DevExpress.Xpf.PivotGrid;
using System.Collections;

namespace WPFPivotGridCustomServerModeSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PivotGridField field = fieldOrderYear;
            pivotGridControl1.BeginUpdate();
            try
            {
                field.FilterValues.Clear();
                field.FilterValues.Add(1998);
                field.FilterValues.FilterType = FieldFilterType.Included;
            }
            finally
            {
                pivotGridControl1.EndUpdate();
            }
            fieldCategoryName.SortMode = FieldSortMode.Custom;
            fieldOrderMonth.SortMode = FieldSortMode.Custom;
        }

        private void pivotGridControl1_CustomServerModeSort(object sender, 
            CustomServerModeSortEventArgs e)
        {
            // Sorting using a cross area object.
            if (e.Field == fieldOrderMonth)
            {
                // Sets the cross area key, by which the "Month" field will be sorted. 
                // In this example, it's one of the "Category" cross area field values.
                CrossAreaKey sorting = e.GetCrossAreaKey(new object[] { "Confections" });

                // Sets the result of the "Month" field's values comparison 
                // by the cross area key object and the "Price" field.
                e.Result = Comparer.Default.Compare(
                    e.GetCellValue1(sorting, fieldPrice),
                    e.GetCellValue2(sorting, fieldPrice)
                );
            }

            // Direct sorting without using a cross area object. 
            if (e.Field == fieldCategoryName)
            {
                // Sets the result of "Category" field's values comparison by the Year and Price fields.
                e.Result = Comparer.Default.Compare(
                    e.GetCellValue1(new object[] { 1998 }, fieldPrice),
                    e.GetCellValue2(new object[] { 1998 }, fieldPrice)
                );
            }
        }
    }
}
