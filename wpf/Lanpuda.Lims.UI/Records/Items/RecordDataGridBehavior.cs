using DevExpress.Mvvm.UI.Interactivity;
using Lanpuda.Lims.InspectionItems.Dtos;
using NUglify.JavaScript.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Lanpuda.Lims.UI.Records.Items
{
    public class RecordDataGridBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty InspectionItemListProperty =
            DependencyProperty.Register(
                "InspectionItemList",
                typeof(ICollection<InspectionItemDto>),
                typeof(RecordDataGridBehavior),
                new PropertyMetadata(null, OnInspectionItemListPropertyChangedCallback)
                );

        public ICollection<InspectionItemDto> InspectionItemList
        {
            get { return (ICollection<InspectionItemDto>)GetValue(InspectionItemListProperty); }
            set 
            { 
                SetValue(InspectionItemListProperty, value);
            }
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            //subscribe to the AssociatedObject events
        }

        protected override void OnDetaching()
        {
            //unsubscribe from the AssociatedObject events
            base.OnDetaching();
        }


        private static void OnInspectionItemListPropertyChangedCallback(DependencyObject d , DependencyPropertyChangedEventArgs e)
        {
            ;
            RecordDataGridBehavior recordDataGridBehavior = (RecordDataGridBehavior)d;
            DataGrid dataGrid = recordDataGridBehavior.AssociatedObject;
            FixedColumns(dataGrid);
            InpectionItemColumns(dataGrid, recordDataGridBehavior.InspectionItemList);
        }


        private static void FixedColumns(DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            //记录编号
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "记录编号";
            column.Binding = new System.Windows.Data.Binding("Number");
            dataGrid.Columns.Add(column);

            //样品编号列
            var columnSampleNumber = new DataGridTextColumn();
            columnSampleNumber.Header = "样品编号";
            columnSampleNumber.Binding = new System.Windows.Data.Binding("SampleNumber");
            dataGrid.Columns.Add(columnSampleNumber);


            //产品物料
            var columnProduct = new DataGridTextColumn();
            columnProduct.Header = "产品物料";
            columnProduct.Binding = new System.Windows.Data.Binding("ProductName");
            dataGrid.Columns.Add(columnProduct);


            //样品类型
            var columnSampleType = new DataGridTextColumn();
            columnSampleType.Header = "样品类型";
            columnSampleType.Binding = new System.Windows.Data.Binding("DicSampleTypeDisplayValue");
            dataGrid.Columns.Add(columnSampleType);


            //样品属性
            var columnSampleProperty = new DataGridTextColumn();
            columnSampleProperty.Header = "样品属性";
            columnSampleProperty.Binding = new System.Windows.Data.Binding("DicSamplePropertyDisplayValue");
            dataGrid.Columns.Add(columnSampleProperty);

            //判级结果
            var columnGradeResult = new DataGridTextColumn();
            columnGradeResult.Header = "判级结果";
            columnGradeResult.Binding = new System.Windows.Data.Binding("DicRatingTypeDisplayValue");
            dataGrid.Columns.Add(columnGradeResult);
        }


        private static void InpectionItemColumns(DataGrid dataGrid , ICollection<InspectionItemDto> inspectionItems)
        {
            if (inspectionItems == null)
            {
                return;
            }
            foreach (var item in inspectionItems)
            {
                var dataGridTextColumn = new DataGridTextColumn();
                dataGridTextColumn.Header = item.ShortName;
                dataGridTextColumn.Binding = new System.Windows.Data.Binding(item.Id.ToString());

                //
                Style styleRecord = new Style();
                styleRecord.TargetType = typeof(DataGridCell);

                //ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri("/Lanpuda.Lims.UI;component/Records/Items/CellStyle.xaml", UriKind.Relative));
                //Style styleBaseRecord = (Style)res["RecorBatchCreateDataGridCellStyle"];
                //styleRecord.BasedOn = styleBaseRecord;
                styleRecord.Setters.Add(new Setter(DataGrid.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                styleRecord.Setters.Add(new Setter(DataGrid.VerticalAlignmentProperty, VerticalAlignment.Center));



                DataTrigger dataTrigger1 = new DataTrigger();
                Binding binding1 = new Binding(item.Id.ToString() + "IsQualified");
                binding1.Mode = BindingMode.TwoWay;
                binding1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                dataTrigger1.Binding = binding1;
                dataTrigger1.Value = null;

                Setter setter11 = new Setter();
                setter11.Property = DataGridCell.BackgroundProperty;
                setter11.Value = new SolidColorBrush(Colors.White);
                Setter setter12 = new Setter();
                setter12.Property = DataGridCell.ForegroundProperty;
                setter12.Value = new SolidColorBrush(Colors.Black);

                dataTrigger1.Setters.Add(setter11);
                dataTrigger1.Setters.Add(setter12);
                styleRecord.Triggers.Add(dataTrigger1);
                //////////////////
                DataTrigger dataTrigger2 = new DataTrigger();
                Binding binding2 = new Binding(item.Id.ToString() + "IsQualified");
                binding2.Mode = BindingMode.TwoWay;
                binding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                dataTrigger2.Binding = binding2;
                dataTrigger2.Value = "True";

                Setter setter21 = new Setter();
                setter21.Property = DataGridCell.BackgroundProperty;
                setter21.Value = new SolidColorBrush(Colors.White);
                Setter setter22 = new Setter();
                setter22.Property = DataGridCell.ForegroundProperty;
                setter22.Value = new SolidColorBrush(Colors.Black);

                dataTrigger2.Setters.Add(setter21);
                dataTrigger2.Setters.Add(setter22);
                styleRecord.Triggers.Add(dataTrigger2);
                /////////////////////////////////
                ///
                DataTrigger dataTrigger3 = new DataTrigger();
                Binding binding3 = new Binding(item.Id.ToString() + "IsQualified");
                binding3.Mode = BindingMode.TwoWay;
                binding3.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                dataTrigger3.Binding = binding3;
                dataTrigger3.Value = "False";

                Setter setter31 = new Setter();
                setter31.Property = DataGridCell.BackgroundProperty;
                setter31.Value = new SolidColorBrush(Colors.Red);
                Setter setter32 = new Setter();
                setter32.Property = DataGridCell.ForegroundProperty;
                setter32.Value = new SolidColorBrush(Colors.White);

                dataTrigger3.Setters.Add(setter31);
                dataTrigger3.Setters.Add(setter32);
                styleRecord.Triggers.Add(dataTrigger3);
                dataGridTextColumn.SetValue(DataGridTextColumn.CellStyleProperty, styleRecord);


                dataGrid.Columns.Add(dataGridTextColumn);
            }
        }
    }
}
