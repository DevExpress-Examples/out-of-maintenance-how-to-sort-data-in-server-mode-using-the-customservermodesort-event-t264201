Imports System.Windows
Imports DevExpress.Xpf.PivotGrid
Imports System.Collections

Namespace WPFPivotGridCustomServerModeSort
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            Dim field As PivotGridField = fieldOrderYear
            pivotGridControl1.BeginUpdate()
            Try
                field.FilterValues.Clear()
                field.FilterValues.Add(1998)
                field.FilterValues.FilterType = FieldFilterType.Included
            Finally
                pivotGridControl1.EndUpdate()
            End Try
            fieldCategoryName.SortMode = FieldSortMode.Custom
            fieldOrderMonth.SortMode = FieldSortMode.Custom
        End Sub

        Private Sub pivotGridControl1_CustomServerModeSort(ByVal sender As Object,
                                    ByVal e As CustomServerModeSortEventArgs)
            ' Sorting using a cross area object.
            If e.Field Is fieldOrderMonth Then
                ' Sets the cross area key, by which the "Month" field will be sorted.
                ' In this example, it's one of the "Category" cross area field values.
                Dim sorting As CrossAreaKey = e.GetCrossAreaKey(New Object() {"Confections"})

                ' Sets the result of the "Month" field's values comparison 
                ' by the cross area key object and the "Price" field.
                e.Result = Comparer.Default.Compare(e.GetCellValue1(sorting, fieldPrice),
                                                    e.GetCellValue2(sorting, fieldPrice))
            End If

            ' Direct sorting without using a cross area object. 
            If e.Field Is fieldCategoryName Then
                ' Sets the result of "Category" field's values comparison by the Year and Price fields.
                e.Result = Comparer.Default.Compare(e.GetCellValue1(New Object() {1998}, fieldPrice),
                                                    e.GetCellValue2(New Object() {1998}, fieldPrice))
            End If
        End Sub
    End Class
End Namespace
