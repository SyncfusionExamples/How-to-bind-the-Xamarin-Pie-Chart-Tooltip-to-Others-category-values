# How to bind the Xamarin.Forms pie chart tooltip to “Others” category values

We always like to group smaller pie chart values into the category "Others" to increase chart readability. But we also think about ways to show those grouped values in UI. 

In this article, we will discuss in detail “How to show grouped values of Xamarin.Forms Pie Chart using tooltip?” By the following ways, we can display the average of clustered values, either can display individual value present in it.

## How to show sum of values that grouped at Xamarin Pie Chart

The example of the code below shows “How to calculate and display the average of grouped value using tooltip template?”


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

## How to bind the Xamarin.Forms Pie Chart grouped data collection to the Tooltip

The example of the code below shows “How to display the values which present inside the group ‘Others’?”
 
Step 1: Declaration IValueConverter to generate BindableLayout ItemsSource

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

Step 2: DataTemplate declarations with any of the BindableLayout.

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

Step 3: DataTemplate defined in the Series Tooltip Template.

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