# How to bind the Xamarin.Forms pie chart tooltip to "Others" category values (SfChart)?

We always like to group smaller pie chart values into the category "Others" to increase chart readability. But we also think about ways to show those grouped values in UI.

In this example illustrates how to show grouped values of [Xamarin.Forms Pie Chart](https://www.syncfusion.com/xamarin-ui-controls/xamarin-charts/chart-types) using tooltip by these following ways, you can display the average of clustered values, either can display the individual value present in it.

## How to show sum of values that grouped at Xamarin Pie Chart

The below code example shows how to calculate and display the average of grouped value using the tooltip template.

### Step 1: Declaration IValueConverter to calculate average values.
```
public class ChartAvgValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            //Return string value.
            if (parameter != null && parameter.ToString() == "Label")
            {
                return value is List<object> ? "Others" : (value as DataModel).XData;
            }
            else
            {
                //Return average value.
                if (value is List<object>)
                {
                    var collection = value as List<object>;
                    var sum = collection.Sum(item => (item as DataModel).YData);
                    return sum / collection.Count;
                }
                else
                {
                    return (value as DataModel).YData;
                }
            }
        }
     
        return null;
    }

}
```

### Step 2: DataTemplate declarations.
```
<ContentView.Resources>
    <local:ChartAvgValueConverter x:Key="sumOfValuesConverter"/>
    <DataTemplate x:Key="TooltipTemplate">
        <StackLayout Orientation="Horizontal">
            <Label Text="{Binding Converter={StaticResource sumOfValuesConverter}, ConverterParameter='Label'}". . />
            <Label Text="{Binding Converter={StaticResource sumOfValuesConverter}}" ../>
        </StackLayout>
    </DataTemplate>
</ContentView.Resources>
```

### Step 3: DataTemplate defined in the Series Tooltip Template.
```
<chart:SfChart BackgroundColor="Transparent">
. . .
    <chart:SfChart.Series>
        <chart:PieSeries 
            ItemsSource="{Binding MonthlyExpenses}" 
            XBindingPath="XData" 
            YBindingPath="YData" 
            EnableTooltip="true"
            GroupMode="Value"
            GroupTo="50"
            TooltipTemplate="{StaticResource TooltipTemplate}"
            >
            
        </chart:PieSeries>
    </chart:SfChart.Series>
    <chart:SfChart.ChartBehaviors>
        <chart:ChartTooltipBehavior BackgroundColor="LightBlue
    </chart:SfChart.ChartBehaviors>
</chart:SfChart>
```

![Tooltip template to show average of grouped values](https://user-images.githubusercontent.com/53489303/200767677-31beb677-b8f8-4367-ace3-9c55756d033d.png)

## How to bind the Xamarin.Forms Pie Chart grouped data collection to the Tooltip

The below code example shows how to display the values, which present inside the group ‘Others’.
 
### Step 1: Declaration IValueConverter to generate BindableLayout ItemsSource
```
public class ChartValueConverter : IValueConverter
{
//Which returns ItemsSource for bindable layout.
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if(value != null)
        {
            IList<DataModel> dataList = new List<DataModel>();
            if (value is DataModel)
            {
                dataList.Add(value as DataModel);
                return dataList;
            }
            else
            {
                return value;
            }
        }

        return value;
    }
}
```

### Step 2: DataTemplate declarations with any of the BindableLayout.
```
<ContentView.Resources>
    <local:ChartValueConverter x:Key="itemsSourceConverter"/>
    <DataTemplate x:Key="TooltipTemplate">
        <StackLayout BindableLayout.ItemsSource="{Binding Converter={StaticResource itemsSourceConverter}}" >
            <BindableLayout.ItemTemplate>
                <DataTemplate>
                    <StackLayout Orientation="Horizontal" HorizontalOptions="FillAndExpand">
                        <Label Text="{Binding XData}" HorizontalTextAlignment="Center"/>
                        <Label Text="{Binding YData}" HorizontalTextAlignment="Center"/>
                    </StackLayout>
                </DataTemplate>
            </BindableLayout.ItemTemplate>
        </StackLayout>
    </DataTemplate>
</ContentView.Resources>
```

### Step 3: DataTemplate defined in the Series Tooltip Template.
```
<chart:SfChart BackgroundColor="Transparent">
. . .
    <chart:SfChart.Series>
        <chart:PieSeries . . .
            EnableTooltip="true"
            GroupMode="Value"
            GroupTo="50"
            TooltipTemplate="{StaticResource TooltipTemplate}"
            >
. . .
        </chart:PieSeries>
    </chart:SfChart.Series>
    <chart:SfChart.ChartBehaviors>
        <chart:ChartTooltipBehavior BackgroundColor="LightBlue
    </chart:SfChart.ChartBehaviors>
</chart:SfChart>
```

![Tooltip template shows individual value present in the others category](https://user-images.githubusercontent.com/53489303/200767964-25b1ed57-fb93-4f40-97d8-03fbed7c7ef5.png)

KB article - [How to bind the Xamarin.Forms pie chart tooltip to “Others” category values](https://www.syncfusion.com/kb/11861/how-to-bind-the-xamarin-forms-pie-chart-tooltip-to-others-category-values)

### Related links

[Group small data points into “others”](https://help.syncfusion.com/xamarin/charts/charttypes#group-small-data-points-into-others)

[Bindable Layouts in Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/layouts/bindable-layouts)

### See also

[How to set size of pie/doughnut?](https://www.syncfusion.com/kb/5525/how-to-set-size-of-pie-doughnut)

[How to explode the pie series slice on touch?](https://www.syncfusion.com/kb/5923/how-to-explode-the-pie-series-slice-on-touch)
